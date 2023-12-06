using MLGStore.Entities;

namespace MLGStore.Entities
{
    public class Article
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set;}
        public string Image { get; set; }
        public int Stock { get; set; }

        public List<ArticleStore> ArticlesStores { get; set; }
        public List<CustomerArticle> CustomersArticles { get; set; }
    }
}
