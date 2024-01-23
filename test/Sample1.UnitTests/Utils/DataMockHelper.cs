using Bogus;
using Sample1.Domain.Entities;

namespace Sample1.UnitTests.Utils;

public static class DataMockHelper
{
    public static ProductItem GetProductItemMock(int? id = default)
    {
        Faker<ProductItem> productFaker = new Faker<ProductItem>()
            .RuleFor(o => o.Id, f => id ?? f.UniqueIndex)
            .RuleFor(o => o.Name, f => f.Name.FullName())
            .RuleFor(o => o.Description, f => f.Lorem.Paragraph())
            .RuleFor(o => o.Price, f => f.Random.Decimal(1, 100))
            .RuleFor(o => o.PictureFileName, f => f.Lorem.Text())
            .RuleFor(o => o.PictureUri, f => f.Internet.Url())
            .RuleFor(o => o.ProductTypeId, f => f.Random.Int(1, 100))
            .RuleFor(o => o.ProductBrandId, f => f.Random.Int(1, 100));

        return productFaker;
    }
}