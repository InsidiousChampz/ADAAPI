using SmsUpdateCustomer_Api.DTOs.Customer;
using SmsUpdateCustomer_Api.DTOs.Customer_Infomations;
using SmsUpdateCustomer_Api.DTOs.Customer_Profiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Validations
{
    public class ControlValidator
    {
        
        public static (bool, string) ValidationsforCustomerLogin(GetCustomerHeaderWithFilter filter)
        {

            // For GetCustomerLoginByIdentityAndLastname Method.

            try
            {

                if (filter.LoginIdentityCard == null)
                {
                    return (false, "IdentityCard is invalid.");
                }

                if (filter.LoginLastName == null)
                {
                    return (false, "LastName is invalid.");
                }

                bool checkIdentityNumberIsNumberOnly = Microsoft.VisualBasic.Information.IsNumeric(filter.LoginIdentityCard);

                bool checklastNameIsStringOnly = IsDigitsOnly(filter.LoginLastName);

                if (checkIdentityNumberIsNumberOnly == false)
                {
                    return (false, "IdentityCard Number have a charactor.");
                }

                if (checklastNameIsStringOnly == false)
                {
                    return (false, "Lastname have a number.");
                }

                (bool, string) check13Digit = IsValidateIdentityCard(filter.LoginIdentityCard);

                if (check13Digit.Item1 == false)
                {
                    return (false, check13Digit.Item2);
                }

                return (true, "Success");

            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }
        }
        public static (bool, string) ValidationsforPayerAndPoliciesByIdentityAndLastName(GetByIdentityAndLastNameDto filter)
        {

            // For GetPayerAndPoliciesByIdentityAndLastName Method.

            try
            {

                if (filter.IdentityCard == null)
                {
                    return (false, "IdentityCard is invalid.");
                }

                if (filter.LastName == null)
                {
                    return (false, "LastName is invalid.");
                }

                bool checkIdentityNumberIsNumberOnly = Microsoft.VisualBasic.Information.IsNumeric(filter.IdentityCard);

                bool checklastNameIsStringOnly = IsDigitsOnly(filter.LastName);

                if (checkIdentityNumberIsNumberOnly == false)
                {
                    return (false, "IdentityCard Number have a charactor.");
                }

                if (checklastNameIsStringOnly == false)
                {
                    return (false, "Lastname have a number.");
                }

                (bool, string) check13Digit = IsValidateIdentityCard(filter.IdentityCard);

                if (check13Digit.Item1 == false)
                {
                    return (false, check13Digit.Item2);
                }

                return (true, "Success");

            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }
        }
        public static (bool, string) ValidationsforAddCustomerProfile(AddProfileDto filter)
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
        public static (bool, string) ValidationsforAddCustomerHotline(AddHotlineDto filter)
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
        public static (bool, string) IsValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (true, "Not have an Email, But it's OK");

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
                    return (false, "Email Format is incorrect.");
                }

                return (true, "Success");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }
        public static (bool, string) IsValidateThaiCharactor(string LineID)
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
        public static (bool, string) IsValidateSpaceInString(string Name)
        {
            try
            {
                if (Name == null || Name.Length == 0)
                {
                    return (false, "Can not blank space in ");
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
        public static (bool, string) IsValidatePhoneNumber(string PhoneNumber)
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
                    return (false, "Phone Number format is not correct");
                }

                return (true, "Success");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }
        public static (bool, string) IsValidateIdentityCard(string idNumber)
        {
            //Accepts only 13 digits, no more no less. 
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
        public static bool IsDigitsOnly(string str)
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


    }
}
