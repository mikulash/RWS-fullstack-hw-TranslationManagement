using DataAccessLayer.Enums;

namespace DataAccessLayer.Models;

public class Translator
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string HourlyRate { get; set; }
    public TranslatorStatus Status { get; set; } = TranslatorStatus.Applicant;
    public string CreditCardNumber { get; set; }
}
