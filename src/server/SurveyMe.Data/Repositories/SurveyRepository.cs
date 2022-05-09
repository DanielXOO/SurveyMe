using SurveyMe.Common.Pagination;
using SurveyMe.Data.Core;
using Microsoft.EntityFrameworkCore;
using SurveyMe.Data.Models;
using SurveyMe.Data.Repositories.Abstracts;
using SurveyMe.DomainModels;

namespace SurveyMe.Data.Repositories
{
    public sealed class SurveyRepository : Repository<Survey>, ISurveyRepository
    {
        public SurveyRepository(DbContext context) : base(context)
        {
        }


        public async Task<PagedResult<Survey>> GetSurveysAsync(int pageSize, int currentPage, string searchRequest,
            SortOrder sortOrder)
        {
            var surveys = GetSurveysQuery();

            if (!string.IsNullOrEmpty(searchRequest))
            {
                surveys = surveys.Where(survey => survey.Name.Contains(searchRequest));
            }

            switch (sortOrder)
            {
                case SortOrder.Descending:
                    surveys = surveys.OrderByDescending(survey => survey.Name);
                    break;
                case SortOrder.Ascending:
                    surveys = surveys.OrderBy(survey => survey.Name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, "Unknown sort order value");
            }

            var result = await surveys.ToPagedResultAsync(pageSize, currentPage);

            return result;
        }

        public async Task<Survey> GetByIdAsync(Guid id)
        {
            var survey = await GetSurveysQuery()
                .FirstOrDefaultAsync(survey => survey.Id == id);

            return survey;
        }

        public async Task<SurveyAnswersStatistic> GetSurveyStatisticById(Guid id)
        {
            var surveys = Data.Include(survey => survey.Answers)
                .ThenInclude(answer => answer.QuestionAnswers)
                .ThenInclude(answer => answer.Options)
                .Include(survey => survey.Questions)
                .ThenInclude(question => question.Options);

            var surveyDb = await surveys.FirstOrDefaultAsync(survey => survey.Id == id);

            //TODO: check surveyDb null
            
            var statistic = new SurveyAnswersStatistic
            {
                Title = surveyDb.Name,
                AnswersCount = surveyDb.Answers.Count,
                QuestionsAnswersStatistic = surveyDb.Questions.Select(questionDb => new QuestionAnswersStatistic
                {
                    Title = questionDb.Title,
                    AnswersCount = surveyDb.Answers.Select(surveyAnswer => surveyAnswer.QuestionAnswers
                        .Where(questionAnswer => questionAnswer.QuestionId == questionDb.Id))
                        .Count(),
                    OptionsAnswersStatistic = questionDb.Options.Select(optionDb => new OptionsAnswersStatistic
                    {
                        Text = optionDb.Text,
                        AnswersCount = surveyDb.Answers.Select(surveyAnswer => surveyAnswer.QuestionAnswers
                            .Where(questionAnswer => questionAnswer.QuestionId == questionDb.Id)
                            .Select(questionAnswer => questionAnswer.Options
                                .Where(optionAnswer => optionAnswer.Id == optionDb.Id)))
                            .Count()
                    }).ToList()
                }).ToList()
            };

            throw new Exception();
        }


        private IQueryable<Survey> GetSurveysQuery()
        {
            return Data.Include(survey => survey.Questions)
                .ThenInclude(question => question.Options);
        }
    }
}