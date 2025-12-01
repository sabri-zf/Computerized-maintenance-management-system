using CMMS_Api.DTO;
using CMMS_Api.Helper;
using Computerized_maintenance_Logic_layer.Module.User_Management;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Computerized_maintenance_Logic_layer.Module.DTO;
using System.Threading.Tasks;
using Computerized_maintenance_Logic_layer.Module.User_Management.Extensions;

namespace CMMS_Api.Controllers
{
    [ApiController]
    [Route("LogInUser")]
    public class LogInUser(ClsUsers _instance, UserService _service) : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("auth")]

        // add permission 

        public async Task<ActionResult<UserLoginDto>> LoginUser(UserLogindto userDto)
        {
            if (userDto is null) return NotFound("Invalid Data");

            var CheckOutAuthorization = await _service.Check_Validation_UserName_And_PasswordAsync(userDto.UserName,userDto.Password);

            if (!CheckOutAuthorization )
            {
                return Unauthorized("Invalid Username or Password");
            }


            var bring_UserInfo = await _instance.BringUserWithIdAsync(userDto.UserName);

            if( bring_UserInfo is null)
            {
                return NotFound("Not Found :User you looked up on it Doesn't exist");
            }


            JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor TokenDescriptor = new SecurityTokenDescriptor()
            {

                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, bring_UserInfo.UserID.ToString()),
                        new Claim(ClaimTypes.Name,bring_UserInfo.UserName),
                        new Claim(ClaimTypes.Email, bring_UserInfo.Email),
                        new Claim(ClaimTypes.Role, bring_UserInfo.Role.ToString()),
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

            string MakeToken = TokenHandler.WriteToken(securityToken);

              
            return Ok(new UserLoginDto(bring_UserInfo.UserName,DateTime.Now.ToString(),MakeToken ));
        }
    }
}
