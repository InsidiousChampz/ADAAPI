using AutoMapper;
using CustomerProFileAPI.Data;
using CustomerProFileAPI.DTOs.Customer_Infomations;
using CustomerProFileAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.Services.Customer_Infomations
{
    public class CustomerInfomationServices : ServiceBase, ICustomerInfomationServices
    {

        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ICustomerInfomationServices> _log;
        private readonly IHttpContextAccessor _httpcontext;
        public CustomerInfomationServices(AppDBContext dBContext, IMapper mapper, ILogger<ICustomerInfomationServices> log, IHttpContextAccessor httpcontext) : base(dBContext, mapper, httpcontext)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _log = log;
            _httpcontext = httpcontext;
        }
        public async Task<ServiceResponse<List<GetCustomerHeaderDto>>> GetCustomerLogin()
        {
            try
            {
                var customerHeader = await _dbContext.Customer_Headers
                .AsNoTracking()
                .ToListAsync();
                var dto = _mapper.Map<List<GetCustomerHeaderDto>>(customerHeader);
                return ResponseResult.Success(dto);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetCustomerHeaderDto>>(ex.Message);
            }
            
        }

        public async Task<ServiceResponse<GetCustomerHeaderDto>> GetCustomerLoginByIdentityAndLastname(GetCustomerHeaderWithFilter filter)
        {
            try
            {
                var customerHeader = await _dbContext.Customer_Headers
                .FirstOrDefaultAsync(x => x.LoginIdentityCard == filter.LoginIdentityCard
                    && x.LoginLastName == filter.LoginLastName);

                if (customerHeader == null)
                {
                    return ResponseResult.Failure<GetCustomerHeaderDto>("Customer Not Found.");
                }
                else
                {
                    var dto = _mapper.Map<GetCustomerHeaderDto>(customerHeader);
                    return ResponseResult.Success(dto);
                }

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetCustomerHeaderDto>(ex.Message);
                
            }
            
        }
    }
}
