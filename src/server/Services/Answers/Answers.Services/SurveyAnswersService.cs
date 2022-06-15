using Answers.Data.Abstracts;
using Answers.Models.Answers;
using Answers.Services.Abstracts;
using Answers.Services.Models.Questions;
using Answers.Services.Models.Surveys;
using SurveyMe.Common.Pagination;

namespace Answers.Services;

public class AnswersService : IAnswersService
{
    private readonly IAnswersUnitOfWork _unitOfWork;

    private readonly ISurveysService _surveysService;

    public AnswersService(IAnswersUnitOfWork unitOfWork, ISurveysService surveysService)
    {
        _unitOfWork = unitOfWork;
        _surveysService = surveysService;
    }


    public async Task<SurveyAnswer> GetAnswerByIdAsync(Guid id)
    {
        var answer = await _unitOfWork.Answers.GetByIdAsync(id);

        return answer;
    }

    public async Task AddAnswerAsync(SurveyAnswer answer, Guid authorId)
    {
        answer.UserId = authorId;
        
        await _unitOfWork.Answers.CreateAsync(answer);
    }

    public async Task<PagedResult<SurveyAnswerResult>> GetSurveyAnswersAsync(int currentPage,
        int pageSize, Guid surveyId)
    {   
        var answers = await _unitOfWork.Answers
            .GetSurveyAnswersAsync(currentPage, pageSize, surveyId);

        var survey = await _surveysService.GetSurveyByIdAsync(surveyId);

        var result = new List<SurveyAnswerResult>();
        
        foreach (var answer in answers.Items)
        {
            var surveyAnswer = new SurveyAnswerResult
            {
                Name = survey.Name,
                UserId = answer.UserId,
                QuestionAnswers = answer.QuestionsAnswers.Join(survey.Questions,
                    questionAnswer => questionAnswer.QuestionId,
                    question => question.Id,
                    (questionAnswer, question) =>
                    {
                        BaseAnswerResult answerResult = questionAnswer switch
                        {
                            TextQuestionAnswer textAnswer => new TextAnswerResult
                            {
                                Title = question.Title,
                                TextAnswer = textAnswer.Text,
                                QuestionType = questionAnswer.QuestionType
                            },
                            CheckboxQuestionAnswer checkboxAnswer => new CheckboxAnswerResult
                            {
                                Title = question.Title,
                                Options = checkboxAnswer.Options.Join(question.Options,
                                    optionAnswer => optionAnswer.OptionId,
                                    questionOption => questionOption.Id,
                                    (_, questionOption) => questionOption.Text)
                                    .ToArray(),
                                QuestionType = questionAnswer.QuestionType
                            },
                            RadioQuestionAnswer radioAnswer => new RadioAnswerResult
                            {
                                Title = question.Title,
                                RadioAnswer = question.Options
                                    .First(option => option.Id == radioAnswer.OptionId)
                                    .Text,
                                QuestionType = questionAnswer.QuestionType
                            },
                            RateQuestionAnswer rateAnswer => new RateAnswerResult
                            {
                                Title = question.Title,
                                Rate = rateAnswer.Rate,
                                QuestionType = questionAnswer.QuestionType
                            },
                            ScaleQuestionAnswer scaleAnswer => new ScaleAnswerResult
                            {
                                Title = question.Title,
                                Scale = scaleAnswer.Scale,
                                QuestionType = questionAnswer.QuestionType
                            },
                            FileQuestionAnswer fileAnswer => new FileAnswerResult
                            {
                                //TODO: Add fields to file answer
                                QuestionType = questionAnswer.QuestionType
                            },
                            _ => throw new ArgumentOutOfRangeException(nameof(questionAnswer),
                                questionAnswer, "No such answer type")
                        };

                        return answerResult;
                    }
                ).ToList()
            };
            
            result.Add(surveyAnswer);
        }

        var resultPage = new PagedResult<SurveyAnswerResult>(result, pageSize, currentPage, result.Count);
        
        return resultPage;
    }
}