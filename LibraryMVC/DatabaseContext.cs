using LibraryMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC
{
	public class DatabaseContext : DbContext
	{
		public const string DefaultSchema = "public";
		public DbSet<Book> Book { get; set; }
		public DatabaseContext() 
		{
		}
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=12345;Database=Library;";
			builder.UseNpgsql(connectionString);
			builder.EnableSensitiveDataLogging();
            base.OnConfiguring(builder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BookConfiguration());
		}
	}
}







