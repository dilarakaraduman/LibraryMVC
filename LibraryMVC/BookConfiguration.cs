using LibraryMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMVC
{
	public class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.ToTable("Book", DatabaseContext.DefaultSchema);
			builder.HasKey(u=> u.Id);
			builder.Property(b => b.Id).HasColumnType("int").IsRequired();
			builder.Property(b => b.Name);
			builder.Property(b => b.Writer);
			builder.Property(b => b.Image); 
			builder.Property(b => b.IsInvisible);
			builder.Property(b => b.Borrower).IsRequired(false);
            builder.Property(b => b.AvaliableDate).IsRequired(false);
        }
	}
}
