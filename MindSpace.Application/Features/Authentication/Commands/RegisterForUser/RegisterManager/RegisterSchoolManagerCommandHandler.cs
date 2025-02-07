using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Commons.Constants;
using MindSpace.Application.Features.Authentication.Services;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using OfficeOpenXml;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterManager
{
    public class RegisterSchoolManagerCommandHandler
        (ILogger<RegisterSchoolManagerCommandHandler> logger,
        UserRegistrationService userRegistrationService) : IRequestHandler<RegisterSchoolManagerCommand>
    {
        public async Task Handle(RegisterSchoolManagerCommand request, CancellationToken cancellationToken)
        {
            var file = request.file;

            if (file == null || request.file.Length == 0)
            {
                throw new BadHttpRequestException("No file uploaded.");
            }

            if (!file.FileName.EndsWith(".xlsx"))
            {
                throw new BadHttpRequestException("Invalid file type.");
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = new MemoryStream())
            {
                var managers = new List<RegisterUserDTO>();
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

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
                        string schoolId = worksheet.Cells[row, 4].Text;
                    }
                }
                await userRegistrationService.RegisterUserAsync(managers);
            }
        }
    }
}