using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCC.Business.Models;

namespace TCC.Data.Mappings
{
    public class ExpenseMapping : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("expenses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Value)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.BeginDate)
                .IsRequired();

            builder.Property(x => x.EndDate);

            builder.Property(x => x.Recurrence);

            builder.Property(x => x.IsRecurring)
                .IsRequired();

            builder.Property(x => x.RecurrenceInterval)
                .IsRequired();

            builder.Property(x => x.Active)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.CategoryId)
                .IsRequired();

            builder.Property(x => x.GroupId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.Group)
                .WithMany()
                .HasForeignKey(x => x.GroupId);
        }
    }
}
