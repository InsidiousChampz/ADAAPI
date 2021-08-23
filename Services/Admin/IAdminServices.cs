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
        //UI7
        Task<ServiceResponseWithPagination<List<GetEditCustomerDto>>> GetCustomerbyAdmin(GetEditCustomerByFilterDto filter);
        Task<ServiceResponse<GetCompareDto>> GetAdminEditRecord(int personId);
        Task<ServiceResponse<List<GetHistoryCustomerDto>>> GetCustomerHistory(int personId);
        Task<ServiceResponse<List<GetHistoryAdminDto>>> GetAdminHistory(int personId);
        Task<ServiceResponse<List<GetCompareDto>>> GetCompareDataOfCustomer(int personId);
        Task<ServiceResponse<List<GetCompareDto>>> GetCompareDataOfCustomerByAdmin(int personId);
        Task<ServiceResponse<int>> GetCountEditRecordByAdmin(int personId);
        Task<ServiceResponse<bool>> GetDoneEditRecordByAdmin(int personId);

        Task<ServiceResponseWithPagination<List<GetPolicyCustomerDataDto>>> GetPolicyDataOfCustomer(GetPolicyCustomerFilterDto filter);

        Task<ServiceResponseWithPagination<List<GetMergeDto>>> GetMergeDataOfCustomerByFilter(GetMergeByFilterDto filter);
        Task<ServiceResponse<List<GetMergeDto>>> GetMergeDataOfCustomer(int personId);
        Task<ServiceResponse<UpdateMergeDto>> UpdateMergeDataOfCustomer(string userId, UpdateMergeDto update);
        Task<ServiceResponse<ConfirmAdminDto>> ConfirmByAdmin(int personId);
        Task<ServiceResponse<GetProfileDto>> AddUpdateTempCustomerProfilebyAdmin(string userId, AddProfileDto newProfile);

        //UI8
        Task<ServiceResponseWithPagination<List<GetEditCustomerDto>>> GetCustomerbyCallCenter(GetEditCustomerByFilterDto filter);
        Task<ServiceResponse<List<GetCompareDto>>> GetCompareDataAllCustomer(int personId);

        //UI9
        Task<ServiceResponseWithPagination<List<GetLoginCustomerDto>>> GetLoginbyCallCenter(GetEditCustomerByFilterDto filter);
        Task<ServiceResponse<GetCompareLoginDto>> GetLoginOfCustomer(int personId);
        Task<ServiceResponse<GetCompareLoginDto>> UpdateLoginOfCustomer(GetCompareLoginDto update);

      


    }
}
