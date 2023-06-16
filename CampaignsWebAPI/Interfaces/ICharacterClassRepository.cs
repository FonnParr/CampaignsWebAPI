using CampaignsWebAPI.Models;

namespace CampaignsWebAPI.Interfaces
{
	public interface ICharacterClassRepository
	{
		ICollection<CharacterClass> GetCharacterClasses();
		CharacterClass? GetCharacterClassById(int id);
		IEnumerable<Character> GetCharactersByClassId(int classId);
		bool Delete(int id);
		bool AddClass(CharacterClass characterClass);
		bool Update(int id, CharacterClass characterClass);
	}
}
