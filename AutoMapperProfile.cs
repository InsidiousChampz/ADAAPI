using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEMPLETEAPI.DTOs;
using TEMPLETEAPI.Models;

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

            //Test
        }
    }
}