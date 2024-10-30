using BusinessLayer.Dtos;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Utils.JobFileParser;

public class JobFileParserContext
{
    private IJobFileParser _fileParser;

    public JobFileParserContext(IJobFileParser fileParser)
    {
        _fileParser = fileParser;
    }

    public JobFileParserContext(string fileExtension)
    {
        _fileParser = fileExtension switch
        {
            ".xml" => new XmlJobFileParser(),
            ".txt" => new TxtJobFileParser(),
            _ => throw new InvalidOperationException("Invalid file extension")
        };
    }

    public void SetParser(IJobFileParser fileParser)
    {
        _fileParser = fileParser;
    }

    public CreateTranslationJobDto Parse(IFormFile file, string customer)
    {
        return _fileParser.Parse(file, customer);
    }
}
