using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P013KatmanliBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P013KatmanliBlog.Data.Configurations
{
    internal class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x=>x.Surname).HasMaxLength(40);
            builder.Property(x=>x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.Phone).HasMaxLength(20);
            builder.Property(x=>x.Password).IsRequired().HasMaxLength(50);

        }
    }
}
