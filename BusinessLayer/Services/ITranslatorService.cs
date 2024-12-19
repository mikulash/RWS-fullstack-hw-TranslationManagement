using BusinessLayer.Dtos;
using DataAccessLayer.Enums;

namespace BusinessLayer.Services;

public interface ITranslatorService
{
    Task<IEnumerable<TranslatorDto>> GetAllTranslatorsAsync();
    Task<IEnumerable<TranslatorDto>> GetTranslatorsByNameAsync(string name);

    Task<bool> AddTranslatorAsync(CreateTranslatorDto translatorDto);
    Task<bool> UpdateTranslatorStatusAsync(int translatorId, TranslatorStatus newStatus);
}
