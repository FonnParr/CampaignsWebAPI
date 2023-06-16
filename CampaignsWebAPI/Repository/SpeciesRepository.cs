using CampaignsWebAPI.Data;
using CampaignsWebAPI.Interfaces;
using CampaignsWebAPI.Models;

namespace CampaignsWebAPI.Repository
{
	public class SpeciesRepository : RepositoryBase, ISpeciesRepository
	{
		public SpeciesRepository(DataContext context) : base(context) { }
		public IEnumerable<Species> GetAllSpecies()
		{
			return _context.Species.ToList();
		}

		public IEnumerable<Character> GetCharactersBySpeciesId(int speciesId)
		{
			return _context.Characters.Where(c=>c.SpeciesId == speciesId);
		}

		public Species? GetSpeciesById(int id)
		{
			return _context.Species.SingleOrDefault(s=>s.ID == id);
		}

		public bool AddSpecies(string name)
		{
			return AddSpecies(new Species { Name = name });
		}

		public bool AddSpecies(Species species)
		{
			if (string.IsNullOrEmpty(species.Name))
				return false;

			if (_context.Species.Any(s => s.Name == species.Name))
				return false;

			_context.Species.Add(species);

			return Save();
		}

		public bool UpdateSpecies(int id, Species species)
		{
			if (string.IsNullOrEmpty(species.Name))
				return false;

			if (_context.Species.Any(s => s.Name == species.Name))
				return false;

			species.ID = id;

			_context.Update(species);
			return Save();
		}

		public bool DeleteSpecies(int id)
		{
			var species = GetSpeciesById(id);
			if (species == null)
				throw new KeyNotFoundException();

            return DeleteSpecies(species);
		}
		public bool DeleteSpecies(Species species)
		{
			_context.RemoveRange(GetCharactersBySpeciesId(species.ID));

			_context.Remove(species);
			return Save();
		}
	}
}
