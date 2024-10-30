using BusinessLayer.Dtos;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Utils.JobFileParser;

public class TxtJobFileParser : IJobFileParser
{
    public CreateTranslationJobDto Parse(IFormFile file, string customer)
    {
        if (customer == "") throw new InvalidOperationException("Customer name is missing");

        using var reader = new StreamReader(file.OpenReadStream());
        var content = reader.ReadToEnd();

        return new CreateTranslationJobDto
        {
            OriginalContent = content,
            CustomerName = customer
        };
    }
}
