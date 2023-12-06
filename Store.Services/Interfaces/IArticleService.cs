using MLGStore.Services.DTOs;

namespace MLGStore.Services.Interfaces
{
    public interface IArticleService
    {
        Task<Result<ArticleDTO>> InsertAsync(CreateArticleWithImageUrlDTO createDto);
        Task<Result<ArticleDTO>> InsertAsync(CreateArticleWithImageFileDTO createDto);
        Task<Result<ArticleDTO>> UpdateAsync(long articleId, CreateArticleWithImageUrlDTO createDto);
        Task<Result<ArticleDTO>> UpdateAsync(long articleId, CreateArticleWithImageFileDTO createDto);
        Task<Result<bool>> DeleteAsync(long articleId);
        Task<Result<ArticleDTO>> FindByIdAsync(long articleId);
        Task<Result<List<ArticleDTO>>> GetAllAsync();

        Task<Result<List<ShoppingCartItemDTO>>> GetShoppingCartItemsAsync(long customerId);
        Task<Result<ShoppingCartItemDTO>> AddShoppingCartItemasync(long customerId, long articleId);
        Task<Result<bool>> RemoveShoppingCartItemasync(long customerId, long articleId);
    }
}
