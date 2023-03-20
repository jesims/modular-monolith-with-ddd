using CompanyName.MyMeetings.BuildingBlocks.Application.Outbox;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Outbox
{
    internal class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("outbox_messages", "sss_administration");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("id").ValueGeneratedNever();
            builder.Property(b => b.Type).HasColumnName("type");
            builder.Property(b => b.Data).HasColumnName("data");
            builder.Property(b => b.OccurredOn).HasColumnName("occurred_on")
                .HasConversion(
                    src => DateTimeConverter.UtcDateTime(src),
                    dest => DateTimeConverter.UtcDateTime(dest));
            builder.Property(b => b.ProcessedDate).HasColumnName("processed_date")
                .HasConversion(
                    src => DateTimeConverter.MaybeUtcDateTime(src),
                    dest => DateTimeConverter.MaybeUtcDateTime(dest));
        }
    }
}