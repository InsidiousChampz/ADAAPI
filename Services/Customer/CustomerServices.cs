using AutoMapper;
using SmsUpdateCustomer_Api.Data;
using SmsUpdateCustomer_Api.DTOs.Customer;
using SmsUpdateCustomer_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using SmsUpdateCustomer_Api.Validations;
using SmsUpdateCustomer_Api.DTOs.Customer_Infomations;
using SmsUpdateCustomer_Api.Models.Customer_Infomations;

namespace SmsUpdateCustomer_Api.Services.Customer
{
    public class CustomerServices : ServiceBase, ICustomerServices
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ICustomerServices> _log;
        private readonly IHttpContextAccessor _httpcontext;
        private const string TEXTSUCCESS = "Success";
        private const string TEXTCUSTOMERNOTFOUND = "ไม่พบลูกค้า.";
        public CustomerServices(AppDBContext dBContext, IMapper mapper, ILogger<ICustomerServices> log, IHttpContextAccessor httpcontext) : base(dBContext, mapper, httpcontext)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _log = log;
            _httpcontext = httpcontext;
        }

        public async Task<ServiceResponse<List<GetPayerDto>>> GetPayerAndPoliciesByIdentityAndLastName(GetByIdentityAndLastNameDto filter)
        {
            try
            {
                // Validate
                (bool, string) retval = ControlValidator.ValidationsforPayerAndPoliciesByIdentityAndLastName(filter);

                if (retval.Item1 == false)
                {
                    return ResponseResult.Failure<List<GetPayerDto>>(retval.Item2);
                }

                // Check table header first.
                var customerHeader = await _dbContext.Customer_Headers
               .FirstOrDefaultAsync(x => x.LoginIdentityCard == filter.IdentityCard && x.LoginLastName == filter.LastName);

                if (customerHeader == null)
                {
                    return ResponseResult.Failure<List<GetPayerDto>>("บัตรประชาชน หรือ นามสกุล ไม่ถูกต้อง");
                }

                var loginData = new Customer_Login_Log
                {
                    IdentityCard = filter.IdentityCard,
                    LastName = filter.LastName,
                    LoginDate = Now()
                };

                _dbContext.Customer_Login_Logs.Add(loginData);
                await _dbContext.SaveChangesAsync();


                #region efCore
                //Payer is must have a policies if not found policies that mean a customer not a payer
                var customer = await _dbContext.Payer_Snapshots
                    .Where(x => x.LastName.Trim() == filter.LastName.Trim()
                     && x.IdentityCard.Trim() == filter.IdentityCard.Trim()
                     && x.Policies.Count > 0)
                    .Include(x => x.Policies)
                    .ThenInclude(o => o.CustomerDetail)
                    .ToListAsync();
                #endregion

                if (customer == null)
                {
                    // 404
                    return ResponseResult.Failure<List<GetPayerDto>>(TEXTCUSTOMERNOTFOUND);
                }
                else
                {

                    // 200
                    if (customer.Count > 0)
                    {
                        var dto = _mapper.Map<List<GetPayerDto>>(customer);
                        return ResponseResult.Success(dto, TEXTSUCCESS);
                    }
                    // 200
                    var dtos = _mapper.Map<List<GetPayerDto>>(customer);
                    return ResponseResult.Failure<List<GetPayerDto>>(TEXTCUSTOMERNOTFOUND);

                }

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetPayerDto>>(ex.Message);

            }
        }

        public async Task<ServiceResponse<List<GetPayerDto>>> GetPayerAndPoliciesByPersonId(int personId)
        {
            try
            {
                var customer = await _dbContext.Payer_Snapshots
                   .Where(x => x.PersonId == personId)
                   .Include(x => x.Policies)
                   .ThenInclude(o => o.CustomerDetail)
                   .ToListAsync();


                if (customer == null)
                {
                    return ResponseResult.Failure<List<GetPayerDto>>(TEXTCUSTOMERNOTFOUND);
                }
                else
                {
                    var dto = _mapper.Map<List<GetPayerDto>>(customer);
                    return ResponseResult.Success(dto, TEXTSUCCESS);
                }

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetPayerDto>>(ex.Message);

            }
        }

        public async Task<ServiceResponse<GetPayerDto>> GetPayerByPersonId(int personId)
        {
            try
            {
                var customer = await _dbContext.Payer_Snapshots
                    .FirstOrDefaultAsync(x => x.PersonId == personId);

                if (customer == null)
                {
                    return ResponseResult.Failure<GetPayerDto>(TEXTCUSTOMERNOTFOUND);
                }
                else
                {
                    var dto = _mapper.Map<GetPayerDto>(customer);
                    return ResponseResult.Success(dto, TEXTSUCCESS);
                }

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetPayerDto>(ex.Message);

            }



            
        }

        
    }
}
