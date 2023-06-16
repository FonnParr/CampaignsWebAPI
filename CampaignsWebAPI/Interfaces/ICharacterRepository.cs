using CampaignsWebAPI.Models;

namespace CampaignsWebAPI.Interfaces
{
	public interface ICharacterRepository
	{
		ICollection<Character> GetCharacters();
		Character? GetCharacterById(int id);

		int GetCharacterLevel(int characterId);

		IEnumerable<ClassLevel> GetClassLevels(int characterId);
		bool AddLevel(int charId, int classId);
		bool RemoveLevel(int charId, int classId);
		bool DeleteCharacter(int id);
		bool UpdateCharacter(int id, Character character);
		bool AddCharacter(Character character);
	}
}
