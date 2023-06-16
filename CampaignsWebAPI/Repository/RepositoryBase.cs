using CampaignsWebAPI.Data;

namespace CampaignsWebAPI.Repository
{
	public abstract class RepositoryBase
	{
		protected readonly DataContext _context;
		public RepositoryBase(DataContext context)
		{
			_context = context;
		}

		protected bool Save()
		{
			return _context.SaveChanges() > 1;
		}
	}
}
