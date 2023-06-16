using CampaignsWebAPI.Models;

namespace CampaignsWebAPI.Interfaces
{
	public interface IPlayerRepository
	{
		ICollection<Player> GetPlayers();
		Player? GetPlayerById(int id);

		IEnumerable<Character> GetCharactersByPlayerId(int playerId);
		IEnumerable<Campaign> GetCampaignsByPlayerId(int playerId);
		bool AddPlayer(Player player);
		bool UpdatePlayer(int id, Player player);
		bool DeletePlayer(int id);
	}
}
