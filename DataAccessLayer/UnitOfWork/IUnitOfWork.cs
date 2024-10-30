using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace DataAccessLayer.UnitOfWork;

public interface IUnitOfWork
{
    IRepository<TranslationJob> TranslationJobs { get; }
    IRepository<Translator> Translators { get; }

    Task Commit();
    Task Rollback();
}
