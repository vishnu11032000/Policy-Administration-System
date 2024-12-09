using ConsumerApi.Controllers;
using ConsumerApi.Models;
using ConsumerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsumerApi.Tests.Controllers
{
    public class QuotesControllerTests
    {
        private Mock<IQuotesService> _mockService;
        private QuotesController _controller;
        private Mock<ILogger<QuotesController>> _mockLogger;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IQuotesService>();
            _mockLogger = new Mock<ILogger<QuotesController>>();
            _controller = new QuotesController(_mockService.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetQuotes_ReturnsOkObjectResult_WithQuote()
        {
            // Arrange
            _mockService.Setup(service => service.GetQuoteAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync("80000 INR");

            // Act
            var result = await _controller.GetQuotes(1, 1, "Equipment");

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual("80000 INR", okResult.Value);
        }

        [Test]
        public async Task GetAllQuotes_ReturnsOkObjectResult_WithAllQuotes()
        {
            // Arrange
            var quotesList = new List<QuotesMaster>
            {
                new QuotesMaster { Quote = "80000 INR" },
                new QuotesMaster { Quote = "80000 INR" }
            };
            _mockService.Setup(service => service.GetAllQuotesAsync()).ReturnsAsync(quotesList);

            // Act
            var result = await _controller.GetAllQuotes();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var quotes = okResult.Value as List<QuotesMaster>;
            Assert.AreEqual(2, quotes.Count);
        }
    }
}
