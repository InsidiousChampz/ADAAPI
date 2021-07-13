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

namespace CustomerProFileAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Auth
            CreateMap<User, UserDto>();
            CreateMap<Role, RoleDto>()
                .ForMember(x => x.RoleName, x => x.MapFrom(x => x.Name));
            CreateMap<RoleDtoAdd, Role>()
                .ForMember(x => x.Name, x => x.MapFrom(x => x.RoleName));
            CreateMap<UserRole, UserRoleDto>();

            //Product
            CreateMap<Product, GetProductDto>();
            //ProductGroup
            CreateMap<ProductGroup, GetProductGroupDto>();
            //ProductAudit
            CreateMap<ProductAudit, GetProductAuditDto>();
            //ProductAuditType
            CreateMap<ProductAuditType, GetProductAuditTypeDto>();
            //OrderList
            CreateMap<OrderList, GetOrderListDto>();
            //Order
            CreateMap<Order, GetOrderDto>();
        }
    }
}