using System;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.Members
{
    internal class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("members", "sss_meetings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property<string>("_login").HasColumnName("login");
            builder.Property<string>("_email").HasColumnName("email");
            builder.Property<string>("_firstName").HasColumnName("first_name");
            builder.Property<string>("_lastName").HasColumnName("last_name");
            builder.Property<string>("_name").HasColumnName("name");
            builder.Property<DateTime>("_createDate").HasColumnName("create_date")
                .HasConversion(
                    src => DateTimeConverter.UtcDateTime(src),
                    dest => DateTimeConverter.UtcDateTime(dest));
        }
    }
}
