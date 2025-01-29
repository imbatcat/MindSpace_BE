using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Commons.Utilities;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Infrastructure.Persistence;
using System.Runtime.CompilerServices;

namespace MindSpace.Infrastructure.Seeders
{
    internal class IdentitySeeder(
        ILogger<IdentitySeeder> logger,
        ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : IDataSeeder
    {
        // =====================================
        // === Props & Fields
        // =====================================

        private const string FAKE_PASSWORD = "pass123";

        // =====================================
        // === Methods
        // =====================================

        /// <summary>
        /// Seeding File 
        /// </summary>
        /// <returns></returns>
        public async Task SeedAsync()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                try
                {
                    IEnumerable<ApplicationUser> users = null;
                    if (!dbContext.Roles.Any())
                    {
                        var roles = GetRoles();
                        dbContext.Roles.AddRange(roles);
                        await dbContext.SaveChangesAsync();
                    }
                    if (!dbContext.Users.Any())
                    {
                        users = GetUsers();
                        foreach (var user in users)
                        {
                            await userManager.CreateAsync(user, FAKE_PASSWORD);
                        }
                    }
                    if (users is not not null && !dbContext.UserRoles.Any()) await GetUserRoles(users);
                }
                catch (Exception ex)
                {
                    logger.LogError("{ex}", ex.Message);
                }
            }
        }

        /// <summary>
        /// Get list of Users
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ApplicationUser> GetUsers()
        {
            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new Student
                {
                    UserName = "student1",
                    NormalizedUserName = "STUDENT1",
                    Email = "student1@example.com",
                    NormalizedEmail = "STUDENT1@EXAMPLE.COM",
                    FullName = "Student One",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    SchoolId = 1
                },
                new Student
                {
                    UserName = "student2",
                    NormalizedUserName = "STUDENT2",
                    Email = "student2@example.com",
                    NormalizedEmail = "STUDENT2@EXAMPLE.COM",
                    FullName = "Student Two",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    SchoolId = 2
                },
                new Student
                {
                    UserName = "student3",
                    NormalizedUserName = "STUDENT3",
                    Email = "student3@example.com",
                    NormalizedEmail = "STUDENT3@EXAMPLE.COM",
                    FullName = "Student Three",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    SchoolId = 3
                },
                new Student
                {
                    UserName = "student4",
                    NormalizedUserName = "STUDENT4",
                    Email = "student4@example.com",
                    NormalizedEmail = "STUDENT4@EXAMPLE.COM",
                    FullName = "Student Four",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    SchoolId = 4
                },
                new Student
                {
                    UserName = "student5",
                    NormalizedUserName = "STUDENT5",
                    Email = "student5@example.com",
                    NormalizedEmail = "STUDENT5@EXAMPLE.COM",
                    FullName = "Student Five",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    SchoolId = 5
                },
                new ApplicationUser {
                    UserName = "admin1",
                    NormalizedUserName = "ADMIN1",
                    Email = "admin1@example.com",
                    NormalizedEmail = "ADMIN1@EXAMPLE.COM",
                    FullName = "Admin One",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Psychologist
                {
                    UserName = "psychologist1",
                    NormalizedUserName = "PSYCHOLOGIST1",
                    Email = "psychologist1@example.com",
                    NormalizedEmail = "PSYCHOLOGIST1@EXAMPLE.COM",
                    FullName = "Psychologist One",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Psychologist
                {
                    UserName = "psychologist2",
                    NormalizedUserName = "PSYCHOLOGIST2",
                    Email = "psychologist2@example.com",
                    NormalizedEmail = "PSYCHOLOGIST2@EXAMPLE.COM",
                    FullName = "Psychologist Two",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Psychologist
                {
                    UserName = "psychologist3",
                    NormalizedUserName = "PSYCHOLOGIST3",
                    Email = "psychologist3@example.com",
                    NormalizedEmail = "PSYCHOLOGIST3@EXAMPLE.COM",
                    FullName = "Psychologist Three",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Manager
                {
                    UserName = "Manager1",
                    NormalizedUserName = "Manager1",
                    Email = "Manager1@example.com",
                    NormalizedEmail = "Manager1@EXAMPLE.COM",
                    FullName = "School Manager One",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    SchoolId = 1
                },
                new Manager
                {
                    UserName = "Manager2",
                    NormalizedUserName = "Manager2",
                    Email = "Manager2@example.com",
                    NormalizedEmail = "Manager2@EXAMPLE.COM",
                    FullName = "School Manager Two",
                    Status = UserStatus.Enabled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    SchoolId = 2
                },
                new ApplicationUser
                {
                    UserName = "parent1",
                    NormalizedUserName = "PARENT1",
                    Email = "parent1@example.com",
                    NormalizedEmail = "PARENT1@EXAMPLE.COM",
                    FullName = "Parent One",
                },
                new ApplicationUser
                {
                    UserName = "parent2",
                    NormalizedUserName = "PARENT2",
                    Email = "parent2@example.com",
                    NormalizedEmail = "PARENT2@EXAMPLE.COM",
                    FullName = "Parent Two",
                }
            };

            return users;
        }

        private IEnumerable<ApplicationRole> GetRoles()
        {
            List<ApplicationRole> roles = new List<ApplicationRole>
            {
                new ApplicationRole(UserRoles.Student) {
                    NormalizedName = UserRoles.Student.ToUpper()
                },
                new ApplicationRole(UserRoles.Admin) {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
                new ApplicationRole(UserRoles.Psychologist) {
                    NormalizedName = UserRoles.Psychologist.ToUpper()
                },
                new ApplicationRole(UserRoles.Manager) {
                    NormalizedName = UserRoles.Manager.ToUpper()
                },
                new ApplicationRole(UserRoles.Parent) {
                    NormalizedName = UserRoles.Parent.ToUpper()
                },
            };

            return roles;
        }

        private async Task GetUserRoles(IEnumerable<ApplicationUser> users)
        {
            foreach (var user in users)
            {
                switch (user.FullName.ToLower())
                {
                    case "student":
                        await userManager.AddToRoleAsync(user, UserRoles.Student);
                        break;

                    case "parent":
                        await userManager.AddToRoleAsync(user, UserRoles.Parent);
                        break;

                    case "admin":
                        await userManager.AddToRoleAsync(user, UserRoles.Admin);
                        break;

                    case "psychologist":
                        await userManager.AddToRoleAsync(user, UserRoles.Psychologist);
                        break;

                    case "manager":
                        await userManager.AddToRoleAsync(user, UserRoles.Manager);
                        break;
                }
            }
        }
    }
}