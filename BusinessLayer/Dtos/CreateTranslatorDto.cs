using DataAccessLayer.Enums;

namespace BusinessLayer.Dtos;

public class CreateTranslatorDto
{
    public string Name { get; set; }
    public string HourlyRate { get; set; }
    public TranslatorStatus Status { get; set; }
    public string CreditCardNumber { get; set; }
}
