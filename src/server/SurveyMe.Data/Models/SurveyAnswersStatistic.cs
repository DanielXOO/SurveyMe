namespace SurveyMe.Data.Models;

public sealed class SurveyAnswersStatistic
{
    public string Title { get; set; }

    public int AnswersCount { get; set; }

    public IEnumerable<QuestionAnswersStatistic> QuestionsAnswersStatistic { get; set; }
}