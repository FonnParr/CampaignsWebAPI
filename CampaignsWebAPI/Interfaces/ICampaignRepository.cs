using CampaignsWebAPI.Models;

namespace CampaignsWebAPI.Interfaces
{
	public interface ICampaignRepository
	{
		ICollection<Campaign> GetCampaigns();
		Campaign? GetCampaignById(int id);
		IEnumerable<Player> GetPlayers(int campaignId);
		bool AddCharacter(int campaignId, Character character);
		bool Update(Campaign campaign);
		IEnumerable<Character> GetCharacters(int campaignId);
		bool Add(Campaign campaign);
		bool Update(int id, Campaign campaign);
		bool Delete(int id);
	}
}
