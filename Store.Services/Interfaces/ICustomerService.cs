using MLGStore.Entities;
using MLGStore.Services.DTOs;

namespace MLGStore.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Result<CustomerDTO>> InsertAsync(CreateCustomerDTO createDto);
        Task<Result<CustomerDTO>> UpdateAsync(long customerId, UpdateCustomerDTO updateDto);
        Task<Result<bool>> DeleteAsync(long customerId);
        Task<Result<CustomerDTO>> FindByIdAsync(long customerId);
        Task<Result<List<CustomerDTO>>> GetAllAsync();
    }
}
