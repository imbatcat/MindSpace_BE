using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.Authentication.Commands.LoginUser;
using MindSpace.Application.Features.Authentication.Commands.RegisterUser;

namespace MindSpace.API.Controllers
{
    [ApiController]
    [Route("api/identities")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterUserCommand command)
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

        //[HttpPost("refresh")]
        //[AllowAnonymous]
        //public async Task<ActionResult> Refresh([FromBody] LoginRequest loginRequest)
        //{
            //var userManager = sp.GetRequiredService<UserManager<User>>();
            //var userStore = sp.GetRequiredService<IUserStore<User>>();
            //var passwordStore = sp.GetRequiredService<PasswordHasher<User>>();
            //var tokenProvider = sp.GetRequiredService<IdTokenProvider>();

            //var signInManager = sp.GetRequiredService<SignInManager<User>>();
            //var user = await userManager.FindByEmailAsync(loginRequest.Email);
            ////TODO: Add !user.EmailVerified)
            //if (user is null)
            //{
            //    return BadRequest("User was not found");
            //}

            //var passwordVerificationResult = passwordStore.VerifyHashedPassword(user, user.PasswordHash!, loginRequest.Password);

            //if (passwordVerificationResult.HasFlag(PasswordVerificationResult.Failed))
            //{
            //    return BadRequest("Incorrect password");
            //}

            ////TODO: maybe add 'remember me' option
            //var result = await signInManager.PasswordSignInAsync(
            //    user,
            //    loginRequest.Password,
            //    isPersistent: false,
            //    lockoutOnFailure: false);

            //if (!result.Succeeded) //result may not succeed due to invalid 2FA code, not just incorrect password.
            //{
            //    return Unauthorized("Your authentication attempt failed, please try again with valid credentials");
            //}
            //string token = tokenProvider.CreateToken(user);

            //return Ok(token);
        //}
    }
}
