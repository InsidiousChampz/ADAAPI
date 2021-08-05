using Microsoft.AspNetCore.Mvc;
using SmsUpdateCustomer_Api.DTOs.Admin;
using SmsUpdateCustomer_Api.DTOs.Customer_Profiles;
using SmsUpdateCustomer_Api.Services.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminServices;

        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        /// <summary>
        ///     สำหรับ ตรวจสอบข้อมูลที่มีการแก้ไข แสดงรายชื่อของคนที่ถูกทำการแก้ไข [UI7 : Datatable]
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

        [HttpGet("EditCustomerByAdmin/filter")]
        public async Task<IActionResult> GetCustomerbyAdmin([FromQuery] GetEditCustomerByFilterDto filter)
        {
            var ret = await _adminServices.GetCustomerbyAdmin(filter);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }

        /// <summary>
        ///     สำหรับ แสดงข้อมูลเพื่อเปรียบเทียบการแก้ไขข้อมูล [UI7 : Tab1]
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
        [HttpGet("CompareCustomer/{personId}")]
        public async Task<IActionResult> GetCompareDataOfCustomer( int personId)
        {
            var ret = await _adminServices.GetCompareDataOfCustomer(personId);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);

        }

        /// <summary>
        ///     สำหรับ แสดงข้อมูลการรวมข้อมูลลูกค้า [UI7 : Tab2]
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
        [HttpGet("MergeCustomer/{personId}")]
        public async Task<IActionResult> GetMergeDataOfCustomer(int personId)
        {
            var ret = await _adminServices.GetMergeDataOfCustomer(personId);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);

        }
             
        /// <summary>
        ///     สำหรับ แสดงข้อมูลการเปลี่ยนแปลงของแต่ละคน [UI7 : Tab 4]
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
        [HttpGet("CustomerHistory/{personId}")]
        public async Task<IActionResult> GetCustomerHistory(int personId)
        {
            var ret = await _adminServices.GetCustomerHistory(personId);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);

        }

        /// <summary>
        ///     สำหรับ ใช้สำหรับยืนยันการแก้ไขของ admin [UI7 : Button]
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
        [HttpPost("ConfirmbyAdmin/confirm")]
        public async Task<IActionResult> ConfirmByAdmin(ConfirmAdminDto confirm)
        {
            var ret = await _adminServices.ConfirmByAdmin(confirm);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }

        /// <summary>
        ///     สำหรับ ดูรายละเอียดอัพเดทข้อมูลลุกค้าของ CallCenter [UI8 + UI9 : Datatable]
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

        [HttpGet("EditCustomerByCallCenter/filter")]
        public async Task<IActionResult> GetCustomerbyCallCenter([FromQuery] GetEditCustomerByFilterDto filter)
        {
            var ret = await _adminServices.GetCustomerbyCallCenter(filter);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }

        /// <summary>
        ///     สำหรับ สำหรับ ใช้สำหรับเปรียบเทียบข้อมูลทั้งหมดของผู้ชำและผู้เอา สำหรับadmin [UI8]
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
        
        [HttpGet("CompareAllCustomer/{personId}")]
        public async Task<IActionResult> GetCompareDataAllCustomer(int personId)
        {
            var ret = await _adminServices.GetCompareDataAllCustomer(personId);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);

        }

        /// <summary>
        ///     สำหรับ ดูข้อมูล Login ของลูกค้าสำหรับ CallCenter [UI9]
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
        [HttpGet("GetCompareLoginOfCustomer/{personId}")]
        public async Task<IActionResult> GetCompareLoginOfCustomer(int personId)
        {
            var ret = await _adminServices.GetCompareLoginOfCustomer(personId);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }

        /// <summary>
        ///     สำหรับ UpdateLogin ของลูกค้าสำหรับ CallCenter [UI9]
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
        [HttpPost("UpdateCompareLoginOfCustomer/update")]
        public async Task<IActionResult> UpdateCompareLoginOfCustomer([FromQuery]GetCompareLoginDto update)
        {
            var ret = await _adminServices.UpdateCompareLoginOfCustomer(update);

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
