using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;

using Domain.Entities.Integrator;

namespace Infrastructure.Persistence.Repositories
{
    public class IntegratorRepositoryAsync : GenericRepositoryAsync<Integrator>, IIntegratorRepositoryAsync
    {
        private readonly DbSet<Integrator> _repository;

        private readonly ApplicationDbContext _context;

        public IntegratorRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            
            this._repository = this._context.Set<Integrator>();
        }
    }
}