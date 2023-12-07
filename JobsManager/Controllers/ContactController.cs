using JobsManager.Dtos;
using JobsManager.Models;
using JobsManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactServise _contactServise;

        public ContactController(IContactServise contactServise)
        {
            _contactServise = contactServise;
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id,
            [FromBody] UpdateContactRequestDto updateContactRequestDto)
        {
            try
            {
                var response = await _contactServise.UpdateAsync(id, updateContactRequestDto);
                return response switch
                {
                    null => NotFound("Contact not found"),
                    < 1 => BadRequest("Something went wrong"),
                    _ => Ok("Contact updated")
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
