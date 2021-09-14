using AutoMapper;
using SmsUpdateCustomer_Api.Data;
using SmsUpdateCustomer_Api.DTOs.Customer_Profiles;
using SmsUpdateCustomer_Api.Models;
using SmsUpdateCustomer_Api.Models.Customer_Profiles;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using SmsUpdateCustomer_Api.Models.Customer_Snapshots;
using System.Text.RegularExpressions;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using SmsUpdateCustomer_Api.Helpers;
using SmsUpdateCustomer_Api.DTOs;
using SmsUpdateCustomer_Api.DTOs.Customer;
using SmsUpdateCustomer_Api.Validations;
using Newtonsoft.Json;

namespace SmsUpdateCustomer_Api.Services.Customer_Profiles
{
    public class CustomerProfileServices : ServiceBase, ICustomerProfileServices
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ICustomerProfileServices> _log;
        private readonly IHttpContextAccessor _httpcontext;

        private const string TEXTSUCCESS = "Success";
        public CustomerProfileServices(AppDBContext dbContext, IMapper mapper, ILogger<ICustomerProfileServices> log, IHttpContextAccessor httpcontext) : base(dbContext, mapper, httpcontext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _log = log;
            _httpcontext = httpcontext;

        }

        #region "Services" 
        public async Task<ServiceResponse<GetHotlineDto>> AddCustomerHotline(AddHotlineDto newhotline)
        {
            try
            {
                //Validate
                (bool, string) retval = ControlValidator.ValidationsforAddCustomerHotline(newhotline);

                if (retval.Item1 == false)
                {
                    return ResponseResult.Failure<GetHotlineDto>(retval.Item2);
                }

                //Process
                var _hotline = new Customer_Profile_Hotline
                {
                    PersonId = newhotline.PersonId,                   
                    FirstName = newhotline.FirstName,
                    LastName = newhotline.LastName,                  
                    PrimaryPhone = newhotline.PrimaryPhone.Trim(),
                    Email = newhotline.Email,
                    Remark = newhotline.Remark,
                    TypeHotLine = newhotline.TypeHotLine,
                    InformDate= Now(),
                    LastUpdated = Now(),
                };

                _dbContext.Customer_Profile_Hotlines.Add(_hotline);
                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<GetHotlineDto>(_hotline);
                return ResponseResult.Success(dto, TEXTSUCCESS);
            }
            catch (Exception ex)
            {

                return ResponseResult.Failure<GetHotlineDto>(ex.Message);
            }
        }
        public async Task<ServiceResponse<GetProfileDto>> AddCustomerProfile(AddProfileDto newProfile)
        {
            try
            {
                //Validate
                (bool, string) retval = ControlValidator.ValidationsforAddCustomerProfile(newProfile);

                if (retval.Item1 == false)
                {
                    return ResponseResult.Failure<GetProfileDto>(retval.Item2);
                }


                ////Validate Document
                //var imgDoc = newProfile.ImagePath;
                //var imgRef = newProfile.ImageReferenceId;
                //var docId = newProfile.DocumentId;

                //if (string.IsNullOrEmpty(imgDoc) || string.IsNullOrEmpty(imgRef) || string.IsNullOrEmpty(docId))
                //{
                //    var chkField = await _dbContext.Payer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == newProfile.PersonId);
                //    if (chkField.FirstName != newProfile.FirstName ||
                //        chkField.LastName != newProfile.LastName ||
                //        chkField.Birthdate != newProfile.Birthdate ||
                //        chkField.IdentityCard != newProfile.IdentityCard ||
                //        chkField.PrimaryPhone != newProfile.PrimaryPhone)
                //    {
                //        return ResponseResult.Failure<GetProfileDto>("Found Mandatory Field Changed Please Upload Document.");
                //    }
                //}

                GetProfileDto dto = default;

                var customer = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == newProfile.PersonId);

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
                    //customer.ImagePath = newProfile.ImagePath;
                    //customer.ImageReferenceId = newProfile.ImageReferenceId;
                    //customer.DocumentId = newProfile.DocumentId;
                    customer.ImagePath = newProfile.ImagePath == ""? customer.ImagePath: newProfile.ImagePath;
                    customer.ImageReferenceId = newProfile.ImageReferenceId == "" ? customer.ImageReferenceId : newProfile.ImageReferenceId;
                    customer.DocumentId = newProfile.DocumentId == "" ? customer.DocumentId : newProfile.DocumentId;
                    customer.EditorId = newProfile.EditorId;
                    customer.ListMergeFrom = newProfile.ListMergeFrom;
                    customer.ListMergeTo = newProfile.ListMergeTo;
                    customer.IsUpdated = newProfile.IsUpdated != false ? true : false;
                    customer.IsConfirm = false;
                    customer.ConfirmDate = new DateTime(1900,01,01);
                    customer.LastUpdated = Now();

                    await _dbContext.SaveChangesAsync();
                    dto = _mapper.Map<GetProfileDto>(customer);
                }
                else
                {
                    //Add
                    var _profile = new Customer_NewProfile
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
                        IsUpdated = newProfile.IsUpdated != false ? true : false,
                        IsConfirm = false,
                        ConfirmDate = new DateTime(1900, 01, 01),
                        LastUpdated = Now(),
                    };

                    _dbContext.Customer_NewProfiles.Add(_profile);
                    await _dbContext.SaveChangesAsync();
                    dto = _mapper.Map<GetProfileDto>(_profile);
                }

                return ResponseResult.Success(dto,TEXTSUCCESS);
            }
            catch (Exception ex)
            {

                return ResponseResult.Failure<GetProfileDto>(ex.Message);
            }
        }

        //public async Task<ServiceResponse<List<GetProfileDto>>> ConfirmCustomerProfile(int editorId)
        //{
        //    try
        //    {
        //        //find a edit record.
        //        var customer = await _dbContext.Customer_NewProfiles
        //            .Where(x => x.IsUpdated == false && x.EditorId == editorId).ToListAsync();

        //        // if not found edit record just get data from snap and save into customer_profile.
        //        if (customer.Count == 0)
        //        {

        //            // Get data from Snapshot for Default Data
        //            var dummy_customer = await _dbContext.Payer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == editorId);

        //            var dummy_listmerg = (from o in await _dbContext.Payer_Snapshots.ToListAsync()
        //                                  where o.IdentityCard == dummy_customer.IdentityCard && o.LastName == dummy_customer.LastName
        //                                  select new GetProfileDto {
        //                                      PersonId = o.PersonId
        //                                  }).ToList();

        //            string temp_listmergefrom = default;
        //            string temp_listmergeto = editorId.ToString();

        //            foreach (var item in dummy_listmerg)
        //            {
        //                temp_listmergefrom = temp_listmergefrom + item.PersonId.ToString() + ',';
        //            }

        //            AddProfileDto addprofile = new AddProfileDto
        //            {
        //                PersonId = dummy_customer.PersonId,
        //                Customer_guid = dummy_customer.Customer_guid,
        //                TitleId = dummy_customer.TitleId,
        //                FirstName = dummy_customer.FirstName,
        //                LastName = dummy_customer.LastName,
        //                Birthdate = dummy_customer.Birthdate,
        //                IdentityCard = dummy_customer.IdentityCard,
        //                PrimaryPhone = dummy_customer.PrimaryPhone,
        //                SecondaryPhone = dummy_customer.SecondaryPhone,
        //                Email = dummy_customer.Email,
        //                LineID = dummy_customer.LineID,
        //                EditorId = editorId,
        //                ImagePath = string.Empty,
        //                ImageReferenceId = string.Empty,
        //                DocumentId = string.Empty,
        //                ListMergeFrom = temp_listmergefrom.Substring(0, temp_listmergefrom.Length -1),
        //                ListMergeTo = temp_listmergeto,
        //                IsUpdated = true,

        //            };

        //            await AddCustomerProfile(addprofile);

        //            //Update header
        //            var header = await _dbContext.Customer_Headers.FirstOrDefaultAsync(x => x.PayerPersonId == editorId);
        //            if (header != null)
        //            {
        //                header.ReplyDate = Now();
        //                header.IsCustomerReply = true;
        //                await _dbContext.SaveChangesAsync();
        //            }

        //            //update follower
        //            var follower = await _dbContext.Customer_FollowUps.FirstOrDefaultAsync(x => x.PersonId == editorId);
        //            if (follower != null)
        //            {
        //                follower.CustomerConfirm = true;
        //                follower.LastUpdated = Now();
        //                await _dbContext.SaveChangesAsync();
        //            }

        //            //update AdminApprove
        //            var AdminApp = await _dbContext.AdminApproves.FirstOrDefaultAsync(x => x.PersonId == editorId);
        //            if (AdminApp != null)
        //            {
        //                AdminApp.IsUpdated = false;
        //                AdminApp.IsCheckMerge = false;
        //                AdminApp.LastUpdated = Now();
        //                await _dbContext.SaveChangesAsync();
        //            }

        //            //** b'cause no change anything then no keep customer transaction
        //            //*** but must keep customer transaction log 
        //            await AddTransactionsByCustomer(addprofile, addprofile, editorId);


        //            var gpf = await _dbContext.Customer_NewProfiles.Where(x => x.PersonId == editorId).ToListAsync();
        //            var dtos = _mapper.Map<List<GetProfileDto>>(gpf);
        //            return ResponseResult.Success(dtos, TEXTSUCCESS);
        //        }


        //        // start Compare data.
        //        foreach (var item in customer)
        //        {
        //            // Check in customer newprofile if found that mean some peaple edited this person before then get data from newprofile.
        //            var customerPreviousEditor = await _dbContext.Customer_NewProfiles
        //               .Where(x => x.PersonId == item.PersonId && x.EditorId != editorId)
        //               .OrderByDescending(x => x.LastUpdated).FirstOrDefaultAsync();

        //            if (customerPreviousEditor == null)
        //            {
        //                // So if not found in customer_newprofile that mean nobody edit this person before then get data from payer_snapshot only!!.
        //                var snapcustomer = await _dbContext.Payer_Snapshots
        //                .Where(x => x.PersonId == item.PersonId).ToListAsync();

        //                //Save Transaction
        //                var addExcept = VerifyData(item, snapcustomer);
        //                if (addExcept.Count > 0)
        //                {
        //                    foreach (var itemexcept in addExcept)
        //                    {
        //                        var cpt = new Customer_Profile_Transaction
        //                        {
        //                            PersonId = item.PersonId,
        //                            EditorId = item.EditorId,//item.EditorId.Value,
        //                            FieldData = itemexcept.FieldData,
        //                            BeforeChange = itemexcept.BeforeChange,
        //                            AfterChange = itemexcept.AfterChange,
        //                            LastUpdated = Now()
        //                        };

        //                        _dbContext.Customer_Profile_Transactions.Add(cpt);
        //                        await _dbContext.SaveChangesAsync();
        //                        var dtos = _mapper.Map<GetProfileTransaction>(cpt);

        //                        if (dtos == null)
        //                        {
        //                            return ResponseResult.Failure<List<GetProfileDto>>("Can't Insert Transaction");
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                //Save Transaction
        //                var addExcept = VerifyData(item, customerPreviousEditor);
        //                if (addExcept.Count > 0)
        //                {
        //                    foreach (var itemexcept in addExcept)
        //                    {
        //                        var cpt = new Customer_Profile_Transaction
        //                        {
        //                            PersonId = item.PersonId,
        //                            EditorId = item.EditorId,//item.EditorId.Value,
        //                            FieldData = itemexcept.FieldData,
        //                            BeforeChange = itemexcept.BeforeChange,
        //                            AfterChange = itemexcept.AfterChange,
        //                            LastUpdated = Now()
        //                        };

        //                        _dbContext.Customer_Profile_Transactions.Add(cpt);
        //                        await _dbContext.SaveChangesAsync();
        //                        var dtos = _mapper.Map<GetProfileTransaction>(cpt);

        //                        if (dtos == null)
        //                        {
        //                            return ResponseResult.Failure<List<GetProfileDto>>("Can't Insert Transaction");
        //                        }
        //                    }
        //                }
        //            }

        //            // update table payer_snapshot and customer_snapshot for new information
        //            var payersnap = await _dbContext.Payer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == item.PersonId);
        //            if (payersnap == null)
        //            {
        //                return ResponseResult.Failure<List<GetProfileDto>>("Can't Update Payer Snapshot");
        //            }
        //            else
        //            {
        //                payersnap.FirstName = item.FirstName;
        //                payersnap.LastName = item.LastName;
        //                payersnap.TitleId = item.TitleId;
        //                payersnap.Birthdate = item.Birthdate;
        //                payersnap.IdentityCard = item.IdentityCard;
        //                payersnap.PrimaryPhone = item.PrimaryPhone;
        //                payersnap.SecondaryPhone = item.SecondaryPhone;
        //                payersnap.Email = item.Email;
        //                payersnap.LineID = item.LineID;
        //                await _dbContext.SaveChangesAsync();
        //            }

        //            var customersnap = await _dbContext.Customer_Snapshots.Where(x => x.PersonId == item.PersonId).ToListAsync();
        //            if (customersnap == null)
        //            {
        //                return ResponseResult.Failure<List<GetProfileDto>>("Can't Update Customer Snapshot");
        //            }
        //            else
        //            {

        //                foreach (var custsnap in customersnap)
        //                {
        //                    custsnap.FirstName = item.FirstName;
        //                    custsnap.LastName = item.LastName;
        //                    custsnap.TitleId = item.TitleId;
        //                    custsnap.Birthdate = item.Birthdate;
        //                    custsnap.IdentityCard = item.IdentityCard;
        //                    custsnap.PrimaryPhone = item.PrimaryPhone;
        //                    custsnap.SecondaryPhone = item.SecondaryPhone;
        //                    custsnap.Email = item.Email;
        //                    custsnap.LineID = item.LineID;
        //                    await _dbContext.SaveChangesAsync();
        //                }                      
        //            }


        //            int idx = customer.FindIndex(x => x.PersonId == item.PersonId);
        //            customer[idx].IsUpdated = true;
        //            await _dbContext.SaveChangesAsync();

        //            //Update header
        //            var header = await _dbContext.Customer_Headers.FirstOrDefaultAsync(x => x.PayerPersonId == editorId);
        //            if (header != null)
        //            {
        //                header.ReplyDate = Now();
        //                header.IsCustomerReply = true;
        //                await _dbContext.SaveChangesAsync();
        //            }

        //            //update follower
        //            var follower = await _dbContext.Customer_FollowUps.FirstOrDefaultAsync(x => x.PersonId == editorId);
        //            if (follower != null)
        //            {
        //                follower.CustomerConfirm = true;
        //                follower.LastUpdated = Now();
        //                await _dbContext.SaveChangesAsync();
        //            }

        //            //update AdminApprove
        //            var AdminApp = await _dbContext.AdminApproves.FirstOrDefaultAsync(x => x.PersonId == editorId);
        //            if (AdminApp != null)
        //            {
        //                AdminApp.IsUpdated = false;
        //                AdminApp.IsCheckMerge = false;
        //                AdminApp.LastUpdated = Now();
        //                await _dbContext.SaveChangesAsync();
        //            }

        //            //Save Transaction Log
        //            await AddTransactionsByCustomer(item.PersonId, editorId);

        //        }

        //        var dto = _mapper.Map<List<GetProfileDto>>(customer);
        //        return ResponseResult.Success(dto, TEXTSUCCESS);
        //    }
        //    catch (Exception ex)
        //    {

        //        return ResponseResult.Failure<List<GetProfileDto>>(ex.Message);
        //    }
        //}

        public async Task<ServiceResponse<List<GetProfileDto>>> ConfirmCustomerProfile(int editorId)
        {
            try
            {
                //find a edit record.
                var customer = await _dbContext.Customer_NewProfiles
                    .Where(x => x.IsUpdated == false && x.EditorId == editorId).ToListAsync();

                // if not found edit record just update customer reply
                if (customer.Count == 0)
                {
                    //Update header
                    var header = await _dbContext.Customer_Headers.FirstOrDefaultAsync(x => x.PayerPersonId == editorId);
                    if (header != null)
                    {
                        header.ReplyDate = Now();
                        header.IsCustomerReply = true;
                        await _dbContext.SaveChangesAsync();
                    }

                    //update follower
                    var follower = await _dbContext.Customer_FollowUps.FirstOrDefaultAsync(x => x.PersonId == editorId);
                    if (follower != null)
                    {
                        follower.CustomerConfirm = true;
                        follower.LastUpdated = Now();
                        await _dbContext.SaveChangesAsync();
                    }

                    //update AdminApprove
                    var AdminApp = await _dbContext.AdminApproves.FirstOrDefaultAsync(x => x.PersonId == editorId);
                    if (AdminApp != null)
                    {
                        AdminApp.IsUpdated = false;
                        AdminApp.IsCheckMerge = false;
                        AdminApp.LastUpdated = Now();
                        await _dbContext.SaveChangesAsync();
                    }


                    var gpf = await _dbContext.Customer_NewProfiles.Where(x => x.PersonId == editorId).ToListAsync();
                    var dtos = _mapper.Map<List<GetProfileDto>>(gpf);
                    return ResponseResult.Success(dtos, TEXTSUCCESS);
                }


                // start Compare data.
                foreach (var item in customer)
                {
                    // Check in customer newprofile if found that mean some peaple edited this person before then get data from newprofile.
                    var customerPreviousEditor = await _dbContext.Customer_NewProfiles
                       .Where(x => x.PersonId == item.PersonId && x.EditorId != editorId)
                       .OrderByDescending(x => x.LastUpdated).FirstOrDefaultAsync();

                    if (customerPreviousEditor == null)
                    {
                        // So if not found in customer_newprofile that mean nobody edit this person before then get data from payer_snapshot only!!.
                        var snapcustomer = await _dbContext.Payer_Snapshots
                        .Where(x => x.PersonId == item.PersonId).ToListAsync();

                        //Save Transaction
                        var addExcept = VerifyData(item, snapcustomer);
                        if (addExcept.Count > 0)
                        {
                            foreach (var itemexcept in addExcept)
                            {
                                var cpt = new Customer_Profile_Transaction
                                {
                                    PersonId = item.PersonId,
                                    EditorId = item.EditorId,//item.EditorId.Value,
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
                                    return ResponseResult.Failure<List<GetProfileDto>>("Can't Insert Transaction");
                                }
                            }
                        }
                    }
                    else
                    {
                        //Save Transaction
                        var addExcept = VerifyData(item, customerPreviousEditor);
                        if (addExcept.Count > 0)
                        {
                            foreach (var itemexcept in addExcept)
                            {
                                var cpt = new Customer_Profile_Transaction
                                {
                                    PersonId = item.PersonId,
                                    EditorId = item.EditorId,//item.EditorId.Value,
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
                                    return ResponseResult.Failure<List<GetProfileDto>>("Can't Insert Transaction");
                                }
                            }
                        }
                    }

                    // update table payer_snapshot and customer_snapshot for new information
                    var payersnap = await _dbContext.Payer_Snapshots.FirstOrDefaultAsync(x => x.PersonId == item.PersonId);
                    if (payersnap == null)
                    {
                        return ResponseResult.Failure<List<GetProfileDto>>("Can't Update Payer Snapshot");
                    }
                    else
                    {
                        payersnap.FirstName = item.FirstName;
                        payersnap.LastName = item.LastName;
                        payersnap.TitleId = item.TitleId;
                        payersnap.Birthdate = item.Birthdate;
                        payersnap.IdentityCard = item.IdentityCard;
                        payersnap.PrimaryPhone = item.PrimaryPhone;
                        payersnap.SecondaryPhone = item.SecondaryPhone;
                        payersnap.Email = item.Email;
                        payersnap.LineID = item.LineID;
                        await _dbContext.SaveChangesAsync();
                    }

                    var customersnap = await _dbContext.Customer_Snapshots.Where(x => x.PersonId == item.PersonId).ToListAsync();
                    if (customersnap == null)
                    {
                        return ResponseResult.Failure<List<GetProfileDto>>("Can't Update Customer Snapshot");
                    }
                    else
                    {

                        foreach (var custsnap in customersnap)
                        {
                            custsnap.FirstName = item.FirstName;
                            custsnap.LastName = item.LastName;
                            custsnap.TitleId = item.TitleId;
                            custsnap.Birthdate = item.Birthdate;
                            custsnap.IdentityCard = item.IdentityCard;
                            custsnap.PrimaryPhone = item.PrimaryPhone;
                            custsnap.SecondaryPhone = item.SecondaryPhone;
                            custsnap.Email = item.Email;
                            custsnap.LineID = item.LineID;
                            await _dbContext.SaveChangesAsync();
                        }
                    }


                    int idx = customer.FindIndex(x => x.PersonId == item.PersonId);
                    customer[idx].IsUpdated = true;
                    await _dbContext.SaveChangesAsync();

                    //Update header
                    var header = await _dbContext.Customer_Headers.FirstOrDefaultAsync(x => x.PayerPersonId == editorId);
                    if (header != null)
                    {
                        header.ReplyDate = Now();
                        header.IsCustomerReply = true;
                        await _dbContext.SaveChangesAsync();
                    }

                    //update follower
                    var follower = await _dbContext.Customer_FollowUps.FirstOrDefaultAsync(x => x.PersonId == editorId);
                    if (follower != null)
                    {
                        follower.CustomerConfirm = true;
                        follower.LastUpdated = Now();
                        await _dbContext.SaveChangesAsync();
                    }

                    //update AdminApprove
                    var AdminApp = await _dbContext.AdminApproves.FirstOrDefaultAsync(x => x.PersonId == editorId);
                    if (AdminApp != null)
                    {
                        AdminApp.IsUpdated = false;
                        AdminApp.IsCheckMerge = false;
                        AdminApp.LastUpdated = Now();
                        await _dbContext.SaveChangesAsync();
                    }

                    //Save Transaction Log
                    await AddTransactionsByCustomer(item.PersonId, editorId);

                }

                var dto = _mapper.Map<List<GetProfileDto>>(customer);
                return ResponseResult.Success(dto, TEXTSUCCESS);
            }
            catch (Exception ex)
            {

                return ResponseResult.Failure<List<GetProfileDto>>(ex.Message);
            }
        }
        public async Task<ServiceResponse<GetProfileDto>> GetCustomerProfile(int personId)
        {
            try
            {
                //Get from newProfile
                var editcustomer = await _dbContext.Customer_NewProfiles
                       .OrderByDescending(x => x.LastUpdated)
                       .FirstOrDefaultAsync(x => x.PersonId == personId);
                    

                if (editcustomer == null)
                {
                    //Get from SnapShot

                    var oldcustomer = await _dbContext.Payer_Snapshots
                    .FirstOrDefaultAsync(x => x.PersonId == personId);

                    if (oldcustomer == null)
                    {
                        return ResponseResult.Failure<GetProfileDto>("Not found customer.");
                    }

                    var dto = _mapper.Map<GetProfileDto>(oldcustomer);
                    return ResponseResult.Success(dto, "Success");
                }
                else
                {
                    var dto = _mapper.Map<GetProfileDto>(editcustomer);
                    return ResponseResult.Success(dto, "Success");
                }

            }
            catch (Exception ex)
            {
                return ResponseResult.Failure<GetProfileDto>(ex.Message);

            }
        }
        public static List<GetProfileTransaction> VerifyData(Customer_NewProfile item, List<Payer_Snapshot> snapcustomer)
        {
            try
            {
                List<GetProfileTransaction> addExcept = new List<GetProfileTransaction>();
                GetProfileTransaction addItem = default;
                int i = 0;
                foreach (var itemsnap in snapcustomer)
                {

                    if (itemsnap.FirstName != item.FirstName)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "FirstName";
                        addItem.BeforeChange = itemsnap.FirstName;
                        addItem.AfterChange = item.FirstName;
                        addExcept.Add(addItem);
                    }

                    if (itemsnap.LastName != item.LastName)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "LastName";
                        addItem.BeforeChange = itemsnap.LastName;
                        addItem.AfterChange = item.LastName;
                        addExcept.Add(addItem);
                    }

                    if (itemsnap.IdentityCard != item.IdentityCard)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "IdentityCard";
                        addItem.BeforeChange = itemsnap.IdentityCard;
                        addItem.AfterChange = item.IdentityCard;
                        addExcept.Add(addItem);
                    }

                    if (itemsnap.Birthdate != item.Birthdate)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "Birthdate";
                        addItem.BeforeChange = itemsnap.Birthdate.ToString();
                        addItem.AfterChange = item.Birthdate.ToString();
                        addExcept.Add(addItem);
                    }

                    if (itemsnap.PrimaryPhone != item.PrimaryPhone)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "PrimaryPhone";
                        addItem.BeforeChange = itemsnap.PrimaryPhone;
                        addItem.AfterChange = item.PrimaryPhone;
                        addExcept.Add(addItem);
                    }

                    if (itemsnap.SecondaryPhone != item.SecondaryPhone)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "SecondaryPhone";
                        addItem.BeforeChange = itemsnap.SecondaryPhone;
                        addItem.AfterChange = item.SecondaryPhone;
                        addExcept.Add(addItem);
                    }

                    if (itemsnap.Email != item.Email)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "Email";
                        addItem.BeforeChange = itemsnap.Email;
                        addItem.AfterChange = item.Email;
                        addExcept.Add(addItem);
                    }


                    if (itemsnap.LineID != item.LineID)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "LineID";
                        addItem.BeforeChange = itemsnap.LineID;
                        addItem.AfterChange = item.LineID;
                        addExcept.Add(addItem);
                    }

                    if (!string.IsNullOrEmpty(item.ImagePath))
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "ImagePath";
                        addItem.BeforeChange = "";
                        addItem.AfterChange = item.ImagePath;
                        addExcept.Add(addItem);
                    }

                    if (!string.IsNullOrEmpty(item.ImageReferenceId))
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "ImageReferenceId";
                        addItem.BeforeChange = "";
                        addItem.AfterChange = item.ImageReferenceId;
                        addExcept.Add(addItem);
                    }

                    if (!string.IsNullOrEmpty(item.DocumentId))
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "DocumentId";
                        addItem.BeforeChange = "";
                        addItem.AfterChange = item.DocumentId;
                        addExcept.Add(addItem);
                    }


                }

                return addExcept.ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }
        public static List<GetProfileTransaction> VerifyData(Customer_NewProfile item, Customer_NewProfile prevoiuscustomer)
        {
            try
            {
                List<GetProfileTransaction> addExcept = new List<GetProfileTransaction>();
                GetProfileTransaction addItem = default;
                if (prevoiuscustomer.FirstName != item.FirstName)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "FirstName";
                    addItem.BeforeChange = prevoiuscustomer.FirstName;
                    addItem.AfterChange = item.FirstName;
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.LastName != item.LastName)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "LastName";
                    addItem.BeforeChange = prevoiuscustomer.LastName;
                    addItem.AfterChange = item.LastName;
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.IdentityCard != item.IdentityCard)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "IdentityCard";
                    addItem.BeforeChange = prevoiuscustomer.IdentityCard;
                    addItem.AfterChange = item.IdentityCard;
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.Birthdate != item.Birthdate)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "Birthdate";
                    addItem.BeforeChange = prevoiuscustomer.Birthdate.ToString();
                    addItem.AfterChange = item.Birthdate.ToString();
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.PrimaryPhone != item.PrimaryPhone)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "PrimaryPhone";
                    addItem.BeforeChange = prevoiuscustomer.PrimaryPhone;
                    addItem.AfterChange = item.PrimaryPhone;
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.SecondaryPhone != item.SecondaryPhone)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "SecondaryPhone";
                    addItem.BeforeChange = prevoiuscustomer.SecondaryPhone;
                    addItem.AfterChange = item.SecondaryPhone;
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.Email != item.Email)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "Email";
                    addItem.BeforeChange = prevoiuscustomer.Email;
                    addItem.AfterChange = item.Email;
                    addExcept.Add(addItem);
                }


                if (prevoiuscustomer.LineID != item.LineID)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "LineID";
                    addItem.BeforeChange = prevoiuscustomer.LineID;
                    addItem.AfterChange = item.LineID;
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.ImagePath != item.ImagePath)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "ImagePath";
                    addItem.BeforeChange = prevoiuscustomer.ImagePath;
                    addItem.AfterChange = item.ImagePath;
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.ImagePath != item.ImageReferenceId)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "ImageReferenceId";
                    addItem.BeforeChange = prevoiuscustomer.ImageReferenceId;
                    addItem.AfterChange = item.ImageReferenceId;
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.ImagePath != item.DocumentId)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "DocumentId";
                    addItem.BeforeChange = prevoiuscustomer.DocumentId;
                    addItem.AfterChange = item.DocumentId;
                    addExcept.Add(addItem);
                }

                return addExcept.ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }
        private async Task<bool> AddTransactionsByCustomer(int personId, int EditorId)
        {
            try
            {
                AddProfileDto oldProfile = new AddProfileDto();
                AddProfileDto newProfile = new AddProfileDto();

                var profile = await _dbContext.Customer_NewProfiles.FirstOrDefaultAsync(x => x.PersonId == personId);

                if (profile == null)
                {
                    var snapcustomer = await _dbContext.Payer_Snapshots
                        .FirstOrDefaultAsync(x => x.PersonId == personId);

                    oldProfile = new AddProfileDto
                    {
                        PersonId = snapcustomer.PersonId,
                        Customer_guid = snapcustomer.Customer_guid,
                        TitleId = snapcustomer.TitleId,
                        FirstName = snapcustomer.FirstName,
                        LastName = snapcustomer.LastName,
                        Birthdate = snapcustomer.Birthdate,
                        IdentityCard = snapcustomer.IdentityCard,
                        PrimaryPhone = snapcustomer.PrimaryPhone,
                        SecondaryPhone = snapcustomer.SecondaryPhone,
                        Email = snapcustomer.Email,
                        LineID = snapcustomer.LineID,
                        EditorId = EditorId,
                        ImagePath = string.Empty,
                        ImageReferenceId = string.Empty,
                        DocumentId = string.Empty,
                        ListMergeFrom = string.Empty,
                        ListMergeTo = string.Empty,
                        IsUpdated = true,
                    };
                    newProfile = oldProfile;

                }
                else
                {
                    var transaction = await _dbContext.Customer_Profile_Transactions.Where(x => x.EditorId == EditorId && x.PersonId == personId).ToListAsync();

                    oldProfile = new AddProfileDto
                    {
                        PersonId = profile.PersonId,
                        Customer_guid = profile.Customer_guid,
                        TitleId = profile.TitleId,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        Birthdate = profile.Birthdate,
                        IdentityCard = profile.IdentityCard,
                        PrimaryPhone = profile.PrimaryPhone,
                        SecondaryPhone = profile.SecondaryPhone,
                        Email = profile.Email,
                        LineID = profile.LineID,
                        EditorId = profile.EditorId,
                        ImagePath = profile.ImagePath,
                        ImageReferenceId = profile.ImageReferenceId,
                        DocumentId = profile.DocumentId,
                        ListMergeFrom = profile.ListMergeFrom,
                        ListMergeTo = profile.ListMergeTo,
                        IsUpdated = true,
                    };

                    foreach (var item in transaction)
                    {
                        switch (item.FieldData)
                        {
                            case "PersonId":
                                oldProfile.PersonId = int.Parse(item.BeforeChange);
                                break;
                            case "FirstName":
                                oldProfile.FirstName = item.BeforeChange;
                                break;
                            case "LastName":
                                oldProfile.LastName = item.BeforeChange;
                                break;
                            case "Customer_guid":
                                oldProfile.Customer_guid = profile.Customer_guid;
                                break;
                            case "TitleId":
                                oldProfile.TitleId = int.Parse(item.BeforeChange);
                                break;
                            case "IdentityCard":
                                oldProfile.IdentityCard = item.BeforeChange;
                                break;
                            case "Birthdate":
                                oldProfile.Birthdate = Convert.ToDateTime(item.BeforeChange);
                                break;
                            case "PrimaryPhone":
                                oldProfile.PrimaryPhone = item.BeforeChange;
                                break;
                            case "SecondaryPhone":
                                oldProfile.SecondaryPhone = item.BeforeChange;
                                break;
                            case "Email":
                                oldProfile.Email = item.BeforeChange;
                                break;
                            case "LineID":
                                oldProfile.LineID = item.BeforeChange;
                                break;
                            case "ImagePath":
                                oldProfile.ImagePath = item.BeforeChange;
                                break;
                            case "ImageReferenceId":
                                oldProfile.ImageReferenceId = item.BeforeChange;
                                break;
                            case "DocumentId":
                                oldProfile.DocumentId = item.BeforeChange;
                                break;
                            case "ListMergeFrom":
                                oldProfile.ListMergeFrom = item.BeforeChange;
                                break;
                            case "ListMergeTo":
                                oldProfile.ListMergeTo = item.BeforeChange;
                                break;
                            case "EditorId":
                                oldProfile.EditorId = int.Parse(item.BeforeChange);
                                break;
                            default:
                                break;
                        }
                    }

                    newProfile = new AddProfileDto
                    {
                        PersonId = profile.PersonId,
                        Customer_guid = profile.Customer_guid,
                        TitleId = profile.TitleId,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        Birthdate = profile.Birthdate,
                        IdentityCard = profile.IdentityCard,
                        PrimaryPhone = profile.PrimaryPhone,
                        SecondaryPhone = profile.SecondaryPhone,
                        Email = profile.Email,
                        LineID = profile.LineID,
                        EditorId = profile.EditorId,
                        ImagePath = profile.ImagePath,
                        ImageReferenceId = profile.ImageReferenceId,
                        DocumentId = profile.DocumentId,
                        ListMergeFrom = profile.ListMergeFrom,
                        ListMergeTo = profile.ListMergeTo,
                        IsUpdated = true,
                    };


                }

                
                var transactionData = new Customer_Profile_Transaction_Log();
                transactionData.PersonId = personId;
                transactionData.EditorId = EditorId;
                transactionData.Description = "";
                transactionData.BeforeChange = JsonConvert.SerializeObject(oldProfile);
                transactionData.AfterChange = JsonConvert.SerializeObject(newProfile);
                transactionData.DateCreated = Now();

                await _dbContext.Customer_Profile_Transaction_Logs.AddRangeAsync(transactionData);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

    }
}
