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
    internal class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(e => e.InsertDate)
               .HasColumnType("datetime")
               .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifyDate).HasColumnType("datetime");

            builder.Property(e => e.DeleteDate).HasColumnType("datetime");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
        }
    }
}
