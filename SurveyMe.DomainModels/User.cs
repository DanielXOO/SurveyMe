namespace SurveyMe.DomainModels;

public class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; }
    
    public string PasswordHash { get; set; }
    
    public DateTime CreationTime { get; set; }

    public ICollection<Role> Roles { get; set; }
}