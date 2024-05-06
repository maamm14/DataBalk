using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserTaskApi.Models.Domain;
using UserTaskApi.Models.DTO.User;

namespace UserTaskApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper, UserManager<User> userManager)
        {

            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")] // Users with "Admin" role can access
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users);

            foreach (var userDto in userDtos)
            {
                var user = await _userManager.FindByIdAsync(userDto.Id);
                var roles = await _userManager.GetRolesAsync(user);
                userDto.Roles = roles.ToArray();
            }

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")] // Users with "Admin" or "User" role can access
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            UserDto userDto = _mapper.Map<UserDto>(user);
            var roles = await _userManager.GetRolesAsync(user);
            userDto.Roles = roles.ToArray(); // Convert List<string> to string[]

            return Ok(userDto);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] AdminRegister registerRequest)
        {

            // Handle the case where no roles are provided
            if (registerRequest.Roles == null || !registerRequest.Roles.Any() || registerRequest.Roles.Any(string.IsNullOrWhiteSpace))
            {
                return BadRequest("User registered Failed. No roles provided.");
            }

            User identityUser = new User
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.Email,
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
            };

            IdentityResult identityResult = await _userManager.CreateAsync(identityUser, registerRequest.Password);

            // Handle the case where user creation fails
            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors);
                // return BadRequest("User registered Failed. User creation failed.");
            }

            identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequest.Roles);

            if (identityResult.Succeeded)
            {
                return Ok("User registered Succeeded");
            }

            // Handle the case where adding roles fails
            return BadRequest("User registered Failed. Adding roles failed.");

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")] // Users with "Admin" or "User" role can access
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Update user properties
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Email = updateUserDto.Email;
            user.UserName = updateUserDto.UserName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors); // Return errors if update fails
            }

            return NoContent(); // User updated successfully
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only users with the "Admin" role can access this method
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                // Handle delete errors
                return BadRequest(result.Errors);
            }

            return NoContent(); // User deleted successfully
        }


    }
}