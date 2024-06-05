using Domain.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data;

public class LibraryDBContext : IdentityDbContext<User>
{
    public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options)
    {

    }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BooksAuthors { get; set; }
    public DbSet<UserBook> UsersBooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");
        modelBuilder.ApplyConfiguration(new AuthorConfig());
        modelBuilder.ApplyConfiguration(new BookConfig());
        modelBuilder.ApplyConfiguration(new BookAuthorConfig());
        modelBuilder.ApplyConfiguration(new UserBookConfig());
    }
}
