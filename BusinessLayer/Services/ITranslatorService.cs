using BusinessLayer.Dtos;

namespace BusinessLayer.Services;

public interface ITranslatorService
{
    public IEnumerable<TranslatorDto> GetAllTranslators();
    public IEnumerable<TranslatorDto> GetTranslatorsByName(string name);

    public bool AddTranslator(TranslatorDto translatorDto);
}
