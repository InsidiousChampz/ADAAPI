using CustomerProFileAPI.DTOs.Customer;
using CustomerProFileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.Services.Customer
{
    public interface ICustomerServices
    {
        Task<ServiceResponse<GetCustomerDto>> GetCustomerByPersonId(int personId);

        Task<ServiceResponse<List<GetCustomerListDto>>> GetCustomerAndPoliciesByPersonId(int personId);
    }
}
