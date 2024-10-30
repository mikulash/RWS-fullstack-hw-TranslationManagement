using BusinessLayer.Dtos;
using DataAccessLayer.Enums;

namespace BusinessLayer.Services;

public interface ITranslatorService
{
    public IEnumerable<TranslatorDto> GetAllTranslators();
    public IEnumerable<TranslatorDto> GetTranslatorsByName(string name);

    public bool AddTranslator(CreateTranslatorDto translatorDto);
    public bool UpdateTranslatorStatus(int translatorId, TranslatorStatus newStatus);
}
