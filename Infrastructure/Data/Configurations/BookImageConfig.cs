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
    internal class BookImageConfig : IEntityTypeConfiguration<BookImage>
    {
        public void Configure(EntityTypeBuilder<BookImage> builder)
        {
            // Define table name
            builder.ToTable("Book_Images");

            // Define primary key
            builder.HasKey(b => b.Id);

            // Configure properties
            builder.Property(b => b.Image)
                .IsRequired()
                .HasColumnType("varbinary(max)");

            builder.Property(b => b.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(b => b.ContentType)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
