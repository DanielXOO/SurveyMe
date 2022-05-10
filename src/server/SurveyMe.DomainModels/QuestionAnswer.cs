using System;
using System.Collections.Generic;

namespace SurveyMe.DomainModels;

public sealed class QuestionAnswer
{
    public Guid Id { get; set; }

    public Guid QuestionId { get; set; }

    public Guid SurveyAnswerId { get; set; }

    public SurveyAnswer SurveyAnswer { get; set; }

    public string TextAnswer { get; set; }

    public double RateAnswer { get; set; }

    public double ScaleAnswer { get; set; }

    public ICollection<QuestionAnswerOption> Options { get; set; }

    public Guid FileAnswerId { get; set; }

    public FileAnswer FileAnswer { get; set; }
}