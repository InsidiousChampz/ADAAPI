using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TEMPLETEAPI.DTOs;
using TEMPLETEAPI.Services.Character;

namespace TEMPLETEAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _charService;

        public CharacterController(ICharacterService charService)
        {
            _charService = charService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCharacters()
        {
            return Ok(await _charService.GetAllCharacters());
        }

        [HttpGet("characterskill")]
        public async Task<IActionResult> GetAllCharacterSkills()
        {
            return Ok(await _charService.GetAllCharacterSkills());
        }

        [HttpGet("skill")]
        public async Task<IActionResult> GetAllSkills()
        {
            return Ok(await _charService.GetAllSkills());
        }

        [HttpGet("weapon")]
        public async Task<IActionResult> GetAllWeapons()
        {
            return Ok(await _charService.GetAllWeapons());
        }

        [HttpGet("characterId")]
        public async Task<IActionResult> GetCharacterById(int characterId)
        {
            return Ok(await _charService.GetCharacterById(characterId));
        }

        [HttpPost("addweapon")]
        public async Task<IActionResult> AddWeapon(AddWeaponDto newWeapon)
        {
            return Ok(await _charService.AddWeapon(newWeapon));
        }

        [HttpPost("addcharacter")]
        public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _charService.AddCharacter(newCharacter));
        }

        [HttpPost("addcharacterskill")]
        public async Task<IActionResult> AddCharacterSkill(AddCharacterSkillDto newCharSkill)
        {
            return Ok(await _charService.AddCharacterSkill(newCharSkill));
        }

        [HttpPost("addskill")]
        public async Task<IActionResult> AddSkill(AddSkillDto newSkill)
        {
            return Ok(await _charService.AddSkill(newSkill));
        }

        [HttpDelete("deletecharacter")]
        public async Task<IActionResult> DeleteCharacterById(int characterId)
        {

            return Ok(await _charService.DeleteCharacterById(characterId));

        }

        [HttpDelete("deleteskill")]
        public async Task<IActionResult> DeleteSkillById(int skillId)
        {

            return Ok(await _charService.DeleteSkillById(skillId));

        }

        [HttpDelete("deleteweapon")]
        public async Task<IActionResult> DeleteweaponById(int weaponId)
        {

            return Ok(await _charService.DeleteWeaponById(weaponId));

        }

        [HttpPost("updatecharacter")]
        public async Task<IActionResult> UpdateCharacterById(int characterId, UpdateCharacterDto updateCharacter)
        {
            return Ok(await _charService.UpdateCharacterById(characterId, updateCharacter));
        }

        [HttpPost("updatecharacterskill")]
        public async Task<IActionResult> UpdateCharacterSkillById(int characterId, UpdateCharacterSkillDto updateCharacterSkill)
        {
            return Ok(await _charService.UpdateCharacterSkillById(characterId, updateCharacterSkill));
        }

        [HttpPost("updateskill")]
        public async Task<IActionResult> UpdateSkillById(int skillId, UpdateSkillDto updateSkill)
        {
            return Ok(await _charService.UpdateSkillById(skillId, updateSkill));
        }

        [HttpPost("updateweapon")]
        public async Task<IActionResult> UpdateWeaponById(int weaponId, UpdateWeaponDto updateWeapon)
        {
            return Ok(await _charService.UpdateWeaponById(weaponId, updateWeapon));
        }



    }
}