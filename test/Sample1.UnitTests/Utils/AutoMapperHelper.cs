using AutoMapper;
using Sample1.Application.Profiles;

namespace Sample1.UnitTests.Utils;

public static class AutoMapperHelper
{
    public static IMapper GetMapperInstance()
    {
        MapperConfiguration config = new (cfg => cfg.AddProfile(new ProductItemProfile()));

        return config.CreateMapper();
    }
}