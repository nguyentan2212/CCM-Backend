using DappAPI.ViewModels;
using System.Collections.Generic;

namespace DappAPI.Services.Auth
{
    public interface IAuthService
    {
        // Generate JWT from public address and roles
        public string GenerateToken(string publicAddress, List<string> roles);
        // Recover public address from signature and message
        public string RecoverPersonalSignature(string message, string signature);
        // Verify message by public address
        public string VerifyMessage(string message, string signature, string publicAddress);
    }
}
