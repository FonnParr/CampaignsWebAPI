namespace CampaignsWebAPI.Models
{


	public class Character
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public int CampaignId { get; set; }
		public int PlayerId { get; set; }
		public int SpeciesId { get; set; }

		public Campaign Campaign { get; set; }
		public Player Player { get; set; }
		public Species Species { get; set; }

		public ICollection<ClassLevel> ClassLevels { get; set; }

	}

}
