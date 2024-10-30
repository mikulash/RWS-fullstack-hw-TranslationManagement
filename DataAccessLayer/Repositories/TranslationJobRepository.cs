using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class TranslationJobRepository(AppDbContext context) : ITranslationJobRepository
{
    public void Add(TranslationJob entity)
    {
        context.TranslationJobs.Add(entity);
    }

    public void Update(TranslationJob entity)
    {
        context.TranslationJobs.Update(entity);
    }

    public void Delete(TranslationJob entity)
    {
        context.TranslationJobs.Remove(entity);
    }

    public async Task<TranslationJob?> GetByIdAsync(int id)
    {
        return await context.TranslationJobs.FindAsync(id);
    }

    public async Task<IEnumerable<TranslationJob>> GetAllAsync()
    {
        return await context.TranslationJobs.ToListAsync();
    }
}
