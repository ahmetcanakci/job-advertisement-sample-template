using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job.Advertisement.Service.Entities;
using MongoDB.Driver;

namespace Job.Advertisement.Service.Repositories
{

    public class EmployerRepositories : IEmployerRepositories
    {

        private const string collectionName = "employers";
        private readonly IMongoCollection<Employer> dbCollection;

        private readonly FilterDefinitionBuilder<Employer> filterBuilder = Builders<Employer>.Filter;


        public EmployerRepositories(IMongoDatabase database)
        {
            dbCollection = database.GetCollection<Employer>(collectionName);
        }

        public async Task CreateEmployerAsync(Employer employer)
        {
            if (employer == null)
            {
                throw new ArgumentNullException(nameof(employer));
            }

            await dbCollection.InsertOneAsync(employer);
        }

        public async Task<IReadOnlyCollection<Employer>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Employer> GetByIdAsync(Guid id)
        {
            FilterDefinition<Employer> filter = filterBuilder.Eq(c => c.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task RemoveEmployerAsync(Guid id)
        {
            FilterDefinition<Employer> filter = filterBuilder.Eq(c => c.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateEmployerAsync(Employer advert)
        {
            if (advert == null)
            {
                throw new ArgumentNullException(nameof(advert));
            }

            FilterDefinition<Employer> filter = filterBuilder.Eq(c => c.Id, advert.Id);
            await dbCollection.ReplaceOneAsync(filter, advert);

        }


    }
}