using SurveyMe.DomainModels.Users;

namespace SurveyMe.DomainModels.Answers;

public sealed class SurveyAnswer
{
    public Guid Id { get; set; }

    public User User { get; set; }

    public Guid UserId { get; set; }
    
    public Guid SurveyId { get; set; }
    
    public ICollection<BaseAnswer> QuestionsAnswers { get; set; }
}