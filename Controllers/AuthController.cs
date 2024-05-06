using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserTaskApi.Models.Domain;
using UserTaskApi.Models.DTO.User;
using UserTaskApi.Repositories.Token;

namespace UserTaskApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<User> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        //POST: api/Auth/Register
        [HttpPost()]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerRequest)
        {

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
            string[] Role = ["User"];
            // Possible null reference argument.
            identityResult = await _userManager.AddToRolesAsync(identityUser, Role);

            if (identityResult.Succeeded)
            {
                return Ok("User registered Succeeded");
            }

            // Handle the case where adding roles fails
            return BadRequest("User registered Failed. Adding roles failed.");

        }

        [HttpPost()]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto loginRequest)
        {
            User? user = await _userManager.FindByEmailAsync(loginRequest.Username);
            if (user != null)
            {
                bool checkPass = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
                if (checkPass)
                {
                    IList<string> role = await _userManager.GetRolesAsync(user);
                    if (role != null)
                    {
                        string jwtToken = _tokenRepository.CreateJWTToken(user, role.ToList());
                        //Create Token
                        LoginResponseDto response = new LoginResponseDto(jwtToken);
                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or Password is incorrect");
        }

    }
}