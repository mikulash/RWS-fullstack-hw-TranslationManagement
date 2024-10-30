using BusinessLayer.Dtos;
using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services;

public interface ITranslationJobService
{
    public TranslationJobDto[] GetJobs();

    bool CreateTranslationJob(CreateTranslationJobDto createTranslationJobDto);

    public bool UpdateJobStatus(int jobId, JobStatus status);

    public bool CreateJobWithFile(IFormFile file, string customer);
}