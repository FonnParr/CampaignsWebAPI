using CampaignsWebAPI.Data;
using CampaignsWebAPI.Interfaces;
using CampaignsWebAPI.Models;

namespace CampaignsWebAPI.Repository
{
	public class PlayerRepository : RepositoryBase, IPlayerRepository
	{
		public PlayerRepository(DataContext context) : base(context) { }
		
		public IEnumerable<Campaign> GetCampaignsByPlayerId(int playerId)
		{
			return _context.Campaigns.Where(c=>c.Characters.Any(c=>c.PlayerId == playerId));
		}

		public IEnumerable<Character> GetCharactersByPlayerId(int playerId)
		{
			return _context.Characters.Where(c=>c.PlayerId==playerId);
		}

		public Player? GetPlayerById(int id)
		{
			return _context.Players.SingleOrDefault(p=>p.ID == id);
		}

		public ICollection<Player> GetPlayers()
		{
			return _context.Players.ToList();
		}

		public bool AddPlayer(Player player)
		{
			if (Exists(player))
				return false;

			_context.Players.Add(player);
			return Save();
		}

		bool Exists(int id)
		{
			return _context.Players.Any(p=>p.ID == id);
		}
		bool Exists(string name)
		{
			return _context.Players.Any(p=>p.Name == name);	
		}
		bool Exists(Player player)
		{
			return Exists(player.ID) || Exists(player.Name);
		}

		public bool UpdatePlayer(int id, Player player)
		{
			if (!Exists(id))
				return false;

			if (Exists(player))
				return false;

			_context.Players.Update(player);

			return Save();
		}

		IEnumerable<ClassLevel> GetClassLevelsForPlayer(int playerId)
		{
			return _context.ClassLevels.Where(cl => cl.Character.PlayerId == playerId);
		}

		public bool DeletePlayer(int id)
		{
			var player = GetPlayerById(id);
			if (player == null)
				return false;

			var characters = GetCharactersByPlayerId(id);
			var levels = GetClassLevelsForPlayer(id);

			_context.ClassLevels.RemoveRange(levels);
			_context.Characters.RemoveRange(characters);
			_context.Players.Remove(player);

			return Save();
		}
	}
}
