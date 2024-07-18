using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNetRpg.Data;
using DotNetRpg.Dtos.Character;
using DotNetRpg.Dtos.Fight;
using DotNetRpg.Services.CharacterService;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DotNetRpg.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public FightService(DataContext context, ICharacterService characterService, IMapper mapper)
        {
            _context = context;
            _characterService = characterService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                
                // Better Solution?
                //var attackerDto = await _characterService.GetCharacterById(request.AttackerId);
                //var attacker = _mapper.Map<Character>(attackerDto);

                //var opponentDto = await _characterService.GetCharacterById(request.OpponentId);
                //var opponent = _mapper.Map<Character>(opponentDto);

                var attacker = await _context.Characters
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

                var opponent = await _context.Characters
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);


                var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
                if (skill == null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doesn't know that skill.";
                    return response;
                }

                int damage = skill.Damage + new Random().Next(attacker.Intelligence);
                damage -= new Random().Next(opponent.Defense);

                if (damage > 0)
                {
                    opponent.HitPoints -= damage;
                }

                if (opponent.HitPoints <= 0)
                {
                    response.Message = $"{opponent.Name} has been defeated!";
                }

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                
                // Better Solution?
                //var attackerDto = await _characterService.GetCharacterById(request.AttackerId);
                //var attacker = _mapper.Map<Character>(attackerDto);

                //var opponentDto = await _characterService.GetCharacterById(request.OpponentId);
                //var opponent = _mapper.Map<Character>(opponentDto);

                var attacker = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

                var opponent = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                int damage = attacker.Weapon.Damage + new Random().Next(attacker.Strength);
                damage -= new Random().Next(opponent.Defense);

                if (damage > 0)
                {
                    opponent.HitPoints -= damage;
                }

                if (opponent.HitPoints <= 0)
                {
                    response.Message = $"{opponent.Name} has been defeated!";
                }

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
