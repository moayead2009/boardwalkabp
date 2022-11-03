using API.Models.Domain;
using API.Models.DTO;
using Data.Contexts;
using Data.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = user.Id }, user);
        //}

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return Ok(status);
            }

            //if (_context.Users.Any(u => u.Email == request.Email))
            if (_context.Users.Any(u => u.Username == request.Username))
            {
                status.StatusCode = 0;
                status.Message = "Invalid username.";
                return Ok(status);
            }

            CreatePasswordHash(request.Password,
                 out byte[] passwordHash,
                 out byte[] passwordSalt);

            var user = new User
            {
                Name = request.Name,
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                //PhoneNumber = request.PhoneNumber,
                //Address = request.Address,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                VerificationToken = CreateRandomToken()
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            status.StatusCode = 1;
            status.Message = "User successfully registered! :D";
            return Ok(status);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
            {
                return Ok(new UserLoginResponse
                {
                    StatusCode = 0,
                    Message = "User not found.",
                });
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Ok(new UserLoginResponse
                {
                    StatusCode = 0,
                    Message = "Invalid password.",
                });
            }

            //if (user.VerifiedAt == null)
            //{
            //    return BadRequest("Not verified!");
            //}

            return Ok(new UserLoginResponse
            {
                Name = user.Name,
                Username = user.Username,
                StatusCode = 1,
                Message = "User successfully logged in! :D",
            });
        }

        // [HttpPost("verify")]
        // public async Task<IActionResult> Verify(string token)
        // {
        //     var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
        //     if (user == null)
        //     {
        //         return BadRequest("Invalid token.");
        //     }

        //     user.VerifiedAt = DateTime.Now;
        //     await _context.SaveChangesAsync();

        //     return Ok("User verified! :)");
        // }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string username)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            return Ok("You may now reset your password.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _context.SaveChangesAsync();

            return Ok("Password successfully reset. :)");
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

    }

}
