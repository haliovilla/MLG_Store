namespace MLGStore.Entities
{
    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<CustomerArticle> CustomersArticles { get; set; }
    }
}
