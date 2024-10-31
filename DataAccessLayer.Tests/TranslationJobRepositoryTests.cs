using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Tests;

[TestSubject(typeof(TranslationJobRepository))]
public class TranslationJobRepositoryTests
{
    private readonly AppDbContext _context;
    private readonly TranslationJobRepository _repository;

    public TranslationJobRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"TestDatabase_{Guid.NewGuid()}")
            .Options;

        _context = new AppDbContext(options);
        _repository = new TranslationJobRepository(_context);
    }

    [Fact]
    public void Add_ShouldAddTranslationJob()
    {
        // Arrange
        var job = new TranslationJob { Id = 1, CustomerName = "Test Customer" };

        // Act
        _repository.Add(job);
        _context.SaveChanges();

        // Assert
        Assert.Equal(1, _context.TranslationJobs.Count());
        Assert.Equal("Test Customer", _context.TranslationJobs.First().CustomerName);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTranslationJob()
    {
        // Arrange
        var job = new TranslationJob { Id = 1, CustomerName = "Test Customer" };
        _context.TranslationJobs.Add(job);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Customer", result.CustomerName);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTranslationJobs()
    {
        // Arrange
        _context.TranslationJobs.AddRange(
            new TranslationJob { Id = 1, CustomerName = "Customer1" },
            new TranslationJob { Id = 2, CustomerName = "Customer2" }
        );
        // Act
        await _context.SaveChangesAsync();

        var result = await _repository.GetAllAsync();
        // Assert
        Assert.Equal(2, result.Count());
    }
}
