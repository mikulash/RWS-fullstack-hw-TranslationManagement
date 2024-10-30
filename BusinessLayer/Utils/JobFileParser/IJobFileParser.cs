using BusinessLayer.Dtos;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Utils.JobFileParser;

public interface IJobFileParser
{
    CreateTranslationJobDto Parse(IFormFile file, string customer);
}
