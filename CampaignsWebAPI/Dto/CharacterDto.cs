namespace CampaignsWebAPI.Dto
{
	public class CharacterDto
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public int CampaignId { get; set; }
		public int PlayerId { get; set; }
		public int SpeciesId { get; set; }
	}
}
