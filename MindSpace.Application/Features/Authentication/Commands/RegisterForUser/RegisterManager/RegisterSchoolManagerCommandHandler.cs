using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.Authentication.DTOs;
using MindSpace.Application.Features.Authentication.Services;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.Owned;
using MindSpace.Domain.Services.Authentication;
using OfficeOpenXml;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterManager
{
    public class RegisterSchoolManagerCommandHandler
        (ILogger<RegisterSchoolManagerCommandHandler> logger,
        UserRegistrationService userRegistrationService,
        IExcelReaderService excelReaderService) : IRequestHandler<RegisterSchoolManagerCommand>
    {
        public async Task Handle(RegisterSchoolManagerCommand request, CancellationToken cancellationToken)
        {
            var results = await excelReaderService.ReadExcelAsync(request.file);

            var managers = new List<RegisterUserDTO>();
            var schoolManagerMappings = new List<(SchoolManager Manager, School School)>();

            foreach (var result in results)
            {
                SchoolManager schoolManager = new SchoolManager()
                {
                    Email = result["Email"],
                    UserName = result["UserName"],
                };

                Address newSchoolAddress = new Address()
                {
                    Street = result["Street"],
                    City = result["City"],
                    Ward = result["Ward"],
                    PostalCode = result["PostalCode"],
                    Province = result["Province"]
                };

                School newSchool = new School()
                {
                    SchoolName = result["SchoolName"],
                    ContactEmail = result["ContactEmail"],
                    PhoneNumber = result["PhoneNumber"],
                    Address = newSchoolAddress,
                    Description = result["Description"],
                    CreateAt = DateTime.Now,
                    JoinDate = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };

                // Store the mapping for later linking after user registration
                schoolManagerMappings.Add((schoolManager, newSchool));
            }

            using (var stream = new MemoryStream())
            {
                await request.file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var userDTO = new RegisterUserDTO()
                        {
                            UserName = worksheet.Cells[row, 1].Text,
                            Email = worksheet.Cells[row, 2].Text,
                            Password = worksheet.Cells[row, 3].Text,
                            Role = UserRoles.SchoolManager
                        };

                        managers.Add(userDTO);
                    }
                }

                // Register users and get their IDs back
                var registeredUsers = (await userRegistrationService.RegisterUserAsync(managers)).ToList();

                // Now link the schools and managers using the returned user IDs
                for (int i = 0; i < registeredUsers.Count; i++)
                {
                    var (manager, school) = schoolManagerMappings[i];
                    var userId = registeredUsers[i].Id;

                    // Set up the bidirectional relationship
                    manager.Id = userId;
                    school.ManagerId = userId;
                    manager.School = school;
                    school.SchoolManager = manager;

                    // Save both entities in a single transaction
                    await userRegistrationService.SaveSchoolManagerAndSchoolAsync(manager, school, cancellationToken);
                }
            }
        }
    }
}