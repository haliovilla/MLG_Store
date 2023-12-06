namespace MLGStore.Entities
{
    public class Store
    {
        public long Id { get; set; }
        public string Branch { get; set; }
        public string Address { get; set; }

        public List<ArticleStore> ArticlesStores { get; set; }
    }
}
