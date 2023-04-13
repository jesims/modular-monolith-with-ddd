using System;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingGroupProposals;

internal class MeetingGroupProposalEntityTypeConfiguration : IEntityTypeConfiguration<MeetingGroupProposal>
{
    public void Configure(EntityTypeBuilder<MeetingGroupProposal> builder)
    {
        builder.ToTable("meeting_group_proposals", "sss_meetings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property<string>("_name").HasColumnName("name");
        builder.Property<string>("_description").HasColumnName("description");
        builder.Property<MemberId>("_proposalUserId").HasColumnName("proposal_user_id");
        builder.Property<DateTime>("_proposalDate").HasColumnName("proposal_date");

        builder.OwnsOne<MeetingGroupLocation>("_location", b =>
        {
            b.Property(p => p.City).HasColumnName("location_city");
            b.Property(p => p.CountryCode).HasColumnName("location_country_code");
        });

        builder.OwnsOne<MeetingGroupProposalStatus>("_status",
            b => { b.Property(p => p.Value).HasColumnName("status_code"); });
    }
}
