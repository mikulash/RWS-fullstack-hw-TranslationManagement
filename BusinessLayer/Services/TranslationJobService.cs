using System.Xml.Linq;
using BusinessLayer.Dtos;
using BusinessLayer.Enums;
using DataAccessLayer.Models;
using DataAccessLayer.UnitOfWork;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services;

public class TranslationJobService(IUnitOfWork unitOfWork) : ITranslationJobService
{
    private const double PricePerCharacter = 0.01;

    public TranslationJobDto[] GetJobs()
    {
        var translationJobs = unitOfWork.TranslationJobs.GetAllAsync().Result;
        return translationJobs.Adapt<TranslationJobDto[]>();
    }

    public bool CreateTranslationJob(CreateTranslationJobDto createTranslationJobDto)
    {
        var translationJob = createTranslationJobDto.Adapt<TranslationJob>();
        translationJob.Status = Enum.GetName(typeof(JobStatus), JobStatus.New) ?? "New";
        translationJob.Price = CalculatePrice(translationJob.OriginalContent);
        unitOfWork.TranslationJobs.Add(translationJob);
        return unitOfWork.Commit().Result;
    }

    public bool UpdateJobStatus(int jobId, JobStatus status)
    {
        var translationJob = unitOfWork.TranslationJobs.GetByIdAsync(jobId).Result;
        if (translationJob == null) return false;

        translationJob.Status = Enum.GetName(typeof(JobStatus), status) ?? "New";
        unitOfWork.TranslationJobs.Update(translationJob);
        return unitOfWork.Commit().Result;
    }

    public bool CreateJobWithFile(IFormFile file, string customer)
    {
        var reader = new StreamReader(file.OpenReadStream());
        string content;

        if (file.FileName.EndsWith(".txt"))
        {
            content = reader.ReadToEnd();
        }
        else if (file.FileName.EndsWith(".xml"))
        {
            var xdoc = XDocument.Parse(reader.ReadToEnd());
            content = xdoc.Root?.Element("Content")?.Value ??
                      throw new InvalidOperationException("Content element is missing");
            // todo check xml and fix overwriting customer
            customer = xdoc.Root?.Element("Customer")?.Value.Trim() ??
                       throw new InvalidOperationException("Customer element is missing");
        }
        else
        {
            throw new NotSupportedException("unsupported file");
        }

        var newJob = new CreateTranslationJobDto
        {
            OriginalContent = content,
            TranslatedContent = "",
            CustomerName = customer
        };

        newJob.Price = CalculatePrice(newJob.OriginalContent);

        return CreateTranslationJob(newJob);
    }

    private static double CalculatePrice(string content, double pricePerCharacter = PricePerCharacter)
    {
        return content.Length * pricePerCharacter;
    }
}
