using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MyFirstWebApp.Models
{

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AuthOptions
    {
        public const string ISSUER = "MyFirstWebAppServer"; 
        public const string AUDIENCE = "MyFirstWebAppClient"; 
        const string KEY = "mysupersecret_secretkey23011989";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
