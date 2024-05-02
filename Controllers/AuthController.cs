using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]")]
public class AuthController : BaseController{
    string secretKey;
    public AuthController(SiteProvider provider, IConfiguration configuration) : base(provider){
        secretKey = configuration.GetValue<string>("Jwt:secretKey") ?? string.Empty;
    }
    [HttpPost("Login")]
    public  IActionResult Login(LoginModel obj){
        try{
            Member? member = provider.Member.Login(obj);
            if (member != null){    
                if (member.isdeleted != null && member.isdeleted.Value){
                    MemberPassword mp = new MemberPassword{
                        memberid = member.memberid,
                        password = obj.Pwd
                    };
                    return BadRequest(provider.Member.PasswordChangeTime(mp));    
                }
                else{
                    string randomMemLogId = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    List<Claim> claims = new List<Claim>{
                        new Claim(ClaimTypes.NameIdentifier, member.memberid),
                        new Claim(ClaimTypes.Name, member.fullname),
                        new Claim(ClaimTypes.Sid, randomMemLogId)
                    };
                    IEnumerable<RoleOfMember> list = provider.Role.GetRoleOfMember(member.memberid);
                    if (list != null){
                        foreach (var item in list){
                            claims.Add(new Claim(ClaimTypes.Role, item.RoleId.ToString()));
                        }
                    }
                    MemberLoginAdd memberLoginAdd = new MemberLoginAdd{
                        memLogId = randomMemLogId,
                        username = obj.Usr
                    };
                    // add memberlogin
                    provider.MemberLogin.Add(memberLoginAdd);

                    // Genereate Token
                    Token token = provider.Helper.GenerateToken(secretKey, claims);
  
                    return Ok(token);        
                }           
            }
            return BadRequest("Người dùng không tồn tại!");
        }catch{
            return BadRequest("Tên đăng nhập hoặc mật khẩu không chính xác!");
        }
    }
    [HttpPost("Refresh")]
    public IActionResult Refresh(Token obj){ 
        try{
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters{
                // tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,
                // ky vao token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),

                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false // khong kiem tra Toke het han
            };
                RefreshToken? refreshToken = provider.RefreshToken.GetRefreshToken(obj.rt);
                if (refreshToken != null){
                    // check 1: AccessToken valid format, neu AccessToken expired la nhay sang catch ngay lap tuc
                    ClaimsPrincipal tokenInVerification = handler.ValidateToken(obj.at, parameters, out SecurityToken validatedToken); // validatedToken: token đã được xác thực

                    // check 2: check alg (thuat toan)
                    if (validatedToken is JwtSecurityToken jwtSecurityToken){
                        var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase); // khi tạo là chữ ký nên có signature, đây là renew nên không cần ký (không cần signature)
                        if (!result){ // fasle
                            return BadRequest("Invalid Token");
                        }
                    }

                    // Genereate Renew Token
                    Token token = provider.Helper.GenerateToken(secretKey, tokenInVerification.Claims);
                    
                    return Ok(token);
                }else{
                    return BadRequest("Refresh token does not exist");  
                }
        }catch{
            return BadRequest("Refresh token failed");
        }
    }
    [HttpPost("Logout"), Authorize]
    public IActionResult Logout(Token obj){
        try{
            RefreshToken? GetRefreshToken = provider.RefreshToken.GetRefreshToken(obj.rt);
            if (GetRefreshToken != null){
                provider.MemberLogin.Edit(GetRefreshToken.memberloginid);
                provider.RefreshToken.DeleteRefreshToken(GetRefreshToken.memberloginid);
                return Ok("Logout thành công");
            }  
            return BadRequest("Logout thất bại");
        }catch{
            return BadRequest("Logout thất bại");
        }
    }
}