// AuthService.cs
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ConsumerApi.Data;
using ConsumerApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

namespace ConsumerApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _agentRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository agentRepository, IConfiguration configuration)
        {
            _agentRepository = agentRepository;
            _configuration = configuration;
        }

        public async Task<Agent> RegisterAsync(AgentRegister model)
        {
            var existingAgent = await _agentRepository.GetAgentByEmailAsync(model.Email);
            if (existingAgent != null)
                throw new Exception("Agent with this email already exists.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var agent = new Agent
            {
                Email = model.Email,
                Password = hashedPassword,
                Role = model.Role,
                Username = model.Username
            };

            await _agentRepository.InsertAgentAsync(agent);
            return agent;
        }

        public async Task<string> LoginAsync(AgentLogin model)
        {
            var agent = await _agentRepository.GetAgentByEmailAsync(model.Email);
            if (agent == null || !BCrypt.Net.BCrypt.Verify(model.Password, agent.Password))
                throw new UnauthorizedAccessException("Invalid credentials.");

            return GenerateJwtToken(agent);
        }

        private string GenerateJwtToken(Agent agent)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, agent.AgentId),
                new Claim(ClaimTypes.Name, agent.Username),
                new Claim(ClaimTypes.Email, agent.Email),
                new Claim(ClaimTypes.Role, agent.Role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(jwtSettings["ExpiryMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
