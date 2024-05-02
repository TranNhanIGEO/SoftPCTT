using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;

namespace WebApi.Services;
public class Helper : BaseRepository{
    public Helper(IDbConnection connection) : base(connection){}
    public byte[] Hash(string plaintext){
        HashAlgorithm algorithm = SHA512.Create();
        return algorithm.ComputeHash(Encoding.ASCII.GetBytes(plaintext));
    }
    public string HashString(string plaintext){
        return Convert.ToHexString(Hash(plaintext)).ToLower();
    }
    public Token GenerateToken(string SecretKey, IEnumerable<Claim> claims){
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SymmetricSecurityKey symmetric = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        SigningCredentials credentials = new SigningCredentials(symmetric, SecurityAlgorithms.HmacSha256Signature);
        JwtSecurityToken securityToken = new JwtSecurityToken(claims: claims, signingCredentials: credentials, expires: DateTime.Now.AddSeconds(30));
        
        string accessToken = handler.WriteToken(securityToken);
        string refreshToken = GenerateRefreshToken();

        ClaimsIdentity identity = new ClaimsIdentity(claims);
        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
        RefreshToken obj = new RefreshToken{
            refreshtokenid = Guid.NewGuid().ToString().Replace("-", string.Empty),
            memberloginid = principal.FindFirstValue(ClaimTypes.Sid)!,
            token = Convert.ToBase64String(Hash(refreshToken + "igeo"))
        };
        connection.Execute("AddRefreshToken", new{
            _refreshtokenid = obj.refreshtokenid,
            _memlogid = obj.memberloginid,
            _token = obj.token
        }, commandType: CommandType.StoredProcedure);
        return new Token{
            at = accessToken,
            rt = refreshToken
        };
    }
    public string GenerateRefreshToken(){
        return Guid.NewGuid().ToString().Replace("-", string.Empty);
        // byte[] random = new byte[32];
        // using(var rand = RandomNumberGenerator.Create()){
        //     rand.GetBytes(random);
        // }
        // return Convert.ToBase64String(random);
    }
    public DateTime ConvertUnixTimeToDateTime(long utcExpireDate){
        DateTime dateTimeInterval = new DateTime(1970,1,1,0,0,0,0, DateTimeKind.Utc);
        dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
        return dateTimeInterval;
    }
}