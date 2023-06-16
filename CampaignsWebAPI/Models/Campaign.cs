namespace CampaignsWebAPI.Models
{
	public class Campaign
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Character> Characters { get; set; }
	}


}
