using Microsoft.AspNetCore.Mvc;
using SmsUpdateCustomer_Api.DTOs.Admin;
using SmsUpdateCustomer_Api.DTOs.Customer_Profiles;
using SmsUpdateCustomer_Api.Services.Admin;
using System;
using System.Collections.Generic;
using System.IO;
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
        ///     สำหรับ แสดงข้อมูลลุกค้าที่ได้ทำการแก้ไขทั้งผู้ชำระและผู้เอา [UI7 : Datatable]
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

        [HttpGet("GetEditCustomerByAdmin/filter")]
        public async Task<IActionResult> GetEditCustomerbyAdmin([FromQuery] GetEditCustomerByFilterDto filter)
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
        ///     สำหรับ แสดงข้อมูลลูกค้าที่แอดมินแก้ไข [UI7]
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

        [HttpGet("GetAdminEditRecord/{personId}")]
        public async Task<IActionResult> GetAdminEditRecord(int personId)
        {
            var ret = await _adminServices.GetAdminEditRecord(personId);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);

        }

        /// <summary>
        ///     สำหรับ แสดงการเปรียบเทียบข้อมูลในส่วนที่แอดมินแก้ไขทั้งก่อนและหลัง [UI7]
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

        [HttpGet("CompareCustomerByAdmin/{personId}")]
        public async Task<IActionResult> GetCompareDataOfCustomerByAdmin(int personId)
        {
            var ret = await _adminServices.GetCompareDataOfCustomerByAdmin(personId);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);

        }

        /// <summary>
        ///     สำหรับ ดึงจำนวน Record เพื่อเปรียบเทียบว่าได้ทำการบันทึกไปแล้วหรือไม่ [UI7 ]
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
        
        [HttpGet("GetCountEditRecordByAdmin/{personId}")]
        public async Task<IActionResult> GetCountEditRecordByAdmin(int personId)
        {
            var ret = await _adminServices.GetCountEditRecordByAdmin(personId);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);

        }
        
        /// <summary>
        ///     สำหรับ Check Record ว่า Admin Confirm ไปหรือยัง [UI7 ]
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
        [HttpGet("GetDoneEditRecordByAdmin/{personId}")]
        public async Task<IActionResult> GetDoneEditRecordByAdmin(int personId)
        {
            var ret = await _adminServices.GetDoneEditRecordByAdmin(personId);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);

        }

        /// <summary>
        ///     สำหรับ แสดงการเปรียบเทียบข้อมูลในส่วนที่ลูกค้าแก้ไขทั้งก่อนและหลัง [UI7 : Tab1]
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
        ///     สำหรับ แสดงข้อมูลการรวมข้อมูลลูกค้า [UI7 : Tab2 + DataTable]
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

        [HttpGet("MergeCustomerByFilter/filter")]
        public async Task<IActionResult> GetMergeDataOfCustomerByFilter([FromQuery] GetMergeByFilterDto filter)
        {
            var ret = await _adminServices.GetMergeDataOfCustomerByFilter(filter);

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
        /// 

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
        ///     สำหรับ บันทึกข้อมูลการเปลี่ยนแปลงของMerge Data [UI7 : Tab 2]
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
        [HttpPost("UpdateMergeDataOfCustomer/{userId}")]
        public async Task<IActionResult> UpdateMergeDataOfCustomer(string userId, UpdateMergeDto update)
        {
            var ret = await _adminServices.UpdateMergeDataOfCustomer(userId,update);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }
       
        /// <summary>
        ///     สำหรับ ข้อมูล policy ลูกค้า [UI7 : Tab 3]
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
        [HttpGet("GetPolicyDataOfCustomer/filter")]
        public async Task<IActionResult> GetPolicyDataOfCustomer([FromQuery] GetPolicyCustomerFilterDto filter)
        {
            var ret = await _adminServices.GetPolicyDataOfCustomer(filter);

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
        /// 

        [HttpGet("CustomerHistory/filter")]
        public async Task<IActionResult> GetCustomerHistory([FromQuery] GetHistoryCustomerFilterDto filter)
        {
            var ret = await _adminServices.GetCustomerHistory(filter);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);

        }

        /// <summary>
        ///     สำหรับ แสดงข้อมูลการเปลี่ยนแปลงของแต่ละคนที่แอดมินแก้ไข [UI7 : Tab 5]
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

        [HttpGet("GetAdminHistory/filter")]
        public async Task<IActionResult> GetAdminHistory([FromQuery] GetHistoryAdminFilterDto filter)
        {
            var ret = await _adminServices.GetAdminHistory(filter);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);

        }
        
        /// <summary>
        ///     สำหรับ ใช้สำหรับบันทึกข้อมูลการเปลี่ยนแปลข้อมูลลุกค้าโดยแอดมิน admin [UI7 : Tab1 Edit]
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

        [HttpPost("AddUpdateTempCustomerProfilebyAdmin/{userId}")]
        public async Task<IActionResult> AddUpdateTempCustomerProfilebyAdmin(string userId, AddProfileDto newProfile)
        {
            var ret = await _adminServices.AddUpdateTempCustomerProfilebyAdmin(userId,newProfile);

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
       
        [HttpPost("ConfirmbyAdmin/confirm/{username}")]
        public async Task<IActionResult> ConfirmByAdmin(ConfirmDataAdminDto confirm, string username)
        {
            var ret = await _adminServices.ConfirmByAdmin(confirm, username);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }

        /// <summary>
        ///     สำหรับ ดูรายละเอียดอัพเดทข้อมูลลุกค้าของ CallCenter [UI8 : Datatable]
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
        ///     สำหรับ ดูรายละเอียดข้อมูลLoginลุกค้าของ CallCenter [UI9 : Datatable]
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

        [HttpGet("GetLoginbyCallCenter/filter")]
        public async Task<IActionResult> GetLoginbyCallCenter([FromQuery] GetEditCustomerByFilterDto filter)
        {
            var ret = await _adminServices.GetLoginbyCallCenter(filter);

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
        [HttpGet("GetLoginOfCustomer/{personId}")]
        public async Task<IActionResult> GetLoginOfCustomer(int personId)
        {
            var ret = await _adminServices.GetLoginOfCustomer(personId);

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
        [HttpPost("UpdateLoginOfCustomer/{UserId}")]
        public async Task<IActionResult> UpdateLoginOfCustomer(string UserId, GetCompareLoginDto update)
        {
            var ret = await _adminServices.UpdateLoginOfCustomer(UserId,update);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }

        /// <summary>
        ///     สำหรับ บันทึกการเปลี่ยนแปลงทั้งหมดของลูกค้าสำหรับ CallCenter [UI9]
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
        [HttpPost("AddTransactionsByCallCenter")]
        public async Task<IActionResult> AddTransactionsByCallCenter(string userId, int personId, string BeforeLastName, string BeforeIdentityCard, string AfterLastName, string AfterIdentityCard)
        {
            var ret = await _adminServices.AddTransactionsByCallCenter(userId, personId, BeforeLastName, BeforeIdentityCard, AfterLastName, AfterIdentityCard);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }


        [HttpPost("FixedLostMergeWithoutChangedDataFromAdmin")]
        public async Task<IActionResult> FixedLostMergeWithoutChangedDataFromAdmin()
        {
            var ret = await _adminServices.FixedLostMergeWithoutChangedDataFromAdmin();

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
