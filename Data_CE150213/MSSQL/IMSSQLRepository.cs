using System.Linq.Expressions;

namespace Data_CE150213.Repository.MSSQL;

public interface IMSSQLRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetAsync(int ID);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filters);
    Task<List<TEntity>> GetAllAsync();
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filters);
    Task<bool> AnyAsync(int ID);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filters);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> filters);
    Task<bool> CreateAsync(TEntity entity);
    Task<bool> CreateRangeAsync(List<TEntity> entities);
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> UpdateRangeAsync(List<TEntity> entities);
    Task<bool> DeleteAsync(TEntity entity);
    Task<bool> DeleteRangeAsync(List<TEntity> entities);
    Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> filters);
}
