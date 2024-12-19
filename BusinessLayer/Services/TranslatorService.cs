using BusinessLayer.Dtos;
using DataAccessLayer.Enums;
using DataAccessLayer.Models;
using DataAccessLayer.UnitOfWork;
using Mapster;

namespace BusinessLayer.Services;

public class TranslatorService : ITranslatorService
{
    private readonly IUnitOfWork _unitOfWork;

    public TranslatorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TranslatorDto>> GetAllTranslatorsAsync()
    {
        var translators = await _unitOfWork.Translators.GetAllAsync();
        return translators.Adapt<IEnumerable<TranslatorDto>>();
    }

    public async Task<IEnumerable<TranslatorDto>> GetTranslatorsByNameAsync(string name)
    {
        var translators = await _unitOfWork.Translators.FindByNameAsync(name);
        return translators.Adapt<IEnumerable<TranslatorDto>>();
    }

    public async Task<bool> AddTranslatorAsync(CreateTranslatorDto translatorDto)
    {
        var translator = translatorDto.Adapt<Translator>();
        _unitOfWork.Translators.Add(translator);
        return await _unitOfWork.Commit();
    }

    public async Task<bool> UpdateTranslatorStatusAsync(int translatorId, TranslatorStatus newStatus)
    {
        var translator = await _unitOfWork.Translators.GetByIdAsync(translatorId);
        if (translator == null) throw new KeyNotFoundException("Translator not found");

        translator.Status = newStatus;
        _unitOfWork.Translators.Update(translator);
        return await _unitOfWork.Commit();
    }
}
