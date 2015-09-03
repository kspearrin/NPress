using Microsoft.AspNet.Identity;
using NPress.Core.Domains;

namespace CP.POS.Mvc.Identity
{
    public class PlaintextPasswordHasher : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            if(hashedPassword == providedPassword)
            {
                return PasswordVerificationResult.Success;
            }

            return PasswordVerificationResult.Failed;
        }
    }
}
