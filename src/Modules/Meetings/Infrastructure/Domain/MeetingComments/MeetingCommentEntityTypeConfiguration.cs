using System;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingComments;

public class MeetingCommentEntityTypeConfiguration : IEntityTypeConfiguration<MeetingComment>
{
    public void Configure(EntityTypeBuilder<MeetingComment> builder)
    {
        builder.ToTable("meeting_comments", "sss_meetings");

        builder.HasKey(c => c.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property<string>("_comment").HasColumnName("comment");
        builder.Property<MeetingId>("_meetingId").HasColumnName("meeting_id");
        builder.Property<MemberId>("_authorId").HasColumnName("author_id");
        builder.Property<MeetingCommentId>("_inReplyToCommentId").HasColumnName("in_reply_to_comment_id");
        builder.Property<bool>("_isRemoved").HasColumnName("is_removed");
        builder.Property<string>("_removedByReason").HasColumnName("removed_by_reason");
        builder.Property<DateTime>("_createDate").HasColumnName("create_date")
            .HasConversion(
                src => DateTimeConverter.UtcDateTime(src),
                dest => DateTimeConverter.UtcDateTime(dest));

        builder.Property<DateTime?>("_editDate").HasColumnName("edit_date")
            .HasConversion(
                src => DateTimeConverter.MaybeUtcDateTime(src),
                dest => DateTimeConverter.MaybeUtcDateTime(dest));
    }
}
