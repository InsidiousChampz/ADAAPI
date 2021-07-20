using CustomerProFileAPI.Services.Customer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerService;

        public CustomerController(ICustomerServices customerService)
        {
            _customerService = customerService;
        }


        [HttpGet("Customer/{personId}")]
        public async Task<IActionResult> GetCustomerByPersonId(int personId)
        {
            var ret = await _customerService.GetCustomerByPersonId(personId);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }

        [HttpGet("CustomerPolicy/{personId}")]
        public async Task<IActionResult> GetCustomerAndPoliciesByPersonId(int personId)
        {
            var ret = await _customerService.GetCustomerAndPoliciesByPersonId(personId);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }


        
    }
}
