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

        #region Menu N' Som.
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
        private static bool AssignBeforeData(IEnumerable<Customer_Profile_Transaction> reload, GetCompareDto original)
        {
            {
                try
                {
                    foreach (var item in reload)
                    {
                        switch (item.FieldData)
                        {
                            case "TitleId":
                                original.TitleId = int.Parse(item.BeforeChange.ToString());
                                break;
                            case "FirstName":
                                original.FirstName = item.BeforeChange.ToString();
                                break;
                            case "LastName":
                                original.LastName = item.BeforeChange.ToString();
                                break;
                            case "IdentityCard":
                                original.IdentityCard = item.BeforeChange.ToString();
                                break;
                            case "Birthdate":
                                original.Birthdate = Convert.ToDateTime(item.BeforeChange);
                                break;
                            case "PrimaryPhone":
                                original.PrimaryPhone = item.BeforeChange.ToString();
                                break;
                            case "SecondaryPhone":
                                original.SecondaryPhone = item.BeforeChange.ToString();
                                break;
                            case "Email":
                                original.Email = item.BeforeChange.ToString();
                                break;
                            case "LineID":
                                original.LineID = item.BeforeChange.ToString();
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
        public async Task<ServiceResponseWithPagination<List<GetEditCustomerDto>>> GetCustomerbyAdmin(GetEditCustomerByFilterDto filter)
        {
            try
            {

                //var customerx = (from ps in _dbContext.Payer_Snapshots
                //                 join ct in _dbContext.Customer_Profile_Transactions
                //                 on ps.PersonId equals ct.PersonId
                //                 join hc in _dbContext.Customer_Headers
                //                 on ct.PersonId equals hc.PayerPersonId
                //                 select new GetEditCustomerDto
                //                 {
                //                     PersonId = ps.PersonId,
                //                     FirstName = ps.FirstName,
                //                     LastName = ps.LastName,
                //                     FullName = ps.FirstName + " " + ps.LastName,
                //                     IdentityCard = ps.IdentityCard,
                //                     PrimaryPhone = ps.PrimaryPhone,
                //                     IsCustomerReply = hc.IsCustomerReply,
                //                     IsSMSSended = hc.IsSMSSended,
                //                     IsAgentConfirm = hc.IsAgentConfirm,
                //                     OrderingField = filter.OrderingField,
                //                     AscendingOrder = filter.AscendingOrder,
                //                 }
                //          ).AsQueryable();

                //var customer = customerx.Distinct().OrderBy(x => x.PersonId).AsQueryable();

                var customer = (from cp in _dbContext.Customer_NewProfiles
                                where cp.IsUpdated == true
                                select new GetEditCustomerDto
                                {
                                    PersonId = cp.PersonId,
                                    FirstName = cp.FirstName,
                                    LastName = cp.LastName,
                                    FullName = cp.FirstName + " " + cp.LastName,
                                    IdentityCard = cp.IdentityCard,
                                    PrimaryPhone = cp.PrimaryPhone,
                                    LastUpdated = cp.LastUpdated,
                                    IsConfirm = cp.IsConfirm,
                                }).AsQueryable();

                if (customer == null)
                {
                    // 404
                    return ResponseResultWithPagination.Failure<List<GetEditCustomerDto>>("Customer Not Found.");
                }
                else
                {

                    if (filter.IsConfirm == true || filter.IsConfirm == false)
                    {
                        customer = customer.Where(x => x.IsConfirm == filter.IsConfirm);
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
                                    IsPayer = o.PersonId != personId ? false : true,
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

                if (original.Count() == 0)
                {
                    return ResponseResult.Failure<List<GetCompareDto>>("Not Found Customer.");
                }


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
                                       IsPayer = o.PersonId != personId ? false : true,
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
                                       ImagePath = o.ImagePath != null ? o.ImagePath : "", //o.ImagePath,
                                       ImageReferenceId = o.ImageReferenceId != null ? o.ImageReferenceId : "",//o.ImageReferenceId,
                                       DocumentId = o.DocumentId != null ? o.DocumentId : "",//o.DocumentId,
                                   }).ToList();

                var customer = original.Union(newcustomer);

                if (customer == null || customer.Count() > 2 || customer.Count() < 2)
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
        public async Task<ServiceResponse<List<GetMergeDto>>> GetMergeDataOfCustomer(int personId)
        {
            try
            {
                var customer = await _dbContext.Customer_NewProfiles.Where(x => x.PersonId == personId).ToListAsync();

                if (customer.Count() == 0)
                {
                    return ResponseResult.Failure<List<GetMergeDto>>("Not Found Customer.");
                }

                int[] inq = customer[0].ListMergeFrom.Split(',').Select(Int32.Parse).ToArray();

                if (inq.Count() <= 1)
                {
                    return ResponseResult.Failure<List<GetMergeDto>>("No data to merge.");
                }

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
        public async Task<ServiceResponse<List<GetHistoryCustomerDto>>> GetCustomerHistory(int personId)
        {
            try
            {
                //var history = (from o in await _dbContext.Customer_Profile_Transactions.ToListAsync()
                //                join s in await _dbContext.Payer_Snapshots.ToListAsync()
                //                on o.PersonId equals s.PersonId
                //                where (o.PersonId == personId)
                //                select new GetHistoryCustomerDto
                //                {
                //                    PersonId = s.PersonId,
                //                    EditorId = o.EditorId,
                //                    FullName = s.FirstName + " " + s.LastName,
                //                    LastUpdate = o.LastUpdated,
                //                    FieldData = o.FieldData,
                //                    BeforeChanged = o.BeforeChange,
                //                    AfterChanged = o.AfterChange,
                //                });
                var personName = await _dbContext.Payer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == personId);
                var history = (from o in await _dbContext.Customer_Profile_Transactions.ToListAsync()
                               join s in await _dbContext.Payer_Snapshots.ToListAsync()
                               on o.EditorId equals s.PersonId
                               where (o.PersonId == personId)
                               select new GetHistoryCustomerDto
                               {
                                   PersonId = s.PersonId,
                                   PersonFullName = personName.FirstName + " " + personName.LastName,
                                   EditorId = o.EditorId,
                                   EditorFullName = s.FirstName + " " + s.LastName,
                                   LastUpdate = o.LastUpdated,
                                   FieldData = o.FieldData,
                                   BeforeChanged = o.BeforeChange,
                                   AfterChanged = o.AfterChange,
                               });

                if (history == null || history.Count() == 0)
                {
                    return ResponseResult.Failure<List<GetHistoryCustomerDto>>("Not have a history detail.");
                }

                var dto = _mapper.Map<List<GetHistoryCustomerDto>>(history);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetHistoryCustomerDto>>(ex.Message);

            }
        }
        public async Task<ServiceResponse<ConfirmAdminDto>> ConfirmByAdmin(ConfirmAdminDto confirm)
        {
            try
            {

                //if (confirm.PersonData == null && confirm.MergeData == null)
                //{

                //    // Update admin confirm in customer_header
                //    await UpdateAdminConfirm(confirm.PersonData.EditorId);

                //    var dtos = _mapper.Map<ConfirmAdminDto>(confirm);
                //    return ResponseResult.Success(dtos, TEXTSUCCESS);
                //}


                //update personData 
                var snapdata = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == confirm.PersonData.PersonId);
                var editdata = await _dbContext.Customer_NewProfiles.Where(x => x.PersonId == confirm.PersonData.PersonId).ToListAsync();

                if (editdata == null)
                {
                    return ResponseResult.Failure<ConfirmAdminDto>("Can't Update person, Please Check this person in customer newprofile. ");
                }


               foreach (var item in editdata)
                {
                    // update new data into customer newprofile
                    item.FirstName = confirm.PersonData.FirstName;
                    item.LastName = confirm.PersonData.LastName;
                    item.TitleId = confirm.PersonData.TitleId;
                    item.IdentityCard = confirm.PersonData.IdentityCard;
                    item.Birthdate = confirm.PersonData.Birthdate;
                    item.PrimaryPhone = confirm.PersonData.PrimaryPhone;
                    item.SecondaryPhone = confirm.PersonData.SecondaryPhone;
                    item.Email = confirm.PersonData.Email;
                    item.LineID = confirm.PersonData.LineID;
                    item.ImagePath = confirm.PersonData.ImagePath;
                    item.ImageReferenceId = confirm.PersonData.ImageReferenceId;
                    item.DocumentId = confirm.PersonData.DocumentId;
                    item.EditorId = confirm.PersonData.EditorId;
                    item.ListMergeFrom = confirm.MergeData.ListMergFrom;
                    item.ListMergeTo = confirm.MergeData.ListMergeTo;
                    item.IsUpdated = true;
                    item.LastUpdated = DateTime.Now;
                    item.IsConfirm = true;
                    item.ConfirmDate = DateTime.Now;
                }
                await _dbContext.SaveChangesAsync();


                
                // after editdata have a new value the get different field and keep in transaction.
                foreach (var item in editdata)
                {
                    var addExcept = Customer_Profiles.CustomerProfileServices.VerifyData(item, snapdata);
                    if (addExcept.Count > 0)
                    {
                        foreach (var itemexcept in addExcept)
                        {
                            var cpt = new Customer_Profile_Transaction
                            {
                                PersonId = item.PersonId,
                                EditorId = item.EditorId.Value,
                                FieldData = itemexcept.FieldData,
                                BeforeChange = itemexcept.BeforeChange,
                                AfterChange = itemexcept.AfterChange,
                                LastUpdated = Now()
                            };

                            _dbContext.Customer_Profile_Transactions.Add(cpt);
                            await _dbContext.SaveChangesAsync();
                            var dtos = _mapper.Map<GetProfileTransaction>(cpt);
                            if (dtos == null)
                            {
                                return ResponseResult.Failure<ConfirmAdminDto>("Can't Insert Transaction.");
                            }
                        }
                    }
                }

                // Update admin confirm in customer_header
                await UpdateAdminConfirm(confirm.PersonData.EditorId);

                var dto = _mapper.Map<ConfirmAdminDto>(confirm);
                return ResponseResult.Success(dto, TEXTSUCCESS);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<ConfirmAdminDto>(ex.Message);
            }
        }
        private async Task<bool> UpdateAdminConfirm(int editorId)
        {
            try
            {
                // Update admin confirm in customer_header
                var updateAdminConfirm = await _dbContext.Customer_Headers.FirstOrDefaultAsync(x => x.PayerPersonId == editorId);

                if (updateAdminConfirm == null)
                {
                    return false;
                }
                updateAdminConfirm.IsAgentConfirm = true;
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion


        #region Menu P'Jam+.
        public async Task<ServiceResponseWithPagination<List<GetEditCustomerDto>>> GetCustomerbyCallCenter(GetEditCustomerByFilterDto filter)
        {
            try
            {
                #region OldQuery
                //var customerx = (from ps in _dbContext.Payer_Snapshots
                //                 join ct in _dbContext.Customer_Profile_Transactions
                //                 on ps.PersonId equals ct.EditorId
                //                 join hc in _dbContext.Customer_Headers
                //                 on ct.EditorId equals hc.PayerPersonId
                //                 select new GetEditCustomerDto
                //                 {
                //                     PersonId = ps.PersonId,
                //                     FirstName = ps.FirstName,
                //                     LastName = ps.LastName,
                //                     FullName = ps.FirstName + " " + ps.LastName,
                //                     IdentityCard = ps.IdentityCard,
                //                     PrimaryPhone = ps.PrimaryPhone,
                //                     OrderingField = filter.OrderingField,
                //                     AscendingOrder = filter.AscendingOrder,
                //                 }
                //          ).AsQueryable();

                //var customer = customerx.Distinct().OrderBy(x => x.PersonId).AsQueryable();
                #endregion

                #region NewQuery
                var customerx = (from cp in _dbContext.Customer_NewProfiles
                                join ct in _dbContext.Policy_Snapshots
                                on cp.PersonId equals ct.PayerPersonId
                                where cp.IsUpdated == true && cp.PersonId == cp.EditorId
                                select new GetEditCustomerDto
                                {
                                    PersonId = cp.PersonId,
                                    FirstName = cp.FirstName,
                                    LastName = cp.LastName,
                                    FullName = cp.FirstName + " " + cp.LastName,
                                    IdentityCard = cp.IdentityCard,
                                    PrimaryPhone = cp.PrimaryPhone,
                                    LastUpdated = cp.LastUpdated,
                                    IsConfirm = cp.IsConfirm,
                                }).AsQueryable();

                var customer = customerx.Distinct().OrderBy(x => x.PersonId).AsQueryable();
                #endregion

                if (customer == null)
                {
                    // 404
                    return ResponseResultWithPagination.Failure<List<GetEditCustomerDto>>("Customer Not Found.");
                }
                else
                {
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
        public async Task<ServiceResponseWithPagination<List<GetLoginCustomerDto>>> GetLoginbyCallCenter(GetEditCustomerByFilterDto filter)
        {
            try
            {


                var customer = (from cp in _dbContext.Customer_Headers
                                 select new GetLoginCustomerDto
                                 {
                                     PersonId = cp.PayerPersonId,
                                     FirstName = cp.FirstName,
                                     LastName = cp.LastName,
                                     FullName = cp.FirstName + " " + cp.LastName,
                                     IdentityCard = cp.IdentityCard,
                                     PrimaryPhone = cp.PrimaryPhone,
                                     LoginIdentityCard = cp.LoginIdentityCard,
                                     LoginLastName = cp.LoginLastName,
                                     LastUpdated = cp.LastUpdated,
                                 }).AsQueryable();

                if (customer == null)
                {
                    // 404
                    return ResponseResultWithPagination.Failure<List<GetLoginCustomerDto>>("Customer Not Found.");
                }
                else
                {
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
                            return ResponseResultWithPagination.Failure<List<GetLoginCustomerDto>>($"Could not order by field: {filter.OrderingField}");
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
                return ResponseResultWithPagination.Failure<List<GetLoginCustomerDto>>(ex.Message);

            }
        }
        public async Task<ServiceResponse<List<GetCompareDto>>> GetCompareDataAllCustomer(int personId)
        {
            try
            {

                var IsInvalid = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == personId && x.IsUpdated == true);
                if (IsInvalid == null)
                {
                    return ResponseResult.Failure<List<GetCompareDto>>("Not found customer.");
                }

                // get payerId and customerId 
                var idDatax = (from ps in await _dbContext.Payer_Snapshots.ToListAsync()
                              join pss in await _dbContext.Policy_Snapshots.ToListAsync()
                              on ps.PersonId equals pss.PayerPersonId
                              where ps.PersonId == personId
                              select pss.CustPersonId);
                
                IList<int> idData = new List<int>();
                if (idDatax.Contains(personId) == false)
                {
                    idData.Add(personId);  
                }

                foreach (var item in idDatax)
                {
                    idData.Add(item.Value);
                }


                // get personal data from every id in idData.
                var allcustomer = await (from o in _dbContext.Payer_Snapshots
                                         where idData.Contains(o.PersonId)
                                         orderby o.PersonId
                                         select new GetCompareDto
                                         {
                                             Caption = "Before",
                                             IsPayer = o.PersonId != personId ? false : true,
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
                                         }).ToListAsync();

                // if not found a payer then exit.
                if (allcustomer.Count() == 0)
                {
                    return ResponseResult.Failure<List<GetCompareDto>>("Not Found Customer.");
                }

                List<GetCompareDto> newList = new List<GetCompareDto>();

                // Loop one by one.
                foreach (var item in allcustomer)
                {
                    var diffield = (from a in await _dbContext.Customer_Profile_Transactions
                                    .Where(x => x.PersonId == item.PersonId).ToListAsync()
                                    select a
                                 );

                    var beforeData = allcustomer.Where(x => x.PersonId == item.PersonId).ToList();
                    var afterData = (from o in allcustomer.Where(x => x.PersonId == item.PersonId)
                                     select new GetCompareDto
                                     {
                                         Caption = "After",
                                         IsPayer = o.PersonId != personId ? false : true,
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


                    // update field before change into allcustomer one by one field by field
                    if (AssignBeforeData(diffield, beforeData) == false)
                    {
                        return ResponseResult.Failure<List<GetCompareDto>>("Can't update data");
                    }

                    var customers = beforeData.Union(afterData);
                    newList.AddRange(customers);
                }


                var dto = _mapper.Map<List<GetCompareDto>>(newList);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetCompareDto>>(ex.Message);

            }
        }
        public async Task<ServiceResponse<GetCompareLoginDto>> GetLoginOfCustomer(int personId)
        {
            try
            {

                var IsCustomer = await _dbContext.Customer_Headers.FirstOrDefaultAsync(x => x.PayerPersonId == personId);

                if (IsCustomer == null)
                {
                    return ResponseResult.Failure<GetCompareLoginDto>("The Customer not found.");
                }

                GetCompareLoginDto LoginData = new GetCompareLoginDto
                {
                    PersonId = IsCustomer.PayerPersonId,
                    IdentityCard = IsCustomer.LoginIdentityCard,
                    LastName = IsCustomer.LoginLastName,
                };

                var dto = _mapper.Map<GetCompareLoginDto>(LoginData);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetCompareLoginDto>(ex.Message);

            }
        }
        public async Task<ServiceResponse<GetCompareLoginDto>> UpdateLoginOfCustomer(GetCompareLoginDto update)
        {
            try
            {

                var IsCustomer = await _dbContext.Customer_Headers.FirstOrDefaultAsync(x => x.PayerPersonId == update.PersonId);

                if (IsCustomer == null)
                {
                    return ResponseResult.Failure<GetCompareLoginDto>("The Customer not found.");
                }

                //First : Update Table login.
                IsCustomer.LoginIdentityCard = update.IdentityCard;
                IsCustomer.LoginLastName = update.LastName;
                await _dbContext.SaveChangesAsync();

                //second : Update Table Payer.
                var payertable = await _dbContext.Payer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == update.PersonId);

                if (payertable == null)
                {
                    return ResponseResult.Failure<GetCompareLoginDto>("The Payer not found.");
                }

                payertable.IdentityCard = update.IdentityCard;
                payertable.LastName = update.LastName;
                await _dbContext.SaveChangesAsync();


                //third : Update Table Customer.
                var customertable = await _dbContext.Customer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == update.PersonId);

                if (customertable == null)
                {
                    return ResponseResult.Failure<GetCompareLoginDto>("The Customer not found.");
                }

                customertable.IdentityCard = update.IdentityCard;
                customertable.LastName = update.LastName;
                await _dbContext.SaveChangesAsync();


                // Last thing : return result.
                var dto = _mapper.Map<GetCompareLoginDto>(update);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetCompareLoginDto>(ex.Message);

            }
        }
        
        #endregion
    }
}
