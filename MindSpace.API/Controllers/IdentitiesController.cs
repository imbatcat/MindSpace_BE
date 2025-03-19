using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Features.ApplicationUsers.Commands.ToggleAccountStatus;
using MindSpace.Application.Features.ApplicationUsers.Commands.UpdateProfile;
using MindSpace.Application.Features.ApplicationUsers.Queries.ViewAllAccounts;
using MindSpace.Application.Features.ApplicationUsers.Queries.ViewAllStudents;
using MindSpace.Application.Features.ApplicationUsers.Queries.ViewProfile;
using MindSpace.Application.Features.ApplicationUsers.Queries.ViewProfileById;
using MindSpace.Application.Features.Authentications.Commands.ConfirmEmail;
using MindSpace.Application.Features.Authentications.Commands.LoginUser;
using MindSpace.Application.Features.Authentications.Commands.LogoutUser;
using MindSpace.Application.Features.Authentications.Commands.RefreshUserAccessToken;
using MindSpace.Application.Features.Authentications.Commands.RegisterForUser.RegisterManager;
using MindSpace.Application.Features.Authentications.Commands.RegisterForUser.RegisterParent;
using MindSpace.Application.Features.Authentications.Commands.RegisterForUser.RegisterPsychologist;
using MindSpace.Application.Features.Authentications.Commands.RegisterForUser.RegisterStudent;
using MindSpace.Application.Features.Authentications.Commands.ResetPassword;
using MindSpace.Application.Features.Authentications.Commands.RevokeUser;
using MindSpace.Application.Features.Authentications.Commands.SendEmailConfirmation;
using MindSpace.Application.Features.Authentications.Commands.SendResetPasswordEmail;
using MindSpace.Application.Features.Schools.Queries.ViewAllSchools;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;
using System.IdentityModel.Tokens.Jwt;

namespace MindSpace.API.Controllers
{
    public class IdentitiesController(
        IMediator mediator,
        UserManager<ApplicationUser> userManager) : BaseApiController
    {
        // ==============================
        // === POST, PUT, DELETE, PATCH
        // ==============================

        // POST /api/identities/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterParentCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        // POST /api/identities/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginUserCommand command)
        {
            LoginResponseDTO response;
            try
            {
                response = await mediator.Send(command);
            } catch (DuplicateUserException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        // POST /api/identities/logout
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await mediator.Send(new LogoutUserCommand
            {
                Response = Response
            });

            Response.Cookies.Delete("refreshToken");
            return NoContent();
        }

        // POST /api/identities/refresh
        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult<RefreshUserAccessTokenDTO>> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Refresh token is required");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(refreshToken);
            // 3 seconds clock skew, this is to account for the time it takes for the token to reach the server
            if (jwtToken.ValidTo < DateTime.Now.AddSeconds(3))
            {
                return Unauthorized("Expired refresh token");
            }

            var user = await userManager.FindByIdAsync(jwtToken.Subject);
            if (user == null || user.RefreshToken == null || !user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid refresh token");
            }

            var newTokens = await mediator.Send(new RefreshUserAccessTokenCommand(user));
            return Ok(newTokens);
        }

        // POST /api/identities/revoke/{id}
        [HttpPost("revoke/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RevokeRefreshToken(string id)
        {
            try
            {
                await mediator.Send(new RevokeUserCommand { UserId = id });
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found");
            }
        }

        // POST /api/identities/register-for/manager
        [HttpPost("register-for/manager")]
        public async Task<IActionResult> RegisterSchoolManager([FromForm] RegisterSchoolManagerCommand command)
        {
            await mediator.Send(command);
            return Ok("All managers have been added");
        }

        // POST /api/identities/register-for/psychologist
        [HttpPost("register-for/psychologist")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RegisterPsychologist([FromForm] RegisterPsychologistCommand command)
        {
            await mediator.Send(command);
            return Ok("All psychologists have been added");
        }

        // POST /api/identities/register-for/student
        [HttpPost("register-for/student")]
        [Authorize(Roles = UserRoles.SchoolManager)]
        public async Task<IActionResult> RegisterStudent([FromForm] RegisterStudentsCommand command)
        {
            await mediator.Send(command);
            return Ok("Student registered successfully");
        }

        // POST /api/identities/send-email-confirmation
        [HttpPost("send-email-confirmation")]
        [Authorize]
        public async Task<IActionResult> SendEmailConfirmation()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Logic to send email confirmation
            await mediator.Send(new SendEmailConfirmationCommand(user));
            return NoContent();
        }

        // POST /api/identities/confirm-email
        [HttpPost("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommand command)
        {
            var result = await mediator.Send(command);
            if (result)
            {
                return Ok("Email confirmed successfully");
            }
            return BadRequest("Email confirmation failed");
        }

        // POST /api/identities/send-reset-password-email
        [HttpPost("send-reset-password-email")]
        [AllowAnonymous]
        public async Task<IActionResult> SendResetPasswordEmail([FromBody] SendResetPasswordEmailCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        // POST /api/identities/reset-password
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await mediator.Send(command);
            if (result)
            {
                return Ok("Password reset successfully");
            }
            return BadRequest("Password reset failed");
        }

        // PUT /api/identities/profile/{id}
        [InvalidateCache("/api/identities|")]
        [HttpPut("profile/{id}")]
        [Authorize]
        public async Task<ActionResult<ApplicationUserProfileDTO>> UpdateProfile([FromBody] UpdateProfileCommand command, [FromRoute] int id)
        {
            command.UserId = id;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        // PUT /api/identities/accounts/{id}/toggle-status
        [InvalidateCache("/api/identities/accounts|")]
        [HttpPut("accounts/{id}/toggle-status")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> ToggleAccountStatus([FromRoute] int id)
        {
            await mediator.Send(new ToggleAccountStatusCommand { UserId = id });
            return NoContent();
        }

        // ==============================
        // === GET
        // ==============================

        // GET /api/identities/profile
        [Cache(600)]
        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<ApplicationUserProfileDTO>> GetProfile()
        {
            var result = await mediator.Send(new ViewProfileQuery());
            return Ok(result);
        }

        // GET /api/identities/profile/{id}
        [Cache(600)]
        [HttpGet("profile/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<ApplicationUserProfileDTO>> GetProfileById(int id)
        {
            var result = await mediator.Send(new ViewProfileByIdQuery { UserId = id });
            return Ok(result);
        }

        // GET /api/identities/accounts
        [Cache(30000)]
        [HttpGet("accounts")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<Pagination<ApplicationUserProfileDTO>>> GetAllAccounts([FromQuery] ApplicationUserSpecParams specParams)
        {
            var result = await mediator.Send(new ViewAllAccountsQuery(specParams));
            return PaginationOkResult(
                result.Data,
                result.Count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }

        // GET /api/identities/accounts/students
        [Cache(30000)]
        [HttpGet("accounts/students")]
        [Authorize(Roles = UserRoles.SchoolManager)]
        public async Task<ActionResult<Pagination<ApplicationUserProfileDTO>>> GetAllStudents([FromQuery] ApplicationUserSpecParams specParams)
        {
            var result = await mediator.Send(new ViewAllStudentsQuery(specParams));
            return PaginationOkResult(
                result.Data,
                result.Count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }
    }
}