﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VehicleApi.Persistance;

namespace VehicleApi.Persistance.Auth.User
{
    public partial class UserService(UserManager<UserInfo> userManager,
        SignInManager<UserInfo> signInManager,
        RoleManager<IdentityRole> roleManager,
        VehicleApiDbContext applicationDbContext,
        TokenSettings tokenSettings)
    {
        public readonly UserManager<UserInfo> _userManager = userManager;
        private readonly SignInManager<UserInfo> _signInManager = signInManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly TokenSettings _tokenSettings = tokenSettings;
        private readonly VehicleApiDbContext _context = applicationDbContext;

        private async Task<UserLoginResponse> GenerateUserToken(UserInfo user)
        {
            var claims = (from ur in _context.UserRoles
                          where ur.UserId == user.Id
                          join r in _context.Roles on ur.RoleId equals r.Id
                          join rc in _context.RoleClaims on r.Id equals rc.RoleId
                          select rc)
              .Where(rc => !string.IsNullOrEmpty(rc.ClaimValue) && !string.IsNullOrEmpty(rc.ClaimType))
              .Select(rc => new Claim(rc.ClaimType!, rc.ClaimValue!))
              .Distinct()
              .ToList();

            var roleClaims = (from ur in _context.UserRoles
                              where ur.UserId == user.Id
                              join r in _context.Roles on ur.RoleId equals r.Id
                              select r)
              .Where(r => !string.IsNullOrEmpty(r.Name))
              .Select(r => new Claim(ClaimTypes.Role, r.Name!))
              .Distinct()
              .ToList();

            claims.AddRange(roleClaims);

            var token = TokenUtil.GetToken(_tokenSettings, user, claims);
            await _userManager.RemoveAuthenticationTokenAsync(user, "REFRESHTOKENPROVIDER", "RefreshToken");
            var refreshToken = await _userManager.GenerateUserTokenAsync(user, "REFRESHTOKENPROVIDER", "RefreshToken");
            await _userManager.SetAuthenticationTokenAsync(user, "REFRESHTOKENPROVIDER", "RefreshToken", refreshToken);
            return new UserLoginResponse() { AccessToken = token, RefreshToken = refreshToken };
        }
    }
}
