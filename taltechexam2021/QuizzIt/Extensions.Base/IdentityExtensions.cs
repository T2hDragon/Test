using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Extensions.Base
{
    public static class IdentityExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal principal)
        {
            return GetUserId<Guid>(principal);
            /*
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                return Guid.Parse(userId);
            }

            return null;
            */
        }

        public static T? GetUserId<T>(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var loggedInUserId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (typeof(T) == typeof(string))
            {
                return (T?) Convert.ChangeType(loggedInUserId, typeof(T?))!;
            }

            if (typeof(T) == typeof(int))
            {
                if (int.TryParse(loggedInUserId, out var id))
                {
                    return (T?) Convert.ChangeType(id, typeof(T?))!;
                }
                return (T?) Convert.ChangeType(null, typeof(T?))!;
            }
            if (typeof(T) == typeof(long))
            {
                if (long.TryParse(loggedInUserId, out var id))
                {
                    return (T?) Convert.ChangeType(id, typeof(T?))!;
                }
                return (T?) Convert.ChangeType(null, typeof(T?))!;
            }
            if (typeof(T) == typeof(Guid))
            {
                if (Guid.TryParse(loggedInUserId, out var id))
                {
                    return (T?) Convert.ChangeType(id, typeof(T?))!;
                }
                return (T?) Convert.ChangeType(null, typeof(T?))!;
            }
            
            throw new Exception("Invalid type provided");
        }


        public static string GenerateJwt(IEnumerable<Claim> claims, string key, string issuer, string audience,
            DateTime expirationDateTime)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer,
                audience, 
                claims, 
                expires: expirationDateTime, 
                signingCredentials: signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}