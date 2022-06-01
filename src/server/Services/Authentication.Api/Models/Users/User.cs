using Authentication.Api.Models.Roles;

namespace Authentication.Api.Models.Users;

public sealed class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string DisplayName { get; set; }

    public string PasswordHash { get; set; }

    public ICollection<Role> Roles { get; set; }
}