using Microsoft.AspNetCore.Mvc;
using DBLaba6.MainDB;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DBLaba6.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly OpendatamodelContext _context;

        public UserController(ILogger<UserController> logger, OpendatamodelContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _context.Users.ToListAsync();

            return Ok(users);
        }


        [HttpGet("id")]
        public async Task<IActionResult> GetUserId(int id)
        {
            var user = await _context.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound($"User with current ID '{id}' wasn't found in the database");
            }

            return Ok(user);
        }


        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deluser = await _context.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();

            if (deluser == null)
            {
                return NotFound("The user with such ID doesn't exist");
            }

            _context.Users.Remove(deluser);

            await _context.SaveChangesAsync();

            return Ok(new { message = "User deleted successfully.", userId = id });
        }


        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                var errorMesgs = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return BadRequest(string.Join(", ", errorMesgs));
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == user.Login);

            if (existingUser != null)
            {
                return Conflict("A user with the same login already exists.");
            }

            var newUser = new User
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password 
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddUser), new { id = newUser.UserId }, newUser);
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return BadRequest(new { errors });
            }

            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);

            if (existUser == null)
            {
                return NotFound($"User with ID '{user.UserId}' not found.");
            }

            var existLogin = await _context.Users.FirstOrDefaultAsync(x => x.Login == user.Login && x.UserId != user.UserId);

            if (existLogin != null)
            {
                return Conflict($"A user with the login '{user.Login}' already exists.");
            }

            existUser.Firstname = user.Firstname;
            existUser.Lastname = user.Lastname;
            existUser.Email = user.Email;
            existUser.Login = user.Login;
            existUser.Password = user.Password;

            await _context.SaveChangesAsync();

            return Ok(new { message = "User updated successfully.", userId = user.UserId });
        }
    }
}
