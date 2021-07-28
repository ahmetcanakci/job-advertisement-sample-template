using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job.Advertisement.Service.Entities;

namespace Job.Advertisement.Service.Repositories
{
    public interface IEmployerRepositories
    {
        Task CreateEmployerAsync(Employer employer);
        Task<IReadOnlyCollection<Employer>> GetAllAsync();
        Task<Employer> GetByIdAsync(Guid id);
        Task RemoveEmployerAsync(Guid id);
        Task UpdateEmployerAsync(Employer advert);
    }

}