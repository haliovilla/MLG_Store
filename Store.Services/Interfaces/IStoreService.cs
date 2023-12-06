using MLGStore.Services.DTOs;

namespace MLGStore.Services.Interfaces
{
    public interface IStoreService
    {
        Task<Result<StoreDTO>> InsertAsync(CreateStoreDTO createDto);
        Task<Result<StoreDTO>> UpdateAsync(long storeId, CreateStoreDTO createDto);
        Task<Result<bool>> DeleteAsync(long customerId);
        Task<Result<StoreDTO>> FindByIdAsync(long storeId);
        Task<Result<List<StoreDTO>>> GetAllAsync();
    }
}
