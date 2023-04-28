using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.InternalCommands
{
    internal class InternalCommandEntityTypeConfiguration : IEntityTypeConfiguration<InternalCommand>
    {
        public void Configure(EntityTypeBuilder<InternalCommand> builder)
        {
            builder.ToTable("internal_commands", "sss_administration");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("id").ValueGeneratedNever();
            builder.Property(b => b.Type).HasColumnName("type");
            builder.Property(b => b.Data).HasColumnName("data");
            builder.Property(b => b.ProcessedDate).HasColumnName("processed_date")
                .HasConversion(
                    src => DateTimeConverter.MaybeUtcDateTime(src),
                    dest => DateTimeConverter.MaybeUtcDateTime(dest));
        }
    }
}