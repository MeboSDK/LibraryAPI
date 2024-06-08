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
    internal class UserBookConfig : IEntityTypeConfiguration<UserBook>
    {
        public void Configure(EntityTypeBuilder<UserBook> builder)
        {
            builder.ToTable("Users_Books");

            builder.Property(d => d.BooksCount).HasColumnName("Books_Count");

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserBooks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Users_A__Books__37A5467C");

            builder.HasOne(d => d.Book)
                .WithMany(p => p.BookUsers)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Books_A__Users__38996AB5");
        }
    }
}
