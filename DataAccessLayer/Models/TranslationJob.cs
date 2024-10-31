using DataAccessLayer.Enums;

namespace DataAccessLayer.Models;

public class TranslationJob
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public JobStatus Status { get; set; } = JobStatus.New;
    public string OriginalContent { get; set; } = string.Empty;
    public string TranslatedContent { get; set; } = string.Empty;
    public double Price { get; set; }
}
