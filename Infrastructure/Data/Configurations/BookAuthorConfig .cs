using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class BookAuthorConfig : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.ToTable("Books_Authors");
         
            builder.HasOne(d => d.Author)
                .WithMany(p => p.AuthorBooks)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Authors_B__Autho__37A5467C");

            builder.HasOne(d => d.Book)
                .WithMany(p => p.BookAuthors)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Authors_B__BookI__38996AB5");
        }
    }
}
