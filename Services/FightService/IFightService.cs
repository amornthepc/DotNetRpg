using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRpg.Dtos.Fight;

namespace DotNetRpg.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
    }
}
