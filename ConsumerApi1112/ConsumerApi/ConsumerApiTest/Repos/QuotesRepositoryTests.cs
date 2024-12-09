﻿using ConsumerApi.Models;
using ConsumerApi.Repository;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConsumerApi.Data;

namespace ConsumerApi.Tests.Repository
{
    public class QuotesRepositoryTests
    {
        private QuotesRepository _repository;
        private Mock<AppDbContext> _mockContext;
        private Mock<DbSet<QuotesMaster>> _mockSet;

        [SetUp]
        public void SetUp()
        {
            // Mock the DbSet
            _mockSet = new Mock<DbSet<QuotesMaster>>();

            // Mock the DbContext and configure the Quotes DbSet
            _mockContext = new Mock<AppDbContext>();
            _mockContext.Setup(m => m.Quotes).Returns(_mockSet.Object);

            // Instantiate the repository with the mocked context
            _repository = new QuotesRepository(_mockContext.Object);
        }

        [Test]
        public async Task GetQuoteAsync_ReturnsQuote_WhenQuoteExists()
        {
            // Arrange
            var quote = new QuotesMaster { BusinessValue = 1000, PropertyValue = 500, PropertyType = "Home", Quote = "Test Quote" };
            var data = new List<QuotesMaster> { quote }.AsQueryable();

            // Mock the FirstOrDefaultAsync behavior
            _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // Act
            var result = await _repository.GetQuoteAsync(1000, 500, "Home");

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Test Quote", result.Quote);
        }

        [Test]
        public async Task GetAllQuotesAsync_ReturnsAllQuotes()
        {
            // Arrange
            var quotesList = new List<QuotesMaster>
            {
                new QuotesMaster { Quote = "Quote1" },
                new QuotesMaster { Quote = "Quote2" }
            }.AsQueryable();

            // Mock the ToListAsync method
            _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.Provider).Returns(quotesList.Provider);
            _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.Expression).Returns(quotesList.Expression);
            _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.ElementType).Returns(quotesList.ElementType);
            _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.GetEnumerator()).Returns(quotesList.GetEnumerator());

            // Act
            var result = await _repository.GetAllQuotesAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
        }
    }
}
