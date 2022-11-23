using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Application.Interfaces;
using Domain.Common;

using Domain.Entities.Integrator;
using Domain.Entities.BankBranch;
using Domain.Entities.BankAccountType;
using Domain.Entities.Currency;
using Domain.Entities.Bank;
using Domain.Entities.BankAccount;
using Domain.Entities.BankAccountActivity;
using Domain.Entities.BankAccountActivityLog;
using Domain.Entities.InternalCommand;
using Domain.Entities.CurrencyActivity;

namespace Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public DbSet<Integrator> Integrator { get; set; }
        public DbSet<BankBranch> BankBranch { get; set; }
        
        public DbSet<Currency> Currency { get; set; }

        public DbSet<Bank> Bank { get; set; }
        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<BankAccountActivity> BankAccountActivity { get; set; }
        public DbSet<BankAccountType> BankAccountType { get; set; }

        public DbSet<InternalCommand> InternalCommand { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateDate = _dateTime.Now;
                        entry.Entity.IsDelete = false;

                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdateDate = _dateTime.Now;
                        break;

                    case EntityState.Deleted:
                        entry.Entity.DeleteDate = _dateTime.Now;
                        entry.Entity.IsDelete = true;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("banks");

            AddEntityModel(builder);

            //All Decimals will have 18,2 Range
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
                {
                    property.SetColumnType("decimal(18,2)");
                }

            base.OnModelCreating(builder);
        }

        protected void AddEntityModel(ModelBuilder builder)
        {
            builder.Entity<InternalCommand>().ToTable("InternalCommands", schema: "app");

            builder.Entity<Bank>().ToTable("Bank", schema: "banks");

            builder.Entity<BankAccount>().ToTable("BankAccount", schema: "banks");
            builder.Entity<BankAccountActivity>().ToTable("BankAccountActivity", schema: "banks");
            builder.Entity<BankAccountActivityLog>().ToTable("BankAccountActivityLog", schema: "banks");
            
            builder.Entity<BankAccountType>().ToTable("BankAccountType", schema: "banks");
            
            builder.Entity<BankBranch>().ToTable("BankBranch", schema: "banks");

            builder.Entity<Currency>().ToTable("Currency", schema: "banks");
            builder.Entity<CurrencyActivity>().ToTable("CurrencyActivity", schema: "banks");

            builder.Entity<Integrator>().ToTable("Integrator", schema: "banks");
        }
    }
}
