using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample1.Application.Common.Exceptions;
using Sample1.Application.Common.Interfaces;

namespace Sample1.Infrastructure.Database.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public IProductRepository Products { get; private set; }

    public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;

        // Inital repositories
        Products = new ProductRepository(_context, _logger);
    }

    public async Task SaveChangeAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Begin saving changes with contextId: {contextId}", _context.ContextId);
            
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("End saving changes");
        }
        catch (DbUpdateConcurrencyException concurrencyException)
        {
            _logger.LogError(concurrencyException, "Concurrency error occured: {message}", concurrencyException.Message);
            
            throw new ConflictException(errorMessage: "Resource conflicted", errorDescription: concurrencyException.Message);
        }
    }

    // Implement dispose process
    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}