using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingMemberCommentLikes;

public class MeetingMemberCommentLikeEntityTypeConfiguration : IEntityTypeConfiguration<MeetingMemberCommentLike>
{
    public void Configure(EntityTypeBuilder<MeetingMemberCommentLike> builder)
    {
        builder.ToTable("meeting_member_comment_likes", "sss_meetings");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property<MeetingCommentId>("_meetingCommentId").HasColumnName("meeting_comment_id");
        builder.Property<MemberId>("_memberId").HasColumnName("member_id");
    }
}
