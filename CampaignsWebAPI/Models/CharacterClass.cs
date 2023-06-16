namespace CampaignsWebAPI.Models
{

	public class CharacterClass
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public ICollection<ClassLevel> ClassLevels { get; set; }
	}
}
