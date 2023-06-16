using AutoMapper;
using CampaignsWebAPI.Dto;
using CampaignsWebAPI.Models;

namespace CampaignsWebAPI.Helper
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
			CreateMap<Campaign, CampaignDto>().ReverseMap();
			CreateMap<CharacterClass, CharacterClassDto>().ReverseMap();
			CreateMap<Character, CharacterDto>().ReverseMap();
			CreateMap<ClassLevel, ClassLevelDto>().ReverseMap();
			CreateMap<Player, PlayerDto>().ReverseMap();
			CreateMap<Species, SpeciesDto>().ReverseMap();
		}
	}
}
