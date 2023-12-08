using JobsManager.Dtos;
using JobsManager.Models;
using JobsManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobServise _jobServise;

        public JobController(IJobServise jobServise)
        {
            _jobServise = jobServise;
        }

        [HttpGet]
        public async Task<ActionResult<List<Job>>> GetAllAsync()
        {
            try
            {
                var result = await _jobServise.GetAllAsync();
                if (!result.Any())
                    return NotFound("No jobs found");

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [HttpPost("{customerId:guid}")]
        public async Task<IActionResult> CreateJobAsync([FromRoute] Guid customerId,
            [FromBody] AddJobRequestDto addJobRequestDto)
        {
            try
            {
                var result = await _jobServise.CreateJobAsync(customerId, addJobRequestDto);
                return result is null ? NotFound("Customer with given id not found") :
                    result < 1 ? BadRequest("Something went wrong") : Ok("Job created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateJobAsync([FromRoute] Guid id,
            [FromBody] UpdateJobRequestDto updateJobRequestDto)
        {
            try
            {
                var response = await _jobServise.UpdateJobAsync(id, updateJobRequestDto);
                return response is null ? NotFound("Job not found") :
                    response < 1 ? BadRequest("Something went wrong") : Ok("Job updated");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteJobAsync([FromRoute] Guid id)
        {
            try
            {
                var response = await _jobServise.DeleteJobAsync(id);
                return response is null ? NotFound("Job not found") :
                    response < 1 ? BadRequest("Something went wrong") : Ok("Job deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
