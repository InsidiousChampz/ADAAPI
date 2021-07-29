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
        ///     สำหรับ GET ข้อมูลลูกค้าในกรณีที่เคยทำการแก้ไขข้อมูลแล้ว ด้วย personID
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
        ///     สำหรับ การแก้ไขข้อมูลลูกค้า ไม่ว่าจะเป็น ผู้เอาประกัน หรือ ผุ้ชำระ ใช้ Method นี้
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
        ///     สำหรับ การให้ลุกค้าระบุข้อมูลเพื่อให้เราติดต่อกลับ ใช้ Method นี้
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
        ///     สำหรับ ปุ่ม ยืนยันทั้งหมด ไม่ว่าจะแก้ไขข้อมูลผู้เอาประกัน ผู้ชำระ Method นี้ ใช้เพื่อยืนยันการแก้ไข
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



        

              [HttpGet("PureAPI")]
        public async Task<IActionResult> PureAPI()
        {

            var ret = await _customerProfileService.PureAPI();

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
