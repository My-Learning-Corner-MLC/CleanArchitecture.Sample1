using System.Linq.Expressions;
using Sample1.Application.Constants;
using Sample1.Domain.Common;

namespace Sample1.Application.Common.Models;

public class GetAllParameters<TEntity> where TEntity : BaseAuditableEntity
{
    public Expression<Func<TEntity, bool>>? FilterExpression { get; init; }

    public int Page { get; init; } = RepositoryConstant.DEFAULT_PAGE_NUMBER;

    public int Size { get; init; } = RepositoryConstant.DEFAULT_SIZE_PER_PAGE;

    public bool IncludeDeleted { get; init; } = false;

    public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderByFunc { get; init; }

    public string? IncludeProperties { get; init; }

    public bool TrackingChanges { get; init; } = false;
    
    public CancellationToken CancellationToken { get; init; } = default;
}