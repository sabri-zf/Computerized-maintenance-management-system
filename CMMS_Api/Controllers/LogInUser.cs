using CMMS_Api.DTO;
using CMMS_Api.Helper;
using Computerized_maintenance_Logic_layer.Module.User_Management;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Computerized_maintenance_Logic_layer.Module.DTO;

namespace CMMS_Api.Controllers
{
    [ApiController]
    [Route("LogInUser")]
    public class LogInUser : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("auth")]

        // add permission 

        public ActionResult<Api_UserLoginDto> LoginUser(UserLogindto userDto)
        {
            if (userDto is null) return NotFound("Invalid Data");

            var UserInfo = ClsUsers.FindUser(userDto.UserName);

            if (!(UserInfo is ClsUsers))
            {
                return Unauthorized("Invalid Username or Password");
            }

            JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor TokenDescriptor = new SecurityTokenDescriptor()
            {

                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, UserInfo.UserID.ToString()),
                        new Claim(ClaimTypes.Name,UserInfo.UserName),
                        new Claim(ClaimTypes.Email, UserInfo.Email)
                    }
                    ),

                Issuer = JwtMapping.Instance!.Issuer,
                Audience = JwtMapping.Instance!.Audience,
                Expires = DateTime.UtcNow.AddMinutes(JwtMapping.Instance.LifeTime),
                SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtMapping.Instance.IssuerSigningKey))
                                        , SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = TokenHandler.CreateToken(TokenDescriptor);

            string produc_Token = TokenHandler.WriteToken(securityToken);

              

            return Ok(new Api_UserLoginDto{ UserName = UserInfo.UserName, Token = produc_Token});
        }
    }
}
