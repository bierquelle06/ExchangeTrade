using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IInternalCommandRepositoryAsync : IGenericRepositoryAsync<Domain.Entities.InternalCommand.InternalCommand>
    {
    }
}
