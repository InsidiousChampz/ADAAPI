using SmsUpdateCustomer_Api.DTOs.Customer_Profiles;
using SmsUpdateCustomer_Api.Services.Customer_Profiles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SmsUpdateCustomer_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerProfilesController : ControllerBase
    {
        private readonly ICustomerProfileServices _customerProfileService;

        public CustomerProfilesController(ICustomerProfileServices customerProfileService)
        {
            _customerProfileService = customerProfileService;
        }


        /// <summary>
        ///     ใช้สำหรับ ปุ่มแก้ไข เพื่อดึงข้อมูลลูกค้าเพื่อเอาไปแก้ไข [UI4]
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
        /// 
        [HttpGet("CustomerProfile/{personId}")]
        public async Task<IActionResult> GetCustomerProfile(int personId)
        {
            var ret = await _customerProfileService.GetCustomerProfile(personId);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }

        /// <summary>
        ///    ใช้สำหรับ บันทึกข้อมูลลูกค้าที่ได้กดทำการแก้ไขไปแล้วแต่ยังไม่ได้ยืนยัน [UI4]
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
        /// 

        [HttpPost("AddCustomerProfile")]
        public async Task<IActionResult> AddCustomerProfile(AddProfileDto newProfile)
        {

            var ret = await _customerProfileService.AddCustomerProfile(newProfile);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);

        }

        /// <summary>
        ///     ใช้สำหรับ บันทึกข้อมูลลูกค้าที่ต้องการให้เจ้าหน้าที่ติดต่อกลับ  [UI2] + [UI5]
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
        /// 

        [HttpPost("AddCustomerHotline")]
        public async Task<IActionResult> AddCustomerHotline(AddHotlineDto newHotline)
        {

            var ret = await _customerProfileService.AddCustomerHotline(newHotline);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);

        }

        /// <summary>
        ///     ใช้สำหรับ บันทึกข้อมูลลูกค้าที่ได้กดทำการแก้ไขไป "เพื่อ" ยืนยันความถูกต้อง [UI5]
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
        /// 

        [HttpPost("ConfirmCustomerProfile")]
        public async Task<IActionResult> ConfirmCustomerProfile(int EditorId)
        {

            var ret = await _customerProfileService.ConfirmCustomerProfile(EditorId);

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
