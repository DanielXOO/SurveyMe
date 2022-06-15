using System.Text.Json;
using System.Text.Json.Serialization;
using SurveyMe.DomainModels.Common;
using SurveyMe.WebApplication.Models.ViewModels.Answers;
using SurveyMe.WebApplication.Models.ViewModels.Files;

namespace SurveyMe.WebApplication.Converters;

public class AnswerViewJsonConverter : JsonConverter<BaseAnswerViewModel>
{
    public override BaseAnswerViewModel? Read(ref Utf8JsonReader reader, Type typeToConvert, 
        JsonSerializerOptions? options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        reader.Read();

        if (reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new JsonException();
        }

        var propertyName = reader.GetString();

        if (propertyName != "questionType")
        {
            throw new JsonException();
        }

        reader.Read();

        var questionType = JsonSerializer.Deserialize<QuestionType>(ref reader, options);

        BaseAnswerViewModel answer = questionType switch
        {
            QuestionType.Text => new TextAnswerViewModel(),
            QuestionType.Radio => new RadioAnswerViewModel(),
            QuestionType.Checkbox => new CheckboxAnswerViewModel(),
            QuestionType.File => new FileAnswerViewModel(),
            QuestionType.Rate => new RateAnswerViewModel(),
            QuestionType.Scale => new ScaleAnswerViewModel(),
            _ => throw new JsonException()
        };
        
        answer.QuestionType = questionType;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return answer;
            }

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                propertyName = reader.GetString();

                reader.Read();
                
                switch (propertyName)
                {
                    case "questionId":
                        var id = reader.GetString();

                        if (id == null)
                        {
                            throw new JsonException();
                        }
                        
                        answer.QuestionId = Guid.Parse(id);
                        break;
                    case "textAnswer":
                        ((TextAnswerViewModel) answer).TextAnswer = reader.GetString();
                        break;
                    case "scaleAnswer":
                        ((ScaleAnswerViewModel) answer).ScaleAnswer = reader.GetDouble();
                        break;
                    case "file":
                        ((FileAnswerViewModel) answer).File =
                            JsonSerializer.Deserialize<FileInfoViewModel>(ref reader, options);
                        break;
                    case "fileInfoId":
                        ((FileAnswerViewModel) answer).FileInfoId = Guid.Parse(reader.GetString());
                        break;
                    case "optionIds":
                        ((CheckboxAnswerViewModel) answer).OptionIds =
                            JsonSerializer.Deserialize<IEnumerable<Guid>>(ref reader, options);
                        break;
                    case "optionId":
                        var optionId = reader.GetString();

                        if (optionId == null)
                        {
                            throw new JsonException();
                        }
                        
                        ((RadioAnswerViewModel) answer).OptionId = Guid.Parse(optionId);
                        break;
                    case "rateAnswer":
                        ((RateAnswerViewModel) answer).RateAnswer = reader.GetDouble();
                        break;
                    default:
                        throw new JsonException();
                }
            }
            
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, BaseAnswerViewModel value, JsonSerializerOptions options)
    {
        
    }
}