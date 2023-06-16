using CampaignsWebAPI.Data;
using CampaignsWebAPI.Interfaces;
using CampaignsWebAPI.Models;

namespace CampaignsWebAPI.Repository
{
	public class CharacterClassRepository : RepositoryBase, ICharacterClassRepository
	{
		public CharacterClassRepository(DataContext context) : base(context) { }

		public CharacterClass? GetCharacterClassById(int id)
		{
			return _context.CharacterClasses.SingleOrDefault(c => c.ID == id);
		}

		public ICollection<CharacterClass> GetCharacterClasses()
		{
			return _context.CharacterClasses.ToList();
		}

		public IEnumerable<Character> GetCharactersByClassId(int classId)
		{
			return _context.Characters.Where(c=>c.ClassLevels.Any(cl=>cl.ClassId == classId));
		}

		public bool Exists(int id)
		{ return _context.CharacterClasses.Any(c=>c.ID == id); }

		public bool Exists(string name)
		{
			return _context.CharacterClasses.Any(c=>c.Name == name);
		}
		public bool Exists(CharacterClass characterClass)
		{
			return Exists(characterClass.ID) || Exists(characterClass.Name);
		}

		public bool AddClass(CharacterClass characterClass)
		{
			if (string.IsNullOrEmpty(characterClass.Name))
				return false;
			if (Exists(characterClass))
				return false;

			_context.CharacterClasses.Add(characterClass);
			return Save();
		}

		public bool Update(int id, CharacterClass characterClass)
		{
			if (string.IsNullOrEmpty(characterClass.Name))
				return false;
			if (!Exists(id))
				return false;
			if (Exists(characterClass.Name))
				return false;

			characterClass.ID = id;

			_context.Update(characterClass);
			return Save();
		}

		public bool Delete(int id)
		{
			var characterClass = GetCharacterClassById(id);
			if (characterClass == null)
				return false;

			_context.RemoveRange(characterClass.ClassLevels);
			_context.Remove(characterClass);

			return Save();
		}
	}
}
