using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmsUpdateCustomer_Api.Data;
using SmsUpdateCustomer_Api.DTOs.Admin;
using SmsUpdateCustomer_Api.DTOs.Customer;
using SmsUpdateCustomer_Api.DTOs.Customer_Profiles;
using SmsUpdateCustomer_Api.Helpers;
using SmsUpdateCustomer_Api.Models;
using SmsUpdateCustomer_Api.Models.Customer_Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Services.Admin
{
    public class AdminServices : ServiceBase, IAdminServices
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<IAdminServices> _log;
        private readonly IHttpContextAccessor _httpcontext;

        private string TEXTSUCCESS = "Success";

        public AdminServices(AppDBContext dBContext, IMapper mapper, ILogger<IAdminServices> log, IHttpContextAccessor httpcontext) : base(dBContext, mapper, httpcontext)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _log = log;
            _httpcontext = httpcontext;
        }

        private static bool AssignBeforeData(IEnumerable<Customer_Profile_Transaction> reload, List<GetCompareDto> original)
        {
            {
                try
                {
                    foreach (var item in reload)
                    {
                        switch (item.FieldData)
                        {
                            case "TitleId":
                                original[0].TitleId = int.Parse(item.BeforeChange.ToString());
                                break;
                            case "FirstName":
                                original[0].FirstName = item.BeforeChange.ToString();
                                break;
                            case "LastName":
                                original[0].LastName = item.BeforeChange.ToString();
                                break;
                            case "IdentityCard":
                                original[0].IdentityCard = item.BeforeChange.ToString();
                                break;
                            case "Birthdate":
                                original[0].Birthdate = Convert.ToDateTime(item.BeforeChange);
                                break;
                            case "PrimaryPhone":
                                original[0].PrimaryPhone = item.BeforeChange.ToString();
                                break;
                            case "SecondaryPhone":
                                original[0].SecondaryPhone = item.BeforeChange.ToString();
                                break;
                            case "Email":
                                original[0].Email = item.BeforeChange.ToString();
                                break;
                            case "LineID":
                                original[0].LineID = item.BeforeChange.ToString();
                                break;
                            default:
                                break;
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<ServiceResponse<List<GetHistoryCustomerDto>>> GetCustomerHistory(int personId)
        {
            try
            {
                var history = (from o in await _dbContext.Customer_Profile_Transactions.ToListAsync()
                                join s in await _dbContext.Payer_Snapshots.ToListAsync()
                                on o.PersonId equals s.PersonId
                                where (o.PersonId == personId)
                                select new GetHistoryCustomerDto
                                {
                                    PersonId = s.PersonId,
                                    FullName = s.FirstName + " " + s.LastName,
                                    LastUpdate = o.LastUpdated,
                                    FieldData = o.FieldData,
                                    BeforeChanged = o.BeforeChange,
                                    AfterChanged = o.AfterChange,
                                });

                if (history == null)
                {
                    return ResponseResult.Failure<List<GetHistoryCustomerDto>>("Can't Get Data");
                }

                var dto = _mapper.Map<List<GetHistoryCustomerDto>>(history);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetHistoryCustomerDto>>(ex.Message);

            }
        }

        public async Task<ServiceResponse<List<GetCompareDto>>> GetCompareDataOfCustomer(int personId)
        {
            try
            {
                //Get from orginal
                //var original = (from o in await _dbContext.Payer_Snapshots.ToListAsync()
                //                join s in await _dbContext.Customer_Profile_Transactions.ToListAsync()
                //                on o.PersonId equals s.PersonId
                //                where(o.PersonId == personId)
                //                select new GetCompareDto
                //                {
                //                    Caption = "Before",
                //                    PersonId = o.PersonId,
                //                    TitleId = o.TitleId,
                //                    FirstName = o.FirstName,
                //                    LastName = o.LastName,
                //                    IdentityCard = o.IdentityCard,
                //                    Birthdate = o.Birthdate,
                //                    PrimaryPhone = o.PrimaryPhone,
                //                    SecondaryPhone = o.SecondaryPhone,
                //                    Email = o.Email,
                //                    LineID = o.LineID,
                //                });


                var original = (from o in await _dbContext.Payer_Snapshots
                                .Where(x => x.PersonId == personId).ToListAsync()
                                select new GetCompareDto
                                {
                                    Caption = "Before",
                                    PersonId = o.PersonId,
                                    TitleId = o.TitleId,
                                    FirstName = o.FirstName,
                                    LastName = o.LastName,
                                    IdentityCard = o.IdentityCard,
                                    Birthdate = o.Birthdate,
                                    PrimaryPhone = o.PrimaryPhone,
                                    SecondaryPhone = o.SecondaryPhone,
                                    Email = o.Email,
                                    LineID = o.LineID,
                                    ImagePath = "",
                                    ImageReferenceId = "",
                                    DocumentId = "",
                                }).ToList();

                var reload = (from a in await _dbContext.Customer_Profile_Transactions
                              .Where(x => x.PersonId == personId).ToListAsync()
                              select a
                              );

                if (AssignBeforeData(reload, original) == false)
                {
                    return ResponseResult.Failure<List<GetCompareDto>>("Can't Assign Data");
                }

                var newcustomer = (from o in await _dbContext.Customer_NewProfiles
                               .Where(o => o.PersonId == personId)
                               .ToListAsync()
                                   select new GetCompareDto
                                   {
                                       Caption = "After",
                                       PersonId = o.PersonId,
                                       TitleId = o.TitleId,
                                       FirstName = o.FirstName,
                                       LastName = o.LastName,
                                       IdentityCard = o.IdentityCard,
                                       Birthdate = o.Birthdate,
                                       PrimaryPhone = o.PrimaryPhone,
                                       SecondaryPhone = o.SecondaryPhone,
                                       Email = o.Email,
                                       LineID = o.LineID,
                                       ImagePath = o.ImagePath,
                                       ImageReferenceId = o.ImageReferenceId,
                                       DocumentId = o.DocumentId,
                                   }).ToList();

                var customer = original.Union(newcustomer);

                if (customer == null)
                {
                    return ResponseResult.Failure<List<GetCompareDto>>("Can't Compare Data ");
                }


                var dto = _mapper.Map<List<GetCompareDto>>(customer);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetCompareDto>>(ex.Message);

            }
        }
        public async Task<ServiceResponseWithPagination<List<GetEditCustomerDto>>> GetEditedCustomer(GetEditCustomerByFilterDto filter)
        {
            try
            {

                var customerx = (from ps in _dbContext.Payer_Snapshots
                                 join ct in _dbContext.Customer_Profile_Transactions
                                 on ps.PersonId equals ct.PersonId
                                 join hc in _dbContext.Customer_Headers
                                 on ct.PersonId equals hc.PayerPersonId
                                 select new GetEditCustomerDto
                                 {
                                     PersonId = ps.PersonId,
                                     FirstName = ps.FirstName,
                                     LastName = ps.LastName,
                                     FullName = ps.FirstName + " " + ps.LastName,
                                     IdentityCard = ps.IdentityCard,
                                     PrimaryPhone = ps.PrimaryPhone,
                                     IsCustomerReply = hc.IsCustomerReply,
                                     IsSMSSended = hc.IsSMSSended,
                                     IsAgentConfirm = hc.IsAgentConfirm,
                                     OrderingField = filter.OrderingField,
                                     AscendingOrder = filter.AscendingOrder,
                                 }
                          ).AsQueryable();

                var customer = customerx.Distinct().OrderBy(x => x.PersonId).AsQueryable();

                if (customer == null)
                {
                    // 404
                    return ResponseResultWithPagination.Failure<List<GetEditCustomerDto>>("Customer Not Found.");
                }
                else
                {

                    if (filter.IsAgentConfirm)
                    {
                        customer = customer.Where(x => x.IsAgentConfirm == filter.IsAgentConfirm);
                    }

                    if (!string.IsNullOrWhiteSpace(filter.FullName))
                    {
                        customer = customer.Where(x => x.FullName.Contains(filter.FullName));
                    }


                    if (!string.IsNullOrWhiteSpace(filter.IdentityCard))
                    {
                        customer = customer.Where(x => x.IdentityCard == filter.IdentityCard);
                    }

                    if (!string.IsNullOrWhiteSpace(filter.PrimaryPhone))
                    {
                        customer = customer.Where(x => x.PrimaryPhone == filter.PrimaryPhone);
                    }

                    if (!string.IsNullOrWhiteSpace(filter.OrderingField))
                    {
                        try
                        {
                            customer = customer.OrderBy($"{filter.OrderingField} {(filter.AscendingOrder ? "ascending" : "descending")}");
                        }
                        catch
                        {
                            return ResponseResultWithPagination.Failure<List<GetEditCustomerDto>>($"Could not order by field: {filter.OrderingField}");
                        }

                      ;
                    }

                    var paginationResult = await _httpcontext.HttpContext.InsertPaginationParametersInResponse(customer, filter.RecordsPerPage, filter.Page);
                    var dto = await customer.Paginate(filter).ToListAsync();
                    return ResponseResultWithPagination.Success(dto, paginationResult, TEXTSUCCESS);
                }

            }
            catch (Exception ex)
            {
                return ResponseResultWithPagination.Failure<List<GetEditCustomerDto>>(ex.Message);

            }
        }   
        public async Task<ServiceResponse<List<GetMergeDto>>> GetMergeDataOfCustomer(int personId)
        {
            try
            {
                var customer = await _dbContext.Customer_NewProfiles.Where(x => x.PersonId == personId).ToListAsync();


                int[] inq = customer[0].ListMergeFrom.Split(',').Select(Int32.Parse).ToArray();
                int ind = int.Parse(customer[0].ListMergeTo);

                var result = from o in _dbContext.Payer_Snapshots
                             where inq.Contains(o.PersonId)
                             orderby o.PersonId
                             select new GetMergeDto
                             {
                                 Caption = "",
                                 PersonId = o.PersonId,
                                 TitleId = o.TitleId,
                                 FirstName = o.FirstName,
                                 LastName = o.LastName,
                                 IdentityCard = o.IdentityCard,
                                 Birthdate = o.Birthdate,
                                 PrimaryPhone = o.PrimaryPhone,
                                 SecondaryPhone = o.SecondaryPhone,
                                 Email = o.Email,
                                 LineID = o.LineID,
                             };

                var lst = await result.ToListAsync();

                foreach (var item in lst)
                {
                    if(item.PersonId == ind)
                    {
                        item.Caption = "Correct";
                    }
                    else
                    {
                        item.Caption = "InCorrect";
                    }
                }



                var dto = _mapper.Map<List<GetMergeDto>>(lst);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetMergeDto>>(ex.Message);

            }
        }
        public async Task<ServiceResponse<List<GetMergeDto>>> UpdateMergeDataOfCustomer(UpdateMergeDto update)
        {
            try
            {
                var customer = await _dbContext.Customer_NewProfiles.Where(x => x.PersonId == update.personId).ToListAsync();

                foreach (var item in customer)
                {
                    item.ListMergeFrom = update.ListMergFrom;
                    item.ListMergeTo = update.ListMergeTo;
                }

                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<List<GetMergeDto>>(customer);
                return ResponseResult.Success(dto, TEXTSUCCESS);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetMergeDto>>(ex.Message);

            }
        }
    }
}
