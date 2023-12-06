using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MLGStore.Data;
using MLGStore.Entities;
using MLGStore.Services.Common;
using MLGStore.Services.DTOs;
using MLGStore.Services.Interfaces;
using MLGStore.Services.Validators;

namespace MLGStore.Services.Services
{
    public class ArticleService : ServiceBase, IArticleService
    {
        private readonly IFileStorageService fileStorageService;
        private readonly string container = "articles";

        public ArticleService(StoreDbContext dbContext,
            ILogger<ArticleService> logger,
            IFileStorageService fileStorageService)
            : base(dbContext, logger)
        {
            this.fileStorageService = fileStorageService;
        }

        public async Task<Result<ArticleDTO>> InsertAsync(CreateArticleWithImageUrlDTO createDto)
        {
            try
            {
                var store = await dbContext.Stores
                    .FirstOrDefaultAsync(x => x.Id == createDto.StoreId);
                if (store == null)
                    return Result<ArticleDTO>
                        .CreateResult(null, "Store not found");

                var entity = mapper.Map<Article>(createDto);

                dbContext.Articles.Add(entity);
                await dbContext.SaveChangesAsync();

                return Result<ArticleDTO>
                    .CreateResult(mapper.Map<ArticleDTO>(entity));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<ArticleDTO>
                    .CreateExceptionResult(ex);
            }
        }

        public async Task<Result<ArticleDTO>> InsertAsync(CreateArticleWithImageFileDTO createDto)
        {
            try
            {
                var store = await dbContext.Stores
                    .FirstOrDefaultAsync(x => x.Id == createDto.StoreId);
                if (store == null)
                    return Result<ArticleDTO>
                        .CreateResult(null, "Store not found");

                var entity = mapper.Map<Article>(createDto);

                if (createDto.Image != null)
                {
                    using var ms = new MemoryStream();
                    await createDto.Image.CopyToAsync(ms);
                    var content = ms.ToArray();
                    var extension = Path.GetExtension(createDto.Image.FileName);
                    entity.Image = await fileStorageService.SaveFileAsync(content, extension, container, createDto.Image.ContentType, GroupFileType.Image);
                }

                dbContext.Articles.Add(entity);
                await dbContext.SaveChangesAsync();

                return Result<ArticleDTO>
                    .CreateResult(mapper.Map<ArticleDTO>(entity));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<ArticleDTO>
                    .CreateExceptionResult(ex);
            }
        }

        public async Task<Result<ArticleDTO>> UpdateAsync(long articleId, CreateArticleWithImageUrlDTO createDto)
        {
            try
            {
                var dbEntity = await dbContext.Articles
                    .Include(x => x.ArticlesStores)
                    .FirstOrDefaultAsync(x => x.Id == articleId);
                if (dbEntity == null)
                    return Result<ArticleDTO>
                        .CreateResult(null, "Article not found");

                var store = await dbContext.Stores
                    .FirstOrDefaultAsync(x => x.Id == createDto.StoreId);
                if (store == null)
                    return Result<ArticleDTO>
                        .CreateResult(null, "Store not found");

                dbEntity = mapper.Map(createDto, dbEntity);
                await dbContext.SaveChangesAsync();

                return Result<ArticleDTO>
                    .CreateResult(mapper.Map<ArticleDTO>(dbEntity));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<ArticleDTO>
                    .CreateExceptionResult(ex);
            }
        }

        public async Task<Result<ArticleDTO>> UpdateAsync(long articleId, CreateArticleWithImageFileDTO createDto)
        {
            try
            {
                var dbEntity = await dbContext.Articles
                    .Include(x => x.ArticlesStores)
    .FirstOrDefaultAsync(x => x.Id == articleId);
                if (dbEntity == null)
                    return Result<ArticleDTO>
                        .CreateResult(null, "Article not found");

                dbEntity = mapper.Map(createDto, dbEntity);

                if (createDto.Image != null)
                {
                    using var ms = new MemoryStream();
                    await createDto.Image.CopyToAsync(ms);
                    var content = ms.ToArray();
                    var extension = Path.GetExtension(createDto.Image.FileName);
                    dbEntity.Image = await fileStorageService.EditFileAsync(content, extension, container, dbEntity.Image, createDto.Image.ContentType, GroupFileType.Image);
                }

                await dbContext.SaveChangesAsync();

                return Result<ArticleDTO>
                    .CreateResult(mapper.Map<ArticleDTO>(dbEntity));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<ArticleDTO>
                    .CreateExceptionResult(ex);
            }
        }

        public async Task<Result<bool>> DeleteAsync(long articleId)
        {
            try
            {
                var dbEntity = await dbContext.Articles
                .Include(x => x.ArticlesStores)
.FirstOrDefaultAsync(x => x.Id == articleId);

                if (dbEntity == null)
                    return Result<bool>
                        .CreateResult(false, "Article not found");

                dbContext.Articles.Remove(dbEntity);
                await dbContext.SaveChangesAsync();

                return Result<bool>.CreateResult(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<bool>.CreateExceptionResult(ex);

            }
        }

        public async Task<Result<ArticleDTO>> FindByIdAsync(long articleId)
        {
            try
            {
                var dbEntity = await dbContext.Articles
                    .FirstOrDefaultAsync(x => x.Id == articleId);
                if (dbEntity == null)
                    return Result<ArticleDTO>
                        .CreateResult(null, "Article not found");

                return Result<ArticleDTO>
                    .CreateResult(mapper.Map<ArticleDTO>(dbEntity));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<ArticleDTO>.CreateExceptionResult(ex);
            }
        }

        public async Task<Result<List<ArticleDTO>>> GetAllAsync()
        {
            try
            {
                var dbEntities = await dbContext.Articles
                    .Include(x=>x.ArticlesStores)
                    .ThenInclude(x=>x.Store)
                    .ToListAsync();

                return Result<List<ArticleDTO>>
                    .CreateResult(mapper.Map<List<ArticleDTO>>(dbEntities));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<List<ArticleDTO>>.CreateExceptionResult(ex);
            }
        }

        public async Task<Result<List<ShoppingCartItemDTO>>> GetShoppingCartItemsAsync(long customerId)
        {
            try
            {
                var queryableArticles = dbContext.Articles
                    .Include(x=>x.CustomersArticles)
                    .AsQueryable();

                queryableArticles= queryableArticles
                    .Where(x => x.CustomersArticles.Select(y => y.CustomerId)
                    .Contains(customerId));

                var items = await queryableArticles.ToListAsync();

                return Result<List<ShoppingCartItemDTO>>
                    .CreateResult(mapper.Map<List<ShoppingCartItemDTO>>(items));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<List<ShoppingCartItemDTO>>.CreateExceptionResult(ex);
            }
        }

        public async Task<Result<ShoppingCartItemDTO>> AddShoppingCartItemasync(long customerId, long articleId)
        {
            try
            {
                var article = await dbContext.Articles
                    .FirstOrDefaultAsync(x => x.Id == articleId);

                if (article == null)
                    return Result<ShoppingCartItemDTO>.CreateResult(null, "Article not found");

                var entity = new CustomerArticle
                {
                    CustomerId = customerId,
                    ArticleId = articleId,
                    Date = DateTime.Now
                };

                dbContext.CustomersArticles.Add(entity);
                await dbContext.SaveChangesAsync();

                article.CustomersArticles.Add(entity);

                return Result<ShoppingCartItemDTO>
                    .CreateResult(mapper.Map<ShoppingCartItemDTO>(article));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<ShoppingCartItemDTO>.CreateExceptionResult(ex);
            }
        }
        
        public async Task<Result<bool>> RemoveShoppingCartItemasync(long customerId, long articleId)
        {
            try
            {
                var dbEntity = await dbContext.CustomersArticles
                    .FirstOrDefaultAsync(x => x.CustomerId == customerId
                        && x.ArticleId == articleId);

                if (dbEntity == null)
                    return Result<bool>.CreateResult(false, "Article not found");

                dbContext.CustomersArticles.Remove(dbEntity);
                await dbContext.SaveChangesAsync();

                return Result<bool>.CreateResult(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<bool>.CreateExceptionResult(ex);
            }
        }


    }
}