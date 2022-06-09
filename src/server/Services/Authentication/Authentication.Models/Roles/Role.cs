using Authentication.Users;

namespace Authentication.Roles;

public sealed class Role
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<User> Users { get; set; }
}