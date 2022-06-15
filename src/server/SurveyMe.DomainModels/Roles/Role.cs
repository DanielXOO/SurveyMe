using SurveyMe.DomainModels.Users;

namespace SurveyMe.DomainModels.Roles;

public sealed class Role
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<User> Users { get; set; }
}