using Microsoft.AspNetCore.Mvc;
using MLGStore.API.Common;
using MLGStore.Services.DTOs;
using MLGStore.Services.Interfaces;

namespace MLGStore.API.Controllers
{
    public class CustomerController : BaseAPIController
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerDTO createDto)
        {
            var result = await customerService.InsertAsync(createDto);

            if (result.Success)
            {
                if (result.Data != null)
                    return Ok(result);

                return BadRequest(result.Message);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> Put(long customerId, [FromForm] UpdateCustomerDTO updateDto)
        {
            var result = await customerService.UpdateAsync(customerId, updateDto);

            if (result.Success)
            {
                if (result.Data != null)
                    return Ok(result);

                return BadRequest(result.Message);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> Delete(long customerId)
        {
            var result = await customerService.DeleteAsync(customerId);

            if (result.Success)
            {
                if (result.Data)
                    return Ok(result);

                return BadRequest(result.Message);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await customerService.GetAllAsync();

            if (result.Success)
                return Ok(result);

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetById(long customerId)
        {
            var result = await customerService.FindByIdAsync(customerId);

            if (result.Success)
            {
                if (result.Data != null)
                    return Ok(result);

                return BadRequest(result.Message);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
