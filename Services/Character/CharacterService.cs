using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TEMPLETEAPI.Models;
using TEMPLETEAPI.DTOs;
using TEMPLETEAPI.Data;

namespace TEMPLETEAPI.Services.Character
{
    public class CharacterService : ICharacterService
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _log;

        public CharacterService(AppDBContext dBContext, IMapper mapper, ILogger<CharacterService> log)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _log = log;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var characters = await _dbContext.Characters
            .Include(x => x.Weapon)
            .Include(x => x.CharacterSkills).ThenInclude(x => x.Skill)
            .AsNoTracking()
            .ToListAsync();
            var dto = _mapper.Map<List<GetCharacterDto>>(characters);
            return ResponseResult.Success(dto);
        }

        public async Task<ServiceResponse<List<GetCharaterSkillDto>>> GetAllCharacterSkills()
        {
            var characterSkill = await _dbContext.CharacterSkills
            .AsNoTracking()
            .ToListAsync();
            var dto = _mapper.Map<List<GetCharaterSkillDto>>(characterSkill);
            return ResponseResult.Success(dto);
        }

        public async Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills()
        {
            var Skill = await _dbContext.Skills
            .AsNoTracking()
            .ToListAsync();
            var dto = _mapper.Map<List<GetSkillDto>>(Skill);
            return ResponseResult.Success(dto);
        }

        public async Task<ServiceResponse<List<GetWeaponDto>>> GetAllWeapons()
        {
            var Weapon = await _dbContext.Weapons
           .AsNoTracking()
           .ToListAsync();
            var dto = _mapper.Map<List<GetWeaponDto>>(Weapon);
            return ResponseResult.Success(dto);
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int characterId)
        {
            var characters = await _dbContext.Characters
            .Include(x => x.Weapon)
            .Include(x => x.CharacterSkills).ThenInclude(x => x.Skill)
            .FirstOrDefaultAsync(x => x.Id == characterId);
            if (characters == null)
            {
                return ResponseResult.Failure<GetCharacterDto>("Character Not Found.");
            }
            else
            {
                var dto = _mapper.Map<GetCharacterDto>(characters);
                return ResponseResult.Success(dto);
            }

        }

        public async Task<ServiceResponse<GetSkillDto>> GetSkillById(int skillId)
        {
            var Skill = await _dbContext.Skills
            .FirstOrDefaultAsync(x => x.Id == skillId);
            if (Skill == null)
            {
                return ResponseResult.Failure<GetSkillDto>("Skill Not Found.");
            }
            else
            {
                var dto = _mapper.Map<GetSkillDto>(Skill);
                return ResponseResult.Success(dto);
            }

        }

        public async Task<ServiceResponse<GetWeaponDto>> GetWeaponrById(int weaponId)
        {
            var weapon = await _dbContext.Weapons
            .FirstOrDefaultAsync(x => x.Id == weaponId);
            if (weapon == null)
            {
                return ResponseResult.Failure<GetWeaponDto>("Weapon Not Found.");
            }
            else
            {
                var dto = _mapper.Map<GetWeaponDto>(weapon);
                return ResponseResult.Success(dto);
            }
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacter(AddCharacterDto newCharacter)
        {

            var _character = new TEMPLETEAPI.Models.Character
            {
                Name = newCharacter.Name,
                HitPoints = newCharacter.HitPoints,
                Strength = newCharacter.Strength,
                Defense = newCharacter.Defense,
                Intelligence = newCharacter.Intelligence
            };

            _dbContext.Characters.Add(_character);
            await _dbContext.SaveChangesAsync();
            var dto = _mapper.Map<GetCharacterDto>(_character);
            return ResponseResult.Success(dto);

        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharSkill)
        {
            _log.LogInformation("Start Add Character Skill Process");
            var character = await _dbContext.Characters
            .Include(x => x.Weapon)
            .Include(x => x.CharacterSkills).ThenInclude(x => x.Skill)
            .FirstOrDefaultAsync(x => x.Id == newCharSkill.CharacterId);

            if (character == null)
            {
                _log.LogInformation("Character not found.");
                return ResponseResult.Failure<GetCharacterDto>("Character not found.");
            }
            _log.LogInformation("Character  found.");
            var skill = await _dbContext.Skills.FirstOrDefaultAsync(x => x.Id == newCharSkill.SkillId);
            if (skill == null)
            {
                _log.LogInformation("Skill not found.");
                return ResponseResult.Failure<GetCharacterDto>("Skill not found.");
            }
            _log.LogInformation("Skill found.");

            var characterSkill = new CharacterSkill
            {
                Character = character,
                Skill = skill
            };

            _dbContext.CharacterSkills.Add(characterSkill);
            await _dbContext.SaveChangesAsync();
            _log.LogInformation("Success");
            var dto = _mapper.Map<GetCharacterDto>(character);
            _log.LogInformation("End");
            return ResponseResult.Success(dto);
        }

        public async Task<ServiceResponse<GetSkillDto>> AddSkill(AddSkillDto newSkill)
        {
            var Checkskill = await _dbContext.Skills
            .Include(x => x.CharacterSkills).ThenInclude(x => x.Skill)
            .FirstOrDefaultAsync(x => x.Name == newSkill.Name);
            if (Checkskill == null)
            {

                var _skill = new Skill
                {
                    Name = newSkill.Name,
                    Damage = newSkill.Damage,

                };

                _dbContext.Skills.Add(_skill);
                await _dbContext.SaveChangesAsync();

                var dto = _mapper.Map<GetSkillDto>(_skill);
                return ResponseResult.Success(dto);

            }

            return ResponseResult.Failure<GetSkillDto>("Skill Already Add.");


        }

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var characters = await _dbContext.Characters
                     .Include(x => x.Weapon)
                     .Include(x => x.CharacterSkills).ThenInclude(x => x.Skill)
                     .FirstOrDefaultAsync(x => x.Id == newWeapon.CharacterId);
            if (characters == null)
            {
                return ResponseResult.Failure<GetCharacterDto>("Character Not Found.");
            }
            else
            {
                var weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    CharacterId = newWeapon.CharacterId
                };

                _dbContext.Weapons.Add(weapon);
                await _dbContext.SaveChangesAsync();

                var dto = _mapper.Map<GetCharacterDto>(characters);
                return ResponseResult.Success(dto);

            }
        }

        public async Task<ServiceResponse<GetCharacterDto>> DeleteCharacterById(int characterId)
        {
            var characters = await _dbContext.Characters
            .Include(x => x.Weapon)
            .Include(x => x.CharacterSkills).ThenInclude(x => x.Skill)
            .FirstOrDefaultAsync(x => x.Id == characterId);
            if (characters == null)
            {
                return ResponseResult.Failure<GetCharacterDto>("Character Not Found.");
            }
            else
            {
                _dbContext.Characters.Remove(characters);
                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<GetCharacterDto>(characters);
                return ResponseResult.Success(dto);
            }

        }

        public async Task<ServiceResponse<GetSkillDto>> DeleteSkillById(int skillId)
        {
            var skill = await _dbContext.Skills
            .Include(x => x.CharacterSkills).ThenInclude(x => x.Skill)
            .FirstOrDefaultAsync(x => x.Id == skillId);
            if (skill == null)
            {
                return ResponseResult.Failure<GetSkillDto>("Skill Not Found.");
            }
            else
            {
                _dbContext.Skills.Remove(skill);
                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<GetSkillDto>(skill);
                return ResponseResult.Success(dto);
            }

        }

        public async Task<ServiceResponse<GetWeaponDto>> DeleteWeaponById(int weaponId)
        {
            var weapon = await _dbContext.Weapons.FirstOrDefaultAsync(x => x.Id == weaponId);
            if (weapon == null)
            {
                return ResponseResult.Failure<GetWeaponDto>("Skill Not Found.");
            }
            else
            {
                _dbContext.Weapons.Remove(weapon);
                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<GetWeaponDto>(weapon);
                return ResponseResult.Success(dto);
            }

        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacterById(int characterId, UpdateCharacterDto updateCharacter)
        {

            var characters = await _dbContext.Characters
            .Include(x => x.Weapon)
            .Include(x => x.CharacterSkills).ThenInclude(x => x.Skill)
            .FirstOrDefaultAsync(x => x.Id == characterId);
            if (characters == null)
            {
                return ResponseResult.Failure<GetCharacterDto>("Character Not Found.");
            }
            else
            {
                characters.Name = updateCharacter.Name;
                characters.HitPoints = updateCharacter.HitPoints;
                characters.Strength = updateCharacter.Strength;
                characters.Defense = updateCharacter.Defense;
                characters.Intelligence = updateCharacter.Intelligence;

                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<GetCharacterDto>(characters);
                return ResponseResult.Success(dto);
            }
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacterSkillById(int characterId, UpdateCharacterSkillDto updateCharacterSkill)
        {

            // This Method cannot use because characterskill field have a  2 primary key that why i can not update
            var charactersskill = await _dbContext.CharacterSkills
            .Include(x => x.Skill)
            .FirstOrDefaultAsync(x => x.CharacterId == characterId);
            if (charactersskill == null)
            {
                return ResponseResult.Failure<GetCharacterDto>("Characterskill Not Assign.");
            }
            else
            {
                charactersskill.SkillId = updateCharacterSkill.SkillId;

                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<GetCharacterDto>(charactersskill);
                return ResponseResult.Success(dto);
            }
        }

        public async Task<ServiceResponse<GetSkillDto>> UpdateSkillById(int skillId, UpdateSkillDto updateSkill)
        {
            var skill = await _dbContext.Skills.FirstOrDefaultAsync(x => x.Id == skillId);
            if (skill == null)
            {
                return ResponseResult.Failure<GetSkillDto>("Skill Not Found.");
            }
            else
            {
                skill.Name = updateSkill.Name;
                skill.Damage = updateSkill.Damage;

                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<GetSkillDto>(skill);
                return ResponseResult.Success(dto);
            }
        }

        public async Task<ServiceResponse<GetWeaponDto>> UpdateWeaponById(int weaponId, UpdateWeaponDto updateWeapon)
        {
            var weapon = await _dbContext.Weapons.FirstOrDefaultAsync(x => x.Id == weaponId);
            if (weapon == null)
            {
                return ResponseResult.Failure<GetWeaponDto>("Weapon Not Found.");
            }
            else
            {
                weapon.Name = updateWeapon.Name;
                weapon.Damage = updateWeapon.Damage;

                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<GetWeaponDto>(weapon);
                return ResponseResult.Success(dto);
            }
        }

    }
}