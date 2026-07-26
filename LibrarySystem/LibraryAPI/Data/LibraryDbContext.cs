using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;

namespace LibraryAPI.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(
        DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public DbSet<BookEntity> Books { get; set; }

    public DbSet<Author> Authors { get; set; }

    protected override void  OnModelCreating(
    ModelBuilder modelBuilder)
{

    modelBuilder.Entity<BookEntity>()
        .HasOne(b => b.Author)
        .WithMany(a => a.Books)
        .HasForeignKey(b => b.AuthorId);


}
}