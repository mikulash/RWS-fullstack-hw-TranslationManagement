using BusinessLayer.Dtos;
using BusinessLayer.Utils.JobFileParser;
using DataAccessLayer.Enums;
using DataAccessLayer.Models;
using DataAccessLayer.UnitOfWork;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services;

public class TranslationJobService : ITranslationJobService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public TranslationJobService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    private double CurrentPricePerCharacter
    {
        get
        {
            const double defaultPricePerCharacter = 0.01;
            var price = _configuration["PricePerCharacter"];

            if (price == null || !double.TryParse(price, out var parsedPrice)) return defaultPricePerCharacter;
            return parsedPrice;
        }
    }

    public async Task<IEnumerable<TranslationJobDto>> GetJobsAsync()
    {
        var translationJobs = await _unitOfWork.TranslationJobs.GetAllAsync();
        return translationJobs.Adapt<IEnumerable<TranslationJobDto>>();
    }

    public async Task<bool> CreateTranslationJobAsync(CreateTranslationJobDto createTranslationJobDto)
    {
        var translationJob = createTranslationJobDto.Adapt<TranslationJob>();
        translationJob.Status = JobStatus.New;
        translationJob.Price = CalculatePrice(translationJob.OriginalContent);
        _unitOfWork.TranslationJobs.Add(translationJob);
        return await _unitOfWork.Commit();
    }

    public async Task<bool> UpdateJobStatusAsync(int jobId, JobStatus status)
    {
        var translationJob = await _unitOfWork.TranslationJobs.GetByIdAsync(jobId);
        if (translationJob == null) return false;

        translationJob.Status = status;
        _unitOfWork.TranslationJobs.Update(translationJob);
        return await _unitOfWork.Commit();
    }

    public async Task<bool> CreateJobWithFileAsync(IFormFile file, string customer)
    {
        var parser = new JobFileParserContext(file.FileName);
        var newJob = parser.Parse(file, customer);

        return await CreateTranslationJobAsync(newJob);
    }

    private double CalculatePrice(string content)
    {
        return content.Length * CurrentPricePerCharacter;
    }
}
