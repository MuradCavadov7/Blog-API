
using BlogApp.BL.DTOs.Options;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.Core.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApp.BL.ExternalServices.Implements;
public class JwtHandler : IJwtHandler
{
    readonly JwtOptions opt;
    public JwtHandler(IOptions<JwtOptions> _opt)
    {
        opt = _opt.Value;
    }
    public string CreateJwtToken(User user, int hours)
    {
        List<Claim> claims = [
            new Claim("Username",user.Username),
            new Claim("Email",user.Email),
            new Claim("Role",user.Role.ToString()),
            new Claim("Fullname",user.Name + " " + user.Surname)
            ];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(opt.SecretKey));
        SigningCredentials credentials = new(key,SecurityAlgorithms.HmacSha256);
        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer:opt.Issuer,
            audience:opt.Audience,
            claims : claims,
            notBefore : DateTime.Now,
            expires : DateTime.Now.AddHours(hours),
            signingCredentials : credentials
            );
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(securityToken);
    }
}
