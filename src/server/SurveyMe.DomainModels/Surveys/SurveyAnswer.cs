using SurveyMe.DomainModels.Answers;
using SurveyMe.DomainModels.Users;

namespace SurveyMe.DomainModels.Surveys;

public sealed class SurveyAnswer
{
    public Guid Id { get; set; }

    public User User { get; set; }

    public Guid UserId { get; set; }

    public Guid SurveyId { get; set; }

    public Survey Survey { get; set; }

    public ICollection<QuestionAnswer> QuestionAnswers { get; set; }
}