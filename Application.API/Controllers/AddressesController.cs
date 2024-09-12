using Application.Domain.Models;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using System.Text.Json;
using Infrastructure.ViewModels;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressRepository addressRepository;
        private readonly int maxPageSize = 10;

        public AddressesController(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }
        [HttpGet("{addressId}", Name = "GetAddress")]
        public async Task<ActionResult<Address>> GetAddress(int addressId)
        {
            var address = await addressRepository.GetByIdAsync(addressId);
            if (address == null) return NotFound();
            return Ok(address);
        }
        [HttpGet(Name = "GetAllAddresses")]
        public async Task<ActionResult<Address>> GetAllAddresses(int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxPageSize)
                pageSize = maxPageSize;

            var (addresses, paginationMetaData) = await addressRepository.GetAllAsync();
            if (addresses == null)
                return NotFound();
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            return Ok(addresses);
        }

        [HttpPatch("{addressId}")]
        public async Task<ActionResult> UpdateAddress(int addressId, JsonPatchDocument<AddressForUpdate> patchDocument)
        {
            var (updatedAddress, errors) = await addressRepository.UpdateByIdAsync(addressId, patchDocument);

            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }

            if (updatedAddress == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Patient> CreateAddress(AddressForCreate address)
        {
            try
            {
                var newAddress = addressRepository.CreateAddress(address);
                if (newAddress == null)
                    return BadRequest();
                return CreatedAtRoute("GetAddress", new { addressId = newAddress.AddressId }, newAddress);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{addressId}")]
        public ActionResult<Address> DeleteAddress(int addressId)
        {
            var address = addressRepository.DeleteById(addressId);
            if (address == null) return NotFound();

            return NoContent();
        }



    }
}
