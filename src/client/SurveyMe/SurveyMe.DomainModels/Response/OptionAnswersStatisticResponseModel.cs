﻿namespace SurveyMe.DomainModels.Response;

public sealed class OptionAnswersStatisticResponseModel
{
    public string OptionText { get; set; }

    public int AnswersCount { get; set; }
}