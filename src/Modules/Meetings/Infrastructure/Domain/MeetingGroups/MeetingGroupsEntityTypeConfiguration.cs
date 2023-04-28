using System;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingGroups
{
    internal class MeetingGroupsEntityTypeConfiguration : IEntityTypeConfiguration<MeetingGroup>
    {
        public void Configure(EntityTypeBuilder<MeetingGroup> builder)
        {
            builder.ToTable("meeting_groups", "sss_meetings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property<string>("_name").HasColumnName("name");
            builder.Property<string>("_description").HasColumnName("description");
            builder.Property<MemberId>("_creatorId").HasColumnName("creator_id");
            builder.Property<DateTime>("_createDate").HasColumnName("create_date")
                .HasConversion(
                    src => DateTimeConverter.UtcDateTime(src),
                    dest => DateTimeConverter.UtcDateTime(dest));

            builder.Property<DateTime?>("_paymentDateTo").HasColumnName("payment_date_to")
                .HasConversion(
                    src => DateTimeConverter.MaybeUtcDateTime(src),
                    dest => DateTimeConverter.MaybeUtcDateTime(dest));

            builder.OwnsOne<MeetingGroupLocation>("_location", b =>
            {
                b.Property(p => p.City).HasColumnName("location_city");
                b.Property(p => p.CountryCode).HasColumnName("location_country_code");
            });

            builder.OwnsMany<MeetingGroupMember>("_members", y =>
            {
                y.ToTable("meeting_group_members", "sss_meetings");
                y.Property<MemberId>("MemberId").HasColumnName("member_id");
                y.Property<MeetingGroupId>("MeetingGroupId").HasColumnName("meeting_group_id");
                y.Property<DateTime>("JoinedDate").HasColumnName("joined_date")
                    .HasConversion(
                        src => DateTimeConverter.UtcDateTime(src),
                        dest => DateTimeConverter.UtcDateTime(dest));

                y.HasKey("MemberId", "MeetingGroupId", "JoinedDate");

                y.Property<DateTime?>("_leaveDate").HasColumnName("leave_date")
                    .HasConversion(
                        src => DateTimeConverter.MaybeUtcDateTime(src),
                        dest => DateTimeConverter.MaybeUtcDateTime(dest));

                y.Property<bool>("_isActive").HasColumnName("is_active");

                y.OwnsOne<MeetingGroupMemberRole>("_role", b =>
                {
                    b.Property<string>(x => x.Value).HasColumnName("role_code");
                });
            });
        }
    }
}
