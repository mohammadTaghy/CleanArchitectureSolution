using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.JWT
{
    public interface IJWTTokenHelper
    {
        string CreateToken(IEnumerable<Claim> cliams);
        IEnumerable<Claim> DecodeJson(string token);
    }
    public class JWTTokenHelper : IJWTTokenHelper
    {

        public JWTTokenHelper(IOptions<JwtSettings> options) { 
            _key=options.Value.Key;
            _issuer = options.Value.Issuer;
            _audience = options.Value.Audience;
        }
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        private JwtSecurityTokenHandler _jwtSecurityTokenHandler = null;
        private JwtSecurityTokenHandler _JwtSecurityTokenHandler
        {
            get
            {
                if (_jwtSecurityTokenHandler == null)
                    _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                return _jwtSecurityTokenHandler;
            }
        }
        
        public string CreateToken(IEnumerable<Claim> cliams)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            var JWToken = new JwtSecurityToken(
                   issuer: _issuer,
                   audience: _audience,
                   claims: cliams,
                   notBefore: DateTimeHelper.CurrentMDateTime,
                   expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                   signingCredentials: credentials
               );
            return _JwtSecurityTokenHandler.WriteToken(JWToken);
        }
        public IEnumerable<Claim> DecodeJson(string token)
        {
            //todo
            //validate
            return _JwtSecurityTokenHandler.ReadJwtToken(token).Claims;
        }
    }
}
