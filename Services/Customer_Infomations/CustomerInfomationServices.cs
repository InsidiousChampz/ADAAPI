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

namespace SmsUpdateCustomer_Api.Services.Customer_Infomations
{
    public class CustomerInfomationServices : ServiceBase, ICustomerInfomationServices
    {

        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ICustomerInfomationServices> _log;
        private readonly IHttpContextAccessor _httpcontext;

        private const string TEXTSUCCESS = "Success";
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
                (bool,string) retval = ValidationsforCustomerLogin(filter);

                if (retval.Item1 == false)
                {
                    ResponseResult.Failure<GetCustomerHeaderDto>(retval.Item2);
                }

                //Process
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
                    return ResponseResult.Success(dto, TEXTSUCCESS);
                }

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetCustomerHeaderDto>(ex.Message);
                
            }
            
        }

        private static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(c))
                {
                    return false;
                }
            }
            return true;
        }

        private static (bool,string) ValidationsforCustomerLogin(GetCustomerHeaderWithFilter filter)
        {

            // For GetCustomerLoginByIdentityAndLastname Method.

            try
            {
              
                if (filter.LoginIdentityCard == null)
                {
                    return (false, "IdentityCard is invalid.");
                }

                if (filter.LoginLastName == null)
                {
                    return (false, "LastName is invalid.");
                }

                bool checkIdentityNumberIsNumberOnly = Microsoft.VisualBasic.Information.IsNumeric(filter.LoginIdentityCard);

                bool checklastNameIsStringOnly = IsDigitsOnly(filter.LoginLastName);

                if (checkIdentityNumberIsNumberOnly == false)
                {
                    return (false, "IdentityCard Number have a charactor.");
                }

                if (checklastNameIsStringOnly == false)
                {
                    return (false, "Lastname have a number.");
                }

                return (true, "Success");

            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }
        }
    }
}
