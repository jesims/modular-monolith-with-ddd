using CompanyName.MyMeetings.Modules.UserAccess.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Domain.Users
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", "sss_users");

            builder.HasKey(x => x.Id);

            builder.Property(b => b.Id).HasColumnName("id");
            builder.Property<string>("_login").HasColumnName("login");
            builder.Property<string>("_email").HasColumnName("email");
            builder.Property<string>("_password").HasColumnName("password");
            builder.Property<bool>("_isActive").HasColumnName("is_active");
            builder.Property<string>("_firstName").HasColumnName("first_name");
            builder.Property<string>("_lastName").HasColumnName("last_name");
            builder.Property<string>("_name").HasColumnName("name");

            builder.OwnsMany<UserRole>("_roles", b =>
            {
                //b.WithOwner().HasForeignKey("user_id");
                b.ToTable("user_roles", "sss_users");
                b.Property<UserId>("UserId").HasColumnName("user_id");
                b.Property<string>("Value").HasColumnName("role_code");
                b.HasKey("UserId", "Value");
            });
        }
    }
}
