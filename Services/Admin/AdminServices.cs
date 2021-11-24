using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ADAAPI.Data;
using ADAAPI.Helpers;
using ADAAPI.Models;
using ADAAPI.Validations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ADAAPI.DTOs.Admin;

namespace ADAAPI.Services.Admin
{
    public class AdminServices : ServiceBase, IAdminServices
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<IAdminServices> _log;
        private readonly IHttpContextAccessor _httpcontext;
        private readonly IConfiguration _configuraton;

        private string TEXTSUCCESS = "Success";

        public AdminServices(AppDBContext dBContext, IMapper mapper, ILogger<IAdminServices> log, IHttpContextAccessor httpcontext, IConfiguration configuraton) : base(dBContext, mapper, httpcontext)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _log = log;
            _httpcontext = httpcontext;
            _configuraton = configuraton;
        }

        public async Task<ServiceResponse<List<GetTableDTO>>> GetAllTableA()
        {
            try
            {
                var tblA = await _dbContext.tbl_A.AsNoTracking().ToListAsync();

                var dto = _mapper.Map<List<GetTableDTO>>(tblA);
                return ResponseResult.Success(dto, TEXTSUCCESS);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetTableDTO>>(ex.Message);
            }
        }

        public async Task<ServiceResponse<List<GetTableDTO>>> GetAllTableB()
        {
            try
            {
                var tblB = await _dbContext.tbl_B.AsNoTracking().ToListAsync();

                var dto = _mapper.Map<List<GetTableDTO>>(tblB);
                return ResponseResult.Success(dto, TEXTSUCCESS);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetTableDTO>>(ex.Message);
            }
        }

        public async Task<ServiceResponse<List<GetTableDTO>>> GetAllTwoTable()
        {
            try
            {
                var tblA = (from o in await _dbContext.tbl_A.ToListAsync()
                            select new GetTableDTO()
                            {
                                empId = o.empId,
                                empFirstname = o.empFirstname,
                                empLastname = o.empLastname,
                                empPhoneNumber = o.empPhoneNumber
                            });

                var tblB = (from o in await _dbContext.tbl_B.ToListAsync()
                            select new GetTableDTO()
                            {
                                empId = o.empId,
                                empFirstname = o.empFirstname,
                                empLastname = o.empLastname,
                                empPhoneNumber = o.empPhoneNumber
                            });

                var tblAll = tblB.Union(tblA);

                var result = (from x in tblAll
                              group new { x } by new
                              {
                                  x.empId,
                                  x.empFirstname,
                                  x.empLastname,
                                  x.empPhoneNumber
                              } into xx
                              select new GetTableDTO
                              {
                                  empId = xx.Key.empId,
                                  empFirstname = xx.Key.empFirstname,
                                  empLastname = xx.Key.empLastname,
                                  empPhoneNumber = xx.Key.empPhoneNumber
                              });

                var dto = _mapper.Map<List<GetTableDTO>>(result);
                return ResponseResult.Success(dto, TEXTSUCCESS);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetTableDTO>>(ex.Message);
            }
        }

        public async Task<ServiceResponse<GetTableDTO>> SaveTableA(AddTableDto payload)
        {
            try
            {
                var AddtableA = new tbl_A
                {
                    empId = payload.empId,
                    empFirstname = payload.empFirstname,
                    empLastname = payload.empLastname,
                    empPhoneNumber = payload.empPhoneNumber,
                    empStatus = 1,
                    createdDate = Now(),
                    isActive = true,
                };
                
                _dbContext.tbl_A.Add(AddtableA);
                await _dbContext.SaveChangesAsync();


                var dto = _mapper.Map<GetTableDTO>(AddtableA);
                return ResponseResult.Success(dto, TEXTSUCCESS);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetTableDTO>(ex.Message);
            }
        }

        public async Task<ServiceResponse<GetTableDTO>> SaveTableB(AddTableDto payload)
        {
            try
            {
                var AddtableB = new tbl_B
                {
                    empId = payload.empId,
                    empFirstname = payload.empFirstname,
                    empLastname = payload.empLastname,
                    empPhoneNumber = payload.empPhoneNumber,
                    empStatus = 1,
                    createdDate = Now(),
                    isActive = true,
                };

                _dbContext.tbl_B.Add(AddtableB);
                await _dbContext.SaveChangesAsync();


                var dto = _mapper.Map<GetTableDTO>(AddtableB);
                return ResponseResult.Success(dto, TEXTSUCCESS);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetTableDTO>(ex.Message);
            }
        }

        public async Task<ServiceResponse<GetTableDTO>> SaveAllTable(AddTableDto payload)
        {
            try
            {

                var AddtableA = new tbl_A
                {
                    empId = payload.empId,
                    empFirstname = payload.empFirstname,
                    empLastname = payload.empLastname,
                    empPhoneNumber = payload.empPhoneNumber,
                    empStatus = 1,
                    createdDate = Now(),
                    isActive = true,
                };

                _dbContext.tbl_A.Add(AddtableA);

                var AddtableB = new tbl_B
                {
                    empId = payload.empId,
                    empFirstname = payload.empFirstname,
                    empLastname = payload.empLastname,
                    empPhoneNumber = payload.empPhoneNumber,
                    empStatus = 1,
                    createdDate = Now(),
                    isActive = true,
                };

                _dbContext.tbl_B.Add(AddtableB);

                await _dbContext.SaveChangesAsync();


                var dto = _mapper.Map<GetTableDTO>(AddtableB);
                return ResponseResult.Success(dto, TEXTSUCCESS);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetTableDTO>(ex.Message);
            }
        }

        public async Task<ServiceResponse<GetTableDTO>> UpdatetableA(string empId,UpdateTableDTO payload)
        {
            try
            {
                var emp = await _dbContext.tbl_A.FirstOrDefaultAsync(x => x.empId == empId);

                if (emp == null)
                {
                    return ResponseResult.Failure<GetTableDTO>("Not Found");
                }

                emp.empFirstname = payload.empFirstname;
                emp.empLastname = payload.empLastname;
                emp.empPhoneNumber = payload.empPhoneNumber;
                emp.createdDate = Now();

                await _dbContext.SaveChangesAsync();

                var dto = _mapper.Map<GetTableDTO>(emp);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetTableDTO>(ex.Message);
            }
        }

        public async Task<ServiceResponse<GetTableDTO>> UpdatetableB(string empId, UpdateTableDTO payload)
        {
            try
            {
                var emp = await _dbContext.tbl_B.FirstOrDefaultAsync(x => x.empId == empId);

                if (emp == null)
                {
                    return ResponseResult.Failure<GetTableDTO>("Not Found");
                }

                emp.empFirstname = payload.empFirstname;
                emp.empLastname = payload.empLastname;
                emp.empPhoneNumber = payload.empPhoneNumber;
                emp.createdDate = Now();

                await _dbContext.SaveChangesAsync();

                var dto = _mapper.Map<GetTableDTO>(emp);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetTableDTO>(ex.Message);
            }
        }

        public async Task<ServiceResponse<GetTableDTO>> UpdateAlltable(string empId, UpdateTableDTO payload)
        {
            try
            {
                var empA = await _dbContext.tbl_A.FirstOrDefaultAsync(x => x.empId == empId);

                if (empA == null)
                {
                    return ResponseResult.Failure<GetTableDTO>("Not Found");
                }

                empA.empFirstname = payload.empFirstname;
                empA.empLastname = payload.empLastname;
                empA.empPhoneNumber = payload.empPhoneNumber;
                empA.createdDate = Now();



                var empB = await _dbContext.tbl_B.FirstOrDefaultAsync(x => x.empId == empId);

                if (empB == null)
                {
                    return ResponseResult.Failure<GetTableDTO>("Not Found");
                }

                empB.empFirstname = payload.empFirstname;
                empB.empLastname = payload.empLastname;
                empB.empPhoneNumber = payload.empPhoneNumber;
                empB.createdDate = Now();

                await _dbContext.SaveChangesAsync();

                var dto = _mapper.Map<GetTableDTO>(empB);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetTableDTO>(ex.Message);
            }
        }

        public async Task<ServiceResponse<GetTableDTO>> DeleteAlltable(string empId)
        {
            try
            {
                var empA = await _dbContext.tbl_A.FirstOrDefaultAsync(x => x.empId == empId);

                if (empA == null)
                {
                    return ResponseResult.Failure<GetTableDTO>("Not Found");
                }

                var empB = await _dbContext.tbl_B.FirstOrDefaultAsync(x => x.empId == empId);

                if (empB == null)
                {
                    return ResponseResult.Failure<GetTableDTO>("Not Found");
                }


                _dbContext.tbl_A.Remove(empA);
                _dbContext.tbl_B.Remove(empB);
                await _dbContext.SaveChangesAsync();

                var dto = _mapper.Map<GetTableDTO>(empB);
                return ResponseResult.Success(dto, TEXTSUCCESS);
            }
            catch (Exception ex)
            {

                return ResponseResult.Failure<GetTableDTO>(ex.Message);
            }
        }
    }
}
