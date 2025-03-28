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
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImageUrl = "https://res.cloudinary.com/ddewgbug1/image/upload/v1743195515/mind-space/zp4frywxsde3sya8zshy.jpg",
                PhoneNumber = "+84345678901",
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
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImageUrl = "https://res.cloudinary.com/ddewgbug1/image/upload/v1743195516/mind-space/awdfep4zwr7ngztslnid.jpg",
                PhoneNumber = "+84345678902",
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
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImageUrl = "https://res.cloudinary.com/ddewgbug1/image/upload/v1743195516/mind-space/acz1cb7sbflq0iuexfre.jpg",
                PhoneNumber = "+84345678903",
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
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImageUrl = "https://res.cloudinary.com/ddewgbug1/image/upload/v1743195517/mind-space/lmxhh3gxwwhatnzskgvx.jpg",
                PhoneNumber = "+84345678904",
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
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImageUrl = "https://res.cloudinary.com/ddewgbug1/image/upload/v1743195516/mind-space/gwygrny5ztzxye7t4c3h.jpg",
                PhoneNumber = "+84345678905",
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
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImageUrl = "https://res.cloudinary.com/ddewgbug1/image/upload/v1743195517/mind-space/buybzzdrmmd2uf8xgszq.jpg",
                PhoneNumber = "+84345678906",
                DateOfBirth = new DateTime(1990, 1, 1)
            },
            new Psychologist
            {
                UserName = "psychologist1",
                NormalizedUserName = "PSYCHOLOGIST1",
                Email = "psychologist1@example.com",
                NormalizedEmail = "PSYCHOLOGIST1@EXAMPLE.COM",
                FullName = "Psychologist One",
                Bio = "Hi, I like cats and doraemon",
                AverageRating = 4.8f,
                SessionPrice = 500000,
                ComissionRate = 0.1m,
                Status = UserStatus.Enabled,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImageUrl = "https://res.cloudinary.com/ddewgbug1/image/upload/v1743195517/mind-space/o2iyjqqabumef71w7sbq.jpg",
                PhoneNumber = "+84345678907",
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
                AverageRating = 4.7f,
                SessionPrice = 600000,
                ComissionRate = 0.15m,
                Status = UserStatus.Enabled,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImageUrl = "https://res.cloudinary.com/ddewgbug1/image/upload/v1743195518/mind-space/r8ytdesaeoo5un0pdh19.jpg",
                PhoneNumber = "+84345678908",
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
                AverageRating = 4.9f,
                SessionPrice = 700000,
                ComissionRate = 0.12m,
                Status = UserStatus.Enabled,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImageUrl = "https://res.cloudinary.com/ddewgbug1/image/upload/v1743195518/mind-space/fyn9nvy1y8lmzcjlyx1q.jpg",
                PhoneNumber = "+84345678909",
                DateOfBirth = new DateTime(1985, 3, 3),
                SpecializationId = 3
            },
            new SchoolManager
            {
                UserName = "manager1",
                NormalizedUserName = "MANAGER1",
                Email = "manager1@example.com",
                NormalizedEmail = "Manager1@EXAMPLE.COM",
                FullName = "SchoolManager One",
                Status = UserStatus.Enabled,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImageUrl = "https://res.cloudinary.com/ddewgbug1/image/upload/v1743195518/mind-space/xg0j9bcn1kge8i9k6cm4.jpg",
                PhoneNumber = "+84345678910",
                SchoolId = 1,
                DateOfBirth = new DateTime(1975, 1, 1)
            },
            new SchoolManager
            {
                UserName = "manager2",
                NormalizedUserName = "MANAGER2",
                Email = "manager2@example.com",
                NormalizedEmail = "Manager2@EXAMPLE.COM",
                FullName = "SchoolManager Two",
                Status = UserStatus.Enabled,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImageUrl = "https://res.cloudinary.com/ddewgbug1/image/upload/v1743195518/mind-space/aknlw64rbxppja5cvesl.jpg",
                PhoneNumber = "+84345678911",
                SchoolId = 2,
                DateOfBirth = new DateTime(1978, 2, 2)
            },
            new Parent()
            {
                UserName = "parent1",
                NormalizedUserName = "PARENT1",
                Email = "parent1@example.com",
                NormalizedEmail = "PARENT1@EXAMPLE.COM",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                FullName = "Parent One",
                ImageUrl = "https://res.cloudinary.com/ddewgbug1/image/upload/v1743195515/mind-space/zp4frywxsde3sya8zshy.jpg",
                PhoneNumber = "+84345678912",
                DateOfBirth = new DateTime(1970, 1, 1)
            },
            new Parent()
            {
                UserName = "parent2",
                NormalizedUserName = "PARENT2",
                Email = "parent2@example.com",
                NormalizedEmail = "PARENT2@EXAMPLE.COM",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                FullName = "Parent Two",
                ImageUrl = "https://res.cloudinary.com/ddewgbug1/image/upload/v1743195516/mind-space/awdfep4zwr7ngztslnid.jpg",
                PhoneNumber = "+84345678913",
                DateOfBirth = new DateTime(1972, 2, 2)
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
            new(UserRoles.SchoolManager)
            {
                NormalizedName = UserRoles.SchoolManager.ToUpper()
            },
            new(UserRoles.Parent)
            {
                NormalizedName = UserRoles.Parent.ToUpper()
            }
        };

        return roles;
    }
}
