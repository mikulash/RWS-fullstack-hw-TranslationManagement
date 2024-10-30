using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class TranslatorRepository(AppDbContext context) :ITranslatorRepository
{
    public void Add(Translator entity)
    {
       context.Translators.Add(entity);
    }

    public void Update(Translator entity)
    {
        context.Translators.Update(entity);
    }

    public void Delete(Translator entity)
    {
        context.Translators.Remove(entity);
    }

    public async Task<Translator?> GetByIdAsync(int id)
    {
        return await context.Translators.FindAsync(id);
    }

    public async Task<IEnumerable<Translator>> GetAllAsync()
    {
        return await context.Translators.ToListAsync();
    }

    public async Task<IEnumerable<Translator>> FindByNameAsync(string name)
    {
        return await context.Translators.Where(t => t.Name == name).ToListAsync();
    }
}
