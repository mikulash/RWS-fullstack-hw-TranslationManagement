using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace DataAccessLayer.UnitOfWork;

public interface IUnitOfWork
{
    ITranslationJobRepository TranslationJobs { get; }
    ITranslatorRepository Translators { get; }

    Task<bool> Commit();
    Task Rollback();
}
