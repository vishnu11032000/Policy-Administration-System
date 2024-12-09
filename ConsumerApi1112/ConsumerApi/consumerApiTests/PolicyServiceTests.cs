using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using PolicyModule.Controllers;
using ConsumerApi.Models;
using ConsumerApi.Payload.Request;
using ConsumerApi.Payload.Response;
using ConsumerApi.Repository;
using ConsumerApi.Services;

namespace PolicyAdministrationSystem.Tests.Controllers
{
    [TestFixture]
    public class PolicyControllerTests
    {
        private Mock<IPolicyService> _policyServiceMock;
        private Mock<IConsumerPolicyRepository> _consumerPolicyRepoMock;
        private Mock<ILogger<PolicyController>> _loggerMock;
        private PolicyController _controller;

        [SetUp]
        public void Setup()
        {
            _policyServiceMock = new Mock<IPolicyService>();
            _consumerPolicyRepoMock = new Mock<IConsumerPolicyRepository>();
            _loggerMock = new Mock<ILogger<PolicyController>>();
            _controller = new PolicyController(_policyServiceMock.Object, _consumerPolicyRepoMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task CreatePolicyAsync_ReturnsOkResult_WhenPolicyCreatedSuccessfully()
        {
            // Arrange
            var createPolicyRequest = new CreatePolicyRequest(1L, "10000 20000"); // Provide required parameters

            _consumerPolicyRepoMock.Setup(r => r.ExistsByConsumerIdAsync(It.IsAny<long>())).ReturnsAsync(false);
            _policyServiceMock.Setup(s => s.CreatePolicyAsync(It.IsAny<CreatePolicyRequest>()))
                              .ReturnsAsync(new MessageResponse("Policy created successfully! 😊"));

            // Act
            var result = await _controller.CreatePolicyAsync(createPolicyRequest) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }


        [Test]
        public async Task IssuePolicyAsync_ReturnsOkResult_WhenPolicyIssuedSuccessfully()
        {
            // Arrange
            var issuePolicyRequest = new IssuePolicyRequest
            {
                ConsumerId = 1L,
                BusinessId = 1L,
                PaymentDetails = "Success",
                AcceptanceStatus = "Accepted"
            };

            var consumerPolicy = new ConsumerPolicy
            {
                ConsumerId = 1L,
                PolicyStatus = "NotIssued" // Ensure policy is not already issued
            };

            // Mock the repository methods
            _consumerPolicyRepoMock.Setup(r => r.ExistsByConsumerIdAsync(It.IsAny<long>())).ReturnsAsync(true);
            _consumerPolicyRepoMock.Setup(r => r.ExistsByBusinessIdAsync(It.IsAny<long>())).ReturnsAsync(true);
            _consumerPolicyRepoMock.Setup(r => r.FindByConsumerIdAsync(It.IsAny<long>())).ReturnsAsync(consumerPolicy);

            // Mock the policy service
            _policyServiceMock.Setup(s => s.IssuePolicyAsync(It.IsAny<IssuePolicyRequest>()))
                              .ReturnsAsync(new MessageResponse("Policy issued successfully! 😊"));

            // Act
            var result = await _controller.IssuePolicyAsync(issuePolicyRequest) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result); // Ensure result is not null
            Assert.AreEqual(200, result.StatusCode); // Ensure response status code is 200 (OK)
        }


        [Test]
        public async Task ViewPolicyAsync_ReturnsOkResult_WhenPolicyViewedSuccessfully()
        {
            // Arrange
            var consumerId = 1L;
            var policyId = "P001";
            var consumerPolicy = new ConsumerPolicy { ConsumerId = consumerId, PolicyId = policyId }; // Mocked ConsumerPolicy

            // Mock the consumer existence and consumer policy retrieval
            _consumerPolicyRepoMock.Setup(r => r.ExistsByConsumerIdAsync(consumerId)).ReturnsAsync(true);
            _consumerPolicyRepoMock.Setup(r => r.FindByConsumerIdAsync(consumerId)).ReturnsAsync(consumerPolicy);

            // Mock the policy service's ViewPolicyAsync method
            _policyServiceMock.Setup(s => s.ViewPolicyAsync(consumerId, policyId))
                              .ReturnsAsync(new PolicyDetailsResponse { ConsumerId = consumerId, PolicyId = policyId });

            // Act
            var result = await _controller.ViewPolicyAsync(consumerId, policyId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result); // Ensure result is not null
            Assert.AreEqual(200, result.StatusCode); // Ensure it's OK (200)
        }

    }
}