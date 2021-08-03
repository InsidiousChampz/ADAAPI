using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmsUpdateCustomer_Api.DTOs;
using SmsUpdateCustomer_Api.DTOs.Product;
using SmsUpdateCustomer_Api.Models;
using SmsUpdateCustomer_Api.Models.Product;
using SmsUpdateCustomer_Api.DTOs.Order;
using SmsUpdateCustomer_Api.Models.Order;
using SmsUpdateCustomer_Api.Models.Customer_Infomations;
using SmsUpdateCustomer_Api.DTOs.Customer_Infomations;
using SmsUpdateCustomer_Api.Models.Customer_Snapshots;
using SmsUpdateCustomer_Api.DTOs.Customer;
using SmsUpdateCustomer_Api.Models.Customer_Profiles;
using SmsUpdateCustomer_Api.DTOs.Customer_Profiles;
using SmsUpdateCustomer_Api.DTOs.Admin;

namespace SmsUpdateCustomer_Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Customer Profile            
            
            CreateMap<Customer_NewProfile, GetMergeDto>();
            CreateMap<Customer_NewProfile, GetProfileDto>();
            CreateMap<Customer_NewProfile, AddProfileDto>();


            // Customer infomations
            CreateMap<Customer_Header, GetCompareLoginDto>();
            CreateMap<Customer_Header, GetCustomerHeaderDto>();
            CreateMap<Customer_Detail, GetCustomerHeaderDto>();

            //Policy_Snapshot
            CreateMap<Policy_Snapshot, GetPolicyDto>();

            //Payer_Snapshot
            CreateMap<Payer_Snapshot, GetEditCustomerByFilterDto>();

            CreateMap<Payer_Snapshot, GetPayerDto>();
            CreateMap<Payer_Snapshot, GetCustomerDto>();
            CreateMap<Payer_Snapshot, GetByIdentityAndLastNameDto>();

            //Customer_Snapshot
            CreateMap<Customer_Snapshot, GetProfileDto>();

            //Customer_Transaction
            
            CreateMap<Customer_Profile_Transaction, GetHistoryCustomerDto>();
            CreateMap<Customer_Profile_Transaction, GetProfileTransaction>();
            CreateMap<Customer_Profile_Transaction, GetProfileTransaction>();

            


        }
    }
}
