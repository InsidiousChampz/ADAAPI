using AutoMapper;
using CustomerProFileAPI.Data;
using CustomerProFileAPI.DTOs.Info;
using CustomerProFileAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.Services.Info
{
    public class InfoService : ServiceBase, IInfoService
    {

        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<InfoService> _log;
        private readonly IHttpContextAccessor _httpcontext;

        public InfoService(AppDBContext dBContext, IMapper mapper, ILogger<InfoService> log, IHttpContextAccessor httpcontext) : base(dBContext, mapper, httpcontext)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _log = log;
            _httpcontext = httpcontext;
        }

        public Task<ServiceResponse<List<GetPersonalInfoDto>>> GetPersonalInfo()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetPersonalInfoDto>> GetPersonalInfoById(int personId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetPersonalInfoDto>> UpdatePersonalInfoById(int personId, UpdatePersonalInfoDto updatePerson)
        {
            throw new NotImplementedException();
        }
    }
}
