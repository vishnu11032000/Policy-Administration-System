using ConsumerApi.Data;
using ConsumerApi.Models;
using ConsumerApi.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;

namespace consumerApi.Tests
{
    [TestFixture]
    public class ConsumerPolicyRepositoryTests
    {
        private AppDbContext _context;
        private ConsumerPolicyRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "ConsumerPolicyDb")
                .Options;
            _context = new AppDbContext(options);
            _repository = new ConsumerPolicyRepository(_context);
        }
        [TearDown]
        public void TearDown()
        {
            // Dispose the context to release resources
            _context.Dispose();
        }

        [Test]
        public async Task FindByConsumerIdAsync_ReturnsConsumerPolicy_WhenConsumerIdExists()
        {
            var policy = new ConsumerPolicy { ConsumerId = 1, PolicyId = "189899B7-88E3-4C6F-A0A1-552A783506B2" };
            _context.ConsumerPolicies.Add(policy);
            await _context.SaveChangesAsync();

            var result = await _repository.FindByConsumerIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("189899B7-88E3-4C6F-A0A1-552A783506B2", result.PolicyId);
        }

        [Test]
        public async Task SaveAsync_UpdatesPolicy()
        {
            var policy = new ConsumerPolicy { ConsumerId = 1, PolicyId = "189899B7-88E3-4C6F-A0A1-552A783506B2" };
            await _repository.SaveAsync(policy);

            var savedPolicy = await _context.ConsumerPolicies.FirstOrDefaultAsync(p => p.PolicyId == "189899B7-88E3-4C6F-A0A1-552A783506B2");

            Assert.IsNotNull(savedPolicy);
            Assert.AreEqual(1, savedPolicy.ConsumerId);
        }
    }
}
