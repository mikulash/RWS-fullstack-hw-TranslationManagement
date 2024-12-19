using BusinessLayer.Dtos;
using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services;

public interface ITranslationJobService
{
    Task<IEnumerable<TranslationJobDto>> GetJobsAsync();

    Task<bool> CreateTranslationJobAsync(CreateTranslationJobDto createTranslationJobDto);

    Task<bool> UpdateJobStatusAsync(int jobId, JobStatus status);

    Task<bool> CreateJobWithFileAsync(IFormFile file, string customer);
}
