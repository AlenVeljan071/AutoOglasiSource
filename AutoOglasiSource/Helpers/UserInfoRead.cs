using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AutoOglasiSource.Helpers
{
    public static class UserInfoRead
    {
       
        public static IEnumerable<Claim> ReadToken(string token)
        {
            var jwt = "";
            jwt = token["Bearer ".Length..];
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(jwt);
            return jwtSecurityToken.Claims;
        }

        public static int ReadUserId(string token)
        {
            var claims = ReadToken(token);
            return Convert.ToInt32(claims.First(x => x.Type == "UserId").Value);
        }
    }
}
