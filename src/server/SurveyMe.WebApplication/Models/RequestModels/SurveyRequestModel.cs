namespace SurveyMe.WebApplication.Models.RequestModels;

public sealed class SurveyRequestModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
        
    public ICollection<QuestionRequestModel> Questions { get; set; }
}