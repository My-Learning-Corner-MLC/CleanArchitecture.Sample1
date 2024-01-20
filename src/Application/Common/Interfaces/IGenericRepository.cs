using System.Linq.Expressions;
using Sample1.Application.Constants;

namespace Sample1.Application.Common.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll(
        Expression<Func<T, bool>>? filterExpression = default, 
        int page = RepositoryConstant.DEFAULT_PAGE_NUMBER, 
        int size = RepositoryConstant.DEFAULT_SIZE_PER_PAGE, 
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderByFunc = default, 
        string? includes = default,
        CancellationToken cancellationToken = default
    );

    Task<T?> GetBy(Expression<Func<T, bool>> predicateExpression, string? includes = default, CancellationToken cancellationToken = default);

    void Add(T entity);

    void AddRange(IEnumerable<T> entites);

    void Delete(T entity);

    void DeleteRange(IEnumerable<T> entities);

    Task<bool> Update(T entity);

    Task<int> CountAll(Expression<Func<T, bool>>? filter = default, CancellationToken cancellationToken = default);
}