using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLGStore.API.Common;
using MLGStore.Services.DTOs;
using MLGStore.Services.Interfaces;

namespace MLGStore.API.Controllers
{
    public class StoreController : BaseAPIController
    {
        private readonly IStoreService storeService;

        public StoreController(IStoreService storeService)
        {
            this.storeService = storeService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateStoreDTO createDto)
        {
            var result = await storeService.InsertAsync(createDto);

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpPut("{storeId}")]
        public async Task<IActionResult> Put(long storeId, [FromBody] CreateStoreDTO createDto)
        {
            var result = await storeService.UpdateAsync(storeId, createDto);

            if (result.Success)
            {
                if (result.Data != null)
                    return Ok(result);

                return BadRequest(result.Message);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpDelete("{storeId}")]
        public async Task<IActionResult> Delete(long storeId)
        {
            var result = await storeService.DeleteAsync(storeId);

            if (result.Success)
            {
                if (result.Data)
                    return Ok(result);

                return BadRequest(result.Message);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await storeService.GetAllAsync();

            if (result.Success)
                return Ok(result);

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("{storeId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(long storeId)
        {
            var result = await storeService.FindByIdAsync(storeId);

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
