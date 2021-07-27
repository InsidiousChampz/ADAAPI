using SmsUpdateCustomer_Api.DTOs.Customer_Infomations;
using SmsUpdateCustomer_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Services.Customer_Infomations
{
    public interface ICustomerInfomationServices
    {
        Task<ServiceResponse<List<GetCustomerHeaderDto>>> GetCustomerLogin();

        Task<ServiceResponse<GetCustomerHeaderDto>> GetCustomerLoginByIdentityAndLastname(GetCustomerHeaderWithFilter filter);
    }
}
