using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MLGStore.Data;
using MLGStore.Entities;
using MLGStore.Services.Common;
using MLGStore.Services.DTOs;
using MLGStore.Services.Interfaces;

namespace MLGStore.Services.Services
{
    public class StoreService : ServiceBase, IStoreService
    {
        public StoreService(StoreDbContext dbContext,
            ILogger<StoreService> logger)
            : base(dbContext, logger)
        {
        }

        public async Task<Result<StoreDTO>> InsertAsync(CreateStoreDTO createDto)
        {
            try
            {
                var entity = mapper.Map<Store>(createDto);
                dbContext.Stores.Add(entity);
                await dbContext.SaveChangesAsync();

                return Result<StoreDTO>
                    .CreateResult(mapper.Map<StoreDTO>(entity));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<StoreDTO>
                    .CreateExceptionResult(ex);
            }
        }

        public async Task<Result<StoreDTO>> UpdateAsync(long storeId, CreateStoreDTO createDto)
        {
            try
            {
                var dbEntity = await dbContext.Stores
                    .FirstOrDefaultAsync(x => x.Id == storeId);

                if (dbEntity == null)
                    return Result<StoreDTO>
                        .CreateResult(null, "Store not found");

                dbEntity = mapper.Map(createDto, dbEntity);
                await dbContext.SaveChangesAsync();

                return Result<StoreDTO>
                    .CreateResult(mapper.Map<StoreDTO>(dbEntity));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<StoreDTO>
                    .CreateExceptionResult(ex);
            }
        }

        public async Task<Result<bool>> DeleteAsync(long storeId)
        {
            try
            {
                var dbEntity = await dbContext.Stores
                    .FirstOrDefaultAsync(x => x.Id == storeId);

                if (dbEntity == null)
                    return Result<bool>
                        .CreateResult(false, "Store not found");

                dbContext.Stores.Remove(dbEntity);
                await dbContext.SaveChangesAsync();

                return Result<bool>.CreateResult(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<bool>
                    .CreateExceptionResult(ex);
            }
        }

        public async Task<Result<StoreDTO>> FindByIdAsync(long storeId)
        {
            try
            {
                var dbEntity = await dbContext.Stores
                    .FirstOrDefaultAsync(x => x.Id == storeId);

                if (dbEntity == null)
                    return Result<StoreDTO>
                        .CreateResult(null, "Store not found");

                return Result<StoreDTO>
                    .CreateResult(mapper.Map<StoreDTO>(dbEntity));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<StoreDTO>
                    .CreateExceptionResult(ex);
            }
        }

        public async Task<Result<List<StoreDTO>>> GetAllAsync()
        {
            try
            {
                var dbEntities = await dbContext.Stores
                    .ToListAsync();

                return Result<List<StoreDTO>>
                    .CreateResult(mapper.Map<List<StoreDTO>>(dbEntities));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<List<StoreDTO>>
                    .CreateExceptionResult(ex);
            }
        }
    }
}