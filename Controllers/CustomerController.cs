using SmsUpdateCustomer_Api.Services.Customer;
using SmsUpdateCustomer_Api.DTOs.Customer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Controllers
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


        /// <summary>
        ///     สำหรับ GET ข้อมูลผู้ชำระ ด้วย personID
        /// </summary>
        /// <returns> 
        ///     List of Personal by JSON format
        /// </returns>
        /// <remarks>
        ///     
        /// </remarks>
        /// <response code="200"> Success </response>
        /// <response code="400"> Bad Request </response>
        /// <response code="401"> Unauthorize </response>
        /// <response code="403"> Forbidden </response>
        /// <response code="404"> Not Found </response>
        /// <response code="500"> Internal Server Error </response>

        [HttpGet("Customer/{personId}")]
        public async Task<IActionResult> GetPayerByPersonId(int personId)
        {
            var ret = await _customerService.GetPayerByPersonId(personId);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }


        /// <summary>
        ///     สำหรับ GET ข้อมูลผู้ชำระ และผู้เอาประกันของเค้า ด้วย personID
        /// </summary>
        /// <returns> 
        ///     List of Personal by JSON format
        /// </returns>
        /// <remarks>
        ///     
        /// </remarks>
        /// <response code="200"> Success </response>
        /// <response code="400"> Bad Request </response>
        /// <response code="401"> Unauthorize </response>
        /// <response code="403"> Forbidden </response>
        /// <response code="404"> Not Found </response>
        /// <response code="500"> Internal Server Error </response>

        [HttpGet("CustomerPolicy/{personId}")]
        public async Task<IActionResult> GetPayerAndPoliciesByPersonId(int personId)
        {
            var ret = await _customerService.GetPayerAndPoliciesByPersonId(personId);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }

        /// <summary>
        ///     สำหรับ GET ข้อมูลผู้ชำระ และผู้เอาประกันของเค้า ด้วย หมายเลขบัตรประชาชน และ นามสกุล
        /// </summary>
        /// <returns> 
        ///     List of Personal by JSON format
        /// </returns>
        /// <remarks>
        ///     
        /// </remarks>
        /// <response code="200"> Success </response>
        /// <response code="400"> Bad Request </response>
        /// <response code="401"> Unauthorize </response>
        /// <response code="403"> Forbidden </response>
        /// <response code="404"> Not Found </response>
        /// <response code="500"> Internal Server Error </response>

        [HttpGet("CustomerPolicyWithLastName/filter")]
        public async Task<IActionResult> GetPayerAndPoliciesByIdentityAndLastname([FromQuery] GetByIdentityAndLastNameDto filter)
        {
            //filter.IdentityCard = "3220100218353";
            //filter.LastName = "พวงแก้ว";
            var ret = await _customerService.GetPayerAndPoliciesByIdentityAndLastName(filter);

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
