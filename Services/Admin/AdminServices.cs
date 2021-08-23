using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmsUpdateCustomer_Api.Data;
using SmsUpdateCustomer_Api.DTOs.Admin;
using SmsUpdateCustomer_Api.DTOs.Customer;
using SmsUpdateCustomer_Api.DTOs.Customer_Profiles;
using SmsUpdateCustomer_Api.Helpers;
using SmsUpdateCustomer_Api.Models;
using SmsUpdateCustomer_Api.Models.Admin;
using SmsUpdateCustomer_Api.Models.Customer_Profiles;
using SmsUpdateCustomer_Api.Validations;
using System;
using System.Collections.Generic;
using System.IO;
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

        //UI 7 : DataTable
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
        //UI 7 : NotUSE
        public async Task<ServiceResponse<GetCompareDto>> GetAdminEditRecord(int personId)
        {
            try
            {
                var original = await _dbContext.AdminApproves.FirstOrDefaultAsync(x => x.PersonId == personId);

                if (original == null)
                {
                    return ResponseResult.Failure<GetCompareDto>("Not Found Customer.");
                }

                var dto = _mapper.Map<GetCompareDto>(original);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetCompareDto>(ex.Message);

            }
        }
        // UI7 : Tab 1 : After Customer Edit Data
        public async Task<ServiceResponse<List<GetCompareDto>>> GetCompareDataOfCustomer(int personId)
        {
            try
            {

                var original = (from o in await _dbContext.Payer_Snapshots
                                .Where(x => x.PersonId == personId).ToListAsync()
                                select new GetCompareDto
                                {
                                    Caption = "Before",
                                    IsPayer = o.PersonId != personId ? false : true,
                                    PersonId = o.PersonId,
                                    Customer_guid = o.Customer_guid,
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
                                    ListMergeFrom = "",
                                    ListMergeTo = "",
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
                                       Customer_guid = o.Customer_guid,
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
                                       ListMergeFrom = o.ListMergeFrom != null ? o.ListMergeFrom : "",
                                       ListMergeTo = o.ListMergeTo != null ? o.ListMergeTo : "",
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
        // UI7 : Tab 1 : After Admin Edit Data
        public async Task<ServiceResponse<List<GetCompareDto>>> GetCompareDataOfCustomerByAdmin(int personId)
        {
            try
            {
                var original = (from bf in await _dbContext.Customer_NewProfiles.ToListAsync()
                                join af in await _dbContext.AdminApproves.ToListAsync()
                                on bf.PersonId equals af.PersonId
                                where bf.PersonId == personId
                                select new { bf, af }).ToList();

                if (original.Count() == 0)
                {
                    return ResponseResult.Failure<List<GetCompareDto>>("Not Found Customer.");
                }

                GetCompareDto bfData = new GetCompareDto
                {
                    Caption = "Before",
                    IsPayer = original[0].bf.PersonId != personId ? false : true,
                    PersonId = original[0].bf.PersonId,
                    Customer_guid = original[0].bf.Customer_guid,
                    TitleId = original[0].bf.TitleId,
                    FirstName = original[0].bf.FirstName,
                    LastName = original[0].bf.LastName,
                    IdentityCard = original[0].bf.IdentityCard,
                    Birthdate = original[0].bf.Birthdate,
                    PrimaryPhone = original[0].bf.PrimaryPhone,
                    SecondaryPhone = original[0].bf.SecondaryPhone,
                    Email = original[0].bf.Email,
                    LineID = original[0].bf.LineID,
                    ImagePath = original[0].bf.ImagePath,
                    ImageReferenceId = original[0].bf.ImageReferenceId,
                    DocumentId = original[0].bf.DocumentId,
                    ListMergeFrom = original[0].bf.ListMergeFrom,
                    ListMergeTo = original[0].bf.ListMergeTo,
                };

                GetCompareDto afData = new GetCompareDto
                {
                    Caption = "After",
                    IsPayer = original[0].af.PersonId != personId ? false : true,
                    PersonId = original[0].af.PersonId,
                    Customer_guid = original[0].af.Customer_guid,
                    TitleId = original[0].af.TitleId,
                    FirstName = original[0].af.FirstName,
                    LastName = original[0].af.LastName,
                    IdentityCard = original[0].af.IdentityCard,
                    Birthdate = original[0].af.Birthdate,
                    PrimaryPhone = original[0].af.PrimaryPhone,
                    SecondaryPhone = original[0].af.SecondaryPhone,
                    Email = original[0].af.Email,
                    LineID = original[0].af.LineID,
                    ImagePath = original[0].af.ImagePath,
                    ImageReferenceId = original[0].af.ImageReferenceId,
                    DocumentId = original[0].af.DocumentId,
                    ListMergeFrom = original[0].af.ListMergeFrom,
                    ListMergeTo = original[0].af.ListMergeTo,
                };

                var reload = (from a in await _dbContext.Customer_Profile_Transactions
                              .Where(x => x.PersonId == personId).ToListAsync()
                              select a
                              );

                if (AssignBeforeData(reload, bfData) == false)
                {
                    return ResponseResult.Failure<List<GetCompareDto>>("Can't Assign Data");
                }

                List<GetCompareDto> retValue = new List<GetCompareDto>();

                retValue.Add(bfData);
                retValue.Add(afData);

                var dto = _mapper.Map<List<GetCompareDto>>(retValue);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetCompareDto>>(ex.Message);

            }
        }
        // UI7 : Tab 1 : For check already record save by Admin
        public async Task<ServiceResponse<int>> GetCountEditRecordByAdmin(int personId)
        {
            try
            {
                var counter = await _dbContext.AdminApproves.Where(x => x.PersonId == personId && x.IsUpdated == false).ToListAsync();

                if (counter.Count() == 0)
                {
                    return ResponseResult.Success(0, TEXTSUCCESS);
                }

                return ResponseResult.Success(counter.Count(), TEXTSUCCESS);
            }
            catch (Exception ex)
            {
                return ResponseResult.Success(0, ex.Message);
            }
        }
        // UI7 : Tab 1 : For check already done by Admin
        public async Task<ServiceResponse<bool>> GetDoneEditRecordByAdmin(int personId)
        {
            try
            {
                var counter = await _dbContext.AdminApproves.Where(x => x.PersonId == personId && x.IsUpdated == true).ToListAsync();

                if (counter.Count() == 0)
                {
                    return ResponseResult.Success(false, TEXTSUCCESS);
                }

                return ResponseResult.Success(true, TEXTSUCCESS);
            }
            catch (Exception ex)
            {
                return ResponseResult.Success(false, ex.Message);
            }
        }
        // UI7 : Tab 1 : Add + Update Temp Record from Admin Edited
        public async Task<ServiceResponse<GetProfileDto>> AddUpdateTempCustomerProfilebyAdmin(string userId, AddProfileDto newProfile)
        {
            try
            {
                //Validate
                (bool, string) retval = ControlValidator.ValidationsforAddCustomerProfile(newProfile);

                if (retval.Item1 == false)
                {
                    return ResponseResult.Failure<GetProfileDto>(retval.Item2);
                }

                //Add transaction by admin
                var customer = await _dbContext.AdminApproves.FirstOrDefaultAsync(x => x.PersonId == newProfile.PersonId);

                bool retAddTransaction = await AddTransactionsByAdmin(customer, newProfile, userId);

                if (retAddTransaction == false)
                {
                    return ResponseResult.Failure<GetProfileDto>("Can't add transaction for admin.");
                }

                #region
                //Customer_NewProfile dataFromCustomerprofile;
                //AddProfileDto bfData;

                //if (customer == null)
                //{
                //    //Get From New Profile for make Default (before Data)
                //    dataFromCustomerprofile = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == newProfile.PersonId);
                //    bfData = new AddProfileDto
                //    {
                //        PersonId = dataFromCustomerprofile.PersonId,
                //        Customer_guid = dataFromCustomerprofile.Customer_guid,
                //        TitleId = dataFromCustomerprofile.TitleId,
                //        FirstName = dataFromCustomerprofile.FirstName,
                //        LastName = dataFromCustomerprofile.LastName,
                //        IdentityCard = dataFromCustomerprofile.IdentityCard,
                //        Birthdate = dataFromCustomerprofile.Birthdate,
                //        PrimaryPhone = dataFromCustomerprofile.PrimaryPhone,
                //        SecondaryPhone = dataFromCustomerprofile.SecondaryPhone,
                //        Email = dataFromCustomerprofile.Email,
                //        LineID = dataFromCustomerprofile.LineID,
                //        ImagePath = dataFromCustomerprofile.ImagePath,
                //        ImageReferenceId = dataFromCustomerprofile.ImageReferenceId,
                //        DocumentId = dataFromCustomerprofile.DocumentId,
                //        EditorId = dataFromCustomerprofile.EditorId,
                //        ListMergeFrom = dataFromCustomerprofile.ListMergeFrom,
                //        ListMergeTo = dataFromCustomerprofile.ListMergeTo,
                //    } ;
                //}
                //else
                //{
                //    bfData = new AddProfileDto
                //    {
                //        PersonId = customer.PersonId,
                //        Customer_guid = customer.Customer_guid,
                //        TitleId = customer.TitleId,
                //        FirstName = customer.FirstName,
                //        LastName = customer.LastName,
                //        IdentityCard = customer.IdentityCard,
                //        Birthdate = customer.Birthdate,
                //        PrimaryPhone = customer.PrimaryPhone,
                //        SecondaryPhone = customer.SecondaryPhone,
                //        Email = customer.Email,
                //        LineID = customer.LineID,
                //        ImagePath = customer.ImagePath,
                //        ImageReferenceId = customer.ImageReferenceId,
                //        DocumentId = customer.DocumentId,
                //        EditorId = customer.EditorId,
                //        ListMergeFrom = customer.ListMergeFrom,
                //        ListMergeTo = customer.ListMergeTo,
                //    };
                //}

                //var transactionData = new AdminApproveTransaction();
                //transactionData.UserId = userId;
                //transactionData.PersonId = newProfile.PersonId;
                //transactionData.BeforeChange = JsonConvert.SerializeObject(bfData);
                //transactionData.AfterChange = JsonConvert.SerializeObject(newProfile);
                //transactionData.CreateDate = Now();
                //transactionData.LastUpdated = Now();

                //await _dbContext.AdminApproveTransactions.AddRangeAsync(transactionData);
                //await _dbContext.SaveChangesAsync();
                #endregion

                GetProfileDto dto = default;
                if (customer != null)
                {
                    //Update
                    customer.PersonId = newProfile.PersonId;
                    customer.Customer_guid = newProfile.Customer_guid;
                    customer.TitleId = newProfile.TitleId;
                    customer.FirstName = newProfile.FirstName;
                    customer.LastName = newProfile.LastName;
                    customer.Birthdate = newProfile.Birthdate;
                    customer.IdentityCard = newProfile.IdentityCard;
                    customer.PrimaryPhone = newProfile.PrimaryPhone;
                    customer.SecondaryPhone = newProfile.SecondaryPhone;
                    customer.Email = newProfile.Email;
                    customer.LineID = newProfile.LineID;
                    customer.ImagePath = newProfile.ImagePath;
                    customer.ImageReferenceId = newProfile.ImageReferenceId;
                    customer.DocumentId = newProfile.DocumentId;
                    customer.EditorId = newProfile.EditorId;
                    customer.ListMergeFrom = newProfile.ListMergeFrom;
                    customer.ListMergeTo = newProfile.ListMergeTo;
                    customer.IsUpdated = false;
                    customer.LastUpdated = Now();

                    await _dbContext.SaveChangesAsync();
                    dto = _mapper.Map<GetProfileDto>(customer);
                }
                else
                {
                    //Add
                    var _profile = new AdminApprove
                    {
                        PersonId = newProfile.PersonId,
                        Customer_guid = newProfile.Customer_guid,
                        TitleId = newProfile.TitleId,
                        FirstName = newProfile.FirstName,
                        LastName = newProfile.LastName,
                        Birthdate = newProfile.Birthdate,
                        IdentityCard = newProfile.IdentityCard,
                        PrimaryPhone = newProfile.PrimaryPhone,
                        SecondaryPhone = newProfile.SecondaryPhone,
                        Email = newProfile.Email,
                        LineID = newProfile.LineID,
                        ImagePath = newProfile.ImagePath,
                        ImageReferenceId = newProfile.ImageReferenceId,
                        DocumentId = newProfile.DocumentId,
                        EditorId = newProfile.EditorId,
                        ListMergeFrom = newProfile.ListMergeFrom,
                        ListMergeTo = newProfile.ListMergeTo,
                        IsUpdated = false,
                        IsCheckMerge = newProfile.ListMergeFrom != newProfile.ListMergeTo ? false : true,
                        LastUpdated = Now(),
                    };

                    _dbContext.AdminApproves.Add(_profile);
                    await _dbContext.SaveChangesAsync();

                    dto = _mapper.Map<GetProfileDto>(_profile);
                }

                return ResponseResult.Success(dto, TEXTSUCCESS);
            }
            catch (Exception ex)
            {

                return ResponseResult.Failure<GetProfileDto>(ex.Message);
            }
        }
        // UI7 : Tab 1 : Add transaction for Admin changed
        private async Task<bool> AddTransactionsByAdmin(AdminApprove customer, AddProfileDto newProfile, string userId)
        {
            try
            {
                //Add transaction by admin
                Customer_NewProfile dataFromCustomerprofile;
                AddProfileDto bfData;

                if (customer == null)
                {
                    //Get From New Profile for make Default (before Data)
                    dataFromCustomerprofile = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == newProfile.PersonId);
                    bfData = new AddProfileDto
                    {
                        PersonId = dataFromCustomerprofile.PersonId,
                        Customer_guid = dataFromCustomerprofile.Customer_guid,
                        TitleId = dataFromCustomerprofile.TitleId,
                        FirstName = dataFromCustomerprofile.FirstName,
                        LastName = dataFromCustomerprofile.LastName,
                        IdentityCard = dataFromCustomerprofile.IdentityCard,
                        Birthdate = dataFromCustomerprofile.Birthdate,
                        PrimaryPhone = dataFromCustomerprofile.PrimaryPhone,
                        SecondaryPhone = dataFromCustomerprofile.SecondaryPhone,
                        Email = dataFromCustomerprofile.Email,
                        LineID = dataFromCustomerprofile.LineID,
                        ImagePath = dataFromCustomerprofile.ImagePath,
                        ImageReferenceId = dataFromCustomerprofile.ImageReferenceId,
                        DocumentId = dataFromCustomerprofile.DocumentId,
                        EditorId = dataFromCustomerprofile.EditorId,
                        ListMergeFrom = dataFromCustomerprofile.ListMergeFrom,
                        ListMergeTo = dataFromCustomerprofile.ListMergeTo,
                    };
                }
                else
                {
                    bfData = new AddProfileDto
                    {
                        PersonId = customer.PersonId,
                        Customer_guid = customer.Customer_guid,
                        TitleId = customer.TitleId,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        IdentityCard = customer.IdentityCard,
                        Birthdate = customer.Birthdate,
                        PrimaryPhone = customer.PrimaryPhone,
                        SecondaryPhone = customer.SecondaryPhone,
                        Email = customer.Email,
                        LineID = customer.LineID,
                        ImagePath = customer.ImagePath,
                        ImageReferenceId = customer.ImageReferenceId,
                        DocumentId = customer.DocumentId,
                        EditorId = customer.EditorId,
                        ListMergeFrom = customer.ListMergeFrom,
                        ListMergeTo = customer.ListMergeTo,
                    };
                }

                var transactionData = new AdminApproveTransaction();
                transactionData.UserId = userId;
                transactionData.PersonId = newProfile.PersonId;
                transactionData.Description = newProfile.ListMergeFrom != bfData.ListMergeFrom ? "แก้ไขการรวมข้อมูล" : "แก้ไขข้อมูลส่วนบุคคล";
                transactionData.BeforeChange = JsonConvert.SerializeObject(bfData);
                transactionData.AfterChange = JsonConvert.SerializeObject(newProfile);
                transactionData.CreateDate = Now();
                transactionData.LastUpdated = Now();

                await _dbContext.AdminApproveTransactions.AddRangeAsync(transactionData);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // UI7 : Tab 2 : DataTable
        public async Task<ServiceResponseWithPagination<List<GetMergeDto>>> GetMergeDataOfCustomerByFilter(GetMergeByFilterDto filter)
        {
            try
            {
                var customer = await _dbContext.Customer_NewProfiles.Where(x => x.PersonId == filter.PersonId).ToListAsync();

                if (customer.Count() == 0)
                {
                    return ResponseResultWithPagination.Failure<List<GetMergeDto>>("Not Found Customer.");
                }

                int[] inq = customer[0].ListMergeFrom.Split(',').Select(Int32.Parse).ToArray();



                if (inq.Count() <= 1)
                {
                    inq[inq.Length - 1] = 0;
                }

                int ind = int.Parse(customer[0].ListMergeTo);

                var result = (from o in _dbContext.Payer_Snapshots
                              where inq.Contains(o.PersonId)
                              orderby o.PersonId
                              select new GetMergeDto
                              {
                                  Caption = o.PersonId != filter.PersonId ? "Merge" : "Master",
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
                              }).AsQueryable();




                if (!string.IsNullOrWhiteSpace(filter.Caption))
                {
                    result = result.Where(x => x.Caption == filter.Caption);
                }

                if (!string.IsNullOrWhiteSpace(filter.OrderingField))
                {
                    try
                    {
                        result = result.OrderBy($"{filter.OrderingField} {(filter.AscendingOrder ? "ascending" : "descending")}");
                    }
                    catch
                    {
                        return ResponseResultWithPagination.Failure<List<GetMergeDto>>($"Could not order by field: {filter.OrderingField}");
                    }

                  ;
                }

                var paginationResult = await _httpcontext.HttpContext.InsertPaginationParametersInResponse(result, filter.RecordsPerPage, filter.Page);
                var dto = await result.Paginate(filter).ToListAsync();
                return ResponseResultWithPagination.Success(dto, paginationResult, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResultWithPagination.Failure<List<GetMergeDto>>(ex.Message);

            }
        }
        // UI7 : Tab 2 : Compare List
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
                                 Caption = o.PersonId != personId ? "Merge" : "Master",
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


                var dto = _mapper.Map<List<GetMergeDto>>(result);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetMergeDto>>(ex.Message);

            }
        }
        // UI7 : Tab 3 : Policy List
        public async Task<ServiceResponseWithPagination<List<GetPolicyCustomerDataDto>>> GetPolicyDataOfCustomer(GetPolicyCustomerFilterDto filter)
        {
            try
            {

                var payer = (from o in _dbContext.Policy_Snapshots.Where(x => x.PayerPersonId == filter.PersonId)
                               select new GetPolicyCustomerDataDto
                               {
                                   CustomerType = "ผู้ชำระเบี้ย",
                                   ProductType = o.ProductTypeDetail,
                                   ApplicationCode = o.ApplicationCode,
                                   StartCoverDate = o.StartCoverDate,
                                   AppStatus = o.AppStatus,
                               }).AsQueryable();

                var customer = (from o in _dbContext.Policy_Snapshots.Where(x => x.CustPersonId == filter.PersonId)
                             select new GetPolicyCustomerDataDto
                             {
                                 CustomerType = "ผู้เอาประกัน",
                                 ProductType = o.ProductTypeDetail,
                                 ApplicationCode = o.ApplicationCode,
                                 StartCoverDate = o.StartCoverDate,
                                 AppStatus = o.AppStatus,
                             }).AsQueryable();

                var result = payer.Union(customer);
                var results = result.OrderBy(x => x.ApplicationCode);

                if (results == null)
                {
                    return ResponseResultWithPagination.Failure<List<GetPolicyCustomerDataDto>>("not found type of customer");
                }

                if (!string.IsNullOrWhiteSpace(filter.OrderingField))
                {
                    try
                    {
                        results = results.OrderBy($"{filter.OrderingField} {(filter.AscendingOrder ? "ascending" : "descending")}");
                    }
                    catch
                    {
                        return ResponseResultWithPagination.Failure<List<GetPolicyCustomerDataDto>>($"Could not order by field: {filter.OrderingField}");
                    }

                  ;
                }

                var paginationResult = await _httpcontext.HttpContext.InsertPaginationParametersInResponse(results, filter.RecordsPerPage, filter.Page);
                var dto = await results.Paginate(filter).ToListAsync();
                return ResponseResultWithPagination.Success(dto, paginationResult, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResultWithPagination.Failure<List<GetPolicyCustomerDataDto>>(ex.Message);

            }
        }
        // UI7 : Tab 2 : Update When data merge changed
        public async Task<ServiceResponse<UpdateMergeDto>> UpdateMergeDataOfCustomer(string userId, UpdateMergeDto update)
        {
            try
            {
                var customer = await _dbContext.AdminApproves.FirstOrDefaultAsync(x => x.PersonId == update.PersonId);
                AddProfileDto addpf;

                // if Admin Approve not Found this record the get data from customer_newprofile
                if (customer == null)
                {
                    //Add transaction
                    var customer_new = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == update.PersonId);

                    addpf = new AddProfileDto {
                        PersonId = customer_new.PersonId,
                        Customer_guid = customer_new.Customer_guid,
                        TitleId = customer_new.TitleId,
                        FirstName = customer_new.FirstName,
                        LastName = customer_new.LastName,
                        IdentityCard = customer_new.IdentityCard,
                        Birthdate = customer_new.Birthdate,
                        PrimaryPhone = customer_new.PrimaryPhone,
                        SecondaryPhone = customer_new.SecondaryPhone,
                        Email = customer_new.Email,
                        LineID = customer_new.LineID,
                        ImagePath = customer_new.ImagePath,
                        ImageReferenceId = customer_new.ImageReferenceId,
                        DocumentId = customer_new.DocumentId,
                        EditorId = customer_new.EditorId,
                        ListMergeFrom = update.ListMergeFrom,
                        ListMergeTo = update.ListMergeTo,
                    };

                    await AddTransactionsByAdmin(customer, addpf, userId);

                    // Add into AdminApprove table
                    var admin_new = new AdminApprove
                    {
                        PersonId = customer_new.PersonId,
                        Customer_guid = customer_new.Customer_guid,
                        TitleId = customer_new.TitleId,
                        FirstName = customer_new.FirstName,
                        LastName = customer_new.LastName,
                        Birthdate = customer_new.Birthdate,
                        IdentityCard = customer_new.IdentityCard,
                        PrimaryPhone = customer_new.PrimaryPhone,
                        SecondaryPhone = customer_new.SecondaryPhone,
                        Email = customer_new.Email,
                        LineID = customer_new.LineID,
                        ImagePath = customer_new.ImagePath,
                        ImageReferenceId = customer_new.ImageReferenceId,
                        DocumentId = customer_new.DocumentId,
                        EditorId = customer_new.EditorId,
                        ListMergeFrom = update.ListMergeFrom,
                        ListMergeTo = update.ListMergeTo,
                        IsUpdated = false,
                        IsCheckMerge = true,
                        LastUpdated = Now(),
                    };

                    _dbContext.AdminApproves.Add(admin_new);
                    await _dbContext.SaveChangesAsync();
                    var dtos = _mapper.Map<UpdateMergeDto>(admin_new);
                    return ResponseResult.Success(dtos, TEXTSUCCESS);
                }

                //Add transaction Log
                addpf = new AddProfileDto
                {
                    PersonId = customer.PersonId,
                    Customer_guid = customer.Customer_guid,
                    TitleId = customer.TitleId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    IdentityCard = customer.IdentityCard,
                    Birthdate = customer.Birthdate,
                    PrimaryPhone = customer.PrimaryPhone,
                    SecondaryPhone = customer.SecondaryPhone,
                    Email = customer.Email,
                    LineID = customer.LineID,
                    ImagePath = customer.ImagePath,
                    ImageReferenceId = customer.ImageReferenceId,
                    DocumentId = customer.DocumentId,
                    EditorId = customer.EditorId,
                    ListMergeFrom = update.ListMergeFrom,
                    ListMergeTo = update.ListMergeTo,
                };

                await AddTransactionsByAdmin(customer, addpf, userId);

                // Update in Admin Approve
                customer.ListMergeFrom = update.ListMergeFrom;
                customer.ListMergeTo = update.ListMergeTo;
                customer.IsCheckMerge = true;
                customer.LastUpdated = Now();
                await _dbContext.SaveChangesAsync();


                var dto = _mapper.Map<UpdateMergeDto>(customer);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<UpdateMergeDto>(ex.Message);

            }
        }
        // UI7 : Tab 4 : DataTable no filter
        public async Task<ServiceResponse<List<GetHistoryCustomerDto>>> GetCustomerHistory(int personId)
        {
            try
            {
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

                //if (history == null || history.Count() == 0)
                //{
                //    return ResponseResult.Failure<List<GetHistoryCustomerDto>>("Not have a history detail.");
                //}

                var dto = _mapper.Map<List<GetHistoryCustomerDto>>(history);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetHistoryCustomerDto>>(ex.Message);

            }
        }
        // UI7 : Tab 5 : DataTable no filter
        public async Task<ServiceResponse<List<GetHistoryAdminDto>>> GetAdminHistory(int personId)
        {
            try
            {
                //var records = await _dbContext.AdminApproveTransactions.Where(x => x.PersonId == personId).ToListAsync();

                var records = (from o in await _dbContext.AdminApproveTransactions.Where(x => x.PersonId == personId).ToListAsync()
                               join a in await _dbContext.AdminApproves.Where(x => x.PersonId == personId).ToListAsync()
                               on o.PersonId equals a.PersonId
                               select new GetHistoryAdminDto
                               {
                                   UserId = o.UserId,
                                   PersonId = o.PersonId,
                                   CustomerName = a.FirstName + " " + a.LastName,
                                   Description = o.Description,
                                   LastUpdated = o.LastUpdated,
                                   BeforeChange = o.BeforeChange,
                                   AfterChange = o.AfterChange,
                               }).ToList();

                if (records == null)
                {
                    return ResponseResult.Failure<List<GetHistoryAdminDto>>("Not have a history detail.");
                }

                var dto = _mapper.Map<List<GetHistoryAdminDto>>(records);
                return ResponseResult.Success(dto, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<List<GetHistoryAdminDto>>(ex.Message);

            }
        }
       
        // UI 7 : After All Done 
        public async Task<ServiceResponse<ConfirmAdminDto>> ConfirmByAdmin(int personId)
        {
            try
            {
                // Update data AddminApprove is LastUpdate and use this record for Customer_Newprofile.
                var AdminProve = await _dbContext.AdminApproves.FirstOrDefaultAsync(x => x.PersonId == personId);

                if (AdminProve == null)
                {
                    // Copy data from customer-newprofile keep record into adminapprove
                    var copydata = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == personId);
                    if (copydata == null)
                    {
                        return ResponseResult.Failure<ConfirmAdminDto>("Can not confirm b'cause not have a customer record.");
                    }

                    var dummydata = new AdminApprove 
                    {
                        PersonId = copydata.PersonId,
                        Customer_guid = copydata.Customer_guid,
                        FirstName = copydata.FirstName,
                        LastName = copydata.LastName,
                        TitleId = copydata.TitleId,
                        IdentityCard = copydata.IdentityCard,
                        Birthdate = copydata.Birthdate,
                        PrimaryPhone = copydata.PrimaryPhone,
                        SecondaryPhone = copydata.SecondaryPhone,
                        Email = copydata.Email,
                        LineID = copydata.LineID,
                        ImagePath = copydata.ImagePath,
                        ImageReferenceId = copydata.ImageReferenceId,
                        DocumentId = copydata.DocumentId,
                        EditorId = copydata.EditorId,
                        ListMergeFrom = copydata.ListMergeFrom,
                        ListMergeTo = copydata.ListMergeTo,
                        IsCheckMerge = true,
                        IsUpdated = true,
                        LastUpdated = Now(),
                    };

                    // add record into adminApprove
                    _dbContext.AdminApproves.Add(dummydata);
                    await _dbContext.SaveChangesAsync();

                    // update customer-newprofile
                    copydata.IsConfirm = true;
                    copydata.LastUpdated = Now();
                    copydata.ConfirmDate = Now();
                    await _dbContext.SaveChangesAsync();


                    // Update confirm in customer_header
                    var Header = await _dbContext.Customer_Headers.FirstOrDefaultAsync(x => x.PayerPersonId == personId);

                    if (Header == null)
                    {
                        return ResponseResult.Failure<ConfirmAdminDto>("Customer_header is invalid");
                    }
                    Header.IsAgentConfirm = true;
                    Header.ConfirmDate = Now();
                    Header.LastUpdated = Now();
                    await _dbContext.SaveChangesAsync();


                    var dtos = _mapper.Map<ConfirmAdminDto>(dummydata);
                    return ResponseResult.Success(dtos, TEXTSUCCESS);

                }


                if (AdminProve.IsCheckMerge == false && AdminProve.ListMergeFrom != AdminProve.ListMergeTo)
                {
                    return ResponseResult.Failure<ConfirmAdminDto>("Please Confirm Merge Data before submit");
                }

                AdminProve.IsCheckMerge = true;
                AdminProve.IsUpdated = true;
                AdminProve.LastUpdated = Now();
                await _dbContext.SaveChangesAsync();

                // Update data from AdminApprove into Customer_Newprofile
                var CustNewProfile = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == personId);

                if (CustNewProfile == null)
                {
                    return ResponseResult.Failure<ConfirmAdminDto>("Customer_newProfile is invalid");
                }

                CustNewProfile.FirstName = AdminProve.FirstName;
                CustNewProfile.LastName = AdminProve.LastName;
                CustNewProfile.TitleId = AdminProve.TitleId;
                CustNewProfile.IdentityCard = AdminProve.IdentityCard;
                CustNewProfile.Birthdate = AdminProve.Birthdate;
                CustNewProfile.PrimaryPhone = AdminProve.PrimaryPhone;
                CustNewProfile.SecondaryPhone = AdminProve.SecondaryPhone;
                CustNewProfile.Email = AdminProve.Email;
                CustNewProfile.LineID = AdminProve.LineID;
                CustNewProfile.ImagePath = AdminProve.ImagePath;
                CustNewProfile.ImageReferenceId = AdminProve.ImageReferenceId;
                CustNewProfile.DocumentId = AdminProve.DocumentId;
                CustNewProfile.EditorId = AdminProve.EditorId;
                CustNewProfile.ListMergeFrom = AdminProve.ListMergeFrom;
                CustNewProfile.ListMergeTo = AdminProve.ListMergeTo;
                CustNewProfile.IsConfirm = true;
                CustNewProfile.ConfirmDate = Now();
                CustNewProfile.LastUpdated = Now();
                await _dbContext.SaveChangesAsync();

                // Update confirm in customer_header
                var updateHeader = await _dbContext.Customer_Headers.FirstOrDefaultAsync(x => x.PayerPersonId == personId);

                if (updateHeader == null)
                {
                    return ResponseResult.Failure<ConfirmAdminDto>("Customer_header is invalid");
                }
                updateHeader.IsAgentConfirm = true;
                updateHeader.ConfirmDate = Now();
                updateHeader.LastUpdated = Now();
                await _dbContext.SaveChangesAsync();

                // Update Payer-snapshot when other payer login will see last update.
                var updatePayer = await _dbContext.Payer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == personId);

                updatePayer.FirstName = AdminProve.FirstName;
                updatePayer.LastName = AdminProve.LastName;
                updatePayer.TitleId = AdminProve.TitleId;
                updatePayer.IdentityCard = AdminProve.IdentityCard;
                updatePayer.Birthdate = AdminProve.Birthdate;
                updatePayer.PrimaryPhone = AdminProve.PrimaryPhone;
                updatePayer.SecondaryPhone = AdminProve.SecondaryPhone;
                updatePayer.Email = AdminProve.Email;
                updatePayer.LineID = AdminProve.LineID;
                updatePayer.LastUpdated = Now();
                await _dbContext.SaveChangesAsync();

                var dto = _mapper.Map<ConfirmAdminDto>(CustNewProfile);
                return ResponseResult.Success(dto, TEXTSUCCESS);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<ConfirmAdminDto>(ex.Message);
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
                                     LastUpdated = cp.LastUpdated.Value,
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
                // Check Payer already into customer newprofile?
                var IsInvalid = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == personId && x.IsUpdated == true);
                if (IsInvalid == null)
                {
                    return ResponseResult.Failure<List<GetCompareDto>>("Not found customer.");
                }

                // Get payerId and customerId. 
                var idDatax = (from ps in await _dbContext.Payer_Snapshots.ToListAsync()
                              join pss in await _dbContext.Policy_Snapshots.ToListAsync()
                              on ps.PersonId equals pss.PayerPersonId
                              where ps.PersonId == personId
                              select pss.CustPersonId);
                
                // Add id into List.
                IList<int> idData = new List<int>();
                if (idDatax.Contains(personId) == false)
                {
                    idData.Add(personId);  
                }

                foreach (var item in idDatax)
                {
                    idData.Add(item.Value);
                }


                // Get personal data from every id in idData.


                var allcustomer = await (from o in _dbContext.Payer_Snapshots
                                  join ex in _dbContext.Customer_NewProfiles
                                  on o.PersonId equals ex.PersonId
                                  into temmGroup
                                  from x in temmGroup.DefaultIfEmpty()
                                  where idData.Contains(o.PersonId)
                                  orderby o.PersonId
                                   /*select new { o, x }*/
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
                                       ImagePath = x.ImagePath != null ? x.ImagePath : "",
                                       ImageReferenceId = x.ImageReferenceId != null ? x.ImageReferenceId : "",
                                       DocumentId = x.DocumentId != null ? x.DocumentId : "",
                                       ListMergeFrom = x.ListMergeFrom != null ? x.ListMergeFrom : "",
                                       ListMergeTo = x.ListMergeTo != null ? x.ListMergeTo : "",
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
                                         ImagePath = o.ImagePath != null ? o.ImagePath : "",
                                         ImageReferenceId = o.ImageReferenceId != null ? o.ImageReferenceId : "",
                                         DocumentId = o.DocumentId != null ? o.DocumentId : "",
                                         ListMergeFrom = o.ListMergeFrom != null ? o.ListMergeFrom : "",
                                         ListMergeTo = o.ListMergeTo != null ? o.ListMergeTo : "",
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

                //Second : Update Table Payer.
                var payertable = await _dbContext.Payer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == update.PersonId);

                if (payertable == null)
                {
                    return ResponseResult.Failure<GetCompareLoginDto>("The Payer not found.");
                }

                payertable.IdentityCard = update.IdentityCard;
                payertable.LastName = update.LastName;
                await _dbContext.SaveChangesAsync();



                //Second Point Five : Check Person as Customer or not?

                var chkCustomer = await _dbContext.Policy_Snapshots.FirstOrDefaultAsync(x => x.CustPersonId == update.PersonId);

                if (chkCustomer != null)
                {
                    //Third : Update Table Customer.
                    var customertable = await _dbContext.Customer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == update.PersonId);

                    if (customertable == null)
                    {
                        return ResponseResult.Failure<GetCompareLoginDto>("The Customer not found.");
                    }

                    customertable.IdentityCard = update.IdentityCard;
                    customertable.LastName = update.LastName;
                    await _dbContext.SaveChangesAsync();
                }


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
