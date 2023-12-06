using AutoMapper;

namespace MLGStore.Services.Automapper
{
    public class EntityMapper
    {
        public static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                cfg.AddProfile<CustomerProfile>();
                cfg.AddProfile<StoreProfile>();
                cfg.AddProfile<ArticleProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper mapper = lazy.Value;
    }
}
