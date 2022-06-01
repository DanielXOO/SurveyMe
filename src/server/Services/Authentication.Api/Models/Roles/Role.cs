using Authentication.Api.Models.Users;

namespace Authentication.Api.Models.Roles;

public sealed class Role
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<User> Users { get; set; }
}