﻿namespace DataAccessLayer.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
}
