namespace CampaignsWebAPI.Models
{

	public class Player
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public ICollection<Character> Characters { get; set; }

	}
}
