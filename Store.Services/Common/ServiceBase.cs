using AutoMapper;
using Microsoft.Extensions.Logging;
using MLGStore.Data;
using MLGStore.Services.Automapper;

namespace MLGStore.Services.Common
{
    public class ServiceBase
    {
        public readonly StoreDbContext dbContext;
        public readonly ILogger<ServiceBase> logger;
        public static IMapper mapper = EntityMapper.lazy.Value;

        public ServiceBase(StoreDbContext dbContext,
            ILogger<ServiceBase> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
    }
}
