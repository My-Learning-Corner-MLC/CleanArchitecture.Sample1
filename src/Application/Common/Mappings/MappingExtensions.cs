using Microsoft.EntityFrameworkCore;
using Sample1.Application.Common.Models;

namespace Sample1.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

    public static async Task<TDestination?> GetItemAsync<TDestination>(this IQueryable<TDestination> queryable, CancellationToken cancellationToken) where TDestination : class
        => await queryable.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
}
