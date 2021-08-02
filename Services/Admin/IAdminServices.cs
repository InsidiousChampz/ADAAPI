using SmsUpdateCustomer_Api.DTOs.Admin;
using SmsUpdateCustomer_Api.DTOs.Customer;
using SmsUpdateCustomer_Api.DTOs.Customer_Profiles;
using SmsUpdateCustomer_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Services.Admin
{
    public interface IAdminServices
    {
        Task<ServiceResponseWithPagination<List<GetEditCustomerDto>>> GetEditedCustomer(GetEditCustomerByFilterDto filter);
        Task<ServiceResponse<List<GetHistoryCustomerDto>>> GetCustomerHistory(int personId);
        Task<ServiceResponse<List<GetCompareDto>>> GetCompareDataOfCustomer(int personId);
        Task<ServiceResponse<List<GetMergeDto>>> GetMergeDataOfCustomer(int personId);
        Task<ServiceResponse<ConfirmAdminDto>> ConfirmByAdmin(ConfirmAdminDto confirm);


    }
}
