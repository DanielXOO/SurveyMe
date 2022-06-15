using System.Text.RegularExpressions;
using Authentication.Users;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Api.Validators;

public sealed class PasswordValidator : IPasswordValidator<User>
{
    public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
    {
        var regexes = new Dictionary<Regex, string>
        {
            { new Regex(@"^[a-zA-Z0-9!@#$%^&*()_+|]+$"), "Such symbols do not allowed" },
            { new Regex(@"[\p{Lu}]"), "Add uppercase letters" },
            { new Regex(@"[\p{Ll}]"), "Add lowercase letters"},
            { new Regex(@"\d"), "Add digits" }
        };
        
        foreach (var regex in regexes)
        {
            if (!regex.Key.IsMatch(password))
            {
                var error = new IdentityError()
                {
                    Description = regex.Value
                };

                var result = IdentityResult.Failed(error);

                return Task.FromResult(result);
            }
        }

        return Task.FromResult(IdentityResult.Success);
    }
}