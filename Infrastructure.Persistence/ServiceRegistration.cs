using Application.Interfaces;
using Application.Interfaces.Repositories;

using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repository;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
               services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            #region Repositories

            AddPersistenceOfRepositories(services);

            #endregion
        }

        /// <summary>
        /// Add Persistence of Repositories 
        /// </summary>
        /// <param name="services"></param>
        private static void AddPersistenceOfRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

            //For Business Repositories

            services.AddTransient<IIntegratorRepositoryAsync, IntegratorRepositoryAsync>();
            services.AddTransient<IBankBranchRepositoryAsync, BankBranchRepositoryAsync>();
            services.AddTransient<IBankAccountTypeRepositoryAsync, BankAccountTypeRepositoryAsync>();
            services.AddTransient<ICurrencyRepositoryAsync, CurrencyRepositoryAsync>();
            services.AddTransient<ICurrencyActivityRepositoryAsync, CurrencyActivityRepositoryAsync>();

            services.AddTransient<IBankRepositoryAsync, BankRepositoryAsync>();
            services.AddTransient<IBankAccountRepositoryAsync, BankAccountRepositoryAsync>();
            services.AddTransient<IBankAccountActivityRepositoryAsync, BankAccountActivityRepositoryAsync>();
            services.AddTransient<IBankAccountActivityLogRepositoryAsync, BankAccountActivityLogRepositoryAsync>();

            //Anything else
        }
    }
}
