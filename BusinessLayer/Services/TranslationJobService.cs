using System.Xml.Linq;
using BusinessLayer.Dtos;
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

    public TranslationJobDto[] GetJobs()
    {
        var translationJobs = unitOfWork.TranslationJobs.GetAllAsync().Result;
        return translationJobs.Adapt<TranslationJobDto[]>();
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
        var reader = new StreamReader(file.OpenReadStream());

        CreateTranslationJobDto newJob;

        if (file.FileName.EndsWith(".txt"))
        {
            if (customer == "") throw new InvalidOperationException("Customer name is missing");

            var content = reader.ReadToEnd();
            newJob = new CreateTranslationJobDto
            {
                OriginalContent = content,
                CustomerName = customer
            };
        }
        else if (file.FileName.EndsWith(".xml"))
        {
            newJob = ParseJobXml(file);
        }
        else
        {
            throw new NotSupportedException("unsupported file type");
        }

        newJob.Price = CalculatePrice(newJob.OriginalContent);

        return CreateTranslationJob(newJob);
    }

    private static CreateTranslationJobDto ParseJobXml(IFormFile file)
    {
        var reader = new StreamReader(file.OpenReadStream());
        var xdoc = XDocument.Parse(reader.ReadToEnd());
        var content = xdoc.Root?.Element("Content")?.Value ??
                      throw new InvalidOperationException("Content element is missing");
        var customer = xdoc.Root?.Element("Customer")?.Value.Trim() ??
                       throw new InvalidOperationException("Customer element is missing");
        return new CreateTranslationJobDto
        {
            OriginalContent = content,
            CustomerName = customer
        };

    }

    private double CalculatePrice(string content)
    {
        var pricePerCharacter = _currentPricePerCharacter;
        return content.Length * pricePerCharacter;
    }
}
