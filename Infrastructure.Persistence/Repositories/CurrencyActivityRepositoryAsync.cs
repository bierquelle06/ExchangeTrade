using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;

using Domain.Entities.CurrencyActivity;

namespace Infrastructure.Persistence.Repositories
{
    public class CurrencyActivityRepositoryAsync : GenericRepositoryAsync<CurrencyActivity>, ICurrencyActivityRepositoryAsync
    {
        private readonly DbSet<CurrencyActivity> _repository;

        private readonly ApplicationDbContext _context;

        public CurrencyActivityRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            this._repository = this._context.Set<CurrencyActivity>();
        }
    }
}