using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEMPLETEAPI.DTOs;
using TEMPLETEAPI.DTOs.Product;
using TEMPLETEAPI.Models;
using TEMPLETEAPI.Models.Product;

namespace TEMPLETEAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Character
            CreateMap<Character, GetCharacterDto>().ForMember(x => x.Skills, x => x.MapFrom(x => x.CharacterSkills.Select(cs => cs.Skill)));
            //Character Skill
            CreateMap<CharacterSkill, GetCharaterSkillDto>();
            //Skill
            CreateMap<Skill, GetSkillDto>();
            //Weapon
            CreateMap<Weapon, GetWeaponDto>();

            //Product
            CreateMap<Product, GetProductDto>();

            //ProductGroup
            CreateMap<ProductGroup, GetProductGroupDto>();
        }
    }
}