using ConsumerApi.Models;
using ConsumerApi.Repository;
using ConsumerApi.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ConsumerApi.Tests
{
    public class QuotesServiceTests
    {
        private Mock<IQuotesRepository> _mockRepository;
        private QuotesService _service;
        private Mock<ILogger<QuotesService>> _mockLogger;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IQuotesRepository>();
            _mockLogger = new Mock<ILogger<QuotesService>>();
            _service = new QuotesService(_mockRepository.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetQuoteAsync_ReturnsQuote_WhenQuoteExists()
        {
            // Arrange
            var quoteMaster = new QuotesMaster { Quote = "80000 INR" };
            _mockRepository.Setup(repo => repo.GetQuoteAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(quoteMaster);

            // Act
            var result = await _service.GetQuoteAsync(1, 1, "Building");

            // Assert
            Assert.AreEqual("80000 INR", result);
        }

        [Test]
        public async Task GetQuoteAsync_ReturnsNoQuoteMessage_WhenQuoteDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetQuoteAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync((QuotesMaster)null);

            // Act
            var result = await _service.GetQuoteAsync(1, 1, "Equipment");

            // Assert
            Assert.AreEqual("No Quotes, Contact Insurance Provider", result);
        }

        [Test]
        public async Task GetAllQuotesAsync_ReturnsAllQuotes()
        {
            // Arrange
            var quotesList = new List<QuotesMaster>
            {
                new QuotesMaster { Quote = "Quote1" },
                new QuotesMaster { Quote = "Quote2" }
            };
            _mockRepository.Setup(repo => repo.GetAllQuotesAsync()).ReturnsAsync(quotesList);

            // Act
            var result = await _service.GetAllQuotesAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
        }
    }
}
