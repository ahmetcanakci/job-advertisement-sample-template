using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job.Advertisement.Service.Dtos;
using Job.Advertisement.Service.Entities;
using Job.Advertisement.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Job.Advertisement.Service.Controllers
{
    [ApiController]
    [Route("employers")]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerRepositories employerRepositories;

        public EmployerController(IEmployerRepositories employerRepositories)
        {
            this.employerRepositories = employerRepositories;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployerDto>> GetByIdAsync(Guid id)
        {
            var item = await employerRepositories.GetByIdAsync(id);

            if (item == null)
                return NotFound();

            return item.AsEmployerDto();

        }
        [HttpGet]
        public async Task<IEnumerable<EmployerDto>> GetAsync()
        {
            var employerItems = (await employerRepositories.GetAllAsync()).Select(emp => emp.AsEmployerDto());
            return employerItems;
        }


        [HttpPost]
        public async Task<ActionResult<EmployerDto>> PostAsync(CreateEmployerDto item)
        {

            var newEmployer = new Employer
            {
                Id = Guid.NewGuid(),
                PhoneNumber = item.PhoneNumber,
                Address = item.Address,
                AdvertCount = 2,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            if (employerRepositories.GetAllAsync().Result.Any(c => c.PhoneNumber.Equals(item.PhoneNumber)))
                return new JsonResult("Same phone number is using from another company");

            await employerRepositories.CreateEmployerAsync(newEmployer);

            return CreatedAtAction(nameof(GetByIdAsync), new { newEmployer.Id }, newEmployer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateEmployerDto updatedItemDto)
        {
            var existingItem = await employerRepositories.GetByIdAsync(id);

            if (existingItem == null)
                return NotFound();


            existingItem.PhoneNumber = updatedItemDto.PhoneNumber;
            existingItem.Address = updatedItemDto.Address;

            existingItem.AdvertCount = updatedItemDto.AdvertCount;
            existingItem.ModifiedDate = DateTime.Now;

            await employerRepositories.UpdateEmployerAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var employerItem = await employerRepositories.GetByIdAsync(id);
            if (employerItem == null)
                return NotFound();

            await employerRepositories.RemoveEmployerAsync(employerItem.Id);

            return NoContent();
        }



    }
}