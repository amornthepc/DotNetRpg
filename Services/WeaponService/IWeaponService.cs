using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRpg.Dtos.Character;
using DotNetRpg.Dtos.Weapon;

namespace DotNetRpg.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}
