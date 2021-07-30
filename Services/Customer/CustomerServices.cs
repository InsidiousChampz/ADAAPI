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

namespace SmsUpdateCustomer_Api.Services.Customer
{
    public class CustomerServices : ServiceBase, ICustomerServices
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ICustomerServices> _log;
        private readonly IHttpContextAccessor _httpcontext;
        private const string TEXTSUCCESS = "Success";
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

                #region efCore
                //Payer is must have a policies if not found policies that mean a customer not a payer
                var customer = await _dbContext.Payer_Snapshots
                    .Where(x => x.LastName == filter.LastName
                     && x.IdentityCard == filter.IdentityCard
                     && x.Policies.Count > 0)
                    .Include(x => x.Policies)
                    .ThenInclude(o => o.CustomerDetail)
                    .ToListAsync();
                #endregion

                if (customer == null)
                {
                    // 404
                    return ResponseResult.Failure<List<GetPayerDto>>("Payer Not Found.");
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
                    return ResponseResult.Success(dtos, "Payer Not Found.");

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
                    return ResponseResult.Failure<List<GetPayerDto>>("Customer Not Found.");
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
                    return ResponseResult.Failure<GetPayerDto>("Customer Not Found.");
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
