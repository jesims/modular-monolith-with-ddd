using System;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.Meetings
{
    internal class MeetingEntityTypeConfiguration : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.ToTable("meetings", "sss_meetings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property<MeetingGroupId>("_meetingGroupId").HasColumnName("meeting_group_id");
            builder.Property<string>("_title").HasColumnName("title");
            builder.Property<string>("_description").HasColumnName("description");
            builder.Property<MemberId>("_creatorId").HasColumnName("creator_id");
            builder.Property<MemberId>("_changeMemberId").HasColumnName("change_member_id");
            builder.Property<DateTime>("_createDate").HasColumnName("create_date")
                .HasConversion(
                    src => DateTimeConverter.UtcDateTime(src),
                    dest => DateTimeConverter.UtcDateTime(dest));

            builder.Property<DateTime?>("_changeDate").HasColumnName("change_date")
                .HasConversion(
                    src => DateTimeConverter.MaybeUtcDateTime(src),
                    dest => DateTimeConverter.MaybeUtcDateTime(dest));

            builder.Property<DateTime?>("_cancelDate").HasColumnName("cancel_date")
                .HasConversion(
                    src => DateTimeConverter.MaybeUtcDateTime(src),
                    dest => DateTimeConverter.MaybeUtcDateTime(dest));

            builder.Property<bool>("_isCanceled").HasColumnName("is_canceled");
            builder.Property<MemberId>("_cancelMemberId").HasColumnName("cancel_member_id");

            builder.OwnsOne<MeetingTerm>("_term", b =>
            {
                b.Property(p => p.StartDate).HasColumnName("term_start_date");
                b.Property(p => p.EndDate).HasColumnName("term_end_date");
            });

            builder.OwnsOne<Term>("_rsvpTerm", b =>
            {
                b.Property(p => p.StartDate).HasColumnName("rsvp_term_start_date");
                b.Property(p => p.EndDate).HasColumnName("rsvp_term_end_date");
            });

            builder.OwnsOne<MoneyValue>("_eventFee", b =>
            {
                b.Property(p => p.Value).HasColumnName("event_fee_value");
                b.Property(p => p.Currency).HasColumnName("event_fee_currency");
            });

            builder.OwnsOne<MeetingLocation>("_location", b =>
            {
                b.Property(p => p.Name).HasColumnName("location_name");
                b.Property(p => p.Address).HasColumnName("location_address");
                b.Property(p => p.PostalCode).HasColumnName("location_postal_code");
                b.Property(p => p.City).HasColumnName("location_city");
            });

            builder.OwnsMany<MeetingAttendee>("_attendees", y =>
            {
                y.ToTable("meeting_attendees", "sss_meetings");
                y.Property<MemberId>("AttendeeId").HasColumnName("attendee_id");
                y.Property<MeetingId>("MeetingId").HasColumnName("meeting_id");
                y.Property<DateTime>("_decisionDate").HasColumnName("decision_date")
                    .HasConversion(
                        src => DateTimeConverter.UtcDateTime(src),
                        dest => DateTimeConverter.UtcDateTime(dest));

                y.HasKey("AttendeeId", "MeetingId", "_decisionDate");
                y.Property<bool>("_decisionChanged").HasColumnName("decision_changed");
                y.Property<int>("_guestsNumber").HasColumnName("guests_number");
                y.Property<DateTime?>("_decisionChangeDate").HasColumnName("decision_change_date")
                    .HasConversion(
                        src => DateTimeConverter.MaybeUtcDateTime(src),
                        dest => DateTimeConverter.MaybeUtcDateTime(dest));

                y.Property<bool>("_isRemoved").HasColumnName("is_removed");
                y.Property<string>("_removingReason").HasColumnName("removing_reason");
                y.Property<MemberId>("_removingMemberId").HasColumnName("removing_member_id");
                y.Property<DateTime?>("_removedDate").HasColumnName("removed_date")
                    .HasConversion(
                        src => DateTimeConverter.MaybeUtcDateTime(src),
                        dest => DateTimeConverter.MaybeUtcDateTime(dest));

                y.Property<bool>("_isFeePaid").HasColumnName("is_fee_paid");

                y.OwnsOne<MeetingAttendeeRole>("_role", b =>
                {
                    b.Property(x => x.Value).HasColumnName("role_code");
                });

                y.OwnsOne<MoneyValue>("_fee", b =>
                {
                    b.Property(p => p.Value).HasColumnName("fee_value");
                    b.Property(p => p.Currency).HasColumnName("fee_currency");
                });
            });

            builder.OwnsMany<MeetingNotAttendee>("_notAttendees", y =>
            {
                y.ToTable("meeting_not_attendees", "sss_meetings");
                y.Property<MemberId>("MemberId").HasColumnName("member_id");
                y.Property<MeetingId>("MeetingId").HasColumnName("meeting_id");
                y.Property<DateTime>("_decisionDate").HasColumnName("decision_date")
                    .HasConversion(
                        src => DateTimeConverter.UtcDateTime(src),
                        dest => DateTimeConverter.UtcDateTime(dest));

                y.HasKey("MemberId", "MeetingId", "_decisionDate");
                y.Property<bool>("_decisionChanged").HasColumnName("decision_changed");
                y.Property<DateTime?>("_decisionChangeDate").HasColumnName("decision_change_date")
                    .HasConversion(
                        src => DateTimeConverter.MaybeUtcDateTime(src),
                        dest => DateTimeConverter.MaybeUtcDateTime(dest));
            });

            builder.OwnsMany<MeetingWaitlistMember>("_waitlistMembers", y =>
            {
                y.ToTable("meeting_waitlist_members", "sss_meetings");
                y.Property<MemberId>("MemberId").HasColumnName("member_id");
                y.Property<MeetingId>("MeetingId").HasColumnName("meeting_id");
                y.Property<DateTime>("SignUpDate").HasColumnName("sign_up_date")
                    .HasConversion(
                        src => DateTimeConverter.UtcDateTime(src),
                        dest => DateTimeConverter.UtcDateTime(dest));

                y.HasKey("MemberId", "MeetingId", "SignUpDate");
                y.Property<bool>("_isSignedOff").HasColumnName("is_signed_off");
                y.Property<DateTime?>("_signOffDate").HasColumnName("sign_off_date")
                    .HasConversion(
                        src => DateTimeConverter.MaybeUtcDateTime(src),
                        dest => DateTimeConverter.MaybeUtcDateTime(dest));

                y.Property<bool>("_isMovedToAttendees").HasColumnName("is_moved_to_attendees");
                y.Property<DateTime?>("_movedToAttendeesDate").HasColumnName("moved_to_attendees_date")
                    .HasConversion(
                        src => DateTimeConverter.MaybeUtcDateTime(src),
                        dest => DateTimeConverter.MaybeUtcDateTime(dest));
            });

            builder.OwnsOne<MeetingLimits>("_meetingLimits", meetingLimits =>
            {
                meetingLimits.Property(x => x.AttendeesLimit).HasColumnName("attendees_limit");
                meetingLimits.Property(x => x.GuestsLimit).HasColumnName("guests_limit");
            });
        }
    }
}
