using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeeService.Infrastructure
{
    internal sealed class AuthOptions
    {
        public const string ISSUER = "BBQUEUE"; 
        public const string AUDIENCE = "BBQUEUE"; 
        //const string KEY = "GjNhfdtGhsuftnRepytxbr";   // ключ для шифрации
        public static string KEY { get; private set; } = "init";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));

        public static void RenewSymmetricSecurityKey(string key)
        {
            KEY = key;
        }

    }
}
