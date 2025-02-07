namespace MindSpace.Infrastructure.Seeders.FakeData;

using Domain.Entities.Constants;
using Domain.Entities.Identity;

internal static class IdentityData
{
    public static IEnumerable<ApplicationUser> GetUsers()
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
                SchoolId = 1,
                DateOfBirth = new DateTime(2000, 1, 1)
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
                SchoolId = 1,
                DateOfBirth = new DateTime(2000, 2, 2)
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
                SchoolId = 1,
                DateOfBirth = new DateTime(2000, 3, 3)
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
                SchoolId = 2,
                DateOfBirth = new DateTime(2000, 4, 4)
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
                SchoolId = 2,
                DateOfBirth = new DateTime(2000, 5, 5)
            },
            new()
            {
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
                Bio = "Hi, I like cats and doraemon",
                Status = UserStatus.Enabled,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DateOfBirth = new DateTime(1980, 1, 1),
                SpecializationId = 1
            },
            new Psychologist
            {
                UserName = "psychologist2",
                NormalizedUserName = "PSYCHOLOGIST2",
                Email = "psychologist2@example.com",
                NormalizedEmail = "PSYCHOLOGIST2@EXAMPLE.COM",
                FullName = "Psychologist Two",
                Bio = "Hi, I like cats and doraemon",
                Status = UserStatus.Enabled,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DateOfBirth = new DateTime(1982, 2, 2),
                SpecializationId = 2
            },
            new Psychologist
            {
                UserName = "psychologist3",
                NormalizedUserName = "PSYCHOLOGIST3",
                Email = "psychologist3@example.com",
                NormalizedEmail = "PSYCHOLOGIST3@EXAMPLE.COM",
                FullName = "Psychologist Three",
                Bio = "Hi, I like cats and doraemon",
                Status = UserStatus.Enabled,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DateOfBirth = new DateTime(1985, 3, 3),
                SpecializationId = 3
            },
            new Manager
            {
                UserName = "manager1",
                NormalizedUserName = "MANAGER1",
                Email = "Manager1@example.com",
                NormalizedEmail = "Manager1@EXAMPLE.COM",
                FullName = "Manager One",
                Status = UserStatus.Enabled,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SchoolId = 1
            },
            new Manager
            {
                UserName = "manager2",
                NormalizedUserName = "MANAGER2",
                Email = "Manager2@example.com",
                NormalizedEmail = "Manager2@EXAMPLE.COM",
                FullName = "Manager Two",
                Status = UserStatus.Enabled,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SchoolId = 2
            },
            new()
            {
                UserName = "parent1",
                NormalizedUserName = "PARENT1",
                Email = "parent1@example.com",
                NormalizedEmail = "PARENT1@EXAMPLE.COM",
                FullName = "Parent One"
            },
            new()
            {
                UserName = "parent2",
                NormalizedUserName = "PARENT2",
                Email = "parent2@example.com",
                NormalizedEmail = "PARENT2@EXAMPLE.COM",
                FullName = "Parent Two"
            }
        };

        return users;
    }

    public static IEnumerable<ApplicationRole> GetRoles()
    {
        List<ApplicationRole> roles = new List<ApplicationRole>
        {
            new(UserRoles.Student)
            {
                NormalizedName = UserRoles.Student.ToUpper()
            },
            new(UserRoles.Admin)
            {
                NormalizedName = UserRoles.Admin.ToUpper()
            },
            new(UserRoles.Psychologist)
            {
                NormalizedName = UserRoles.Psychologist.ToUpper()
            },
            new(UserRoles.Manager)
            {
                NormalizedName = UserRoles.Manager.ToUpper()
            },
            new(UserRoles.Parent)
            {
                NormalizedName = UserRoles.Parent.ToUpper()
            }
        };

        return roles;
    }
}
