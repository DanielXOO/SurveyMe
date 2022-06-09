using System.Text.Json;
using System.Text.Json.Serialization;
using Answers.Api.Models.Request.Answers;
using Answers.Models.Questions;

namespace Answers.Api.Converters;

public class AnswerJsonConverter : JsonConverter<BaseAnswerRequestModel>
{
    public override BaseAnswerRequestModel? Read(ref Utf8JsonReader reader, Type typeToConvert, 
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

        BaseAnswerRequestModel answer = questionType switch
        {
            QuestionType.Text => new TextAnswerRequestModel(),
            QuestionType.Radio => new RadioAnswerRequestModel(),
            QuestionType.Checkbox => new CheckboxAnswerRequestModel(),
            QuestionType.File => new FileAnswerRequestModel(),
            QuestionType.Rate => new RateAnswerRequestModel(),
            QuestionType.Scale => new ScaleAnswerRequestModel(),
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
                        ((TextAnswerRequestModel) answer).TextAnswer = reader.GetString();
                        break;
                    case "scaleAnswer":
                        ((ScaleAnswerRequestModel) answer).ScaleAnswer = reader.GetDouble();
                        break;
                    case "fileInfoId":
                        ((FileAnswerRequestModel) answer).FileInfoId = Guid.Parse(reader.GetString());
                        break;
                    case "optionIds":
                        ((CheckboxAnswerRequestModel) answer).OptionIds =
                            JsonSerializer.Deserialize<IEnumerable<Guid>>(ref reader, options);
                        break;
                    case "optionId":
                        var optionId = reader.GetString();

                        if (optionId == null)
                        {
                            throw new JsonException();
                        }
                        
                        ((RadioAnswerRequestModel) answer).OptionId = Guid.Parse(optionId);
                        break;
                    case "rateAnswer":
                        ((RateAnswerRequestModel) answer).RateAnswer = reader.GetDouble();
                        break;
                    default:
                        throw new JsonException();
                }
            }
            
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, BaseAnswerRequestModel value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}