using ApplicationBuildingPlatformAPI.Models;
using ApplicationBuildingPlatformAPI.Models.Domain;
using ApplicationBuildingPlatformAPI.Models.DTO;
using ApplicationBuildingPlatformAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApplicationBuildingPlatformAPI.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ITokenService _tokenService;

        public AuthController(DatabaseContext databaseContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService
            )
        {
            this._databaseContext = databaseContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._tokenService = tokenService;
        }

        // we may comment this method if we want to limit admins
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin([FromBody] Register registerDTO)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return Ok(status);
            }

            // check if user exists
            var userExists = await userManager.FindByNameAsync(registerDTO.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return Ok(status);
            }

            var user = new ApplicationUser
            {
                UserName = registerDTO.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = registerDTO.Email,
                Name = registerDTO.Name
            };

            // create a user here
            var result = await userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return Ok(status);
            }

            // specifing role
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            status.StatusCode = 1;
            status.Message = "Sucessfully registered";
            return Ok(status);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] Register registerDTO)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return Ok(status);
            }

            // check if user exists
            var userExists = await userManager.FindByNameAsync(registerDTO.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return Ok(status);
            }

            var user = new ApplicationUser
            {
                UserName = registerDTO.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = registerDTO.Email,
                Name = registerDTO.Name
            };

            // create a user here
            var result = await userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return Ok(status);
            }

            // specifing role
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }
            status.StatusCode = 1;
            status.Message = "Sucessfully registered";
            return Ok(status);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterClient([FromBody] Register registerDTO)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return Ok(status);
            }

            // check if user exists
            var userExists = await userManager.FindByNameAsync(registerDTO.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return Ok(status);
            }

            var user = new ApplicationUser
            {
                UserName = registerDTO.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = registerDTO.Email,
                Name = registerDTO.Name
            };

            // create a user here
            var result = await userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return Ok(status);
            }

            // specifing role
            if (!await roleManager.RoleExistsAsync(UserRoles.Client))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Client));

            if (await roleManager.RoleExistsAsync(UserRoles.Client))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Client);
            }
            status.StatusCode = 1;
            status.Message = "Sucessfully registered";
            return Ok(status);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login loginDTO)
        {
            var user = await userManager.FindByNameAsync(loginDTO.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginDTO.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var token = _tokenService.GetToken(authClaims);
                var refreshToken = _tokenService.GetRefreshToken();
                var tokenInfo = _databaseContext.TokenInfo.FirstOrDefault(a => a.Username == user.UserName);

                if (tokenInfo == null)
                {
                    var info = new TokenInfo
                    {
                        Username = user.UserName,
                        RefreshToken = refreshToken,
                        RefreshTokenExpiry = DateTime.Now.AddDays(7)
                    };
                    _databaseContext.TokenInfo.Add(info);
                }

                else
                {
                    tokenInfo.RefreshToken = refreshToken;
                    tokenInfo.RefreshTokenExpiry = DateTime.Now.AddDays(7);
                }
                try
                {
                    // await _databaseContext.SaveChangesAsync();
                    _databaseContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(new LoginResponse
                {
                    Name = user.Name,
                    Username = user.UserName,
                    Token = token.TokenString,
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo,
                    StatusCode = 1,
                    Message = "Logged in"
                });
            }

            // if login failed
            return Ok(
                new LoginResponse
                {
                    StatusCode = 0,
                    Message = "Invalid Username or Password",
                    Token = "",
                    Expiration = null
                });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword changePasswordDTO)
        {
            var status = new Status();

            // check validations
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the valid fields";
                return Ok(status);
            }
            // find the user
            var user = await userManager.FindByNameAsync(changePasswordDTO.Username);
            if (user is null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return Ok(status);
            }

            // check current password
            if (!await userManager.CheckPasswordAsync(user, changePasswordDTO.CurrentPassword))
            {
                status.StatusCode = 0;
                status.Message = "Invalid current password";
                return Ok(status);
            }

            // change password
            var result = await userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "Failed to change password";
                return Ok(status);
            }

            status.StatusCode = 1;
            status.Message = "Password has changed successfully";
            return Ok(result);
        }


    }
}
