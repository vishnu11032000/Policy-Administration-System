//using ConsumerApi.Models;
//using ConsumerApi.Repository;
//using Moq;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using ConsumerApi.Data;

//namespace ConsumerTest1
//{
//    public class Tests
//    {
//            private QuotesRepository _repository;
//            private Mock<AppDbContext> _mockContext;
//            private Mock<DbSet<QuotesMaster>> _mockSet;

//            [SetUp]
//            public void SetUp()
//            {
//                // Mock the DbSet
//                _mockSet = new Mock<DbSet<QuotesMaster>>();

//                // Mock the DbContext and configure the Quotes DbSet
//                _mockContext = new Mock<AppDbContext>();
//                _mockContext.Setup(m => m.Quotes).Returns(_mockSet.Object);

//                // Instantiate the repository with the mocked context
//                _repository = new QuotesRepository(_mockContext.Object);
//            }

//            [Test]
//            public async Task GetQuoteAsync_ReturnsQuote_WhenQuoteExists()
//            {
//                // Arrange
//                var quote = new QuotesMaster { BusinessValue = 1, PropertyValue = 1, PropertyType = "Equipment", Quote = "80000 INR" };
//                var data = new List<QuotesMaster> { quote }.AsQueryable();

//                // Mock the FirstOrDefaultAsync behavior
//                _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.Provider).Returns(data.Provider);
//                _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.Expression).Returns(data.Expression);
//                _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.ElementType).Returns(data.ElementType);
//                _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

//                // Act
//                var result = await _repository.GetQuoteAsync(1, 1, "80000 INR");

//                // Assert
//                Assert.NotNull(result);
//                Assert.AreEqual("80000 INR", result.Quote);
//            }

//            [Test]
//            public async Task GetAllQuotesAsync_ReturnsAllQuotes()
//            {
//                // Arrange
//                var quotesList = new List<QuotesMaster>
//            {
//                new QuotesMaster { Quote = "Quote1" },
//                new QuotesMaster { Quote = "Quote2" }
//            }.AsQueryable();

//                // Mock the ToListAsync method
//                _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.Provider).Returns(quotesList.Provider);
//                _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.Expression).Returns(quotesList.Expression);
//                _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.ElementType).Returns(quotesList.ElementType);
//                _mockSet.As<IQueryable<QuotesMaster>>().Setup(m => m.GetEnumerator()).Returns(quotesList.GetEnumerator());

//                // Act
//                var result = await _repository.GetAllQuotesAsync();

//                // Assert
//                Assert.AreEqual(2, result.Count);
//            }
//        }
//    }


using ConsumerApi.Models;
using ConsumerApi.Repository;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConsumerApi.Data;
using Moq;
using Moq.EntityFrameworkCore;

namespace ConsumerTest1
{
    public class Tests
    {

        private Mock<AppDbContext> _mockContext;
        private QuotesRepository _repository;

        [SetUp]
        public void SetUp()
        {
            // Mock the DbContext
            _mockContext = new Mock<AppDbContext>();

            // Instantiate the repository with the mocked context
            _repository = new QuotesRepository(_mockContext.Object);
        }

        [Test]
        public async Task GetQuoteAsync_ReturnsQuote_WhenQuoteExists()
        {
            // Arrange
            var businessValue = 100;
            var propertyValue = 500;
            var propertyType = "Equipment";
            var expectedQuote = new QuotesMaster
            {
                BusinessValue = businessValue,
                PropertyValue = propertyValue,
                PropertyType = propertyType,
                Quote = "80000 INR"
            };

            // Mock the DbSet behavior for FirstOrDefaultAsync
            var mockQuotes = new List<QuotesMaster> { expectedQuote }.AsQueryable();
            _mockContext.Setup(m => m.Quotes).ReturnsDbSet(mockQuotes);

            // Act
            var result = await _repository.GetQuoteAsync(businessValue, propertyValue, propertyType);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("80000 INR", result.Quote);
        }

        [Test]
        public async Task GetQuoteAsync_ReturnsNull_WhenQuoteDoesNotExist()
        {
            // Arrange
            var businessValue = 200;
            var propertyValue = 600;
            var propertyType = "Machinery";

            // Mock an empty DbSet
            _mockContext.Setup(m => m.Quotes).ReturnsDbSet(new List<QuotesMaster>());

            // Act
            var result = await _repository.GetQuoteAsync(businessValue, propertyValue, propertyType);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllQuotesAsync_ReturnsAllQuotes()
        {
            // Arrange
            var quotesList = new List<QuotesMaster>
            {
                new QuotesMaster { BusinessValue = 100, PropertyValue = 200, PropertyType = "House", Quote = "50000 INR" },
                new QuotesMaster { BusinessValue = 300, PropertyValue = 400, PropertyType = "Car", Quote = "30000 INR" }
            };

            // Mock the DbSet behavior for ToListAsync
            _mockContext.Setup(m => m.Quotes).ReturnsDbSet(quotesList);

            // Act
            var result = await _repository.GetAllQuotesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("50000 INR", result[0].Quote);
        }

        [Test]
        public async Task GetAllQuotesAsync_ReturnsEmptyList_WhenNoQuotesExist()
        {
            // Arrange
            _mockContext.Setup(m => m.Quotes).ReturnsDbSet(new List<QuotesMaster>());

            // Act
            var result = await _repository.GetAllQuotesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    } 
}

