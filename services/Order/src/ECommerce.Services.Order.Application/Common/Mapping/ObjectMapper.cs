namespace ECommerce.Services.Order.Application.Common.Mapping;
public class ObjectMapper
{
    private static readonly Lazy<IMapper> lazy = new (() => {
        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<MappingProfile>();
        });

        return config.CreateMapper();
    });

    public static IMapper Mapper => lazy.Value;
}