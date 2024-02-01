using Moq;
using Sample1.Application.Common.Exceptions;
using Sample1.Application.Common.Interfaces;
using Sample1.Application.ProductItems.Commands.DeleteProductItem;
using Sample1.Domain.Entities;

namespace Sample1.UnitTests.HandlerTests;

public class DeleteProductItemCommandHandlerTests
{
    [Fact]
    public async Task Handler_ProductNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var deleteCommandFaker = DataMockHelper.DeleteProductItemCommandMock();
        
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork
            .Setup(uow => uow.Products.GetById(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(default(ProductItem));

        var commandHandler = new DeleteProductItemCommandHandler(mockUnitOfWork.Object);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(deleteCommandFaker, CancellationToken.None));
    }
}