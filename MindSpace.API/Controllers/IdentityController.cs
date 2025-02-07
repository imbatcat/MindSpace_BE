using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.Authentication.Commands.LoginUser;
using MindSpace.Application.Features.Authentication.Commands.RefreshUserAccessToken;
using MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterManager;
using MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterParent;
using MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterPsychologist;
using MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterStudent;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace MindSpace.API.Controllers
{
    public class IdentityController(
        IMediator mediator,
        UserManager<ApplicationUser> userManager) : BaseApiController
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterParentCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginUserCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Refresh token is required");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(refreshToken);

            if (jwtToken.ValidTo < DateTime.UtcNow.AddSeconds(3))
            {
                return Unauthorized("Invalid or expired refresh token");
            }

            var user = await userManager.FindByIdAsync(jwtToken.Subject);
            if (user == null || user.RefreshToken == null || !user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid refresh token");
            }

            var newTokens = await mediator.Send(new RefreshUserAccessTokenCommand(user));
            return Ok(newTokens);
        }

        // Admin registers account for School Manager
        [HttpPost("register-for/manager")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RegisterSchoolManager([FromForm] RegisterSchoolManagerCommand command)
        {
            await mediator.Send(command);

            return Ok("All managers have been added");
        }

        // Admin registers account for Psychologist
        [HttpPost("register-for/psychologist")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RegisterPsychologist([FromBody] RegisterPsychologistCommand command)
        {
            await mediator.Send(command);
            return Ok("All psychologists have been added");
        }

        // School Manager registers account for students
        [HttpPost("register-for/student")]
        [Authorize(Roles = UserRoles.SchoolManager)]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentsCommand command)
        {
            await mediator.Send(command);
            return Ok("Student registered successfully");
        }
    }
}