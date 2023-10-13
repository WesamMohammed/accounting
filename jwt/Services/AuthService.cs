
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using jwt.Helpers;
using jwt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Jwt _jwt;

        public AuthService(UserManager<ApplicationUser> userManager , IOptions<Jwt> jwt, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
        }

        public async Task<AuthModel> LoginAsync(LoginModel loginModel)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if(user is null || !await _userManager.CheckPasswordAsync(user,loginModel.Password))
            {
                authModel.Message = "Email or Password in Correct";
                return authModel;
            }
            var token = await CreatJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(token);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.IsAuthenticated = true;
            authModel.Expiration = token.ValidTo;
            authModel.Roles = roles.ToList();

            return authModel;


        }

        public async Task<AuthModel> RegisterAsync(RegisterModel registerModel)
        {
            var authModel = new AuthModel();
            if (await _userManager.FindByEmailAsync(registerModel.Email) is not null)
            {
                authModel.Message = "This Email already exist";
                return authModel;

            }
            if (await _userManager.FindByNameAsync(registerModel.UserName) is not null)
            {
                authModel.Message = "This UserName already exist";
                return authModel;
           
            }
            var user = new ApplicationUser()
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Email = registerModel.Email,
                UserName = registerModel.UserName,

            };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors = $"{error.Description},";
                }
                authModel.Message = errors;
                return authModel;
            }
            await _userManager.AddToRoleAsync(user, "User");

            var token = await CreatJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            authModel.Token =new JwtSecurityTokenHandler().WriteToken(token);
            authModel.Email=user.Email;
            authModel.UserName=user.UserName;
            authModel.IsAuthenticated = true;
            authModel.Expiration = token.ValidTo;
            authModel.Roles=roles.ToList();

            return authModel;
        }


        public async Task<string> AddRoleToUser(AddRoleToUserModel addRoleToUserModel)
        {
            var user = await _userManager.FindByIdAsync(addRoleToUserModel.UserId);
            if(user is null || !await _roleManager.RoleExistsAsync(addRoleToUserModel.Role))
            {
                return "User Or Role Not exist";

            }
            if(await _userManager.IsInRoleAsync(user, addRoleToUserModel.Role))
            {
                return "this User already has this Role";
            }

            var result = await _userManager.AddToRoleAsync(user, addRoleToUserModel.Role);
            return result.Succeeded  ? String.Empty:"something went wrong";

        }





        private async Task<JwtSecurityToken> CreatJwtToken(ApplicationUser user)
        {
            var userClaims= await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach(var role in roles)
            {
                var therole = await _roleManager.Roles.Where(a => a.Name == role).FirstOrDefaultAsync();
                var permissions = await _roleManager.GetClaimsAsync(therole);
                foreach (var permission in permissions)
                {
                    roleClaims.Add(new Claim(permission.Type, permission.Value));
                }
               
            }
            var claims = new[]
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub,user.UserName),
               new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id),

            }.Union(userClaims).Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var singinCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                signingCredentials: singinCredentials,
                expires: DateTime.Now.AddMinutes(_jwt.DurationInMinuts),
                claims: claims
                );
            return jwtSecurityToken;
            
        }


    }
}
