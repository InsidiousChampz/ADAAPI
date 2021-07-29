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

        public async Task<ServiceResponse<GetHotlineDto>> AddCustomerHotline(AddHotlineDto newhotline)
        {
            try
            {
                //Validate
                (bool, string) retval = ValidationsforAddCustomerHotline(newhotline);

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
                    PrimaryPhone = newhotline.PrimaryPhone,
                    Email = newhotline.Email,
                    Remark = newhotline.Remark,
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
                (bool, string) retval = ValidationsforAddCustomerProfile(newProfile);

                if (retval.Item1 == false)
                {
                    return ResponseResult.Failure<GetProfileDto>(retval.Item2);
                }

                //Process
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
                    Email=newProfile.Email,
                    LineID=newProfile.LineID,
                    ImagePath = newProfile.ImagePath,
                    ImageReferenceId = newProfile.ImageReferenceId,
                    DocumentId = newProfile.DocumentId,
                    EditorId = newProfile.EditorId,
                    ListMergeFrom = newProfile.ListMergeFrom,
                    ListMergeTo = newProfile.ListMergeTo,
                    IsUpdated = false,
                    LastUpdated = Now(),
                };

                _dbContext.Customer_NewProfiles.Add(_profile);
                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<GetProfileDto>(_profile);
                return ResponseResult.Success(dto,TEXTSUCCESS);
            }
            catch (Exception ex)
            {

                return ResponseResult.Failure<GetProfileDto>(ex.Message);
            }
        }
        public async Task<ServiceResponse<List<GetProfileDto>>> ConfirmCustomerProfile(int editorId)
        {
            try
            {
                //this is a new one.
                var customer = await _dbContext.Customer_NewProfiles
                    .Where(x => x.IsUpdated == false && x.EditorId == editorId).ToListAsync();

                if (customer.Count == 0)
                {
                    return ResponseResult.Failure<List<GetProfileDto>>("Not found record to change.");
                }

                //Compare
                foreach (var item in customer)
                {
                    //Check in customer newprofile if found that mean some peaple edited this person before.
                    //then get data from newprofile
                    
                    var customerPreviousEditor = await _dbContext.Customer_NewProfiles
                       .Where(x => x.PersonId == item.PersonId && x.EditorId != editorId)
                       .OrderByDescending(x => x.LastUpdated).FirstOrDefaultAsync();

                    if (customerPreviousEditor == null)
                    {
                        //So if not found in customer_newprofile that mean nobody edit this person before.
                        //then get data from snapshot
                        var snapcustomer = await _dbContext.Customer_Snapshots
                        .Where(x => x.PersonId == item.PersonId).ToListAsync();
                        
                        //Save Transaction
                        var addExcept = VerifyData(item, snapcustomer);
                        if (addExcept.Count > 0)
                        {
                            foreach (var itemexcept in addExcept)
                            {
                                var cpt = new Customer_Profile_Transaction
                                {
                                    PersonId = item.PersonId.Value,
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
                                    PersonId = item.PersonId.Value,
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
                                    return ResponseResult.Failure<List<GetProfileDto>>("Can't Insert Transaction");
                                }
                            }
                        }
                    }
                }



                customer[0].IsUpdated = true;
                await _dbContext.SaveChangesAsync();

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
                    //You can get from Customer_snapshot or Payer_Snapshot also because it a same data.

                    var oldcustomer = await _dbContext.Customer_Snapshots
                    .FirstOrDefaultAsync(x => x.PersonId == personId);
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
        private static List<GetProfileTransaction> VerifyData(Customer_NewProfile item, List<Customer_Snapshot> snapcustomer)
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
                        addItem.FieldData = "FirstName:";
                        addItem.BeforeChange = itemsnap.FirstName;
                        addItem.AfterChange = item.FirstName;
                        addExcept.Add(addItem);
                    }

                    if (itemsnap.LastName != item.LastName)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "LastName:";
                        addItem.BeforeChange = itemsnap.LastName;
                        addItem.AfterChange = item.LastName;
                        addExcept.Add(addItem);
                    }

                    if (itemsnap.IdentityCard != item.IdentityCard)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "IdentityCard:";
                        addItem.BeforeChange = itemsnap.IdentityCard;
                        addItem.AfterChange = item.IdentityCard;
                        addExcept.Add(addItem);
                    }

                    if (itemsnap.Birthdate != item.Birthdate)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "Birthdate:";
                        addItem.BeforeChange = itemsnap.Birthdate.ToString();
                        addItem.AfterChange = item.Birthdate.ToString();
                        addExcept.Add(addItem);
                    }

                    if (itemsnap.PrimaryPhone != item.PrimaryPhone)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "PrimaryPhone:";
                        addItem.BeforeChange = itemsnap.PrimaryPhone;
                        addItem.AfterChange = item.PrimaryPhone;
                        addExcept.Add(addItem);
                    }

                    if (itemsnap.SecondaryPhone != item.SecondaryPhone)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "SecondaryPhone:";
                        addItem.BeforeChange = itemsnap.SecondaryPhone;
                        addItem.AfterChange = item.SecondaryPhone;
                        addExcept.Add(addItem);
                    }

                    if (itemsnap.Email != item.Email)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "Email:";
                        addItem.BeforeChange = itemsnap.Email;
                        addItem.AfterChange = item.Email;
                        addExcept.Add(addItem);
                    }


                    if (itemsnap.LineID != item.LineID)
                    {
                        addItem = new GetProfileTransaction();
                        addItem.FieldData = "LineID:";
                        addItem.BeforeChange = itemsnap.LineID;
                        addItem.AfterChange = item.LineID;
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
        private static List<GetProfileTransaction> VerifyData(Customer_NewProfile item, Customer_NewProfile prevoiuscustomer)
        {
            try
            {
                List<GetProfileTransaction> addExcept = new List<GetProfileTransaction>();
                GetProfileTransaction addItem = default;
                if (prevoiuscustomer.FirstName != item.FirstName)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "FirstName:";
                    addItem.BeforeChange = prevoiuscustomer.FirstName;
                    addItem.AfterChange = item.FirstName;
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.LastName != item.LastName)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "LastName:";
                    addItem.BeforeChange = prevoiuscustomer.LastName;
                    addItem.AfterChange = item.LastName;
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.IdentityCard != item.IdentityCard)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "IdentityCard:";
                    addItem.BeforeChange = prevoiuscustomer.IdentityCard;
                    addItem.AfterChange = item.IdentityCard;
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.Birthdate != item.Birthdate)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "Birthdate:";
                    addItem.BeforeChange = prevoiuscustomer.Birthdate.ToString();
                    addItem.AfterChange = item.Birthdate.ToString();
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.PrimaryPhone != item.PrimaryPhone)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "PrimaryPhone:";
                    addItem.BeforeChange = prevoiuscustomer.PrimaryPhone;
                    addItem.AfterChange = item.PrimaryPhone;
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.SecondaryPhone != item.SecondaryPhone)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "SecondaryPhone:";
                    addItem.BeforeChange = prevoiuscustomer.SecondaryPhone;
                    addItem.AfterChange = item.SecondaryPhone;
                    addExcept.Add(addItem);
                }

                if (prevoiuscustomer.Email != item.Email)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "Email:";
                    addItem.BeforeChange = prevoiuscustomer.Email;
                    addItem.AfterChange = item.Email;
                    addExcept.Add(addItem);
                }


                if (prevoiuscustomer.LineID != item.LineID)
                {
                    addItem = new GetProfileTransaction();
                    addItem.FieldData = "LineID:";
                    addItem.BeforeChange = prevoiuscustomer.LineID;
                    addItem.AfterChange = item.LineID;
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
        private static (bool, string) ValidationsforAddCustomerProfile(AddProfileDto filter)
        {

            // For AddCustomerProfile Method.

            try
            {
                (bool, string) FirstNameValidate = IsValidateSpaceInString(filter.FirstName);

                if (FirstNameValidate.Item1 == false)
                {
                    return (false, FirstNameValidate.Item2 + "First Name");
                }

                (bool, string) IdentityCardValidate = IsValidateIdentityCard(filter.IdentityCard);

                if (IdentityCardValidate.Item1 == false)
                {
                    return (false, IdentityCardValidate.Item2);
                }

                (bool, string) PhoneNumberValidate = IsValidatePhoneNumber(filter.PrimaryPhone);

                if (PhoneNumberValidate.Item1 == false)
                {
                    return (false, PhoneNumberValidate.Item2);
                }

                (bool, string) LineValidate = IsValidateThaiCharactor(filter.LineID);

                if (LineValidate.Item1 == false)
                {
                    return (false, LineValidate.Item2);
                }

                (bool, string) EmailValidate = IsValidateEmail(filter.Email);

                if (EmailValidate.Item1 == false)
                {
                    return (false, EmailValidate.Item2);
                }

                return (true, "Success");

            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }
        }
        private static (bool, string) ValidationsforAddCustomerHotline(AddHotlineDto filter)
        {
            // For AddCustomerProfile Method.

            try
            {
                (bool, string) FirstNameValidate = IsValidateSpaceInString(filter.FirstName);

                if (FirstNameValidate.Item1 == false)
                {
                    return (false, FirstNameValidate.Item2 + "First Name");
                }

                (bool, string) LastNameValidate = IsValidateSpaceInString(filter.LastName);

                if (LastNameValidate.Item1 == false)
                {
                    return (false, LastNameValidate.Item2 + "Last Name");
                }


                (bool, string) PhoneNumberValidate = IsValidatePhoneNumber(filter.PrimaryPhone);

                if (PhoneNumberValidate.Item1 == false)
                {
                    return (false, PhoneNumberValidate.Item2);
                }

                (bool, string) EmailValidate = IsValidateEmail(filter.Email);

                if (EmailValidate.Item1 == false)
                {
                    return (false, EmailValidate.Item2);
                }

                return (true, "Success");

            }
            catch (Exception ex)
            {

                return (false, ex.Message);
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
        private static (bool, string) IsValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (true,"Not have an Email, But it's OK");

            try
            {
                // Validate Thai Charactor
                Regex rx = new Regex("^([ๅภถุึคตจขชๆไำพะัีรนยบลฃฟหกดเ้่าสวงผปแอิืทมใฝ๑๒๓๔ู฿๕๖๗๘๙๐ฎฑธํ๊ณฯญฐฅฤฆฏโฌ็๋ษศซฉฮฺ์ฒฬฦ])+$");

                
                MatchCollection matched = rx.Matches(email);

                if (matched.Count > 0)
                {
                    return (false, "Can not use thai charactor.");
                }

                // Validate Emaol Format
                var checkEmail = new EmailAddressAttribute();

                if (!checkEmail.IsValid(email))
                {
                    return (false,"Email Format is incorrect.");
                }

                return (true,"Success");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }
        private static (bool, string) IsValidateThaiCharactor(string LineID)
        {
            try
            {
                //Validate thai Charactor
                // Define a regular expression for repeated words.
                Regex rx = new Regex("^([/]|[\\]|[ ]|[\n]|[.]|[ๅภถุึคตจขชๆไำพะัีรนยบลฃฟหกดเ้่าสวงผปแอิืทมใฝ๑๒๓๔ู฿๕๖๗๘๙๐ฎฑธํ๊ณฯญฐฅฤฆฏโฌ็๋ษศซฉฮฺ์ฒฬฦ])+$");

                // Find matches.
                MatchCollection matched = rx.Matches(LineID);

                if (matched.Count > 0)
                {
                    return (false, "Can not use thai charactor.");
                }

                return (true, "Success");
            }
            catch (Exception ex)
            {
                return (false, ex.Message); 
                
            }

        }
        private static (bool, string) IsValidateSpaceInString(string Name)
        {
            try
            {
                if (Name == null)
                {
                    return (false, "Can not Blank in ");
                }

                if (Name.Any(char.IsWhiteSpace))
                {
                    return (false, "Can not have a space between ");
                }


                bool isBlank = Name[Name.Length - 1].ToString().Trim().Length > 0 ? true : false;

                if (isBlank == false)
                {
                    return (false, "Found Space in ");
                }

                return (true, "Success");

           }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public static (bool,string) IsValidatePhoneNumber(string PhoneNumber)
        {
            //Accepts only 10 digits, no more no less. 
            try
            {
                string pattern = @"(?<!\d)\d{10}(?!\d)";
                // Create a Regex  
                Regex rg = new Regex(pattern);

                // Get all matches  
                MatchCollection matched = rg.Matches(PhoneNumber);

                if (matched.Count == 0)
                {
                    return (false,"Phone Number format is not correct");
                }

                return (true,"Success");
               
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }
        public static (bool, string) IsValidateIdentityCard(string idNumber)
        {
            //Accepts only 10 digits, no more no less. 
            try
            {
                string pattern = @"(?<!\d)\d{13}(?!\d)";
                // Create a Regex  
                Regex rg = new Regex(pattern);

                // Get all matches  
                MatchCollection matched = rg.Matches(idNumber);

                if (matched.Count == 0)
                {
                    return (false, "IdentityCard Number format is not correct");
                }

                return (true, "Success");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }

        public async Task<ServiceResponseWithPagination<List<GetProfileDto>>> PureAPI()
        {
            try
            {
                //" Enum to Query it's Work"
                //" List to List  it's Work"
                //" Enum to Enum  it's Not Work b'cause datareader open also."

                // Return IEnumerable
                var enumTable = _dbContext.Policy_Snapshots
                    //.AsQueryable();
                    //.ToList();
                    .AsEnumerable();


                // Return IQueryable
                var queryTable = _dbContext.Payer_Snapshots
                    .AsQueryable();
                    //.ToList();
                    //.AsEnumerable();

                #region "Example 1"
                //var result = queryTable
                //            .Join(enumTable
                //            , q => q.PersonId
                //            , e => e.PayerPersonId
                //            , (q, e) => new GetProfileDto
                //            {
                //                PersonId = q.PersonId,
                //                FirstName = e.PayerName,
                //                LastName = q.LastName,
                //                TitleId = e.CustPersonId,
                //                LineID = e.ApplicationCode,
                //            }).AsQueryable();
                #endregion

                #region "Example 2"
                //Example 2
                var result =
                           (from q in queryTable
                            join e in enumTable
                            on q.PersonId equals e.PayerPersonId
                            select new GetProfileDto
                            {
                                PersonId = q.PersonId,
                                FirstName = e.PayerName,
                                LastName = q.LastName,
                                TitleId = e.CustPersonId,
                                LineID = e.ApplicationCode,
                                OrderingField = "LineID",
                                AscendingOrder = true,
                            } 
                           ) 
                           .AsQueryable();
                #endregion

                #region "Filter"
                //filter
                //result = result.Where(x => x.LineID.Contains("MT6403")); //MT64030009

                //if (!string.IsNullOrWhiteSpace("LineID"))
                //{
                //    try
                //    {
                //        GetProfileDto gdto = new GetProfileDto
                //        {
                //            OrderingField = "LineID",
                //            AscendingOrder = true,
                //        };

                //        result = result.OrderBy($"{gdto.OrderingField} {(gdto.AscendingOrder ? "ascending" : "descending")}");

                //    }
                //    catch
                //    {
                //        return ResponseResultWithPagination.Failure<List<GetProfileDto>>("Ordering Fail");
                //    }
                //}
                #endregion

                var pagination = await _httpcontext.HttpContext.InsertPaginationParametersInResponse(result, 1, 1);
                PaginationDto pdto = new PaginationDto();
                var dto = result.Paginate(pdto).ToList();
                return ResponseResultWithPagination.Success(dto, pagination);

            }
            catch (Exception ex)
            {
                return ResponseResultWithPagination.Failure<List<GetProfileDto>>(ex.Message);

            }
        }
    }
}
