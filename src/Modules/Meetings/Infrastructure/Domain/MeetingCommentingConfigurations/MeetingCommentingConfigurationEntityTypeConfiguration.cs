using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingCommentingConfigurations
{
    public class MeetingCommentingConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<MeetingCommentingConfiguration>
    {
        public void Configure(EntityTypeBuilder<Modules.Meetings.Domain.MeetingCommentingConfigurations.MeetingCommentingConfiguration> builder)
        {
            builder.ToTable("meeting_commenting_configurations", "sss_meetings");

            builder.HasKey(c => c.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property<MeetingId>("_meetingId").HasColumnName("meeting_id");
            builder.Property<bool>("_isCommentingEnabled").HasColumnName("is_commenting_enabled");
        }
    }
}