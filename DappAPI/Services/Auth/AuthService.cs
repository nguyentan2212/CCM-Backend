using Microsoft.IdentityModel.Tokens;
using Nethereum.Signer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DappAPI.Services.Auth
{
    public class AuthService : IAuthService
    {
        string jwtSecret;
        int jwtLifespan;
        public AuthService(string jwtSecret, int jwtLifespan)
        {
            this.jwtSecret = jwtSecret;
            this.jwtLifespan = jwtLifespan;
        }

        // Generate JWT from public address and roles
        public string GenerateToken(string publicAddress, List<string> roles)
        {
            var expirationTime = DateTime.UtcNow.AddSeconds(jwtLifespan);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, publicAddress));
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationTime,              
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            
            return token;
        }

        // Recover public address from signature and message
        public string RecoverPersonalSignature(string message, string signature)
        {         
            EthereumMessageSigner signer = new EthereumMessageSigner();
           
            string publicAddress = signer.EncodeUTF8AndEcRecover(message, signature);
            return publicAddress;
        }

        public string VerifyMessage(string message, string signature, string publicAddress)
        {
            string address = RecoverPersonalSignature(message, signature);
            if (address != publicAddress)
            {
                return null;
            }
            return address;
        }
    }
}
