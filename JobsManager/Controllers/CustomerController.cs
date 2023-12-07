using JobsManager.Dtos;
using JobsManager.Models;
using JobsManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<Customer>> GetAllAsync()
        {
            try
            {
                var result = await _customerService.GetAllAsync();
                if (!result.Any())
                    return NotFound("No customers found");

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("{id:guid}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Customer>> GetById([FromRoute] Guid id)
        {
            try
            {
                var result = await _customerService.GetCustomerAsync(id);
                if (result is null)
                    return NotFound("Customer not found");
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var response = await _customerService.Delete(id);
                return response switch
                {
                    null => NotFound("Customer not found"),
                    > 0 => Ok("Customer deleted"),
                    _ => BadRequest("Something went wrong")
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddCustomerRequestDto addCustomerRequestDto)
        {
            try
            {
                var result = await _customerService.CreateAsync(addCustomerRequestDto);
                if (result.Item1 is null)
                    return BadRequest("Something went wrong");

                return CreatedAtAction(nameof(GetById), new { id = result.Item1 }, result.Item2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id,
            [FromBody] UpdateCustomerRequestDto updateCustomerRequestDto)
        {
            try
            {
                var result = await _customerService.UpdateAsync(id, updateCustomerRequestDto);
                return result switch
                {
                    null => NotFound("Customer not found"),
                    < 1 => BadRequest("Something went wrong"),
                    _ => Ok("User Updated")
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
