using System;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.Members.MemberSubscriptions;

internal class MemberSubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<MemberSubscription>
{
    public void Configure(EntityTypeBuilder<MemberSubscription> builder)
    {
        builder.ToTable("member_subscriptions", "sss_meetings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property<DateTime>("_expirationDate").HasColumnName("expiration_date")
            .HasConversion(
                src => DateTimeConverter.UtcDateTime(src),
                dest => DateTimeConverter.UtcDateTime(dest));
    }
}
