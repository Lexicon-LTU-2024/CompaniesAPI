using Companies.Infrastructure.Data;
using Domain.Contracts;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Infrastructure.Repository;
public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected DBContext Context { get; }
    protected DbSet<T> DbSet { get; }

    public RepositoryBase(DBContext db)
    {
        Context  = db;
        DbSet = db.Set<T>();
    }

    public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ? DbSet.AsNoTracking() : 
                        DbSet;

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges ? DbSet.Where(expression).AsNoTracking() :
                        DbSet.Where(expression);

    public void Update(T entity) => DbSet.Update(entity);
    public async Task CreateAsync(T entity) => await DbSet.AddAsync(entity);
    public void Delete(T entity) => DbSet.Remove(entity);

}
