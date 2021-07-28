using Microsoft.AspNetCore.Mvc;
using Job.Advertisement.Service.Dtos;
using System.Collections.Generic;
using System;
using System.Linq;
using Job.Advertisement.Service.Repositories;
using System.Threading.Tasks;
using Job.Advertisement.Service.Entities;
using Job.Advertisement.Service.Helper;
using Nest;

namespace Job.Advertisement.Service.Controllers
{
    [ApiController]
    [Route("adverts")]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertRepositories advertRepository;
        private readonly IEmployerRepositories employerRepositories;
        private readonly IElasticClient elasticClient;


        public AdvertController(IAdvertRepositories advertRepository, IEmployerRepositories employerRepositories, IElasticClient elasticClient)
        {
            this.advertRepository = advertRepository;
            this.employerRepositories = employerRepositories;
            this.elasticClient = elasticClient;

        }
        [HttpGet]
        public async Task<IEnumerable<AdvertDto>> GetAsync()
        {
            var advertItems = (await advertRepository.GetAllAsync()).Select(advertItem => advertItem.AsDto());
            return advertItems;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdvertDto>> GetByIdAsync(Guid id)
        {
            var item = await advertRepository.GetByIdAsync(id);

            if (item == null)
                return NotFound();

            return item.AsDto();

        }

        [HttpPost]
        public async Task<ActionResult<AdvertDto>> PostAsync(CreateAdvertDto item)
        {

            var newAdvert = new Advert
            {
                Id = Guid.NewGuid(),
                FirmId = item.FirmId,
                Position = item.Position,
                AdvertDescription = item.AdvertDescription,
                Airtime = DateTime.Now.AddDays(15),
                AdvertQuality = 0,
                SideBenefits = item.SideBenefits,
                WorkType = item.WorkType,
                Salary = item.Salary
            };
            newAdvert.AdvertQuality = AdvertHelper.CalculateAdvertQuality(newAdvert);

            var employer = await employerRepositories.GetByIdAsync(item.FirmId);

            if (employer != null && employer.AdvertCount > 0)
            {
                await advertRepository.CreateAdvertAsync(newAdvert);
                employer.AdvertCount -= 1;
                await employerRepositories.UpdateEmployerAsync(employer);

                await elasticClient.IndexDocumentAsync(newAdvert);
            }

            else
                return new JsonResult("Firm has not available advert count");

            return CreatedAtAction(nameof(GetByIdAsync), new { newAdvert.Id }, newAdvert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateAdvertDto updatedItemDto)
        {
            var existingItem = await advertRepository.GetByIdAsync(id);

            if (existingItem == null)
                return NotFound();


            existingItem.Position = updatedItemDto.Position;
            existingItem.AdvertDescription = updatedItemDto.AdvertDescription;

            existingItem.SideBenefits = updatedItemDto.SideBenefits;
            existingItem.WorkType = updatedItemDto.WorkType;
            existingItem.Salary = updatedItemDto.Salary;
            existingItem.AdvertQuality = AdvertHelper.CalculateAdvertQuality(existingItem);

            await advertRepository.UpdateAdvertAsync(existingItem);

            // await elasticClient.UpdateAsync<Advert>(existingItem.Id, u => u.Index("advert").Doc(new Advert { Id = existingItem.Id,  Position = updatedItemDto.Position.ToString() ,Airtime = existingItem.Airtime}));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var advertItem = await advertRepository.GetByIdAsync(id);
            if (advertItem == null)
                return NotFound();

            await advertRepository.RemoveAdvertAsync(advertItem.Id);

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchJob(DateTime date)
        {

            var searchResponse = await elasticClient.SearchAsync<Advert>(s => s
                     .Source(sf => sf
                         .Includes(i => i
                             .Fields(
                                 f => f.Id,
                                 f => f.Airtime
                             )
                         )
                     )
                     .Query(q => q
                         .MatchAll()
                     )
                 );

            if (!searchResponse.IsValid)
                return new JsonResult(null);

            return new JsonResult(searchResponse.Documents.Where(c => c.Airtime <= date).Select(c => new { JobId = c.Id, Airtime = c.Airtime }));
        }

        [HttpGet("firmadverts")]
        public async Task<IEnumerable<Advert>> GetFirmAdverts(Guid firmId)
        {
            return await advertRepository.GetFirmAdvertsAsync(firmId);
        }
    }
}