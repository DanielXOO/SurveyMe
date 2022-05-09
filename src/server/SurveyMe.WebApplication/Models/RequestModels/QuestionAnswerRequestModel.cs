namespace SurveyMe.WebApplication.Models.RequestModels;

public sealed class QuestionAnswerRequestModel
{
    public Guid Id { get; set; }

    public Guid QuestionId { get; set; }
    
    public string TextAnswer { get; set; }

    public double RateAnswer { get; set; }

    public double ScaleAnswer { get; set; }
    
    public Guid FileId { get; set; }
        
    public ICollection<Guid>? OptionIds { get; set; }
}