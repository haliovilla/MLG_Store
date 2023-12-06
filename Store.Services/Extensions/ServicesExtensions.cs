using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MLGStore.Data.Extensions;
using MLGStore.Entities;
using MLGStore.Services.Interfaces;
using MLGStore.Services.Services;

namespace MLGStore.Services.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureServicesExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDataExtensions(configuration);

            services.AddScoped<IPasswordHasher<Customer>, PasswordHasher<Customer>>();


            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
        }
    }
}
