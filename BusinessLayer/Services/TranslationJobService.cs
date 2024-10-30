using BusinessLayer.Dtos;
using BusinessLayer.Utils.JobFileParser;
using DataAccessLayer.Enums;
using DataAccessLayer.Models;
using DataAccessLayer.UnitOfWork;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services;

public class TranslationJobService(IUnitOfWork unitOfWork, IConfiguration configuration) : ITranslationJobService
{
    private double _currentPricePerCharacter
    {
        get
        {
            const double defaultPricePerCharacter = 0.01;
            var price = configuration["PricePerCharacter"];

            if (price == null || !double.TryParse(price, out var parsedPrice)) return defaultPricePerCharacter;
            return parsedPrice;
        }
    }

    public IEnumerable<TranslationJobDto> GetJobs()
    {
        var translationJobs = unitOfWork.TranslationJobs.GetAllAsync().Result;
        return translationJobs.Adapt<IEnumerable<TranslationJobDto>>();
    }

    public bool CreateTranslationJob(CreateTranslationJobDto createTranslationJobDto)
    {
        var translationJob = createTranslationJobDto.Adapt<TranslationJob>();
        translationJob.Status = JobStatus.New;
        translationJob.Price = CalculatePrice(translationJob.OriginalContent);
        unitOfWork.TranslationJobs.Add(translationJob);
        return unitOfWork.Commit().Result;
    }

    public bool UpdateJobStatus(int jobId, JobStatus status)
    {
        var translationJob = unitOfWork.TranslationJobs.GetByIdAsync(jobId).Result;
        if (translationJob == null) return false;

        translationJob.Status = status;
        unitOfWork.TranslationJobs.Update(translationJob);
        return unitOfWork.Commit().Result;
    }

    public bool CreateJobWithFile(IFormFile file, string customer)
    {
        var parser = new JobFileParserContext(file.FileName);
        var newJob = parser.Parse(file, customer);

        return CreateTranslationJob(newJob);
    }

    private double CalculatePrice(string content)
    {
        return content.Length * _currentPricePerCharacter;
    }
}
