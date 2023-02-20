using ApiDevelopment.Context;
using ApiDevelopment.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiDevelopment.Controllers
{
    [ApiController]
    
    public class AuthenticationController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        public readonly PostgreSQLDataContext _context;
        public AuthenticationController(PostgreSQLDataContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context; 
        }
        [HttpPost("Login")]
        public ActionResult<User> Login(Auth request)
        {
            try
            {
                if(request != null)
                {
                    var repoData = _context.registrations.Where(x=> x.Name == request.Name
                    && x.Password == request.Password).FirstOrDefault();
                    if(repoData == null)
                    {
                        throw new ApplicationException("Invalid Credentials");
                    }
                    else
                    {
                        string token = CreateToken(user);
                        return Ok(new
                        {
                            IsSuccess = true,
                            Message = "Logged in successful",
                            UserDetail = repoData,
                            Token = token
                        });
                    }
                }
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string CreateToken(User user) 
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Name)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature );
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
       
    }
}
