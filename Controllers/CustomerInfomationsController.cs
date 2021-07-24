using CustomerProFileAPI.DTOs.Customer_Infomations;
using CustomerProFileAPI.Services.Customer_Infomations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.Controllers
{
    //[Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerInfomationsController : ControllerBase
    {
        private readonly ICustomerInfomationServices _customerInfomationService;

        public CustomerInfomationsController(ICustomerInfomationServices customerInfomationService)
        {
            _customerInfomationService = customerInfomationService;
        }

        /// <summary>
        ///     สำหรับ GET Customer login ทั้งหมดทุกคน.
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
        
        [HttpGet("CustomerHeader")]       
        //[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetCustomerLogin()
        {
            var ret = await _customerInfomationService.GetCustomerLogin();

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }

        /// <summary>
        ///     สำหรับ GET Customer login ด้วย บัตรประชาชน, นามสกุล เพื่อเข้าสู่ระบบ.
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

        [HttpGet("CustomerHeader/filter")]
        public async Task<IActionResult> GetCustomerLoginByIdentityAndLastname([FromQuery] GetCustomerHeaderWithFilter filter)
        {
            var ret = await _customerInfomationService.GetCustomerLoginByIdentityAndLastname(filter);

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
