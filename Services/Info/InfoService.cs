using AutoMapper;
using INFOEDITORAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFOEDITORAPI.Services.Info
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
    }
}
