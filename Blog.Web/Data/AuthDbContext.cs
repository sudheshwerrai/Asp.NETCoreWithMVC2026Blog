using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed Roles (Admin, SuperAdmin, User)

            string adminRoleId = "cde15906-5c51-4928-8817-64d5731db9a0";
            string superAdminRoleId = "2a55759a-8247-453c-9a52-59ec9681b2b7";
            string userRoleId = "4d117d6d-74f2-4407-966f-4e3cb65ab167";
            
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="Admin",
                    Id=adminRoleId,
                    ConcurrencyStamp=adminRoleId
                },
                new IdentityRole
                {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=superAdminRoleId,
                    ConcurrencyStamp=superAdminRoleId
                },
                new IdentityRole
                {
                    Name="User",
                    NormalizedName="User",
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
            //Seed SuperAdminUser
            string superAdminId = "d8f93bfe-df48-4aa2-bd16-ee21245cd71e";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@blogapp.com",
                NormalizedUserName = "superadmin@blogapp.com",
                Email = "superadmin@blogapp.com",
                NormalizedEmail = "superadmin@blogapp.com",
                Id=superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "SuperAdmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);


            //Add all roles to the SuperAdminUser

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId=adminRoleId,
                    UserId=superAdminId
                },
                 new IdentityUserRole<string>
                {
                    RoleId=superAdminRoleId,
                    UserId=superAdminId
                },
                  new IdentityUserRole<string>
                {
                    RoleId=userRoleId,
                    UserId=superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

        }
    }
}
