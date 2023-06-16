using CampaignsWebAPI.Data;
using CampaignsWebAPI.Interfaces;
using CampaignsWebAPI.Models;

namespace CampaignsWebAPI.Repository
{
	public class CharacterRepository : RepositoryBase, ICharacterRepository
	{	public CharacterRepository(DataContext context) : base(context) { }
        public Character? GetCharacterById(int id)
		{
			return _context.Characters.SingleOrDefault(c=>c.ID == id);
		}

		public int GetCharacterLevel(int characterId)
		{
			return GetClassLevels(characterId).Sum(cl => cl.Level);
		}

		public ICollection<Character> GetCharacters()
		{
			return _context.Characters.ToList();
		}

		public IEnumerable<ClassLevel> GetClassLevels(int characterId)
		{
			return _context.ClassLevels.Where(cl => cl.PcId == characterId);
		}

		public bool AddLevel(int charId, int classId)
		{
			var characterClass = _context.CharacterClasses.SingleOrDefault(cl=>cl.ID == classId);

			if (characterClass == null)
				throw new KeyNotFoundException();

			var existingLevel = GetClassLevels(charId).Where(cl => cl.ClassId == classId).FirstOrDefault();

			if (existingLevel == null)
			{
				var newLevel = new ClassLevel { Level = 1, ClassId = classId, PcId = charId };
				_context.ClassLevels.Add(newLevel);
			}
			else
			{
				existingLevel.Level++;
				_context.Update(existingLevel);
			}

			return Save();
		}

		public bool RemoveLevel(int charId, int classId)
		{
			var characterClass = _context.CharacterClasses.SingleOrDefault(cl => cl.ID == classId);

			if (characterClass == null)
				throw new KeyNotFoundException();

			var existingLevel = GetClassLevels(charId).Where(cl => cl.ClassId == classId).FirstOrDefault();

			if (existingLevel == null)
				return false;

			existingLevel.Level--;

			if (existingLevel.Level < 1)
				_context.Remove(existingLevel);
			else
				_context.Update(existingLevel);
			
			return Save();
		}

		public bool AddCharacter(Character character)
		{
			if (string.IsNullOrEmpty(character.Name))
				return false;

			_context.Characters.Add(character);

			return Save();
		}

		public bool UpdateCharacter(int id, Character character)
		{
			if (string.IsNullOrEmpty(character.Name))
				return false;

			character.ID = id;

			_context.Update(character);
			return Save();
		}

		public bool DeleteCharacter(int id)
		{
			var character = GetCharacterById(id);

			if (character == null)
				throw new KeyNotFoundException();

			return DeleteCharacter(character);
		}
		public bool DeleteCharacter(Character character)
		{
			_context.RemoveRange(GetClassLevels(character.ID));

			_context.Remove(character);
			return Save();
		}
	}
}
