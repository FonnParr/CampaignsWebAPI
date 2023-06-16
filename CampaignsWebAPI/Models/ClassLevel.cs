namespace CampaignsWebAPI.Models;



public class ClassLevel
{
	public int ClassId { get; set; }
	public int PcId { get; set; }
	public int Level { get; set; }

	public CharacterClass CharacterClass { get; set; }
	public Character Character { get; set; }

}
