using MLGStore.Services.DTOs;

namespace MLGStore.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Result<string>> LoginAsync(LoginRequestDTO loginRequest);
    }
}
