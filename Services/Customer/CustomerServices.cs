using AutoMapper;
using CustomerProFileAPI.Data;
using CustomerProFileAPI.DTOs.Customer;
using CustomerProFileAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CustomerProFileAPI.Services.Customer
{
    public class CustomerServices : ServiceBase, ICustomerServices
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ICustomerServices> _log;
        private readonly IHttpContextAccessor _httpcontext;

        public CustomerServices(AppDBContext dBContext, IMapper mapper, ILogger<ICustomerServices> log, IHttpContextAccessor httpcontext) : base(dBContext, mapper, httpcontext)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _log = log;
            _httpcontext = httpcontext;
        }

        public async Task<ServiceResponse<List<GetCustomerListDto>>> GetCustomerAndPoliciesByPersonId(int personId)
        {
            try
            {
                var customer = await _dbContext.Customer_Snapshots
                    .Include(x => x.Policies)
                    .Where(x => x.PersonId == personId)
                    .ToListAsync();


                if (customer == null)
                {
                    return ResponseResult.Failure<List<GetCustomerListDto>>("Customer Not Found.");
                }
                else
                {
                    var dto = _mapper.Map<List<GetCustomerListDto>>(customer);
                    return ResponseResult.Success(dto);
                }

                //var customer = await _dbContext.Customer_Snapshots
                //    .Join(_dbContext.Policy_Snapshots
                //    , cs => cs.PersonId
                //    , ps => ps.PayerPersonId
                //    ,(cs,ps) => new GetCustomerByPersonIdDto {
                //        PersonId = cs.PersonId,
                //        Customer_guid = cs.Customer_guid,
                //        TitleId = cs.TitleId,
                //        FirstName = cs.FirstName,
                //        LastName = cs.LastName ,
                //        Birthdate = cs.Birthdate,
                //        IdentityCard = cs.IdentityCard,
                //        PrimaryPhone = cs. PrimaryPhone,
                //        SecondaryPhone = cs.SecondaryPhone,
                //        Email = cs.Email,   
                //        LineID = cs.LineID,
                //        WorkAddressId = cs.WorkAddressId,
                //        WorkAddressName = cs.WorkAddressName,
                //        WorkAddress1 = cs.WorkAddress1,
                //        WorkAddress2 = cs.WorkAddress2,
                //        WorkAddressSubDistrictCode = cs.WorkAddressSubDistrictCode,
                //        WorkAddressSubDistrict = cs.WorkAddressSubDistrict,
                //        WorkAddressDistrict = cs.WorkAddressDistrict,
                //        WorkAddressProvince = cs.WorkAddressProvince,
                //        WorkAddressZipCode = cs.WorkAddressZipCode,
                //        ApplicationCode = ps.ApplicationCode,
                //        ProductType = ps.ProductType,
                //        Product = ps.Product,
                //        Premium = ps.Premium,
                //        CustPersonId = ps.CustPersonId,
                //        Cust_guid = ps.Cust_guid,
                //        CustName = ps.CustName,
                //        PayerPersonId = ps.PayerPersonId,
                //        Payer_guid = ps.Payer_guid,
                //        PayerName = ps.PayerName,
                //    })
                //    .Where(cs => cs.PersonId == personId)
                //    .ToListAsync();

                //    //SELECT*
                //    //FROM ss.SnapCustomer sc
                //    //JOIN ss.SnapPolicy sp
                //    //ON sc.PersonId = sp.PayerPersonId
                //    //WHERE sc.PersonId = 3247


                //if (customer == null)
                //{
                //    return ResponseResult.Failure<List<GetCustomerByPersonIdDto>>("Customer Not Found.");
                //}
                //else
                //{
                //    var dto = _mapper.Map<List<GetCustomerByPersonIdDto>>(customer);
                //    return ResponseResult.Success(dto);
                //}
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetCustomerListDto>>(ex.Message);

            }
        }

        public async Task<ServiceResponse<GetCustomerDto>> GetCustomerByPersonId(int personId)
        {
            try
            {
                var customer = await _dbContext.Customer_Snapshots
                    .FirstOrDefaultAsync(x => x.PersonId == personId);

                if (customer == null)
                {
                    return ResponseResult.Failure<GetCustomerDto>("Customer Not Found.");
                }
                else
                {
                    var dto = _mapper.Map<GetCustomerDto>(customer);
                    return ResponseResult.Success(dto);
                }

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetCustomerDto>(ex.Message);

            }



            
        }
    }
}
