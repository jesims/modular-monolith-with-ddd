using CompanyName.MyMeetings.Modules.Administration.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Domain.Members
{
    internal class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("members", "sss_administration");

            builder.HasKey(x => x.Id);

            builder.Property(b => b.Id).HasColumnName("id");
            builder.Property<string>("_login").HasColumnName("login");
            builder.Property<string>("_email").HasColumnName("email");
            builder.Property<string>("_firstName").HasColumnName("first_name");
            builder.Property<string>("_lastName").HasColumnName("last_name");
            builder.Property<string>("_name").HasColumnName("name");
        }
    }
}
