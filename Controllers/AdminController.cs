using Microsoft.AspNetCore.Mvc;
using ADAAPI.DTOs.Admin;
using ADAAPI.Services.Admin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ADAAPI.Controllers
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

        [HttpGet("GetAllTableA")]
        public async Task<IActionResult> GetAllTableA()
        {
            try
            {
                var ret = await _adminServices.GetAllTableA();

                if (ret.IsSuccess == true)
                {
                    // 200
                    return Ok(ret);
                }

                // 404
                return NotFound(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllTableB")]
        public async Task<IActionResult> GetAllTableB()
        {
            try
            {
                var ret = await _adminServices.GetAllTableB();

                if (ret.IsSuccess == true)
                {
                    // 200
                    return Ok(ret);
                }

                // 404
                return NotFound(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllTwoTable")]
        public async Task<IActionResult> GetAllTwoTable()
        {
            try
            {
                var ret = await _adminServices.GetAllTwoTable();

                if (ret.IsSuccess == true)
                {
                    // 200
                    return Ok(ret);
                }

                // 404
                return NotFound(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SaveTableA")]
        public async Task<IActionResult> SaveTableA(AddTableDto payload)
        {
            try
            {
                var ret = await _adminServices.SaveTableA(payload);

                if (ret.IsSuccess == true)
                {
                    // 200
                    return Ok(ret);
                }

                // 404
                return NotFound(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SaveTableB")]
        public async Task<IActionResult> SaveTableB(AddTableDto payload)
        {
            try
            {
                var ret = await _adminServices.SaveTableB(payload);

                if (ret.IsSuccess == true)
                {
                    // 200
                    return Ok(ret);
                }

                // 404
                return NotFound(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SaveAllTable")]
        public async Task<IActionResult> SaveAllTable(AddTableDto payload)
        {
            try
            {
                var ret = await _adminServices.SaveAllTable(payload);

                if (ret.IsSuccess == true)
                {
                    // 200
                    return Ok(ret);
                }

                // 404
                return NotFound(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdatetableA/{empId}")]
        public async Task<IActionResult> UpdatetableA(string empId, UpdateTableDTO payload)
        {
            var ret = await _adminServices.UpdatetableA(empId, payload);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }

        [HttpPost("UpdatetableB/{empId}")]
        public async Task<IActionResult> UpdatetableB(string empId, UpdateTableDTO payload)
        {
            var ret = await _adminServices.UpdatetableB(empId, payload);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }

        [HttpPost("UpdateAlltable/{empId}")]
        public async Task<IActionResult> UpdateAlltable(string empId, UpdateTableDTO payload)
        {
            var ret = await _adminServices.UpdateAlltable(empId, payload);

            if (ret.IsSuccess == true)
            {
                // 200
                return Ok(ret);
            }

            // 404
            return NotFound(ret);
        }

        [HttpPost("DeleteAlltable/{empId}")]
        public async Task<IActionResult> DeleteAlltable(string empId)
        {
            var ret = await _adminServices.DeleteAlltable(empId);

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
