using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STANDARDAPI.DTOs;
using STANDARDAPI.DTOs.Product;
using STANDARDAPI.Models;
using STANDARDAPI.Models.Product;
using STANDARDAPI.DTOs.Order;
using STANDARDAPI.Models.Order;

namespace STANDARDAPI
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