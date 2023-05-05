using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P013KatmanliBlog.Core.Entities;

namespace P013KatmanliBlog.Data.Configurations
{
    internal class PostConfigurations : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x=>x.Title).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.Body).IsRequired().HasMaxLength(1000);
            builder.HasOne(x=>x.Category).WithMany(x=>x.Posts).HasForeignKey(x=>x.CategoryId);
            builder.HasOne(x=>x.User).WithMany(x=>x.Posts).HasForeignKey(x=>x.UserId);
        }
    }
}
