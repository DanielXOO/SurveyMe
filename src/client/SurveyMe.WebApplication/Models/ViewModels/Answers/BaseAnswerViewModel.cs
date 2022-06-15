using SurveyMe.DomainModels.Common;

namespace SurveyMe.WebApplication.Models.ViewModels.Answers;

public abstract class BaseAnswerViewModel
{
    public QuestionType QuestionType { get; set; }
    
    public Guid QuestionId { get; set; }
}