using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Tests;

public class UnitOfWorkTests
{
    private readonly AppDbContext _context;
    private readonly UnitOfWork.UnitOfWork _unitOfWork;

    public UnitOfWorkTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"TestDatabase_{Guid.NewGuid()}")
            .Options;

        _context = new AppDbContext(options);
        _unitOfWork = new UnitOfWork.UnitOfWork(_context);
    }

    [Fact]
    public void TranslationJobs_ShouldReturnRepository()
    {
        // Act
        var translationJobRepo = _unitOfWork.TranslationJobs;

        // Assert
        Assert.NotNull(translationJobRepo);
        Assert.IsType<TranslationJobRepository>(translationJobRepo);
    }

    [Fact]
    public void Translators_ShouldReturnRepository()
    {
        // Act
        var translatorRepo = _unitOfWork.Translators;

        // Assert
        Assert.NotNull(translatorRepo);
        Assert.IsType<TranslatorRepository>(translatorRepo);
    }

    [Fact]
    public async Task Commit_ShouldReturnTrue_WhenChangesSaved()
    {
        // Arrange
        var job = new TranslationJob
        {
            Id = 1,
            CustomerName = "Test Customer",
            OriginalContent = "Original text",
            TranslatedContent = "Translated text",
            Price = 100.0
        };

        // Act
        _unitOfWork.TranslationJobs.Add(job);

        var result = await _unitOfWork.Commit();

        // Assert
        Assert.True(result);
        Assert.Equal(1, await _context.TranslationJobs.CountAsync());
    }

    [Fact]
    public async Task Commit_ShouldReturnFalse_WhenNoChangesSaved()
    {
        // Act
        var result = await _unitOfWork.Commit();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Rollback_ShouldRevertChanges()
    {
        // Arrange
        const double originalPrice = 100.0;
        var job = new TranslationJob
        {
            Id = 1,
            CustomerName = "Test Customer",
            OriginalContent = "Original content",
            TranslatedContent = "Translated content",
            Price = originalPrice
        };

        _context.TranslationJobs.Add(job);
        await _context.SaveChangesAsync();

        // Modify the job
        job.Price = 300.0;
        _context.Entry(job).State = EntityState.Modified;

        // Act - Rollback the change
        await _unitOfWork.Rollback();

        // Assert - Verify that Rollback restores the original state of the entity
        var result = await _unitOfWork.TranslationJobs.GetByIdAsync(1);
        Assert.NotNull(result);
        Assert.Equal(originalPrice, result.Price);
    }
}
