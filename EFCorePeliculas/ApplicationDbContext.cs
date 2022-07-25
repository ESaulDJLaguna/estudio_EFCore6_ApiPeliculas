using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}
	}
}
