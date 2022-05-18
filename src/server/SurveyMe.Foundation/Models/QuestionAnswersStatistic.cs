using System.Collections.Generic;
using SurveyMe.DomainModels.Questions;

namespace SurveyMe.Foundation.Models;

public sealed class QuestionAnswersStatistic
{
    public string QuestionTitle { get; set; }

    public QuestionType QuestionType { get; set; }
    
    public int AnswersCount { get; set; }
    
    public double AverageRate { get; set; }

    public double AverageScale { get; set; }

    public ICollection<OptionAnswersStatistic> OptionAnswersStatistic { get; set; }
}