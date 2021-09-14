using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Services.Admin
{
    public class AdminServices : ServiceBase, IAdminServices
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<IAdminServices> _log;
        private readonly IHttpContextAccessor _httpcontext;
        private readonly IConfiguration _configuraton;

        private string TEXTSUCCESS = "Success";
        private string TEXTCUSTOMERNOTFOUND = "ไม่พบลูกค้า";

        public AdminServices(AppDBContext dBContext, IMapper mapper, ILogger<IAdminServices> log, IHttpContextAccessor httpcontext, IConfiguration configuraton) : base(dBContext, mapper, httpcontext)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _log = log;
            _httpcontext = httpcontext;
            _configuraton = configuraton;
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
                                original[0].FirstName = item.BeforeChange != null ? item.BeforeChange : "";
                                break;
                            case "LastName":
                                original[0].LastName = item.BeforeChange != null ? item.BeforeChange : "";
                                break;
                            case "IdentityCard":
                                original[0].IdentityCard = item.BeforeChange != null ? item.BeforeChange : "";
                                break;
                            case "Birthdate":
                                original[0].Birthdate = Convert.ToDateTime(item.BeforeChange);
                                break;
                            case "PrimaryPhone":
                                original[0].PrimaryPhone = item.BeforeChange != null ? item.BeforeChange : "";
                                break;
                            case "SecondaryPhone":
                                original[0].SecondaryPhone = item.BeforeChange != null ? item.BeforeChange : "";
                                break;
                            case "Email":
                                original[0].Email = item.BeforeChange != null ? item.BeforeChange : "";
                                break;
                            case "LineID":
                                original[0].LineID = item.BeforeChange != null ? item.BeforeChange : "";
                                
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
                                original.FirstName = item.BeforeChange != null ? item.BeforeChange : "";
                                break;
                            case "LastName":
                                original.LastName = item.BeforeChange != null ? item.BeforeChange : "";
                                break;
                            case "IdentityCard":
                                original.IdentityCard = item.BeforeChange != null ? item.BeforeChange : "";
                                break;
                            case "Birthdate":
                                original.Birthdate = Convert.ToDateTime(item.BeforeChange);
                                break;
                            case "PrimaryPhone":
                                original.PrimaryPhone = item.BeforeChange != null ? item.BeforeChange : "";
                                break;
                            case "SecondaryPhone":
                                original.SecondaryPhone = item.BeforeChange != null ? item.BeforeChange : "";
                                break;
                            case "Email":
                                original.Email = item.BeforeChange != null ? item.BeforeChange : "";
                                break;
                            case "LineID":
                                original.LineID = item.BeforeChange != null ? item.BeforeChange : "";
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
        private static bool AssignAfterData(IEnumerable<Customer_Profile_Transaction> reload, List<GetCompareDto> original)
        {
            {
                try
                {
                    foreach (var item in reload)
                    {
                        switch (item.FieldData)
                        {
                            case "TitleId":
                                original[0].TitleId = int.Parse(item.AfterChange.ToString());
                                break;
                            case "FirstName":
                                original[0].FirstName = item.AfterChange != null ? item.AfterChange : "";
                                break;
                            case "LastName":
                                original[0].LastName = item.AfterChange != null ? item.AfterChange : "";
                                break;
                            case "IdentityCard":
                                original[0].IdentityCard = item.AfterChange != null ? item.AfterChange : "";
                                break;
                            case "Birthdate":
                                original[0].Birthdate = Convert.ToDateTime(item.AfterChange);
                                break;
                            case "PrimaryPhone":
                                original[0].PrimaryPhone = item.AfterChange != null ? item.AfterChange : "";
                                break;
                            case "SecondaryPhone":
                                original[0].SecondaryPhone = item.AfterChange != null ? item.AfterChange : "";
                                break;
                            case "Email":
                                original[0].Email = item.AfterChange != null ? item.AfterChange : "";
                                break;
                            case "LineID":
                                original[0].LineID = item.AfterChange != null ? item.AfterChange : "";
                                break;
                            case "ImagePath":
                                original[0].ImagePath = item.AfterChange != null ? item.AfterChange : "";
                                break;
                            case "ImageReferenceId":
                                original[0].ImageReferenceId = item.AfterChange != null ? item.AfterChange : "";
                                break;
                            case "DocumentId":
                                original[0].DocumentId = item.AfterChange != null ? item.AfterChange : "";
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
                #region "OldCode"
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
                #endregion

                var customer = (from c in _dbContext.Customer_NewProfiles.AsNoTracking()
                                where c.IsUpdated == true && c.IsMerged != true
                                group c by new { c.PersonId, c.FirstName, c.LastName, c.IdentityCard, c.PrimaryPhone, c.LastUpdated, c.IsConfirm }
                                into cp
                                select new GetEditCustomerDto
                                {
                                    PersonId = cp.Key.PersonId,
                                    FirstName = cp.Key.FirstName,
                                    LastName = cp.Key.LastName,
                                    FullName = cp.Key.FirstName + " " + cp.Key.LastName,
                                    IdentityCard = cp.Key.IdentityCard,
                                    PrimaryPhone = cp.Key.PrimaryPhone,
                                    LastUpdated = cp.Key.LastUpdated,
                                    IsConfirm = cp.Key.IsConfirm,
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
                    return ResponseResult.Failure<GetCompareDto>(TEXTCUSTOMERNOTFOUND);
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
                    return ResponseResult.Failure<List<GetCompareDto>>(TEXTCUSTOMERNOTFOUND);
                }


                var reload = (from a in await _dbContext.Customer_Profile_Transactions
                              .Where(x => x.PersonId == personId).ToListAsync()
                              select a);

                if (AssignBeforeData(reload, original) == false)
                {
                    return ResponseResult.Failure<List<GetCompareDto>>("ไม่สามารถแสดงข้อมูลก่อนหน้าการเปลี่ยนแปลงได้.");
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
                                       ImagePath = "",
                                       //ImagePath = o.DocumentId != "" ? GetImagePathFromSSSDoc(o.DocumentId) : "",
                                       ImageReferenceId = o.ImageReferenceId != null ? o.ImageReferenceId : "", //o.ImageReferenceId,
                                       DocumentId = o.DocumentId != null ? o.DocumentId : "", //o.DocumentId,
                                       ListMergeFrom = o.ListMergeFrom != null ? o.ListMergeFrom : "",
                                       ListMergeTo = o.ListMergeTo != null ? o.ListMergeTo : "",
                                   }).ToList();

                newcustomer[0].ImagePath = newcustomer[0].DocumentId != "" ? await GetImagePathFromSSSDoc(newcustomer[0].DocumentId):"";

                var customer = original.Union(newcustomer);

                if (customer == null || customer.Count() > 2 || customer.Count() < 2)
                {
                    return ResponseResult.Failure<List<GetCompareDto>>("ไม่สามารถเปรียบเทียบข้อมูลได้.");
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
                var original = await (from bf in _dbContext.Customer_NewProfiles.AsNoTracking()
                                      join af in _dbContext.AdminApproves.AsNoTracking()
                                      on bf.PersonId equals af.PersonId
                                      where bf.PersonId == personId
                                      select new { bf, af }).ToListAsync();

                if (original.Count() == 0)
                {
                    return ResponseResult.Failure<List<GetCompareDto>>(TEXTCUSTOMERNOTFOUND);
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
                    //ImagePath = original[0].af.ImagePath,
                    ImagePath = original[0].af.DocumentId != "" ? await GetImagePathFromSSSDoc(original[0].af.DocumentId) : "",
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
                    return ResponseResult.Failure<List<GetCompareDto>>("ไม่สามารถแสดงข้อมูลก่อนหน้าการเปลี่ยนแปลงได้.");
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
                var userdata = userId.Split(" ",StringSplitOptions.RemoveEmptyEntries);

                //Validate
                (bool, string) retval = ControlValidator.ValidationsforAddCustomerProfile(newProfile);

                if (retval.Item1 == false)
                {
                    return ResponseResult.Failure<GetProfileDto>(retval.Item2);
                }

                //Add transaction by admin
                var customer = await _dbContext.AdminApproves.FirstOrDefaultAsync(x => x.PersonId == newProfile.PersonId);

                bool retAddTransaction = await AddTransactionsPersonByAdmin(customer, newProfile, userId);

                if (retAddTransaction == false)
                {
                    return ResponseResult.Failure<GetProfileDto>("ไม่สามารถบันทึกรายการข้อมูลสำหรับแอดมินได้.");
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
                    customer.FirstName = newProfile.FirstName.Trim();
                    customer.LastName = newProfile.LastName.Trim();
                    customer.Birthdate = newProfile.Birthdate;
                    customer.IdentityCard = newProfile.IdentityCard.Trim();
                    customer.PrimaryPhone = newProfile.PrimaryPhone.Trim();
                    customer.SecondaryPhone = newProfile.SecondaryPhone.Trim();
                    customer.Email = newProfile.Email.Trim();
                    customer.LineID = newProfile.LineID.Trim();
                    customer.ImagePath = string.Empty; //newProfile.ImagePath; //Error b'cause return path from S3 Over length 255
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
                        FirstName = newProfile.FirstName.Trim(),
                        LastName = newProfile.LastName.Trim(),
                        Birthdate = newProfile.Birthdate,
                        IdentityCard = newProfile.IdentityCard,
                        PrimaryPhone = newProfile.PrimaryPhone.Trim(),
                        SecondaryPhone = newProfile.SecondaryPhone.Trim(),
                        Email = newProfile.Email.Trim(),
                        LineID = newProfile.LineID.Trim(),
                        ImagePath = string.Empty, //newProfile.ImagePath; //Error b'cause return path from S3 Over length 255 
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
        private async Task<bool> AddTransactionsMergeByAdmin(AdminApprove customer, AddProfileDto newProfile, string userId)
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
                transactionData.Description = "แก้ไขการรวมข้อมูล";
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
        private async Task<bool> AddTransactionsConfirmByAdmin(AdminApprove beforedata, AdminApprove afterdata, string userId)
        {
            try
            {
                //Add transaction by admin

                var transactionData = new AdminApproveTransaction();
                transactionData.UserId = userId;
                transactionData.PersonId = afterdata.PersonId;
                transactionData.Description = "ยืนยันข้อมูลโดยแอดมิน";
                transactionData.BeforeChange = JsonConvert.SerializeObject(beforedata);
                transactionData.AfterChange = JsonConvert.SerializeObject(afterdata);
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
        private async Task<bool> AddTransactionsPersonByAdmin(AdminApprove customer, AddProfileDto newProfile, string userId)
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
                transactionData.Description = "แก้ไขข้อมูลส่วนบุคคล";
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
                    return ResponseResultWithPagination.Failure<List<GetMergeDto>>(TEXTCUSTOMERNOTFOUND);
                }

                int[] inq = customer[0].ListMergeFrom.Split(',').Select(Int32.Parse).ToArray();
                int ind = int.Parse(customer[0].ListMergeTo);

                if (inq.Count() <= 1)
                {
                    inq[inq.Length - 1] = 0;
                }

                var newvalue = await _dbContext.AdminApproves.FirstOrDefaultAsync(x => x.PersonId == filter.PersonId);

                IQueryable<GetMergeDto> result;

                if (newvalue != null)
                {
                    result = (from o in _dbContext.Payer_Snapshots
                                  where inq.Contains(o.PersonId)
                                  orderby o.PersonId
                                  select new GetMergeDto
                                  {
                                      Caption = o.PersonId != filter.PersonId ? "Merge" : "Master",
                                      PersonId = o.PersonId,
                                      TitleId = o.TitleId,
                                      FirstName = o.PersonId == filter.PersonId ? newvalue.FirstName != o.FirstName ? newvalue.FirstName : o.FirstName : o.FirstName,
                                      LastName = o.PersonId == filter.PersonId ? newvalue.LastName != o.LastName ? newvalue.LastName : o.LastName : o.LastName,
                                      IdentityCard = o.PersonId == filter.PersonId ? newvalue.IdentityCard != o.IdentityCard ? newvalue.IdentityCard : o.IdentityCard : o.IdentityCard,
                                      Birthdate = o.PersonId == filter.PersonId ? newvalue.Birthdate != o.Birthdate ? newvalue.Birthdate : o.Birthdate : o.Birthdate,
                                      PrimaryPhone = o.PersonId == filter.PersonId ? newvalue.PrimaryPhone != o.PrimaryPhone ? newvalue.PrimaryPhone : o.PrimaryPhone : o.PrimaryPhone,
                                      SecondaryPhone = o.PersonId == filter.PersonId ? newvalue.SecondaryPhone != o.SecondaryPhone ? newvalue.SecondaryPhone : o.SecondaryPhone : o.SecondaryPhone,
                                      Email = o.PersonId == filter.PersonId ? newvalue.Email != o.Email ? newvalue.Email : o.Email : o.Email,
                                      LineID = o.PersonId == filter.PersonId ? newvalue.LineID != o.LineID ? newvalue.LineID : o.LineID : o.LineID,
                                  }).AsQueryable();
                }
                else
                {
                    result = (from o in _dbContext.Payer_Snapshots
                              where inq.Contains(o.PersonId)
                              orderby o.PersonId
                              select new GetMergeDto
                              {
                                  Caption = o.PersonId != filter.PersonId ? "Merge" : "Master",
                                  PersonId = o.PersonId,
                                  TitleId = o.TitleId,
                                  FirstName =  o.FirstName,
                                  LastName =  o.LastName,
                                  IdentityCard =  o.IdentityCard,
                                  Birthdate =  o.Birthdate,
                                  PrimaryPhone =  o.PrimaryPhone,
                                  SecondaryPhone =  o.SecondaryPhone,
                                  Email =  o.Email,
                                  LineID =  o.LineID,
                              }).AsQueryable();
                }


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
                    return ResponseResult.Failure<List<GetMergeDto>>(TEXTCUSTOMERNOTFOUND);
                }

                int[] inq = customer[0].ListMergeFrom.Split(',').Select(Int32.Parse).ToArray();

                if (inq.Count() <= 1)
                {
                    return ResponseResult.Failure<List<GetMergeDto>>("ไม่มีข้อมูลสำหรับการรวมข้อมูลลุกค้า.");
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
                    return ResponseResultWithPagination.Failure<List<GetPolicyCustomerDataDto>>("ไม่พบข้อมุลลูกค้า.");
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

                    await AddTransactionsMergeByAdmin(customer, addpf, userId);

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

                await AddTransactionsMergeByAdmin(customer, addpf, userId);

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
        // UI7 : Tab 4 : DataTable filter
        public async Task<ServiceResponseWithPagination<List<GetHistoryCustomerDto>>> GetCustomerHistory(GetHistoryCustomerFilterDto filter)
        {
            try
            {

                //personid 67499 editorid 67500

                //var history = await _dbContext.Customer_Profile_Transactions.Where(x => x.PersonId == filter.personId).ToListAsync();
                //var editorName = await _dbContext.Payer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == history[0].EditorId);
                //var personName = await _dbContext.Payer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == filter.personId);

                //GetHistoryCustomerDto retHistory;
                //List<GetHistoryCustomerDto> listHistory = new List<GetHistoryCustomerDto>();

                //foreach (var item in history)
                //{
                //    retHistory = new GetHistoryCustomerDto
                //    {
                //        PersonId = item.PersonId,
                //        PersonFullName = personName.FirstName + " " + personName.LastName,
                //        EditorId = item.EditorId,
                //        EditorFullName = editorName.FirstName + " " + editorName.LastName,
                //        LastUpdate = item.LastUpdated,
                //        FieldData = item.FieldData,
                //        BeforeChanged = item.BeforeChange,
                //        AfterChanged = item.AfterChange,
                //    };
                //    listHistory.Add(retHistory);
                //}

                //var dto = _mapper.Map<List<GetHistoryCustomerDto>>(listHistory);
                //return ResponseResult.Success(dto, TEXTSUCCESS);

                var history = (from o in _dbContext.Customer_Profile_Transactions.AsNoTracking()
                               join a in _dbContext.Payer_Snapshots.AsNoTracking()
                               on o.PersonId equals a.PersonId
                               join b in _dbContext.Payer_Snapshots.AsNoTracking()
                               on o.EditorId equals b.PersonId
                               where o.PersonId == filter.personId
                               select new GetHistoryCustomerDto
                               {
                                   PersonId = o.PersonId,
                                   PersonFullName = a.FirstName + " " + a.LastName,
                                   EditorId = o.EditorId,
                                   EditorFullName = b.FirstName + " " + b.LastName,
                                   LastUpdate = o.LastUpdated,
                                   FieldData = o.FieldData,
                                   BeforeChanged = o.BeforeChange,
                                   AfterChanged = o.AfterChange,
                               }).AsQueryable();

                var rawHistory = history.AsQueryable();

                if (!string.IsNullOrWhiteSpace(filter.OrderingField))
                {
                    try
                    {
                        rawHistory = rawHistory.OrderBy($"{filter.OrderingField} {(filter.AscendingOrder ? "ascending" : "descending")}");
                    }
                    catch
                    {
                        return ResponseResultWithPagination.Failure<List<GetHistoryCustomerDto>>($"Could not order by field: {filter.OrderingField}");
                    }
                }

                var paginationResult = await _httpcontext.HttpContext.InsertPaginationParametersInResponse(rawHistory, filter.RecordsPerPage, filter.Page);
                var dto = await rawHistory.Paginate(filter).ToListAsync();
                return ResponseResultWithPagination.Success(dto, paginationResult, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResultWithPagination.Failure<List<GetHistoryCustomerDto>>(ex.Message);

            }
        }
        // UI7 : Tab 5 : DataTable filter
        public async Task<ServiceResponseWithPagination<List<GetHistoryAdminDto>>> GetAdminHistory(GetHistoryAdminFilterDto filter)
        {
            try
            {
                var records = (from o in _dbContext.AdminApproveTransactions.AsNoTracking()
                               join a in _dbContext.AdminApproves.AsNoTracking()
                               on o.PersonId equals a.PersonId
                               where o.PersonId == filter.personId
                               && a.PersonId == filter.personId
                               select new GetHistoryAdminDto
                               {
                                   UserId = o.UserId,
                                   PersonId = o.PersonId,
                                   CustomerName = a.FirstName + " " + a.LastName,
                                   Description = o.Description,
                                   LastUpdated = o.LastUpdated,
                                   BeforeChange = o.BeforeChange,
                                   AfterChange = o.AfterChange,
                               }).AsQueryable();//.ToList();



                if (records == null)
                {
                    return ResponseResultWithPagination.Failure<List<GetHistoryAdminDto>>("ไม่พบรายละเอียดการบันทึกข้อมูลย้อยหลัง.");
                }

                //var dto = _mapper.Map<List<GetHistoryAdminDto>>(records);
                //return ResponseResult.Success(dto, TEXTSUCCESS);

                var rawAdminHistory = records.AsQueryable();


                if (string.IsNullOrWhiteSpace(filter.OrderingField))
                {
                    filter.OrderingField = "LastUpdated";
                    filter.AscendingOrder = false;

                }



                if (!string.IsNullOrWhiteSpace(filter.OrderingField))
                {
                    try
                    {
                        rawAdminHistory = rawAdminHistory.OrderBy($"{filter.OrderingField} {(filter.AscendingOrder ? "ascending" : "descending")}");
                    }
                    catch
                    {
                        return ResponseResultWithPagination.Failure<List<GetHistoryAdminDto>>($"Could not order by field: {filter.OrderingField}");
                    }
                }
                

                var paginationResult = await _httpcontext.HttpContext.InsertPaginationParametersInResponse(rawAdminHistory, filter.RecordsPerPage, filter.Page);
                var dto = await rawAdminHistory.Paginate(filter).ToListAsync();
                return ResponseResultWithPagination.Success(dto, paginationResult, TEXTSUCCESS);

            }
            catch (Exception ex)
            {
                return ResponseResultWithPagination.Failure<List<GetHistoryAdminDto>>(ex.Message);

            }
        }

        // UI 7 : After All Done Old Version..
        #region Old Version..
        //public async Task<ServiceResponse<ConfirmAdminDto>> ConfirmByAdmin(ConfirmDataAdminDto confirm, string username)
        //{

        //    try
        //    {
        //        // Update data AddminApprove is LastUpdate and use this record for Customer_Newprofile.
        //        var AdminProve = await _dbContext.AdminApproves.FirstOrDefaultAsync(x => x.PersonId == confirm.PersonId);

        //        if (AdminProve == null)
        //        {
        //            // Copy data from customer-newprofile keep record into adminapprove
        //            var copydata = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == confirm.PersonId);
        //            if (copydata == null)
        //            {
        //                return ResponseResult.Failure<ConfirmAdminDto>("Can not confirm b'cause not have a customer record.");
        //            }

        //            var dummydata = new AdminApprove 
        //            {
        //                PersonId = copydata.PersonId,
        //                Customer_guid = copydata.Customer_guid,
        //                FirstName = copydata.FirstName,
        //                LastName = copydata.LastName,
        //                TitleId = copydata.TitleId,
        //                IdentityCard = copydata.IdentityCard,
        //                Birthdate = copydata.Birthdate,
        //                PrimaryPhone = copydata.PrimaryPhone,
        //                SecondaryPhone = copydata.SecondaryPhone,
        //                Email = copydata.Email,
        //                LineID = copydata.LineID,
        //                ImagePath = copydata.ImagePath,
        //                ImageReferenceId = copydata.ImageReferenceId,
        //                DocumentId = copydata.DocumentId,
        //                EditorId = copydata.EditorId,
        //                ListMergeFrom = copydata.ListMergeFrom,
        //                ListMergeTo = copydata.ListMergeTo,
        //                IsCheckMerge = true,
        //                IsUpdated = true,
        //                LastUpdated = Now(),
        //            };

        //            // Keep Log admin confirm

        //            await AddTransactionsConfirmByAdmin(dummydata, dummydata, username);

        //            // Send data to DataCenter API
        //            var ret_dummy = await SendRecordToDataCenter(dummydata, confirm.UserId);

        //            if (ret_dummy.Item1 == false)
        //            {
        //                //return ResponseResult.Failure<ConfirmAdminDto>("ไม่สามารถส่งข้อมูลไปที่ DataCenter ได้.");
        //                return ResponseResult.Failure<ConfirmAdminDto>(ret_dummy.Item2);
        //            }

        //            // add record into adminApprove
        //            _dbContext.AdminApproves.Add(dummydata);
        //            await _dbContext.SaveChangesAsync();

        //            // update customer-newprofile
        //            copydata.IsConfirm = true;
        //            copydata.LastUpdated = Now();
        //            copydata.ConfirmDate = Now();
        //            await _dbContext.SaveChangesAsync();


        //            // Update confirm in customer_header
        //            var Header = await _dbContext.Customer_Headers.FirstOrDefaultAsync(x => x.PayerPersonId == confirm.PersonId);

        //            if (Header != null)
        //            {
        //                Header.IsAgentConfirm = true;
        //                Header.ConfirmDate = Now();
        //                Header.LastUpdated = Now();
        //                await _dbContext.SaveChangesAsync();
        //            }

        //            //update follower and mark merge person is false
        //            IList<int> idDatas = new List<int>();
        //            var lstPersonIds = (dummydata.ListMergeFrom.Split(",", StringSplitOptions.RemoveEmptyEntries));

        //            foreach (var item in lstPersonIds)
        //            {
        //                idDatas.Add(int.Parse(item));
        //            }

        //            var listFollowers = await (from o in _dbContext.Customer_FollowUps
        //                                       where idDatas.Contains(o.PersonId)
        //                                       orderby o.PersonId
        //                                       select o).ToListAsync();


        //            for (int i = 0; i < listFollowers.Count; i++)
        //            {
        //                if (listFollowers[i].PersonId != confirm.PersonId)
        //                {
        //                    listFollowers[i].AdminConfirm = true;
        //                    listFollowers[i].Result = false;
        //                    listFollowers[i].LastUpdated = Now();
        //                    await _dbContext.SaveChangesAsync();
        //                }
        //                else
        //                {
        //                    listFollowers[i].AdminConfirm = true;
        //                    listFollowers[i].Result = true;
        //                    listFollowers[i].LastUpdated = Now();
        //                    await _dbContext.SaveChangesAsync();
        //                }
        //            }

        //            // Set flag record merged
        //            var updateRecords = await (from o in _dbContext.Customer_NewProfiles
        //                                       where idDatas.Contains(o.PersonId)
        //                                       select o).ToListAsync();

        //            foreach (var item in updateRecords)
        //            {
        //                if (item.PersonId != confirm.PersonId) 
        //                {
        //                    item.IsMerged = true;
        //                    item.IsMergedBy = confirm.PersonId.ToString();
        //                }
        //                else
        //                {
        //                    item.IsMerged = false;
        //                    item.IsMergedBy = confirm.PersonId.ToString();
        //                }

        //            }

        //            await _dbContext.SaveChangesAsync();


        //            var dtos = _mapper.Map<ConfirmAdminDto>(dummydata);
        //            return ResponseResult.Success(dtos, TEXTSUCCESS);

        //        }


        //        if (AdminProve.IsCheckMerge == false && AdminProve.ListMergeFrom != AdminProve.ListMergeTo)
        //        {
        //            return ResponseResult.Failure<ConfirmAdminDto>("กรุณายืนยันการรวมข้อมูลลุกค้าก่อน.");
        //        }

        //        //Get Customerprofile data for add log and replace bt adminapprove
        //        var CustNewProfile = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == confirm.PersonId);
        //        var CustomerProve = new AdminApprove
        //        {
        //            PersonId = CustNewProfile.PersonId,
        //            Customer_guid = CustNewProfile.Customer_guid,
        //            FirstName = CustNewProfile.FirstName,
        //            LastName = CustNewProfile.LastName,
        //            TitleId = CustNewProfile.TitleId,
        //            IdentityCard = CustNewProfile.IdentityCard,
        //            Birthdate = CustNewProfile.Birthdate,
        //            PrimaryPhone = CustNewProfile.PrimaryPhone,
        //            SecondaryPhone = CustNewProfile.SecondaryPhone,
        //            Email = CustNewProfile.Email,
        //            LineID = CustNewProfile.LineID,
        //            ImagePath = CustNewProfile.ImagePath,
        //            ImageReferenceId = CustNewProfile.ImageReferenceId,
        //            DocumentId = CustNewProfile.DocumentId,
        //            EditorId = CustNewProfile.EditorId,
        //            ListMergeFrom = CustNewProfile.ListMergeFrom,
        //            ListMergeTo = CustNewProfile.ListMergeTo,
        //            IsCheckMerge = true,
        //            IsUpdated = true,
        //            LastUpdated = Now(),
        //        };

        //        // Add log admin confirm data
        //        await AddTransactionsConfirmByAdmin(CustomerProve, AdminProve, username);

        //        // Send data to DataCenter API
        //        var ret = await SendRecordToDataCenter(AdminProve, confirm.UserId);

        //        if (ret.Item1 == false)
        //        {
        //            //return ResponseResult.Failure<ConfirmAdminDto>("ไม่สามารถส่งข้อมูลไปที่ DataCenter ได้.");
        //            return ResponseResult.Failure<ConfirmAdminDto>(ret.Item2);
        //        }


        //        AdminProve.IsCheckMerge = true;
        //        AdminProve.IsUpdated = true;
        //        AdminProve.LastUpdated = Now();
        //        await _dbContext.SaveChangesAsync();



        //        // Update data AdminApprove into Customer_Newprofile from variable in line 1377
        //        if (CustNewProfile != null)
        //        {
        //            CustNewProfile.FirstName = AdminProve.FirstName;
        //            CustNewProfile.LastName = AdminProve.LastName;
        //            CustNewProfile.TitleId = AdminProve.TitleId;
        //            CustNewProfile.IdentityCard = AdminProve.IdentityCard;
        //            CustNewProfile.Birthdate = AdminProve.Birthdate;
        //            CustNewProfile.PrimaryPhone = AdminProve.PrimaryPhone;
        //            CustNewProfile.SecondaryPhone = AdminProve.SecondaryPhone;
        //            CustNewProfile.Email = AdminProve.Email;
        //            CustNewProfile.LineID = AdminProve.LineID;
        //            CustNewProfile.ImagePath = AdminProve.ImagePath;
        //            CustNewProfile.ImageReferenceId = AdminProve.ImageReferenceId;
        //            CustNewProfile.DocumentId = AdminProve.DocumentId;
        //            CustNewProfile.EditorId = AdminProve.EditorId;
        //            CustNewProfile.ListMergeFrom = AdminProve.ListMergeFrom;
        //            CustNewProfile.ListMergeTo = AdminProve.ListMergeTo;
        //            CustNewProfile.IsConfirm = true;
        //            CustNewProfile.ConfirmDate = Now();
        //            CustNewProfile.LastUpdated = Now();
        //            await _dbContext.SaveChangesAsync();
        //        }



        //        // Update confirm in customer_header
        //        var updateHeader = await _dbContext.Customer_Headers.FirstOrDefaultAsync(x => x.PayerPersonId == confirm.PersonId);

        //        if (updateHeader != null)
        //        {
        //            updateHeader.IsAgentConfirm = true;
        //            updateHeader.ConfirmDate = Now();
        //            updateHeader.LastUpdated = Now();
        //            await _dbContext.SaveChangesAsync();
        //        }


        //        // Update Payer-snapshot when other payer login will see last update.
        //        var updatePayer = await _dbContext.Payer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == confirm.PersonId);

        //        updatePayer.FirstName = AdminProve.FirstName;
        //        updatePayer.LastName = AdminProve.LastName;
        //        updatePayer.TitleId = AdminProve.TitleId;
        //        updatePayer.IdentityCard = AdminProve.IdentityCard;
        //        updatePayer.Birthdate = AdminProve.Birthdate;
        //        updatePayer.PrimaryPhone = AdminProve.PrimaryPhone;
        //        updatePayer.SecondaryPhone = AdminProve.SecondaryPhone;
        //        updatePayer.Email = AdminProve.Email;
        //        updatePayer.LineID = AdminProve.LineID;
        //        updatePayer.LastUpdated = Now();
        //        await _dbContext.SaveChangesAsync();

        //        //update follower and mark merge person is false
        //        IList<int> idData = new List<int>();
        //        var lstPersonId = (AdminProve.ListMergeFrom.Split(",", StringSplitOptions.RemoveEmptyEntries));

        //        foreach (var item in lstPersonId)
        //        {
        //            idData.Add(int.Parse(item));
        //        }

        //        var listFollower = await (from o in _dbContext.Customer_FollowUps
        //                           where idData.Contains(o.PersonId)
        //                           orderby o.PersonId
        //                           select o).ToListAsync();


        //        for (int i = 0; i < listFollower.Count; i++)
        //        {
        //            if (listFollower[i].PersonId != confirm.PersonId)
        //            {
        //                listFollower[i].AdminConfirm = true;
        //                listFollower[i].Result = false;
        //                listFollower[i].LastUpdated = Now();
        //                await _dbContext.SaveChangesAsync();
        //            }
        //            else
        //            {
        //                listFollower[i].AdminConfirm = true;
        //                listFollower[i].Result = true;
        //                listFollower[i].LastUpdated = Now();
        //                await _dbContext.SaveChangesAsync();
        //            }
        //        }

        //        // Set flag record merged
        //        var updateRecord = await (from o in _dbContext.Customer_NewProfiles
        //                                   where idData.Contains(o.PersonId)
        //                                   select o).ToListAsync();

        //        foreach (var item in updateRecord)
        //        {
        //            if (item.PersonId != confirm.PersonId)
        //            {
        //                item.IsMerged = true;
        //                item.IsMergedBy = confirm.PersonId.ToString();
        //            }
        //            else
        //            {
        //                item.IsMerged = false;
        //                item.IsMergedBy = confirm.PersonId.ToString();
        //            }

        //        }

        //        await _dbContext.SaveChangesAsync();

        //        var dto = _mapper.Map<ConfirmAdminDto>(CustNewProfile);
        //        return ResponseResult.Success(dto, TEXTSUCCESS);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ResponseResult.Failure<ConfirmAdminDto>(ex.Message);
        //    }
        //}

        // UI 7 : After All Done 
        #endregion

        // UI 7 : After All Done New Version..
        public async Task<ServiceResponse<ConfirmAdminDto>> ConfirmByAdmin(ConfirmDataAdminDto confirm, string username)
        {

            try
            {
                bool flgUpdateCustomer = false;

                // Update data AddminApprove is LastUpdate and use this record for Customer_Newprofile.
                var AdminProve = await _dbContext.AdminApproves.FirstOrDefaultAsync(x => x.PersonId == confirm.PersonId);

                // If Admin just confirm but not change anything.
                if (AdminProve == null)
                {
                    // Copy data from customer-newprofile keep record into adminapprove
                    var copydata = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == confirm.PersonId);
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
                        IsCheckMerge = copydata.ListMergeFrom != copydata.ListMergeTo? false : true,
                        IsUpdated = false,
                        LastUpdated = Now(),
                    };

                    // save into Admin Approve.
                    _dbContext.AdminApproves.Add(dummydata);
                    await _dbContext.SaveChangesAsync();

                    // set flag update customer is true.
                    flgUpdateCustomer = true;

                    // assign data back through variable.
                    AdminProve = dummydata;
                }

                // check data is already to merge
                if (AdminProve.IsCheckMerge == false && AdminProve.ListMergeFrom != AdminProve.ListMergeTo)
                {
                    return ResponseResult.Failure<ConfirmAdminDto>("กรุณายืนยันการรวมข้อมูลลุกค้าก่อน.");
                }

                // write log before do anything.
                if (flgUpdateCustomer == false)
                {
                    //Get Customerprofile data and add log for before data.
                    var CustNewProfile = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == confirm.PersonId);
                    var CustomerProve = new AdminApprove
                    {
                        PersonId = CustNewProfile.PersonId,
                        Customer_guid = CustNewProfile.Customer_guid,
                        FirstName = CustNewProfile.FirstName,
                        LastName = CustNewProfile.LastName,
                        TitleId = CustNewProfile.TitleId,
                        IdentityCard = CustNewProfile.IdentityCard,
                        Birthdate = CustNewProfile.Birthdate,
                        PrimaryPhone = CustNewProfile.PrimaryPhone,
                        SecondaryPhone = CustNewProfile.SecondaryPhone,
                        Email = CustNewProfile.Email,
                        LineID = CustNewProfile.LineID,
                        ImagePath = CustNewProfile.ImagePath,
                        ImageReferenceId = CustNewProfile.ImageReferenceId,
                        DocumentId = CustNewProfile.DocumentId,
                        EditorId = CustNewProfile.EditorId,
                        ListMergeFrom = CustNewProfile.ListMergeFrom,
                        ListMergeTo = CustNewProfile.ListMergeTo,
                        IsCheckMerge = true,
                        IsUpdated = true,
                        LastUpdated = Now(),
                    };
                    
                    // Add log admin confirm data
                    await AddTransactionsConfirmByAdmin(CustomerProve, AdminProve, username);
                }
                else
                {
                    // Add log admin confirm data
                    await AddTransactionsConfirmByAdmin(AdminProve, AdminProve, username);
                }
                

                // Send data to DataCenter API
                var ret = await SendRecordToDataCenter(AdminProve, confirm.UserId);

                if (ret.Item1 == false)
                {
                    //return ResponseResult.Failure<ConfirmAdminDto>("ไม่สามารถส่งข้อมูลไปที่ DataCenter ได้.");
                    return ResponseResult.Failure<ConfirmAdminDto>(ret.Item2);
                }


                AdminProve.IsCheckMerge = true;
                AdminProve.IsUpdated = true;
                AdminProve.LastUpdated = Now();
                await _dbContext.SaveChangesAsync();



                // Update data AdminApprove into Customer_Newprofile from variable in line 1377
                var CustProfile = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == confirm.PersonId);
                
                if (CustProfile != null)
                {
                    CustProfile.FirstName = AdminProve.FirstName;
                    CustProfile.LastName = AdminProve.LastName;
                    CustProfile.TitleId = AdminProve.TitleId;
                    CustProfile.IdentityCard = AdminProve.IdentityCard;
                    CustProfile.Birthdate = AdminProve.Birthdate;
                    CustProfile.PrimaryPhone = AdminProve.PrimaryPhone;
                    CustProfile.SecondaryPhone = AdminProve.SecondaryPhone;
                    CustProfile.Email = AdminProve.Email;
                    CustProfile.LineID = AdminProve.LineID;
                    CustProfile.ImagePath = AdminProve.ImagePath;
                    CustProfile.ImageReferenceId = AdminProve.ImageReferenceId;
                    CustProfile.DocumentId = AdminProve.DocumentId;
                    CustProfile.EditorId = AdminProve.EditorId;
                    CustProfile.ListMergeFrom = AdminProve.ListMergeFrom;
                    CustProfile.ListMergeTo = AdminProve.ListMergeTo;
                    CustProfile.IsConfirm = true;
                    CustProfile.ConfirmDate = Now();
                    CustProfile.LastUpdated = Now();
                    await _dbContext.SaveChangesAsync();
                }

                // Update confirm in customer_header
                var updateHeader = await _dbContext.Customer_Headers.FirstOrDefaultAsync(x => x.PayerPersonId == confirm.PersonId);

                if (updateHeader != null)
                {
                    updateHeader.IsAgentConfirm = true;
                    updateHeader.ConfirmDate = Now();
                    updateHeader.LastUpdated = Now();
                    await _dbContext.SaveChangesAsync();
                }

                // Update Payer-snapshot when other payer login will see last update.
                var updatePayer = await _dbContext.Payer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == confirm.PersonId);

                if (updatePayer != null)
                {
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
                }
                

                //update follower and mark merge person is false
                IList<int> idData = new List<int>();
                var lstPersonId = (AdminProve.ListMergeFrom.Split(",", StringSplitOptions.RemoveEmptyEntries));

                foreach (var item in lstPersonId)
                {
                    idData.Add(int.Parse(item));
                }

                var listFollower = await (from o in _dbContext.Customer_FollowUps
                                          where idData.Contains(o.PersonId)
                                          orderby o.PersonId
                                          select o).ToListAsync();

                for (int i = 0; i < listFollower.Count; i++)
                {
                    if (listFollower[i].PersonId != confirm.PersonId)
                    {
                        listFollower[i].AdminConfirm = true;
                        listFollower[i].Result = false;
                        listFollower[i].LastUpdated = Now();
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        listFollower[i].AdminConfirm = true;
                        listFollower[i].Result = true;
                        listFollower[i].LastUpdated = Now();
                        await _dbContext.SaveChangesAsync();
                    }
                }

                // Set flag record merged
                var updateRecord = await (from o in _dbContext.Customer_NewProfiles
                                          where idData.Contains(o.PersonId)
                                          select o).ToListAsync();


                // if data in customer-profile is e
                if (updateRecord.Count != idData.Count)
                {
                    for (int i = 0; i < idData.Count; i++)
                    {
                        if (idData[i] == confirm.PersonId)
                        {
                            idData.RemoveAt(i);
                        }
                    }

                    var copydata = await _dbContext.Payer_Snapshots.Where(x => idData.Contains(x.PersonId)).ToListAsync();

                    Customer_NewProfile CopyCust = new Customer_NewProfile();
                    AdminApprove CopyAdmin = new AdminApprove();
                    List<Customer_NewProfile> lstCopy = new List<Customer_NewProfile>();

                    foreach (var item in copydata)
                    {
                        CopyCust = new Customer_NewProfile
                        {
                            PersonId = item.PersonId,
                            Customer_guid = item.Customer_guid,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            TitleId = item.TitleId,
                            IdentityCard = item.IdentityCard,
                            Birthdate = item.Birthdate,
                            PrimaryPhone = item.PrimaryPhone,
                            SecondaryPhone = item.SecondaryPhone,
                            Email = item.Email,
                            LineID = item.LineID,
                            EditorId = item.PersonId,
                            ListMergeFrom = AdminProve.ListMergeFrom,
                            ListMergeTo = AdminProve.ListMergeTo,
                            IsConfirm = true,
                            IsUpdated = false, // for log in to change data but not show in admin
                            ConfirmDate = Now(),
                            LastUpdated = Now(),

                        };

                        CopyAdmin = new AdminApprove
                        {
                            PersonId = item.PersonId,
                            Customer_guid = item.Customer_guid,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            TitleId = item.TitleId,
                            IdentityCard = item.IdentityCard,
                            Birthdate = item.Birthdate,
                            PrimaryPhone = item.PrimaryPhone,
                            SecondaryPhone = item.SecondaryPhone,
                            Email = item.Email,
                            LineID = item.LineID,
                            EditorId = confirm.PersonId,
                            IsUpdated = true,
                            IsCheckMerge = true,
                            ListMergeFrom = AdminProve.ListMergeFrom,
                            ListMergeTo = AdminProve.ListMergeTo,
                            LastUpdated = Now(),
                        };

                        _dbContext.Customer_NewProfiles.Add(CopyCust);
                        _dbContext.AdminApproves.Add(CopyAdmin);
                        lstCopy.Add(CopyCust);
                    }

                    await _dbContext.SaveChangesAsync();
                    updateRecord.AddRange(lstCopy);
                }

                
                foreach (var item in updateRecord)
                {
                    if (item.PersonId != confirm.PersonId)
                    {
                        item.IsMerged = true;
                        item.IsMergedBy = confirm.PersonId.ToString();
                    }
                    else
                    {
                        item.IsMerged = false;
                        item.IsMergedBy = confirm.PersonId.ToString();
                    }
                }

                await _dbContext.SaveChangesAsync();

                var dto = _mapper.Map<ConfirmAdminDto>(CustProfile);
                return ResponseResult.Success(dto, TEXTSUCCESS);
            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<ConfirmAdminDto>(ex.Message);
            }
        }

        private async Task<(bool,string)> SendRecordToDataCenter(AdminApprove AdminProve,int UserId)
        {
            try
            {

                DataCenterDTO dct = new DataCenterDTO
                {
                    PersonIdList = AdminProve.ListMergeFrom,
                    ToPersonId = AdminProve.ListMergeTo,
                    TitleId = AdminProve.TitleId,
                    FirstName = AdminProve.FirstName,
                    LastName = AdminProve.LastName,
                    IdentityCard = AdminProve.IdentityCard,
                    Birthdate = AdminProve.Birthdate,
                    PrimaryPhone = AdminProve.PrimaryPhone,
                    SecondaryPhone = AdminProve.SecondaryPhone,
                    Email = AdminProve.Email,
                    LineID = AdminProve.LineID,
                    DocumentCode = AdminProve.DocumentId,
                    UpdatedByUserId = UserId,
                };

                using (var httpClient = new HttpClient())
                {

                    #region content
                    //var content = new MultipartFormDataContent();
                    //content.Add(new StringContent(AdminProve.ListMergeFrom), "PersonIdList");
                    //content.Add(new StringContent(AdminProve.ListMergeTo), "ToPersonId");
                    //content.Add(new StringContent(AdminProve.TitleId.Value.ToString()), "TitleId");
                    //content.Add(new StringContent(AdminProve.FirstName), "FirstName");
                    //content.Add(new StringContent(AdminProve.LastName.ToString()), "LastName");
                    //content.Add(new StringContent(AdminProve.IdentityCard), "IdentityCard");
                    //content.Add(new StringContent(AdminProve.Birthdate.ToString()), "Birthdate");
                    //content.Add(new StringContent(AdminProve.PrimaryPhone), "PrimaryPhone");
                    //content.Add(new StringContent(AdminProve.SecondaryPhone.ToString()), "SecondaryPhone");
                    //content.Add(new StringContent(AdminProve.Email), "Email");
                    //content.Add(new StringContent(AdminProve.LineID), "LineID");
                    //content.Add(new StringContent(AdminProve.DocumentId), "DocumentCode");
                    //content.Add(new StringContent("Admin"), "UpdateByUserId");
                    #endregion

                    var jsonSender = JsonConvert.SerializeObject(dct);
                    var content = new StringContent(jsonSender,Encoding.UTF8,"application/json");
                    //var Url_Api = "http://uat.siamsmile.co.th:9116/api/Person/UpdatePerson";
                    //var Url_Api = "http://192.168.100.169:9116/api/Person/UpdatePerson";

                    var Url_Api = _configuraton.GetSection("ApiLists:DataCenter").Value;


                    using (var response = await httpClient.PostAsync(Url_Api, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var retval = JsonConvert.DeserializeObject<GetResultDataCenterDto>(apiResponse);   
                        return (retval.IsResult,"Data Center Error: " + retval.Msg);
                    }
                }

               
            }
            catch (Exception ex)
            {
                return (false, "Data Center Error: " + ex.Message);
            }
        }

        private async Task<string> GetImagePathFromSSSDoc(string docId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {

                    var Url_Api = _configuraton.GetSection("ApiLists:SSSDocument").Value + "?DocumentId=" + docId;

                    httpClient.BaseAddress = new Uri(Url_Api);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //example
                    //docId = "DC210100000001";

                    using (HttpResponseMessage response = await httpClient.GetAsync(Url_Api).ConfigureAwait(true))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        
                        var retval = JsonConvert.DeserializeObject<GetResultDocumentDataDto>(apiResponse);

                        

                        if (retval is null)
                        {
                            return "";
                        }

                        return retval.Data[0].pathFullDoc;
                    }
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
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

                #region OldQuery2
                //var customerx = (from cp in _dbContext.Customer_NewProfiles.AsNoTracking() //personId :1
                //                 join ct in _dbContext.Policy_Snapshots.AsNoTracking() //PayerPersoId :N
                //                 on cp.PersonId equals ct.PayerPersonId
                //                 where cp.IsUpdated == true && cp.PersonId == cp.EditorId
                //                 select new GetEditCustomerDto
                //                 {
                //                     PersonId = cp.PersonId,
                //                     FirstName = cp.FirstName,
                //                     LastName = cp.LastName,
                //                     FullName = cp.FirstName + " " + cp.LastName,
                //                     IdentityCard = cp.IdentityCard,
                //                     PrimaryPhone = cp.PrimaryPhone,
                //                     LastUpdated = cp.LastUpdated,
                //                     IsConfirm = cp.IsConfirm,
                //                 }).AsQueryable();

                //var customer = customerx.Distinct().OrderBy(x => x.PersonId).AsQueryable();

                #endregion

                #region NewQuery

                var customer = (from cp in _dbContext.Customer_NewProfiles.AsNoTracking()
                                join ct in _dbContext.Policy_Snapshots.AsNoTracking()
                                on cp.PersonId equals ct.PayerPersonId
                                where cp.IsUpdated == true && cp.PersonId == cp.EditorId
                                //orderby cp.PersonId ascending
                                group cp by new { cp.PersonId, cp.FirstName, cp.LastName, cp.IdentityCard, cp.PrimaryPhone, cp.LastUpdated, cp.IsConfirm }
                                into newcp
                                //orderby newcp.Key.PersonId ascending
                                select new GetEditCustomerDto
                                {
                                    PersonId = newcp.Key.PersonId,
                                    FirstName = newcp.Key.FirstName,
                                    LastName = newcp.Key.LastName,
                                    FullName = newcp.Key.FirstName + " " + newcp.Key.LastName,
                                    IdentityCard = newcp.Key.IdentityCard,
                                    PrimaryPhone = newcp.Key.PrimaryPhone,
                                    LastUpdated = newcp.Key.LastUpdated,
                                    IsConfirm = newcp.Key.IsConfirm,
                                }).AsQueryable();

                #endregion

                if (customer == null)
                {
                    // 404
                    return ResponseResultWithPagination.Failure<List<GetEditCustomerDto>>(TEXTCUSTOMERNOTFOUND);
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
                    return ResponseResultWithPagination.Failure<List<GetLoginCustomerDto>>(TEXTCUSTOMERNOTFOUND);
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
                    return ResponseResult.Failure<List<GetCompareDto>>(TEXTCUSTOMERNOTFOUND);
                }

                // Get payerId and customerId. 

                var pdata = await _dbContext.Policy_Snapshots.Where(x => x.PayerPersonId == personId).ToListAsync();
                IList<int> idData = new List<int>();
                foreach (var item in pdata)
                {
                    idData.Add(item.CustPersonId.Value);
                }

                if (idData.Contains(personId) == false)
                {
                    idData.Add(personId);
                }

                var idDatas = idData.Distinct();


                // Get personal data from every id in idData.

                var basepayer = await _dbContext.Payer_Snapshots.Where(x => idDatas.Contains(x.PersonId)).ToListAsync();

                var persondata = new GetCompareDto();
                var allcustomer = new List<GetCompareDto>();
                foreach (var o in basepayer)
                {
                    persondata = new GetCompareDto
                    {
                        Caption = "Before",
                        IsPayer = o.PersonId != personId ? false : true,
                        Customer_guid = o.Customer_guid,
                        PersonId = o.PersonId,
                        TitleId = o.TitleId,
                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        IdentityCard = o.IdentityCard,
                        Birthdate = o.Birthdate,
                        PrimaryPhone = o.PrimaryPhone,
                        SecondaryPhone = o.SecondaryPhone,
                        Email = o.Email == null ? "" : o.Email,
                        LineID = o.LineID == null?"": o.LineID,
                        ImagePath = "",
                        ImageReferenceId = "",
                        DocumentId =  "",
                        ListMergeFrom = "",
                        ListMergeTo =  "",
                    };

                    allcustomer.Add(persondata);
                };
               
                // if not found a payer then exit.
                if (allcustomer.Count() == 0)
                {
                    return ResponseResult.Failure<List<GetCompareDto>>(TEXTCUSTOMERNOTFOUND);
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
                                         Customer_guid = o.Customer_guid,
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
                        return ResponseResult.Failure<List<GetCompareDto>>("ไม่สามารถปรับปรุงรายการลูกค้า.");
                    }

                    if (AssignAfterData(diffield, afterData) == false)
                    {
                        return ResponseResult.Failure<List<GetCompareDto>>("ไม่สามารถปรับปรุงรายการลูกค้า.");
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
                    return ResponseResult.Failure<GetCompareLoginDto>(TEXTCUSTOMERNOTFOUND);
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
        public async Task<ServiceResponse<GetCompareLoginDto>> UpdateLoginOfCustomer(string UserId,GetCompareLoginDto update)
        {
            try
            {

                var IsCustomer = await _dbContext.Customer_Headers.Where(x => x.PayerPersonId == update.PersonId).ToListAsync();

                if (IsCustomer == null)
                {
                    return ResponseResult.Failure<GetCompareLoginDto>(TEXTCUSTOMERNOTFOUND);
                }

                //First : Update Table login.
                foreach (var item in IsCustomer)
                {
                    item.LoginIdentityCard = update.IdentityCard;
                    item.LoginLastName = update.LastName;
                }

                await _dbContext.SaveChangesAsync();


                //IsCustomer.LoginIdentityCard = update.IdentityCard
                //IsCustomer.LoginLastName = update.LastName;
                //await _dbContext.SaveChangesAsync();

                //Second : Update Table Payer.
                var payertable = await _dbContext.Payer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == update.PersonId);

                if (payertable == null)
                {
                    return ResponseResult.Failure<GetCompareLoginDto>(TEXTCUSTOMERNOTFOUND);
                }

                await AddTransactionsByCallCenter(UserId, update.PersonId, payertable.LastName, payertable.IdentityCard, update.LastName, update.IdentityCard);

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
                        return ResponseResult.Failure<GetCompareLoginDto>(TEXTCUSTOMERNOTFOUND);
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
        public async Task<ServiceResponse<bool>> AddTransactionsByCallCenter(string userId, int personId, string BeforeLastName, string BeforeIdentityCard, string AfterLastName, string AfterIdentityCard)
        {
            try
            {
                //Add transaction by admin
                var bfData = new[] { "LastName:" + BeforeLastName, "IdentityCard:" + BeforeIdentityCard };
                var afData = new[] { "LastName:" + AfterLastName, "IdentityCard:" + AfterIdentityCard };

                var transactionData = new AdminApproveTransaction();
                transactionData.UserId = userId;
                transactionData.PersonId = personId;
                transactionData.Description = userId + " แก้ไขข้อมูล login ลูกค้า";
                transactionData.BeforeChange = JsonConvert.SerializeObject(bfData);
                transactionData.AfterChange = JsonConvert.SerializeObject(afData);
                transactionData.CreateDate = Now();
                transactionData.LastUpdated = Now();

                await _dbContext.AdminApproveTransactions.AddRangeAsync(transactionData);
                await _dbContext.SaveChangesAsync();

                return ResponseResult.Success(true, TEXTSUCCESS);
            }
            catch (Exception ex)
            {
                return ResponseResult.Success(false, ex.Message);
            }
        }
        public async Task<ServiceResponse<bool>> FixedLostMergeWithoutChangedDataFromAdmin()
        {
            try
            {

                var allpersonId = await _dbContext.Customer_NewProfiles
                    .Where(x => x.ListMergeFrom.Length > 5)
                    .ToListAsync();

                if (allpersonId == null)
                {
                    return ResponseResult.Success(false, "No Data for Check.");
                }

                                
                IList<int> idData = new List<int>();
                
                foreach (var item in allpersonId)
                {
                    var psId = item.ListMergeFrom.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var subItem in psId)
                    {
                        if (subItem != item.ListMergeTo)
                        {
                            idData.Add(int.Parse(subItem));
                            
                        }

                    }
                }

                var listFollower = await (from o in _dbContext.Customer_FollowUps
                                          where idData.Contains(o.PersonId) 
                                          && o.Result == true
                                          select o).ToListAsync();

                listFollower.ForEach(c => c.Result = true);
                await _dbContext.SaveChangesAsync();

                return ResponseResult.Success(true, TEXTSUCCESS);
            }
            catch (Exception ex)
            {
                return ResponseResult.Success(false, ex.Message);
            }
        }

        #endregion

    }
}
