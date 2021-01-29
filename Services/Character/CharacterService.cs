using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TEMPLETEAPI.Models;
using TEMPLETEAPI.DTOs;
using TEMPLETEAPI.Data;
using TEMPLETEAPI.DTOs.Fight;
using System;
using System.Linq;

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
        public async Task<ServiceResponse<AttackResultDto>> WeaponAtk(WeaponAttackDto request)
        {
            try
            {
                var attacker = await _dbContext.Characters
                .Include(x => x.Weapon)
                .FirstOrDefaultAsync(x => x.Id == request.AttackerId);

                if (attacker is null)
                {
                    var msg = $"This attackerId {request.AttackerId} not found.";
                    _log.LogError(msg);
                    return ResponseResult.Failure<AttackResultDto>(msg);
                }

                var opponent = await _dbContext.Characters
                .Include(x => x.Weapon)
                .FirstOrDefaultAsync(x => x.Id == request.OpponentId);

                if (opponent is null)
                {
                    var msg = $"This opponentId {request.OpponentId} not found.";
                    _log.LogError(msg);
                    return ResponseResult.Failure<AttackResultDto>(msg);
                }

                int damage;
                damage = attacker.Weapon.Damage + attacker.Strength;
                damage -= opponent.Defense;

                if (damage > 0)
                {
                    opponent.HitPoints -= damage;
                }

                string atkResultMessage;

                if (opponent.HitPoints <= 0)
                {
                    atkResultMessage = $"{opponent.Name} is dead.";
                }
                else
                {
                    atkResultMessage = $"{opponent.Name} HP Remain {opponent.HitPoints}";
                }

                _dbContext.Characters.Update(opponent);
                await _dbContext.SaveChangesAsync();

                var dto = new AttackResultDto
                {
                    AttackerName = attacker.Name,
                    AttackHP = attacker.HitPoints,
                    OpponentName = opponent.Name,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage,
                    AttackResultMessage = atkResultMessage
                };

                _log.LogInformation(atkResultMessage);
                _log.LogInformation("Weapon attack done.");

                return ResponseResult.Success(dto);

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return ResponseResult.Failure<AttackResultDto>(ex.Message);
            }
        }
        public async Task<ServiceResponse<AttackResultDto>> SkillAtk(SkillAttackDto request)
        {
            try
            {
                var attacker = await _dbContext.Characters
                .Include(x => x.CharacterSkills).ThenInclude(x => x.Skill)
                .FirstOrDefaultAsync(x => x.Id == request.AttackerId);

                if (attacker is null)
                {
                    var msg = $"This attackerId {request.AttackerId} not found.";
                    _log.LogError(msg);
                    return ResponseResult.Failure<AttackResultDto>(msg);
                }

                var opponent = await _dbContext.Characters
                .Include(x => x.Weapon)
                .FirstOrDefaultAsync(x => x.Id == request.OpponentId);

                if (opponent is null)
                {
                    var msg = $"This opponentId {request.OpponentId} not found.";
                    _log.LogError(msg);
                    return ResponseResult.Failure<AttackResultDto>(msg);
                }

                var charSkill = await _dbContext.CharacterSkills.Include(x => x.Skill)
                .FirstOrDefaultAsync(x => x.CharacterId == request.AttackerId && x.SkillId == request.SkillId);
                if (charSkill is null)
                {
                    var msg = $"This Attacker doesn't know this skill {request.OpponentId}.";
                    _log.LogError(msg);
                    return ResponseResult.Failure<AttackResultDto>(msg);
                }

                int damage;
                damage = charSkill.Skill.Damage + attacker.Intelligence;
                damage -= opponent.Defense;

                if (damage > 0)
                {
                    opponent.HitPoints -= damage;
                    _log.LogInformation($"Skill Dmg = {charSkill.Skill.Damage}");
                    _log.LogInformation($"{attacker.Name} Intelligent = {attacker.Intelligence}");
                    _log.LogInformation($"{opponent.Name} Defend = {opponent.Defense}");
                    _log.LogInformation($"Skill Dmg = {charSkill.Skill.Damage} Total damage = {damage}");
                }

                string atkResultMessage;

                if (opponent.HitPoints <= 0)
                {
                    atkResultMessage = $"{opponent.Name} is dead.";
                }
                else
                {
                    atkResultMessage = $"{opponent.Name} HP Remain {opponent.HitPoints}";
                }

                _dbContext.Characters.Update(opponent);
                await _dbContext.SaveChangesAsync();

                var dto = new AttackResultDto
                {
                    AttackerName = attacker.Name,
                    AttackHP = attacker.HitPoints,
                    OpponentName = opponent.Name,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage,
                    AttackResultMessage = atkResultMessage
                };

                _log.LogInformation(atkResultMessage);
                _log.LogInformation("Skill attack done.");

                return ResponseResult.Success(dto);

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return ResponseResult.Failure<AttackResultDto>(ex.Message);
            }
        }
        public async Task<ServiceResponse<GetCharacterDto>> RemoveWeapon(int characterId)
        {
            var character = await _dbContext.Characters
                       .Include(x => x.Weapon)
                       .Include(x => x.CharacterSkills).ThenInclude(x => x.Skill)
                       .FirstOrDefaultAsync(x => x.Id == characterId);

            if (character == null)
            {
                return ResponseResult.Failure<GetCharacterDto>("Character not found.");
            }

            var weapon = await _dbContext.Weapons.Where(x => x.CharacterId == characterId).FirstOrDefaultAsync();
            if (weapon is null)
            {
                return ResponseResult.Failure<GetCharacterDto>("Weapon not found.");
            }

            _dbContext.Weapons.Remove(weapon);
            await _dbContext.SaveChangesAsync();

            var dto = _mapper.Map<GetCharacterDto>(character);

            return ResponseResult.Success(dto);
        }
        public async Task<ServiceResponse<GetCharacterDto>> RemoveSkill(int characterId)
        {
            var character = await _dbContext.Characters
                       .Include(x => x.Weapon)
                       .Include(x => x.CharacterSkills).ThenInclude(x => x.Skill)
                       .FirstOrDefaultAsync(x => x.Id == characterId);

            if (character == null)
            {
                return ResponseResult.Failure<GetCharacterDto>("Character not found.");
            }

            var charSkill = await _dbContext.CharacterSkills.Where(x => x.CharacterId == characterId).ToListAsync();
            if (charSkill is null)
            {
                return ResponseResult.Failure<GetCharacterDto>("Skill not found.");
            }

            _dbContext.CharacterSkills.RemoveRange(charSkill);
            await _dbContext.SaveChangesAsync();

            var dto = _mapper.Map<GetCharacterDto>(character);
            return ResponseResult.Success(dto);
        }
    }
}