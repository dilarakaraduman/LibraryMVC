using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace LibraryMVC.Models
{
	public class DatabaseContext : DbContext
	{
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
			//builder.UseNpgsql("MyDatabaseConnection");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BookConfiguration());
		}
	}
}







