using Application.DTOs.Customer;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            IReadOnlyList<CustomerDto> customers = 
                await  _customerService.GetAllCustomersAsync();
            return Json(new {customers});
        }
        [HttpPost("InsertCustomer")]
        public async Task<IActionResult> InsertCustomer(AddCustomerDto addCustomerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Guid newCustomerId = await _customerService.InsertCustomer(addCustomerDto);
            if (newCustomerId == Guid.Empty)
                return BadRequest(ModelState);
            return Ok(newCustomerId);
        }
    }
}
