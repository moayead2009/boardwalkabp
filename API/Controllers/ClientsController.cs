using API.Models.Domain;
using API.Models.DTO;
using Data.Contexts;
using Data.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ClientsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(Guid id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        //[HttpPost]
        //public async Task<ActionResult<Client>> PostClient(Client client)
        //{
        //    _context.Clients.Add(client);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetClient", new { id = client.Id }, client);
        //}

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(Guid id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(ClientRegisterRequest request)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return Ok(status);
            }

            //if (_context.Clients.Any(c => c.Email == request.Email))
            if (_context.Clients.Any(c => c.Username == request.Username))
            {
                status.StatusCode = 0;
                status.Message = "Invalid username.";
                return Ok(status);
            }

            CreatePasswordHash(request.Password,
                 out byte[] passwordHash,
                 out byte[] passwordSalt);

            var client = new Client
            {
                Name = request.Name,
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                //ApplicationId = request.ApplicationId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                VerificationToken = CreateRandomToken()
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            status.StatusCode = 1;
            status.Message = "Client successfully registered! :D";
            return Ok(status);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(ClientLoginRequest request)
        {
            //var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == request.Email);
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Username == request.Username);
            if (client == null)
            {
                return Ok(new ClientLoginResponse
                {
                    StatusCode = 0,
                    Message = "Client not found.",
                });
            }

            if (!VerifyPasswordHash(request.Password, client.PasswordHash, client.PasswordSalt))
            {
                return Ok(new ClientLoginResponse
                {
                    StatusCode = 0,
                    Message = "Invalid password.",
                });
            }

            //if (client.VerifiedAt == null)
            //{
            //    return BadRequest("Not verified!");
            //}

            return Ok(new ClientLoginResponse
            {
                Name = client.Name,
                Username = client.Username,
                StatusCode = 1,
                Message = "Client successfully logged in! :D",
            });
        }

        //[HttpPost("verify")]
        //public async Task<IActionResult> Verify(string token)
        //{
        //    var client = await _context.Clients.FirstOrDefaultAsync(c => c.VerificationToken == token);
        //    if (client == null)
        //    {
        //        return BadRequest("Invalid token.");
        //    }

        //    client.VerifiedAt = DateTime.Now;
        //    await _context.SaveChangesAsync();

        //    return Ok("Client verified! :)");
        //}

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string username)
        {
            //var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == email);
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Username == username);
            if (client == null)
            {
                return BadRequest("Client not found.");
            }

            client.PasswordResetToken = CreateRandomToken();
            client.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            return Ok("You may now reset your password.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.PasswordResetToken == request.Token);
            if (client == null || client.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            client.PasswordHash = passwordHash;
            client.PasswordSalt = passwordSalt;
            client.PasswordResetToken = null;
            client.ResetTokenExpires = null;

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
