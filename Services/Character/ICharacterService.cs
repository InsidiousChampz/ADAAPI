using System.Collections.Generic;
using System.Threading.Tasks;
using TEMPLETEAPI.DTOs;
using TEMPLETEAPI.DTOs.Fight;
using TEMPLETEAPI.Models;

namespace TEMPLETEAPI.Services.Character
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        Task<ServiceResponse<List<GetCharaterSkillDto>>> GetAllCharacterSkills();
        Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills();
        Task<ServiceResponse<List<GetWeaponDto>>> GetAllWeapons();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int characterId);
        Task<ServiceResponse<GetSkillDto>> GetSkillById(int skillId);
        Task<ServiceResponse<GetWeaponDto>> GetWeaponrById(int weaponId);
        Task<ServiceResponse<GetCharacterDto>> AddCharacter(AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharSkill);
        Task<ServiceResponse<GetSkillDto>> AddSkill(AddSkillDto newSkill);
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
        Task<ServiceResponse<GetCharacterDto>> DeleteCharacterById(int characterId);
        Task<ServiceResponse<GetSkillDto>> DeleteSkillById(int skillId);
        Task<ServiceResponse<GetWeaponDto>> DeleteWeaponById(int weaponId);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacterById(int characterId, UpdateCharacterDto updateCharacter);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacterSkillById(int characterId, UpdateCharacterSkillDto updateCharacterSkill);
        Task<ServiceResponse<GetSkillDto>> UpdateSkillById(int skillId, UpdateSkillDto updateSkill);
        Task<ServiceResponse<GetWeaponDto>> UpdateWeaponById(int weaponId, UpdateWeaponDto updateWeapon);


        //Mark Learning Day2.
        Task<ServiceResponse<AttackResultDto>> WeaponAtk(WeaponAttackDto request);
        Task<ServiceResponse<AttackResultDto>> SkillAtk(SkillAttackDto request);
        Task<ServiceResponse<GetCharacterDto>> RemoveWeapon(int characterId);
        Task<ServiceResponse<GetCharacterDto>> RemoveSkill(int characterId);
    }
}