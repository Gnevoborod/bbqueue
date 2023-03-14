using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace bbqueue.Infrastructure
{
    internal sealed class AuthOptions
    {
        public const string ISSUER = "BBQUEUE"; 
        public const string AUDIENCE = "BBQUEUE"; 
        const string KEY = "GjNhfdtGhsuftnRepytxbr";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
