using ConsumerApi.Data;
using ConsumerApi.Models;
using ConsumerApi.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace consumerApiTests
{
    [TestFixture]
    public class QuotesRepositoryTests
    {
        private AppDbContext _context;
        private QuotesRepository _quotesRepository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new AppDbContext(options);
            _quotesRepository = new QuotesRepository(_context);

            // Seed the in-memory database with data
            _context.Quotes.AddRange(new List<QuotesMaster>
            {
                new QuotesMaster { Id = 1, BusinessValue = 1, PropertyValue = 1, PropertyType = "Equipment", Quote = "80000 INR" },
                new QuotesMaster { Id = 2, BusinessValue = 2, PropertyValue = 1, PropertyType = "Building", Quote = "80000 INR" }
            });

            _context.SaveChanges();
        }

        [Test]
        public async Task GetQuoteAsync_ValidValues_ShouldReturnQuote()
        {
            // Act
            var result = await _quotesRepository.GetQuoteAsync(1, 1, "Equipment");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("80000 INR", result.Quote);
        }

        [Test]
        public async Task GetAllQuotesAsync_ShouldReturnAllQuotes()
        {
            // Act
            var result = await _quotesRepository.GetAllQuotesAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("80000 INR", result[0].Quote);
            Assert.AreEqual("80000 INR", result[1].Quote);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

}
