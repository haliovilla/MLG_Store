using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLGStore.API.Common;
using MLGStore.Services.DTOs;
using MLGStore.Services.Interfaces;
using MLGStore.Services.Services;

namespace MLGStore.API.Controllers
{
    public class ArticleController : BaseAPIController
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpPost("CreateArticleWithImageUrl")]
        public async Task<IActionResult> CreateArticleWithImageUrl([FromBody] CreateArticleWithImageUrlDTO createDto)
        {
            var result = await articleService.InsertAsync(createDto);

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost("CreateArticleWithImageFile")]
        public async Task<IActionResult> CreateArticleWithImageFile([FromForm] CreateArticleWithImageFileDTO createDto)
        {
            var result = await articleService.InsertAsync(createDto);

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpPut("UpdateArticleWithImageUrl/{articleId}")]
        public async Task<IActionResult> UpdateArticleWithImageUrl(long articleId, [FromBody] CreateArticleWithImageUrlDTO createDto)
        {
            var result = await articleService.UpdateAsync(articleId, createDto);

            if (result.Success)
            {
                if (result.Data != null)
                    return Ok(result);

                return BadRequest(result.Message);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpPut("UpdateArticleWithImageFile/{articleId}")]
        public async Task<IActionResult> UpdateArticleWithImageFile(long articleId, [FromForm] CreateArticleWithImageFileDTO createDto)
        {
            var result = await articleService.UpdateAsync(articleId, createDto);

            if (result.Success)
            {
                if (result.Data != null)
                    return Ok(result);

                return BadRequest(result.Message);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpDelete("{articleId}")]
        public async Task<IActionResult> Delete(long articleId)
        {
            var result = await articleService.DeleteAsync(articleId);

            if (result.Success)
            {
                if (result.Data != null)
                    return Ok(result);

                return BadRequest(result.Message);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await articleService.GetAllAsync();

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("{articleId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(long articleId)
        {
            var result = await articleService.FindByIdAsync(articleId);

            if (result.Success)
            {
                if (result.Data != null)
                    return Ok(result);

                return NotFound(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("GetShoppingCartItems")]
        public async Task<IActionResult> GetShoppingCartItems()
        {
            var customerIdClaim = HttpContext.User.FindFirst("customerid");

            if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out int customerId))
                return Unauthorized();

            var result = await articleService.GetShoppingCartItemsAsync(customerId);

            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost("AddShoppingCartItem")]
        public async Task<IActionResult> AddShoppingCartItem([FromBody] long articleId)
        {
            var customerIdClaim = HttpContext.User.FindFirst("customerid");

            if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out int customerId))
                return Unauthorized();

            var result = await articleService.AddShoppingCartItemasync(customerId, articleId);

            if (result.Success)
            {
                if (result.Data != null)
                    return Ok(result);

                return BadRequest(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
