namespace MLGStore.Entities
{
    public class ArticleStore
    {
        public long ArticleId { get; set; }
        public long StoreId { get; set; }
        public Store Store { get; set; }
        public DateTime Date { get; set; }
    }
}
