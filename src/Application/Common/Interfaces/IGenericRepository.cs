using System.Linq.Expressions;
using Sample1.Application.Common.Models;
using Sample1.Domain.Common;

namespace Sample1.Application.Common.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseAuditableEntity
{
    Task<IEnumerable<TEntity>> GetAll(GetAllParameters<TEntity> parameters);

    Task<TEntity?> GetBy(
        Expression<Func<TEntity, bool>> predicateExpression,
        bool includeDeleted = false,
        string? includeProperties = default,
        bool trackingChanges = false,
        CancellationToken cancellationToken = default
    );

    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entites);

    void Delete(TEntity entity);

    void DeleteRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    Task<int> CountAll(
        Expression<Func<TEntity, bool>>? filterExpression = default,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default
    );
}