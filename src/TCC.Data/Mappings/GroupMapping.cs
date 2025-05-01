using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCC.Business.Models;

namespace TCC.Data.Mappings
{
    public class GroupMapping : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("groups");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Active)
                .IsRequired();

            builder
                .HasMany(g => g.Categories)
                .WithOne(c => c.Group)
                .HasForeignKey(c => c.GroupId);

            builder
                .Navigation(g => g.Categories) // <- expõe ao EF como coleção navegável
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
