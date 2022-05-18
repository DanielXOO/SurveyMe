using SurveyMe.DomainModels.Questions;
using SurveyMe.DomainModels.Users;

namespace SurveyMe.DomainModels.Surveys;

public sealed class Survey
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime? LastChangeDate { get; set; }

    public User Author { get; set; }

    public Guid AuthorId { get; set; }

    public ICollection<Question> Questions { get; set; }

    public ICollection<SurveyAnswer> Answers { get; set; }
}