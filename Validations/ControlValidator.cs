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
                    return (false, "ไม่พบบัตรประชาชน.");
                }

                if (filter.LoginLastName == null)
                {
                    return (false, "ไม่พบนามสกุล.");
                }

                bool checkIdentityNumberIsNumberOnly = Microsoft.VisualBasic.Information.IsNumeric(filter.LoginIdentityCard);

                bool checklastNameIsStringOnly = IsDigitsOnly(filter.LoginLastName);

                if (checkIdentityNumberIsNumberOnly == false)
                {
                    return (false, "พบอักระอยู่ในหมายเลขบัตรประชาชน.");
                }

                if (checklastNameIsStringOnly == false)
                {
                    return (false, "พบตัวเลขในนามสกุล.");
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
                    return (false, "ไม่พบหมายเลขบัตรประชาชน");
                }

                if (filter.LastName == null)
                {
                    return (false, "ไม่พบนามสกุล");
                }

                bool checkIdentityNumberIsNumberOnly = Microsoft.VisualBasic.Information.IsNumeric(filter.IdentityCard);

                bool checklastNameIsStringOnly = IsDigitsOnly(filter.LastName);

                if (checkIdentityNumberIsNumberOnly == false)
                {
                    return (false, "พบอักขระในหมายเลขบัตรประชาชน");
                }

                if (checklastNameIsStringOnly == false)
                {
                    return (false, "ในนามสกุล พบ ตัวเลข");
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
                //(bool, string) FirstNameValidate = IsValidateSpaceInString(filter.FirstName);

                //if (FirstNameValidate.Item1 == false)
                //{
                //    return (false, FirstNameValidate.Item2 + " ชื่อ");
                //}

                //(bool, string) FirstNameSpaceValidate = IsValidateSpaceBeforeString(filter.FirstName);

                //if (FirstNameSpaceValidate.Item1 == false)
                //{
                //    return (false, FirstNameSpaceValidate.Item2 + " ชื่อ");
                //}

                //(bool, string) LastNameValidate = IsValidateSpaceInString(filter.LastName);
                
                //if (LastNameValidate.Item1 == false)
                //{
                //    return (false, LastNameValidate.Item2 + " นามสกุล");
                //}

                //(bool, string) LastNameValidate = IsValidateSpaceBeforeString(filter.LastName);

                //if (LastNameValidate.Item1 == false)
                //{
                //    return (false, LastNameValidate.Item2 + " นามสกุล");
                //}

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

                (bool, string) checkPhoneIsDigitOnly = IsPhoneDigitsOnly(filter.PrimaryPhone);

                if (checkPhoneIsDigitOnly.Item1 == false)
                {
                    return (false, checkPhoneIsDigitOnly.Item2);
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
                    return (false, FirstNameValidate.Item2 + " ชื่อ");
                }

                (bool, string) LastNameValidate = IsValidateSpaceInString(filter.LastName);

                if (LastNameValidate.Item1 == false)
                {
                    return (false, LastNameValidate.Item2 + " นามสกุล");
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
                return (true, "ไม่พบอีเมล์");

            try
            {
                // Validate Thai Charactor
                Regex rx = new Regex("^([ๅภถุึคตจขชๆไำพะัีรนยบลฃฟหกดเ้่าสวงผปแอิืทมใฝ๑๒๓๔ู฿๕๖๗๘๙๐ฎฑธํ๊ณฯญฐฅฤฆฏโฌ็๋ษศซฉฮฺ์ฒฬฦ])+$");


                //foreach (var item in collection)
                //{

                //}

                MatchCollection matched = rx.Matches(email);

                if (matched.Count > 0)
                {
                    return (false, "ไม่สามารถระบุภาษาไทยลงไปได้.");
                }

                // Validate Email Format
                var checkEmail = new EmailAddressAttribute();

                if (!checkEmail.IsValid(email))
                {
                    return (false, "อีเมล์ไม่ถูกต้อง");
                }

                return (true, "Success");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }
        public static (bool, string) IsValidateThaiCharactor(string Data)
        {
            try
            {
                //Validate thai Charactor
                // Define a regular expression for repeated words.
                Regex rx = new Regex("^([/]|[\\]|[ ]|[\n]|[.]|[ๅภถุึคตจขชๆไำพะัีรนยบลฃฟหกดเ้่าสวงผปแอิืทมใฝ๑๒๓๔ู฿๕๖๗๘๙๐ฎฑธํ๊ณฯญฐฅฤฆฏโฌ็๋ษศซฉฮฺ์ฒฬฦ])+$");

                // Find matches.
                MatchCollection matched = rx.Matches(Data);

                if (matched.Count > 0)
                {
                    return (false, "ไม่สามารถใช้ภาษาไทยได้.");
                }

                return (true, "Success");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);

            }

        }
        public static (bool, string) IsValidateSpaceBeforeString(string Name)
        {
            try
            {
                if (Char.IsWhiteSpace(Name, 0))
                {
                    return (false, "พบค่าว่างอยู่ข้างหน้า ");
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
                    return (false, "ไม่สามารถมีค่าว่างใน");
                }

                if (Name.Any(char.IsWhiteSpace))
                {
                    return (false, "ไม่สามารถมีค่าว่างระหว่าง");
                }


                bool isBlank = Name[Name.Length - 1].ToString().Trim().Length > 0 ? true : false;

                if (isBlank == false)
                {
                    return (false, "พบค่าว่างใน");
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
                    return (false, "หมายเลขโทรศัพท์ไม่ถูกต้อง");
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
                    return (false, "หมายเลขบัตรประชาชนไม่ถูกต้อง");
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
        public static (bool, string) IsPhoneDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(c))
                {
                    
                }
                else
                {
                    //if (!Char.IsWhiteSpace(c))
                    //{
                    //    return (false, "พบอักขระในหมายเลขโทรศัพท์");
                    //}

                    return (false, "พบอักขระในหมายเลขโทรศัพท์");

                }
            }

            return (true, "Success");
        }



    }
}
