using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCC.Business.Models;

namespace TCC.Data.Mappings
{
    public class UserGroupMapping : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable("user_groups");

            builder.HasKey(x => x.Id);

            builder.HasIndex(ug => new { ug.UserId, ug.GroupId }).IsUnique(); // Garante que um user não esteja duplicado no mesmo grupo

            builder.Property(x => x.Admin)
                .IsRequired();

            builder.Property(x => x.Favorite)
                .IsRequired();

            builder.HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);

            builder.HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId);
        }
    }
}
