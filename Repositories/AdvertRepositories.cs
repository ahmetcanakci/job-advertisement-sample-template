using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job.Advertisement.Service.Entities;
using MongoDB.Driver;

namespace Job.Advertisement.Service.Repositories
{

    public class AdvertRepositories : IAdvertRepositories
    {

        private const string collectionName = "adverts";

        private readonly IMongoCollection<Advert> dbCollection;

        private readonly FilterDefinitionBuilder<Advert> filterBuilder = Builders<Advert>.Filter;


        public AdvertRepositories(IMongoDatabase database)
        {
            dbCollection = database.GetCollection<Advert>(collectionName);
        }

        public async Task<IReadOnlyCollection<Advert>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Advert> GetByIdAsync(Guid id)
        {
            FilterDefinition<Advert> filter = filterBuilder.Eq(c => c.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAdvertAsync(Advert advert)

        {
            if (advert == null)
            {
                throw new ArgumentNullException(nameof(advert));
            }

            await dbCollection.InsertOneAsync(advert);
        }

        public async Task UpdateAdvertAsync(Advert advert)

        {
            if (advert == null)
            {
                throw new ArgumentNullException(nameof(advert));
            }

            FilterDefinition<Advert> filter = filterBuilder.Eq(c => c.Id, advert.Id);
            await dbCollection.ReplaceOneAsync(filter, advert);
        }

        public async Task RemoveAdvertAsync(Guid id)

        {
            FilterDefinition<Advert> filter = filterBuilder.Eq(c => c.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }

        public async Task<IReadOnlyCollection<Advert>> GetFirmAdvertsAsync(Guid firmId)
        {
            FilterDefinition<Advert> filter = filterBuilder.Eq(c => c.FirmId, firmId);
            return await dbCollection.Find(filter).ToListAsync();
        }
    }

}