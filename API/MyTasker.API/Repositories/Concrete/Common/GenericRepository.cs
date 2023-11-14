using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyTasker.API.Context;
using MyTasker.API.Repositories.Abstract;
using MyTasker.Core.Models;
using System.Linq.Expressions;

namespace MyTasker.API.Repositories.Concrete;

public class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : BaseModel
{
    private readonly MyTaskerContext _context;

    public GenericRepository(MyTaskerContext context)
    {
        _context = context;
    }

    public DbSet<TSource> Table => _context.Set<TSource>();

    public async Task<List<TSource>?> GetAllAsync() => await Table.AsNoTracking().ToListAsync();

    public async Task<List<TSource>?> GetAllAsync(Expression<Func<TSource, bool>> filter) =>
        await Table.AsNoTracking().Where(filter).ToListAsync();

    public async Task<int> GetCountAsync() => await Table.CountAsync();

    public async Task<int> GetCountAsync(Expression<Func<TSource, bool>> filter) =>
        await Table.CountAsync(filter);

    public async Task<TSource?> GetSingleAsync(Expression<Func<TSource, bool>> filter) =>
        await Table.FirstOrDefaultAsync(filter);

    public async Task<bool> InsertAsync(TSource item)
    {
        try
        {
            EntityEntry<TSource> entityEntry = await Table.AddAsync(item);
            await SaveAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> RemoveAsync(int id)
    {
        try
        {
            var model = await Table.FirstOrDefaultAsync(x => x.Id == id);
            Table.Remove(model);
            await SaveAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> RemoveRangeAsync(List<int> ids)
    {
        try
        {
            var models = await Table.AsNoTracking().Where(x => ids.Contains(x.Id)).ToListAsync();
            Table.RemoveRange(models);
            await SaveAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public async Task<bool> UpdateAsync(TSource item)
    {
        try
        {
            EntityEntry<TSource> entityEntry = Table.Update(item);
            await SaveAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
