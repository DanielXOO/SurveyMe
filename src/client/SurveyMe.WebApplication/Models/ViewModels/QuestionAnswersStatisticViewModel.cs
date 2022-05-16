﻿using SurveyMe.DomainModels.Common;

namespace SurveyMe.WebApplication.Models.ViewModels;

public sealed class QuestionAnswersStatisticViewModel
{
    public string QuestionTitle { get; set; }

    public QuestionType QuestionType { get; set; }
    
    public int AnswersCount { get; set; }

    public double AverageRate { get; set; }

    public double AverageScale { get; set; }

    public ICollection<OptionAnswersStatisticViewModel> OptionAnswersStatistic { get; set; }
}