using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerProFileAPI.DTOs;
using CustomerProFileAPI.DTOs.Product;
using CustomerProFileAPI.Models;
using CustomerProFileAPI.Models.Product;
using CustomerProFileAPI.DTOs.Order;
using CustomerProFileAPI.Models.Order;
using CustomerProFileAPI.Models.Customer_Infomations;
using CustomerProFileAPI.DTOs.Customer_Infomations;
using CustomerProFileAPI.Models.Customer_Snapshots;
using CustomerProFileAPI.DTOs.Customer;

namespace CustomerProFileAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Customer infomations
            CreateMap<Customer_Header, GetCustomerHeaderDto>();
            CreateMap<Customer_Detail, GetCustomerHeaderDto>();



            //Customer_Snapshot
            CreateMap<Policy_Snapshot, GetPolicyDto>();
            CreateMap<Customer_Snapshot, GetCustomerDto>();
            CreateMap<Customer_Snapshot, GetCustomerListDto>();


            //Policy_Snapshot
            //CreateMap<Policy_Snapshot, GetPolicyDto>();

            ////Product
            //CreateMap<Product, GetProductDto>();
            ////ProductGroup
            //CreateMap<ProductGroup, GetProductGroupDto>();
            ////ProductAudit
            //CreateMap<ProductAudit, GetProductAuditDto>();
            ////ProductAuditType
            //CreateMap<ProductAuditType, GetProductAuditTypeDto>();
            ////OrderList
            //CreateMap<OrderList, GetOrderListDto>();
            ////Order
            //CreateMap<Order, GetOrderDto>();
        }
    }
}
