using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ADAAPI.Validations
{
    public class ControlValidator
    {
        
        
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
