using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MLGStore.Entities;

namespace MLGStore.Data
{
    public class SeedData
    {
        public static async Task InsertCustomersAsync(StoreDbContext dbContext, ILoggerFactory loggerFactory, IPasswordHasher<Customer> passwordHasher)
        {
            if (!dbContext.Customers.Any())
            {
                var logger = loggerFactory.CreateLogger<SeedData>();
                try
                {
                    dbContext.Customers.AddRange(CreateCustomersList(passwordHasher));
                    await dbContext.SaveChangesAsync();

                    logger.LogInformation("Seed Data: Customers created");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }
            }
        }

        public static async Task InsertStoresAsync(StoreDbContext dbContext, ILoggerFactory loggerFactory)
        {
            if (!dbContext.Stores.Any())
            {
                var logger = loggerFactory.CreateLogger<SeedData>();
                try
                {
                    dbContext.Stores.AddRange(CreateStoresList());
                    await dbContext.SaveChangesAsync();

                    logger.LogInformation("Seed Data: Stores created");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }
            }
        }

        public static async Task InsertArticlesAsync(StoreDbContext dbContext, ILoggerFactory loggerFactory)
        {
            if (!dbContext.Articles.Any() && dbContext.Stores.Any())
            {
                var stores = await dbContext.Stores.ToListAsync();

                var logger = loggerFactory.CreateLogger<SeedData>();
                try
                {
                    dbContext.Articles.AddRange(CreateArticlesList(stores));
                    await dbContext.SaveChangesAsync();

                    logger.LogInformation("Seed Data: Stores created");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }
            }
        }

        private static List<Customer> CreateCustomersList(IPasswordHasher<Customer> passwordHasher)
        {
            var list = new List<Customer>();

            for (int i = 0; i < 20; i++)
            {
                var customer = new Customer
                {
                    Name = $"Cliente {i}",
                    Surnames = $"LastName{i}",
                    Address = $"Domicilio {i}",
                    Username = $"cliente{i}"
                };
                customer.Password = passwordHasher.HashPassword(customer, $"password-{i}");
                list.Add(customer);
            }
            return list;
        }

        private static List<Store> CreateStoresList()
        {
            var list = new List<Store>();

            for (int i = 0; i < 10; i++)
            {
                var store = new Store
                {
                    Branch = $"Sucursal {i}",
                    Address = $"Domicilio {i}"
                };
                list.Add(store);
            }
            return list;
        }

        private static List<Article> CreateArticlesList(List<Store> stores)
        {
            var list = new List<Article>();

            foreach (var store in stores)
            {
                list.AddRange(CreateStoreArticles(store.Id));
            }

            return list;
        }

        private static List<Article> CreateStoreArticles(long storeId)
        {
            var list = new List<Article>();
            var rand = new Random();
            var qty = rand.Next(10, 25);

            for (int i = 0; i < qty; i++)
            {
                var article = new Article
                {
                    Code = rand.Next(100000, 999999).ToString(),
                    Description = $"Artículo {storeId}{i}",
                    Price = Convert.ToDecimal(rand.Next(100, 9999)),
                    Stock = rand.Next(10, 100),
                    Image = $"https://picsum.photos/200/300?random={i}",
                    ArticlesStores = new List<ArticleStore>()
                    {
                        new ArticleStore
                {
                    StoreId = storeId
                }
                    }
                };
                list.Add(article);
            }

            return list;
        }






    }
}
