using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SurveyMe.DomainModels.Users;

namespace SurveyMe.Foundation.Validators;

public sealed class PasswordValidator : IPasswordValidator<User>
{
    public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
    {
        var regexes = new Dictionary<Regex, string>
        {
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