using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.Username)
            .IsUnique();
        builder.HasIndex(x=>x.Email)
            .IsUnique();
        builder.Property(x => x.Image)
            .HasMaxLength(255);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(32);
        builder.Property(x => x.Surname)
            .IsRequired()
            .HasMaxLength(32);
        builder.Property(x => x.Username)
           .HasMaxLength(32);
        builder.Property(x => x.Email)
            .HasMaxLength(64);

    }
}
