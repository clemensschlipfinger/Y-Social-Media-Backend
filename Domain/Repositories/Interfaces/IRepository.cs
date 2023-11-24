
using System.Linq.Expressions;


namespace Domain.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class {
    Task<TEntity?> ReadAsync(int id);
    IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> filter);
    IQueryable<TEntity> Read();
    Task<TEntity> CreateAsync(TEntity entity);
    Task<List<TEntity>> CreateAsync(List<TEntity> entity);
    Task UpdateAsync(TEntity entity);
    Task UpdateAsync(IEnumerable<TEntity> entity);
    Task DeleteAsync(TEntity entity);
    Task DeleteAsync(IEnumerable<TEntity> entity);
    Task DeleteAsync(Expression<Func<TEntity, bool>> filter);
}