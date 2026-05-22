using Microsoft.AspNetCore.Mvc; 
using Microsoft.IdentityModel.Tokens; 
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims; 
using System.Text; 
 
namespace FundAdministrationAPI.Controllers 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class AuthenticationController : ControllerBase 
    { 
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger)
        {           
            _logger = logger;
        }

        [HttpPost("GetToken")] 
        public IActionResult GetToken() 
        { 
            _logger.LogInformation("Generate Auth Token");
            var claims = new[] 
            { 
                new Claim(ClaimTypes.Name, "admin") 
            }; 
 
            var key = new SymmetricSecurityKey( 
                Encoding.UTF8.GetBytes("ThisIsMySymmetricSecurityKeyJwtKey123456789")); 
 
            var creds = new SigningCredentials( 
                key, 
                SecurityAlgorithms.HmacSha256); 
 
            var token = new JwtSecurityToken( 
                claims: claims, 
                expires: DateTime.Now.AddHours(1), 
                signingCredentials: creds); 
 
            var tokenString = new JwtSecurityTokenHandler() 
                .WriteToken(token); 
 
            return Ok(new 
            { 
                token = tokenString 
            }); 
        } 
    } 
}