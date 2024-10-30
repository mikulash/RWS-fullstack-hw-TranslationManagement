using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface ITranslatorRepository : IRepository<Translator>
{
    Task<IEnumerable<Translator>> FindByNameAsync(string name);
}
