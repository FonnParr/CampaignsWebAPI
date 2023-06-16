using CampaignsWebAPI.Models;

namespace CampaignsWebAPI.Interfaces
{
	public interface ISpeciesRepository
	{
		IEnumerable<Species> GetAllSpecies();
		Species? GetSpeciesById(int id);
		IEnumerable<Character> GetCharactersBySpeciesId(int speciesId);
		bool AddSpecies(string name);
		bool AddSpecies(Species species);
		bool DeleteSpecies(Species species);
		bool UpdateSpecies(int id, Species species);
		bool DeleteSpecies(int id);
	}
}
