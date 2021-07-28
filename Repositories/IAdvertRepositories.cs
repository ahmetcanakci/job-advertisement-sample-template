using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job.Advertisement.Service.Entities;

namespace Job.Advertisement.Service.Repositories
{
    public interface IAdvertRepositories
    {
        Task CreateAdvertAsync(Advert advert);
        Task<IReadOnlyCollection<Advert>> GetAllAsync();
        Task<Advert> GetByIdAsync(Guid id);
        Task RemoveAdvertAsync(Guid id);
        Task UpdateAdvertAsync(Advert advert);

        Task<IReadOnlyCollection<Advert>> GetFirmAdvertsAsync(Guid firmId);
    }

}