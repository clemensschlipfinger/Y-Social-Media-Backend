using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;

namespace Domain.Repositories.Implementations;

public abstract class ARepository<TEntity> : IAsyncDisposable, IRepository<TEntity> where TEntity : class  {
    protected readonly YDbContext Context;
    protected readonly DbSet<TEntity> Table;

    protected ARepository(IDbContextFactory<YDbContext> dbContextFactory) {
        Context = dbContextFactory.CreateDbContext();
        Table = Context.Set<TEntity>();
    }

    public async Task<TEntity?> ReadAsync(int id) => await Table.FindAsync(id);
    public async Task<IEnumerable<TEntity>> ReadAsync() => await Table.ToListAsync();

    public async Task<IEnumerable<TEntity>> ReadAsync(int limit, int offset) =>
        await Table.Skip(offset).Take(limit).ToListAsync();

    public async Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter) =>
        await Table.Where(filter).ToListAsync();

    public async Task<IEnumerable<TEntity>> ReadAsync(int limit, int offset, Expression<Func<TEntity, bool>> filter) =>
        await Table.Where(filter).Skip(offset).Take(limit).ToListAsync();
    
    public async Task<TEntity> CreateAsync(TEntity course) {
        Table.Add(course);
        await Context.SaveChangesAsync();
        return course;
    }

    public async Task<List<TEntity>> CreateAsync(List<TEntity> entity) {
        Table.AddRange(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(TEntity entity) {
        Context.ChangeTracker.Clear();
        Table.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(IEnumerable<TEntity> entity) {
        Context.ChangeTracker.Clear();
        Table.UpdateRange(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity) {
        Context.ChangeTracker.Clear();
        Table.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(IEnumerable<TEntity> entity) {
        Context.ChangeTracker.Clear();
        Table.RemoveRange(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> filter) {
        Context.ChangeTracker.Clear();
        Table.RemoveRange(Table.Where(filter));
        await Context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await Context.DisposeAsync();
    }
}