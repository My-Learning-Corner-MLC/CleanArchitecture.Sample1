namespace Sample1.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IProductRepository Products { get; }

    Task SaveChangeAsync(CancellationToken cancellationToken);
}