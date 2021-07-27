using SmsUpdateCustomer_Api.DTOs.Customer;
using SmsUpdateCustomer_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Services.Customer
{
    public interface ICustomerServices
    {
        Task<ServiceResponse<GetPayerDto>> GetPayerByPersonId(int personId);

        Task<ServiceResponse<List<GetPayerDto>>> GetPayerAndPoliciesByPersonId(int personId);

        Task<ServiceResponse<List<GetPayerDto>>> GetPayerAndPoliciesByIdentityAndLastName(GetByIdentityAndLastNameDto filter);

    }
}
