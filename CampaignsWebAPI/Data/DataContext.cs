using CampaignsWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CampaignsWebAPI.Data
{
	public class DataContext : DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

		public DbSet<Campaign> Campaigns { get; set; }
		public DbSet<Character> Characters { get; set; }
		public DbSet<CharacterClass> CharacterClasses { get; set; }
		public DbSet<ClassLevel> ClassLevels { get; set; }
		public DbSet<Player> Players { get; set; }
		public DbSet<Species> Species { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ClassLevel>()
				.HasKey(cl => new { cl.ClassId, cl.PcId });
			modelBuilder.Entity<ClassLevel>()
				.HasOne(cl => cl.CharacterClass)
				.WithMany(cc => cc.ClassLevels)
				.HasForeignKey(cl => cl.ClassId);
			modelBuilder.Entity<ClassLevel>()
				.HasOne(cl=>cl.Character)
				.WithMany(c=>c.ClassLevels)
				.HasForeignKey(cl => cl.PcId);
		}
	}
}
