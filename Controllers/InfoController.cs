using INFOEDITORAPI.DTOs.Info;
using INFOEDITORAPI.Services.Info;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFOEDITORAPI.Data
{
    //[Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class InfoController : ControllerBase
    {
        private readonly IInfoService _infoService;

        public InfoController(IInfoService infoService)
        {
            _infoService = infoService;
        }

        /// <summary>
        ///     สำหรับ GET Personal Information ทั้งหมดทุกคน.
        /// </summary>
        /// <returns> 
        ///     List of Personal by JSON format
        /// </returns>
        /// <remarks>
        ///     
        /// </remarks>
        /// <response code="200" note="AAA" links= "AA2"> Success </response>
        /// <response code="400"> Bad Request </response>
        /// <response code="401"> Unauthorize </response>
        /// <response code="403"> Forbidden </response>
        /// <response code="404"> Not Found </response>
        /// <response code="500"> Internal Server Error </response>

        [HttpGet("Info"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        //[HttpGet("GetInfo")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetPersonalInfo()
        {
            //try
            //{
            //    return Ok(await _infoService.GetPersonalInfo());
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message + "/" + ex.Data + "/" + ex.InnerException);
            //    return BadRequest();
            //}

            return Ok(await _infoService.GetPersonalInfo());


        }

        /// <summary>
        /// สำหรับ GET Personal Information เป็นรายบุคคล.
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>

        [HttpGet("GetInfo/{personId}")]
        public async Task<IActionResult> GetPersonalInfoById(int personId)
        {
            return Ok(await _infoService.GetPersonalInfoById(personId));
        }

        /// <summary>
        /// สำหรับ Update Personal information เป้นรายบุคคล
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="updateperson"></param>
        /// <returns></returns>

        [HttpPut("UpdatePersonalInfo/{personId}")]
        public async Task<IActionResult> UpdatePersonalInfoById(int personId, UpdatePersonalInfoDto updateperson)
        {
            
            return Ok(await _infoService.UpdatePersonalInfoById(personId, updateperson));
        }

        
    }
}
