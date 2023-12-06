using Microsoft.EntityFrameworkCore;
using MLGStore.Entities;

namespace MLGStore.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleStore> ArticlesStores { get; set; }
        public DbSet<CustomerArticle> CustomersArticles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleStore>()
                .HasKey(x => new { x.ArticleId, x.StoreId });

            modelBuilder.Entity<CustomerArticle>()
                .HasKey(x => new { x.CustomerId, x.ArticleId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
