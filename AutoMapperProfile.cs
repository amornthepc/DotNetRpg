using AutoMapper;
using DotNetRpg.Dtos.Character;
using DotNetRpg.Dtos.Fight;
using DotNetRpg.Dtos.Skill;

namespace DotNetRpg;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, GetCharacterDto>();
        CreateMap<AddCharacterDto, Character>();
        //CreateMap<UpdateCharacterDto, Character>();
        CreateMap<Weapon, GetWeaponDto>();
        CreateMap<Skill, GetSkillDto>();
        CreateMap<Character, HighScoreDto>();
    }
}
