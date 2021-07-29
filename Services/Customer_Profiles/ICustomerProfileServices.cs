using SmsUpdateCustomer_Api.DTOs.Customer_Profiles;
using SmsUpdateCustomer_Api.Models;
using SmsUpdateCustomer_Api.Models.Customer_Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Services.Customer_Profiles
{
    public interface ICustomerProfileServices
    {
        Task<ServiceResponse<GetProfileDto>> GetCustomerProfile(int personId);
        Task<ServiceResponse<GetProfileDto>> AddCustomerProfile(AddProfileDto newProfile);
        Task<ServiceResponse<GetHotlineDto>> AddCustomerHotline(AddHotlineDto newhotline);
        Task<ServiceResponse<List<GetProfileDto>>> ConfirmCustomerProfile(int EditorId);

        Task<ServiceResponseWithPagination<List<GetProfileDto>>> PureAPI();
    }
}
