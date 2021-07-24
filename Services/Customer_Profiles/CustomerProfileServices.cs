using AutoMapper;
using CustomerProFileAPI.Data;
using CustomerProFileAPI.DTOs.Customer_Profiles;
using CustomerProFileAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.Services.Customer_Profiles
{
    public class CustomerProfileServices : ServiceBase, ICustomerProfileServices
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ICustomerProfileServices> _log;
        private readonly IHttpContextAccessor _httpcontext;

        public CustomerProfileServices(AppDBContext dbContext, IMapper mapper, ILogger<ICustomerProfileServices> log, IHttpContextAccessor httpcontext) : base(dbContext, mapper, httpcontext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _log = log;
            _httpcontext = httpcontext;
        }

        public async Task<ServiceResponse<GetProfileDto>> NewCustomerProfile(int personId)
        {
            try
            {
                var customer = await _dbContext.Customer_Snapshots
                   .Where(x => x.PersonId == personId)
                   .ToListAsync();

                if (customer == null)
                {
                    return ResponseResult.Failure<GetProfileDto>("Customer Not Found.");
                }
                else
                {
                    var dto = _mapper.Map<GetProfileDto>(customer);
                    return ResponseResult.Success(dto);
                }

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetProfileDto>(ex.Message);

            }
        }
    }
}
