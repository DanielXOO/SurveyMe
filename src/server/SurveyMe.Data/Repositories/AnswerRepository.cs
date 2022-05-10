using Microsoft.EntityFrameworkCore;
using SurveyMe.Data.Core;
using SurveyMe.Data.Models;
using SurveyMe.Data.Repositories.Abstracts;
using SurveyMe.DomainModels;

namespace SurveyMe.Data.Repositories;

public sealed class AnswerRepository : Repository<SurveyAnswer>, IAnswerRepository
{
    public AnswerRepository(DbContext context) : base(context)
    {
    }


    public async Task<SurveyAnswer> GetByIdAsync(Guid id)
    {
        var answers = await GetAnswersQuery()
            .FirstOrDefaultAsync(answer => answer.Id == id);

        return answers;
    }

    public async Task<SurveyAnswersStatistic> GetSurveyStatistic(Survey survey)
    {
        var surveyStatistic = new SurveyAnswersStatistic
        {
            AnswersCount = await Data.CountAsync(answer => answer.SurveyId == survey.Id),
            SurveyTitle = survey.Name,
            QuestionAnswersStatistic = new List<QuestionAnswersStatistic>()
        };

        foreach (var surveyQuestion in survey.Questions)
        {
            var questionAnswers = Data.Include(answer => answer.QuestionAnswers)
                .SelectMany(answer => answer.QuestionAnswers
                    .Where(questionAnswer => questionAnswer.QuestionId == surveyQuestion.Id));

            var questionAnswersStatistic = new QuestionAnswersStatistic
            {
                QuestionTitle = surveyQuestion.Title,
                AnswersCount = await questionAnswers.CountAsync(),
                QuestionType = surveyQuestion.Type
            };

            switch (surveyQuestion.Type)
            {
                case QuestionType.Radio or QuestionType.Checkbox:
                    questionAnswersStatistic.OptionAnswersStatistic = surveyQuestion.Options
                        .Select(option => new OptionAnswersStatistic
                        {
                            OptionText = option.Text,
                            AnswersCount = questionAnswers.SelectMany(questions => questions.Options)
                                .Count(optionAnswer => optionAnswer.QuestionOptionId == option.Id)
                        }).ToList();
                    break;
                case QuestionType.Rate:
                    questionAnswersStatistic.AverageRate = questionAnswers
                        .Average(questionAnswer => questionAnswer.RateAnswer);
                    break;
                case QuestionType.Scale:
                    questionAnswersStatistic.AverageScale = questionAnswers
                        .Average(questionAnswer => questionAnswer.RateAnswer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(surveyQuestion.Type), "No such type");
            }
            surveyStatistic.QuestionAnswersStatistic.Add(questionAnswersStatistic);
        }

        return surveyStatistic;
    }


    private IQueryable<SurveyAnswer> GetAnswersQuery()
    {
        return Data
            .Include(answer => answer.QuestionAnswers)
            .ThenInclude(answer => answer.Options);
    }
}