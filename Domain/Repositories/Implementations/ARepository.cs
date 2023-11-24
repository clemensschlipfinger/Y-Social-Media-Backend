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

public abstract class ARepository<TEntity> : IRepository<TEntity> where TEntity : class {
    protected readonly YDbContext Context;
    protected readonly DbSet<TEntity> Table;

    protected ARepository(YDbContext context) {
        Context = context;
        Table = Context.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> Read() {
        return Table;
    }

    public virtual async Task<TEntity?> ReadAsync(int id) {
        return await Table.FindAsync(id);
    }

    public virtual IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> filter) {
        return Table.Where(filter);
    }

    public virtual async Task<TEntity> CreateAsync(TEntity course) {
        Table.Add(course);
        await Context.SaveChangesAsync();
        return course;
    }

    public virtual async Task<List<TEntity>> CreateAsync(List<TEntity> entity) {
        Table.AddRange(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(TEntity entity) {
        Context.ChangeTracker.Clear();
        Table.Update(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(IEnumerable<TEntity> entity) {
        Context.ChangeTracker.Clear();
        Table.UpdateRange(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity) {
        Context.ChangeTracker.Clear();
        Table.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(IEnumerable<TEntity> entity) {
        Context.ChangeTracker.Clear();
        Table.RemoveRange(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> filter) {
        Context.ChangeTracker.Clear();
        Table.RemoveRange(Table.Where(filter));
        await Context.SaveChangesAsync();
    }
}