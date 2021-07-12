using INFOEDITORAPI.DTOs.Info;
using INFOEDITORAPI.Services.Info;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFOEDITORAPI.Data
{

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
        /// สำหรับ GET Personal Information ทั้งหมดทุกคน.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Info")]
        public async Task<IActionResult> GetPersonalInfo()
        {
            return Ok(await _infoService.GetPersonalInfo());

        }

        /// <summary>
        /// สำหรับ GET Personal Information เป็นรายบุคคล.
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>

        [HttpGet("Info/{personId}")]
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
