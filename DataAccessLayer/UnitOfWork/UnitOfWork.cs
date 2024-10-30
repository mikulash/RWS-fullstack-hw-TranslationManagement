using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.UnitOfWork;

public class UnitOfWork(AppDbContext context): IUnitOfWork
{
    private ITranslationJobRepository? _translationJobs;
    private ITranslatorRepository? _translators;

    // get the repository if it exists, otherwise create a new one
    public ITranslationJobRepository TranslationJobs => _translationJobs ??= new TranslationJobRepository(context);
    public ITranslatorRepository Translators => _translators ??= new TranslatorRepository(context);

    public async Task<bool> Commit()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task Rollback()
    {
        foreach (var entry in context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Deleted:
                    await entry.ReloadAsync();
                    break;

            }
        }

    }
}
