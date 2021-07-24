using CustomerProFileAPI.DTOs.Customer_Profiles;
using CustomerProFileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.Services.Customer_Profiles
{
    public interface ICustomerProfileServices
    {
        Task<ServiceResponse<GetProfileDto>> NewCustomerProfile(int personId);
    }
}
