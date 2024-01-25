using MediatR;
using Moq;
using Sample1.API.Endpoints;
using Sample1.Application.Common.Exceptions;

namespace Sample1.UnitTests.EndPointTests;

public class DeleteProductItemDetailTests
{
    [Fact]
    public async Task DeleteProductItem_InvalidId_ShouldThrowValidationException()
    {
        // Arrange
        var mockInvalidId = -1;
        var mockSender = new Mock<ISender>();
        var productEndpoint = new ProductItems();

        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(() => productEndpoint.DeleteProductItem(mockSender.Object, mockInvalidId));
    }
}