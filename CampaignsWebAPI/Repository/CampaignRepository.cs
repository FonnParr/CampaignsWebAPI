using CampaignsWebAPI.Data;
using CampaignsWebAPI.Interfaces;
using CampaignsWebAPI.Models;

namespace CampaignsWebAPI.Repository
{
	public class CampaignRepository : RepositoryBase, ICampaignRepository
	{

		public CampaignRepository(DataContext context) : base(context) { }

        public ICollection<Campaign> GetCampaigns()
		{
			return _context.Campaigns.ToList();
		}

		public Campaign? GetCampaignById(int id)
		{
			return _context.Campaigns.SingleOrDefault(c => c.Id == id);
		}
		public IEnumerable<Player> GetPlayers(int campaignId)
		{
			return _context.Players.Where(p => p.Characters.Any(c => c.CampaignId == campaignId));
		}

		public bool Update(Campaign campaign)
		{
			_context.Campaigns.Update(campaign);
			return Save();
		}

		public bool Exists(int id)
		{
			return _context.Campaigns.Any(c => c.Id == id);
		}
		public bool Exists(string name)
		{
			return _context.Campaigns.Any(c => c.Name == name);
		}
		public bool Exists(Campaign campaign)
		{
			return Exists(campaign.Id) || Exists(campaign.Name);
		}

		public IEnumerable<Character> GetCharacters(int campaignId)
		{
			return _context.Characters.Where(c=>c.CampaignId == campaignId);
		}

		public bool AddCharacter(int campaignId, Character character) {
			
			if (!Exists(campaignId))
				throw new KeyNotFoundException();

			character.CampaignId = campaignId;
			if (!_context.Characters.Any(c => c.ID == character.ID))
				_context.Characters.Add(character);
			else
				_context.Update(character);

			return Save();
		}

		public bool Add(Campaign campaign)
		{
			if (Exists(campaign))
				return false;

			_context.Add(campaign);
			return Save();
		}

		public bool Update(int id, Campaign campaign)
		{
			if (!Exists(id))
				return false;

			_context.Update(campaign);
			return Save();
		}

		public bool Delete(int id)
		{
			if (!Exists(id))
				return false;

			var campaign = GetCampaignById(id);
			var characters = GetCharacters(id);

			_context.RemoveRange(characters);
			_context.Remove(campaign);

			return Save();
		}
	}
}
