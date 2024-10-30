using BusinessLayer.Dtos;
using BusinessLayer.Enums;
using DataAccessLayer.Models;
using DataAccessLayer.UnitOfWork;
using Mapster;

namespace BusinessLayer.Services;

public class TranslatorService(IUnitOfWork unitOfWork) : ITranslatorService
{
    public IEnumerable<TranslatorDto> GetAllTranslators()
    {
        var translators = unitOfWork.Translators.GetAllAsync().Result;
        return translators.Adapt<IEnumerable<TranslatorDto>>();
    }

    public IEnumerable<TranslatorDto> GetTranslatorsByName(string name)
    {
        var translators = unitOfWork.Translators.FindByNameAsync(name);
        return translators.Adapt<IEnumerable<TranslatorDto>>();
    }

    public bool AddTranslator(TranslatorDto translatorDto)
    {
        var translator = translatorDto.Adapt<Translator>();
        unitOfWork.Translators.Add(translator);
        return unitOfWork.Commit().Result;
    }

    public bool UpdateTranslatorStatus(int translatorId, TranslatorStatus newStatus)
    {
        var translator = unitOfWork.Translators.GetByIdAsync(translatorId).Result;
        if (translator == null)
        {
            throw new KeyNotFoundException("Translator not found");
        }

        translator.Status = Enum.GetName(typeof(TranslatorStatus), newStatus) ?? "Applicant";
        unitOfWork.Translators.Update(translator);
        return unitOfWork.Commit().Result;
    }
}
