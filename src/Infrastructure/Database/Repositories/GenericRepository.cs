using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample1.Application.Common.Interfaces;
using Sample1.Application.Constants;
using Sample1.Domain.Common;

namespace Sample1.Infrastructure.Database.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseAuditableEntity
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly ILogger<UnitOfWork> _logger;

    public GenericRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger) 
        => (_dbSet, _logger) = (context.Set<TEntity>(), logger);

    public virtual void Add(TEntity entity)
    {
        _logger.LogInformation("Adding a new {recordType} record", entity.GetType().Name);
        _dbSet.Add(entity);
    }

    public virtual void AddRange(IEnumerable<TEntity> entites)
    {
        _logger.LogInformation("Adding {recordCount} new {recordType} records", entites.Count(), entites.First().GetType().Name);

        _dbSet.AddRange(entites);
    }

    public virtual async Task<int> CountAll(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Counting all records");
        IQueryable<TEntity> queryable = _dbSet;
        if (filter is not null)
        {
            queryable = queryable.Where(filter);
        }

        return await queryable.CountAsync(cancellationToken);
    }

    public virtual void Delete(TEntity entity)
    {
        _logger.LogInformation("Deleting {recordType} record: {recordId}", entity.GetType().Name, entity.Id);
        entity.IsDeleted = true;
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        _logger.LogInformation(
            "Deleting {recordCount} {recordType} records: {recordIds}", 
            entities.Count(),
            entities.First().GetType().Name,
            string.Join(", ", entities.Select(e => e.Id).ToArray())
        );

        entities.ToList().ForEach(e => e.IsDeleted = true);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll(
        Expression<Func<TEntity, bool>>? filterExpression = null, 
        int page = RepositoryConstant.DEFAULT_PAGE_NUMBER, 
        int size = RepositoryConstant.DEFAULT_SIZE_PER_PAGE, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderByFunc = null, 
        string? includes = null,
        CancellationToken cancellationToken = default
    )
    {
        _logger.LogInformation("Getting all records with page {pageNumber} and size {sizeNumber}", page, size);
        IQueryable<TEntity> query = _dbSet;
        if (filterExpression is not null)
        {
            query = query.Where(filterExpression);
        }

        if (page is not 0 && size is not 0)
        {
            var skip = GetSkippingRecord(page, size);
            query = query.Skip(skip).Take(size);
        }

        if (includes is not null)
        {
            var includeProperties = includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
        }

        query = query.AsNoTracking();

        return orderByFunc is not null 
            ? await orderByFunc(query).ToListAsync(cancellationToken) 
            : (IEnumerable<TEntity>)await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetBy(Expression<Func<TEntity, bool>> predicateExpression, string? includes = null, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting record with predicate condition");
        IQueryable<TEntity> query = _dbSet;
        if (includes is not null)
        {
            var includeProperties = includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
        }

        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(predicateExpression, cancellationToken);
    }

    public virtual Task<bool> Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    private static int GetSkippingRecord(int page, int size)
    {
        return (page - 1) * size;
    }
}