using AutoMapper;
using MLGStore.Entities;
using MLGStore.Services.DTOs;

namespace MLGStore.Services.Automapper
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<CreateArticleWithImageFileDTO, Article>()
                .ForMember(e => e.Image, opt => opt.Ignore())
                .ForMember(e => e.ArticlesStores, opt => opt.MapFrom(MapArticlesSStores));

            CreateMap<CreateArticleWithImageUrlDTO, Article>()
                .ForMember(e => e.ArticlesStores, opt => opt.MapFrom(MapArticlesSStores));

            CreateMap<Article, ArticleDTO>()
                .ForMember(d => d.Store, opt => opt.MapFrom(e => MapArticleStore(e)));

            CreateMap<Article, ShoppingCartItemDTO>()
                .ForMember(d => d.Date, opt => opt.MapFrom(e => MapDate(e)));
        }

        private StoreDTO MapArticleStore(Article article)
        {
            if (article.ArticlesStores == null || article.ArticlesStores.Count == 0)
                return null;

            return new StoreDTO
            {
                Id = article.ArticlesStores[0].Store.Id,
                Branch = article.ArticlesStores[0].Store.Branch,
                Address = article.ArticlesStores[0].Store.Address
            };
        }

        private DateTime MapDate(Article article)
        {
            if (article.CustomersArticles == null)
            {
                return new DateTime();
            }
            if (article.CustomersArticles.Count <= 0)
            {
                return new DateTime();
            }
            return article.CustomersArticles[0].Date;
        }

        private List<ArticleStore> MapArticlesSStores(CreateArticleWithImageUrlDTO dTO, Article article)
        {
            return new List<ArticleStore>
            {
            new ArticleStore
            {
                StoreId=dTO.StoreId,
                Date=DateTime.Now
            }
            };
        }

        private List<ArticleStore> MapArticlesSStores(CreateArticleWithImageFileDTO dTO, Article article)
        {
            return new List<ArticleStore>
            {
            new ArticleStore
            {
                StoreId=dTO.StoreId,
                Date=DateTime.Now
            }
            };
        }
    }
}
