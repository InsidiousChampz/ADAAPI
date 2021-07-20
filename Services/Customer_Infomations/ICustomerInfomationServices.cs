using CustomerProFileAPI.DTOs.Customer_Infomations;
using CustomerProFileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.Services.Customer_Infomations
{
    public interface ICustomerInfomationServices
    {
        Task<ServiceResponse<List<GetCustomerHeaderDto>>> GetCustomerLogin();

        Task<ServiceResponse<GetCustomerHeaderDto>> GetCustomerLoginByIdentityAndLastname(GetCustomerHeaderWithFilter filter);
    }
}
