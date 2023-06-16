namespace CampaignsWebAPI.Models
{
	public class Species
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public ICollection<Character> Characters { get; set; }
	}
}
