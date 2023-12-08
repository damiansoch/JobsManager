using JobsManager.Dtos;
using JobsManager.Models;
using JobsManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressServise _addressServise;

        public AddressController(IAddressServise addressServise)
        {
            _addressServise = addressServise;
        }

        [HttpGet]
        public async Task<ActionResult<List<Address>>> GetAllAsync()
        {
            try
            {
                var result = await _addressServise.GetAllAsync();
                if (!result.Any())
                    return NotFound("No addresses found");
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

     

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            try
            {
                var response = await _addressServise.DeleteAsync(id);
                return response is null ? NotFound("Address not found") :
                    response < 1 ? BadRequest("Something went wrong") : Ok("Address deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost("{customerId:guid}")]
        public async Task<IActionResult> CreateAsync([FromRoute] Guid customerId,
            [FromBody] AddAddressRequestDto addAddressRequestDto)
        {
            try
            {
                var response = await _addressServise.CreateAsync(customerId, addAddressRequestDto);
                return response is null ? NotFound("Customer with given id not found") : response < 1 ? BadRequest("Something went wrong") : Ok("Address Added");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id,
            [FromBody] UpdateAddressRequestDto updateAddressRequestDto)
        {
            try
            {
                var response = await _addressServise.UpdateAsync(id, updateAddressRequestDto);
                return response is null ? NotFound("Address not found") :
                    response < 1 ? BadRequest("Something went wrong") : Ok("Address updated");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
