﻿using Microsoft.AspNetCore.Http;
using MindSpace.Application.Commons.Identity;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using System.Security.Claims;

namespace MindSpace.Infrastructure.Services.AuthenticationServices
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = httpContextAccessor?.HttpContext?.User ?? throw new InvalidOperationException("User context is not present");

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);

            return new CurrentUser(userId, email, roles);
        }

        public int? GetCurrentUserId()
        {
            var user = httpContextAccessor?.HttpContext.User ?? throw new InvalidOperationException("User context is not present");

            if (!user.Identities.Any() || !user.Identity.IsAuthenticated)
            {
                return null;
            }
            int.TryParse(user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value, out int intUserId);
            return intUserId;
        }
    }
}