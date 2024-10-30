using System.Xml.Linq;
using BusinessLayer.Dtos;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Utils.JobFileParser;

public class XmlJobFileParser : IJobFileParser
{
    public CreateTranslationJobDto Parse(IFormFile file, string customer)
    {
        using var reader = new StreamReader(file.OpenReadStream());

        var xdoc = XDocument.Parse(reader.ReadToEnd());
        var content = xdoc.Root?.Element("Content")?.Value ??
                      throw new InvalidOperationException("Content element is missing");

        var parsedCustomer = xdoc.Root?.Element("Customer")?.Value.Trim() ??
                             throw new InvalidOperationException("Customer element is missing");
        return new CreateTranslationJobDto
        {
            OriginalContent = content,
            CustomerName = parsedCustomer
        };
    }
}
