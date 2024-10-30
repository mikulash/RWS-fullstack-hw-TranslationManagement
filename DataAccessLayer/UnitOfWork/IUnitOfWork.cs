using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace DataAccessLayer.UnitOfWork;

public interface IUnitOfWork
{
    ITranslatorJobRepository TranslationJobs { get; }
    ITranslatorRepository Translators { get; }

    Task Commit();
    Task Rollback();
}
