using AutoMapper;
using CustomerProFileAPI.Data;
using CustomerProFileAPI.DTOs.Customer;
using CustomerProFileAPI.Models;
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

        public async Task<ServiceResponse<List<GetCustomerProfileDto>>> GetCustomerAndPoliciesByIdentityAndLastName(GetByIdentityAndLastNameDto filter)
        {
            try
            {
                #region efCore
                //Payer is must have a policies if not found policies that mean a customer not a payer
                var customer = await _dbContext.Customer_Snapshots
                    .Where(x => x.LastName == filter.LastName
                     && x.IdentityCard == filter.IdentityCard
                     && x.Policies.Count > 0)
                    .Include(x => x.Policies)
                    .ThenInclude(o => o.CustomerDetail)
                    .ToListAsync();
                #endregion

                #region Linq Join Example 1
                //var customer = from e in _dbContext.Customer_Snapshots
                //               join d in _dbContext.Policy_Snapshots on e.PersonId equals d.PayerPersonId into table1
                //               from d in table1.ToList()
                //               join i in _dbContext.Customer_Snapshots on d.CustPersonId equals i.PersonId into table2
                //               from i in table2.ToList()
                //               select new 
                //               {
                //                   PersonId = e.PersonId,
                //                   Customer_guid = e.Customer_guid,
                //                   TitleId = e.TitleId,
                //                   FirstName = e.FirstName,
                //                   LastName = e.LastName,
                //                   Birthdate = e.Birthdate,
                //                   IdentityCard = e.IdentityCard,
                //                   PrimaryPhone = e.PrimaryPhone,
                //                   SecondaryPhone = e.SecondaryPhone,
                //                   Email = e.Email,
                //                   LineID = e.LineID,

                //                   ApplicationCode = d.ApplicationCode,
                //                   dCustomer_guid = d.Customer_guid,
                //                   ProductType = d.ProductType,
                //                   Product = d.Product,
                //                   Premium = d.Premium,
                //                   CustPersonId = d.CustPersonId,
                //                   CustName = d.CustName,
                //                   PayerName = d.PayerName,

                //                   iPersonId = i.PersonId,
                //                   iTitleId = i.TitleId,
                //                   iFirstName = i.FirstName,
                //                   iLastName = i.LastName,
                //                   iBirthdate = i.Birthdate,
                //                   iIdentityCard = i.IdentityCard,
                //                   iPrimaryPhone =  i.PrimaryPhone,
                //                   iSecondaryPhone =  i.SecondaryPhone,
                //                   iEmail =  i.Email,
                //                   iLineID = i.LineID,
                //               };


                //foreach (var item in customer)
                //{
                //    var GetPayerDto = new GetPayerDto
                //    {
                //        PersonId = item.PersonId,
                //        Customer_guid = item.Customer_guid,
                //        TitleId = item.TitleId,
                //        FirstName = item.FirstName,
                //        LastName = item.LastName,
                //        Birthdate = item.Birthdate,
                //        IdentityCard = item.IdentityCard,
                //        PrimaryPhone = item.PrimaryPhone,
                //        SecondaryPhone = item.SecondaryPhone,
                //        Email = item.Email,
                //        LineID = item.LineID,
                //    };
                //};
                #endregion


                #region Linq Join Example 2
                //var customer = await _dbContext.Customer_Snapshots
                //    .Join(_dbContext.Policy_Snapshots
                //    , cs => cs.PersonId
                //    , ps => ps.PayerPersonId
                //    , (cs, ps) => new GetPayerDto
                //    {
                //        PersonId = cs.PersonId,
                //        Customer_guid = cs.Customer_guid,
                //        TitleId = cs.TitleId,
                //        FirstName = cs.FirstName,
                //        LastName = cs.LastName,
                //        Birthdate = cs.Birthdate,
                //        IdentityCard = cs.IdentityCard,
                //        PrimaryPhone = cs.PrimaryPhone,
                //        SecondaryPhone = cs.SecondaryPhone,
                //        Email = cs.Email,
                //        LineID = cs.LineID,
                //        Policies = new List<GetPolicyDto>
                //    {
                //        new GetPolicyDto
                //        {
                //            ApplicationCode = ps.ApplicationCode,
                //            Customer_guid = ps.Customer_guid,
                //            ProductType = ps.ProductType,
                //            Product = ps.Product,
                //            Premium = ps.Premium,
                //            CustPersonId = ps.CustPersonId,
                //            CustName = ps.CustName,
                //            PayerName = ps.PayerName,

                //            CustomerDetail = new GetCustomerDto
                //            {
                //                PersonId = ps.CustPersonId,
                //                TitleId = (_dbContext.Customer_Snapshots.Where(x => x.PersonId == ps.CustPersonId)).Select(x => x.TitleId).FirstOrDefault(),
                //                FirstName = (_dbContext.Customer_Snapshots.Where(x => x.PersonId == ps.CustPersonId)).Select(x => x.FirstName).FirstOrDefault(),
                //                LastName = (_dbContext.Customer_Snapshots.Where(x => x.PersonId == ps.CustPersonId)).Select(x => x.LastName).FirstOrDefault(),
                //                Birthdate = (_dbContext.Customer_Snapshots.Where(x => x.PersonId == ps.CustPersonId)).Select(x => x.Birthdate).FirstOrDefault(),
                //                IdentityCard = (_dbContext.Customer_Snapshots.Where(x => x.PersonId == ps.CustPersonId)).Select(x => x.IdentityCard).FirstOrDefault(),
                //                PrimaryPhone = (_dbContext.Customer_Snapshots.Where(x => x.PersonId == ps.CustPersonId)).Select(x => x.PrimaryPhone).FirstOrDefault(),
                //                SecondaryPhone = (_dbContext.Customer_Snapshots.Where(x => x.PersonId == ps.CustPersonId)).Select(x => x.SecondaryPhone).FirstOrDefault(),
                //                Email = (_dbContext.Customer_Snapshots.Where(x => x.PersonId == ps.CustPersonId)).Select(x => x.Email).FirstOrDefault(),
                //                LineID = (_dbContext.Customer_Snapshots.Where(x => x.PersonId == ps.CustPersonId)).Select(x => x.LineID).FirstOrDefault(),
                //            }
                //        }
                //    }
                //    }).Where(x => x.LastName == filter.LastName
                //     && x.IdentityCard == filter.IdentityCard)
                // .GroupBy(x => x.PersonId)
                //.ToListAsync();
                #endregion

                if (customer == null)
                {
                    // 404
                    return ResponseResult.Failure<List<GetCustomerProfileDto>>("Payer Not Found.");
                }
                else
                {
                    // 200
                    if (customer.Count > 0)
                    {
                        var dto = _mapper.Map<List<GetCustomerProfileDto>>(customer);
                        return ResponseResult.Success(dto, "Success");
                    }
                    // 200
                    var dtos = _mapper.Map<List<GetCustomerProfileDto>>(customer);
                    return ResponseResult.Success(dtos, "Payer Not Found.");

                }

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetCustomerProfileDto>>(ex.Message);

            }
        }

        public async Task<ServiceResponse<List<GetCustomerProfileDto>>> GetCustomerAndPoliciesByPersonId(int personId)
        {
            try
            {
                var customer = await _dbContext.Customer_Snapshots
                   .Where(x => x.PersonId == personId)
                   .Include(x => x.Policies)
                   .ThenInclude(o => o.CustomerDetail)
                   .ToListAsync();


                if (customer == null)
                {
                    return ResponseResult.Failure<List<GetCustomerProfileDto>>("Customer Not Found.");
                }
                else
                {
                    var dto = _mapper.Map<List<GetCustomerProfileDto>>(customer);
                    return ResponseResult.Success(dto);
                }

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetCustomerProfileDto>>(ex.Message);

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
