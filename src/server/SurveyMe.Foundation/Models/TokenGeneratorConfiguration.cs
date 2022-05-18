using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SurveyMe.Foundation.Models;

public sealed class TokenGeneratorConfiguration
{
    public string Issuer { get; set; }

    public string Audience { get; set; }

    public string Key { get; set; }

    public  SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}