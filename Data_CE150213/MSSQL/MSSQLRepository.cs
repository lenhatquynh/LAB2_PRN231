using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data_CE150213.Repository.MSSQL;

public class MSSQLRepository<TDbContext, TEntity> : IMSSQLRepository<TEntity> where TDbContext : DbContext where TEntity : class
{
    private readonly TDbContext DbContext;
    private readonly DbSet<TEntity> DbSet;

    public MSSQLRepository(TDbContext DbContext)
    {
        this.DbContext = DbContext;
        DbSet = this.DbContext.Set<TEntity>();
    }

    public async Task<bool> AnyAsync(int ID)
    {
        var result = await DbSet.FindAsync(ID);
        return result != null;
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filters)
    {
        return await DbSet.AnyAsync(filters);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filters)
    {
        return await DbSet.CountAsync(filters);
    }

    public async Task<bool> CreateAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        var result = await DbContext.SaveChangesAsync();
        return Convert.ToBoolean(result);
    }

    public async Task<bool> CreateRangeAsync(List<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
        var result = await DbContext.SaveChangesAsync();
        return Convert.ToBoolean(result);
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        var result = await DbContext.SaveChangesAsync();
        return Convert.ToBoolean(result);
    }

    public async Task<bool> DeleteRangeAsync(List<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
        var result = await DbContext.SaveChangesAsync();
        return Convert.ToBoolean(result);
    }

    public async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> filters)
    {
        var entities = await GetAllAsync(filters);
        return await DeleteRangeAsync(entities);
    }

    public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filters)
    {
        return Task.FromResult(DbSet.Where(filters).ToList());
    }

    public Task<List<TEntity>> GetAllAsync()
    {
        return Task.FromResult(DbSet.ToList());
    }

    public async Task<TEntity> GetAsync(int ID)
    {
        return await DbSet.FindAsync(ID);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filters)
    {
        return await DbSet.FirstOrDefaultAsync(filters);
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        var result = await DbContext.SaveChangesAsync();
        return Convert.ToBoolean(result);
    }

    public async Task<bool> UpdateRangeAsync(List<TEntity> entities)
    {
        DbSet.UpdateRange(entities);
        var result = await DbContext.SaveChangesAsync();
        return Convert.ToBoolean(result);
    }
}
