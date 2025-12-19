using CollegeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration; 
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;

        }


        [HttpPost]
        public ActionResult Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide username and password");
            }

            LoginResponseDTO response = new() { Username = model.UserName };
            string audience = string.Empty;
            string issuer = string.Empty;
            byte[] key = null;

            var keyJWTSecretforGoogle = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretforGoogle"));
            var keyJWTSecretforMicrosoft = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretforMicrosoft"));
            var keyJWTSecretforLocal = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretforLocal"));

            if (model.Policy == "Local")
            {
                issuer = _configuration.GetValue<string>("LocalIssuer");
                audience = _configuration.GetValue<string>("LocalAudience");
                key = keyJWTSecretforLocal;
            }

            else if (model.Policy == "Microsoft")
            {

                issuer = _configuration.GetValue<string>("MicrosoftIssuer");
                audience = _configuration.GetValue<string>("MicrosoftAudience");

                key = keyJWTSecretforMicrosoft;
            }
            else if (model.Policy == "Google")
            {
                issuer = _configuration.GetValue<string>("GoogleIssuer");
                audience = _configuration.GetValue<string>("GoogleAudience");
                key = keyJWTSecretforGoogle;
            }



            if (model.UserName == "Nischal" && model.Password == "Nischal123")
            {
                //Here we need to generate jwt token 
                 key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretforLocal"));
                var tokenHandler = new JwtSecurityTokenHandler(); // responsible for creating jwt and writing jwt as string
                var tokenDescriptor = new SecurityTokenDescriptor() // the object describes what goes inside token
                {

                    Issuer = issuer,
                    Audience = audience,


                    //Add claim who the user is 
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    { 
                    
                        //Username 
                        new Claim(ClaimTypes.Name,model.UserName),
                        //Role
                        new Claim(ClaimTypes.Role,"Admin")
                    }),

                    //Expiration to the token
                    Expires = DateTime.Now.AddHours(4),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)

                };


                //Now Generate the token

                var token = tokenHandler.CreateToken(tokenDescriptor);
                response.token = tokenHandler.WriteToken(token);

            }
            else
            {
                return Ok("Invalid username and password");
            }
            return Ok(response);
        }

       

    }
}