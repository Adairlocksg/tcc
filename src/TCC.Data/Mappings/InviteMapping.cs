using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCC.Business.Models;

namespace TCC.Data.Mappings
{
    public class InviteMapping : IEntityTypeConfiguration<Invite>
    {
        public void Configure(EntityTypeBuilder<Invite> builder)
        {
            builder.ToTable("invites");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.GroupId)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Ignore(x => x.IsAccepted);
            builder.Ignore(x => x.IsRejected);
            builder.Ignore(x => x.IsPending);

            //

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Group)
                .WithMany()
                .HasForeignKey(x => x.GroupId);
        }
    }
}
