using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using SmsUpdateCustomer_Api.DTOs.Report;
using SmsUpdateCustomer_Api.Services.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportServices _reportServices;

        public ReportController(IReportServices reportServices)
        {
            _reportServices = reportServices;
        }

        /// <summary>
        ///     สำหรับ รายงานการแสดงผลการส่ง SMS ให้ลุกค้า [U10]
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

        [HttpGet("ExportSMSReport/datedata")]
        public async Task<IActionResult> GetSMSSended([FromQuery] GetSMSDto datedata)
        {

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "SMSSended.xlsx";

            var fileContent = await _reportServices.GetSMSSended(datedata);

            if (fileContent != null && fileContent.Length > 0)
            {
                // 200
                return File(fileContent, contentType, fileName);

            }

            // 404
            return NotFound();
        }

        /// <summary>
        ///     สำหรับ รายงานติดต่อกลับลูกค้า [U11] 
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
        [HttpGet("ExportFollowupReport/datedata")]
        public async Task<IActionResult> GetFollowupCustomerReply([FromQuery] GetFollowupDto datedata)
        {

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "Followup.xlsx";

            var fileContent = await _reportServices.GetFollowupCustomerReply(datedata);

            if (fileContent != null && fileContent.Length > 0)
            {
                // 200
                return File(fileContent, contentType, fileName);
            }

            // 404
            return NotFound();
        }

        /// <summary>
        ///     สำหรับ รายงานติดตามการแก้ไขข้อมูลลูกค้า [U12]
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
        [HttpGet("ExportEditFollowupReport/{branchId}")]
        public async Task<IActionResult> GetFollowupCustomerEdit(int branchId)
        {

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "CustomerEdit.xlsx";

            var fileContent = await _reportServices.GetFollowupCustomerEdit(branchId);

            if (fileContent != null && fileContent.Length > 0)
            {
                // 200
                return File(fileContent, contentType, fileName);
            }

            // 404
            return NotFound();
        }
    }
    
}
