using System;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Domain.UserRegistrations;

internal class UserRegistrationEntityTypeConfiguration : IEntityTypeConfiguration<UserRegistration>
{
    public void Configure(EntityTypeBuilder<UserRegistration> builder)
    {
        builder.ToTable("user_registrations", "sss_users");

        builder.HasKey(x => x.Id);

        builder.Property(b => b.Id).HasColumnName("id");
        builder.Property<string>("_login").HasColumnName("login");
        builder.Property<string>("_email").HasColumnName("email");
        builder.Property<string>("_password").HasColumnName("password");
        builder.Property<string>("_firstName").HasColumnName("first_name");
        builder.Property<string>("_lastName").HasColumnName("last_name");
        builder.Property<string>("_name").HasColumnName("name");
        builder.Property<DateTime>("_registerDate").HasColumnName("register_date")
            .HasConversion(
                src => DateTimeConverter.UtcDateTime(src),
                dest => DateTimeConverter.UtcDateTime(dest));

        builder.Property<DateTime?>("_confirmedDate").HasColumnName("confirmed_date")
            .HasConversion(
                src => DateTimeConverter.MaybeUtcDateTime(src),
                dest => DateTimeConverter.MaybeUtcDateTime(dest));

        builder.OwnsOne<UserRegistrationStatus>("_status",
            b => { b.Property(x => x.Value).HasColumnName("status_code"); });
    }
}
