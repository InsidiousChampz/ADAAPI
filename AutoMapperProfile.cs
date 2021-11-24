using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADAAPI.DTOs;
using ADAAPI.Models;
using ADAAPI.DTOs.Admin;


namespace ADAAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            
            //Admin
            CreateMap<tbl_A, GetTableDTO>().ReverseMap();
            CreateMap<tbl_B, GetTableDTO>().ReverseMap();
            CreateMap<tbl_A, AddTableDto>().ReverseMap();
            CreateMap<tbl_B, AddTableDto>().ReverseMap();
            CreateMap<tbl_A, UpdateTableDTO>().ReverseMap();
            CreateMap<tbl_B, UpdateTableDTO>().ReverseMap();
        }
    }
}
