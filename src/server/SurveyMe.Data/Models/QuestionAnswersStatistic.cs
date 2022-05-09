namespace SurveyMe.Data.Models;

public sealed class QuestionAnswersStatistic
{
    public string Title { get; set; }

    public int AnswersCount { get; set; }

    public ICollection<OptionsAnswersStatistic> OptionsAnswersStatistic { get; set; }
}