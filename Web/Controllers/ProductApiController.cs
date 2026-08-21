using Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController(IProductService productService) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> Products()
        {
            var products = await productService.GetAllProducts();
            return Ok(products);
        }
    }
}
