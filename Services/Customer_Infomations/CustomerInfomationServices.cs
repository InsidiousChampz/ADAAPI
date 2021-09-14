using AutoMapper;
using SmsUpdateCustomer_Api.Data;
using SmsUpdateCustomer_Api.DTOs.Customer_Infomations;
using SmsUpdateCustomer_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmsUpdateCustomer_Api.Validations;

namespace SmsUpdateCustomer_Api.Services.Customer_Infomations
{
    public class CustomerInfomationServices : ServiceBase, ICustomerInfomationServices
    {

        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ICustomerInfomationServices> _log;
        private readonly IHttpContextAccessor _httpcontext;

        private const string TEXTSUCCESS = "Success";
        private const string TEXTCUSTOMERNOTFOUND = "ไม่พบลูกค้า.";
        
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
                return ResponseResult.Success(dto, TEXTSUCCESS);
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
                //Validate
                (bool,string) retval = ControlValidator.ValidationsforCustomerLogin(filter);

                if (retval.Item1 == false)
                {
                    return ResponseResult.Failure<GetCustomerHeaderDto>(retval.Item2);
                }

                //Process
                var customerHeader = await _dbContext.Customer_Headers
                .FirstOrDefaultAsync(x => x.LoginIdentityCard == filter.LoginIdentityCard
                    && x.LoginLastName == filter.LoginLastName);

                if (customerHeader == null)
                {
                    return ResponseResult.Failure<GetCustomerHeaderDto>(TEXTCUSTOMERNOTFOUND);
                }
                else
                {
                    var dto = _mapper.Map<GetCustomerHeaderDto>(customerHeader);
                    return ResponseResult.Success(dto, TEXTSUCCESS);
                }

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetCustomerHeaderDto>(ex.Message);
                
            }
            
        }

        

        
    }
}
