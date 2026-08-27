using Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockQuantityApiController(IStockQuantityService quantityService) : ControllerBase
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> DecrementProductQuantity(Guid productId,
    int requestedQuantity)
        {
            var success = await quantityService.DecrementStockQuantity(productId, requestedQuantity);
            return success? Ok(success) : BadRequest();
        }
    }
}
