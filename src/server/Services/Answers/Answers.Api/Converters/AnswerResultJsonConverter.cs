using System.Text.Json;
using System.Text.Json.Serialization;
using Answers.Api.Models.Response.Results;

namespace Answers.Api.Converters;

public class AnswerResultJsonConverter : JsonConverter<BaseAnswerResultResponseModel>
{
    public override BaseAnswerResultResponseModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, BaseAnswerResultResponseModel value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        
        writer.WriteNumber("questionType", (int)value.QuestionType);
        writer.WriteString("title", value.Title);
        switch (value)
        {
            case TextAnswerResultResponseModel textAnswer:
                writer.WriteString("textAnswer", textAnswer.TextAnswer);
                break;
            case FileAnswerResultResponseModel fileAnswer:
                //TODO: Add files support
                throw new NotImplementedException();
                break;
            case CheckboxAnswerResultResponseModel checkboxAnswer:
                writer.WritePropertyName("options");
                writer.WriteStartArray();
                foreach (var option in checkboxAnswer.Options)
                {
                    writer.WriteStringValue(option);
                }
                writer.WriteEndArray();
                break;
            case RadioAnswerResultResponseModel radioAnswer:
                writer.WriteString("radioAnswer", radioAnswer.RadioAnswer);
                break;
            case RateAnswerResultResponseModel rateAnswer:
                writer.WriteNumber("rate", rateAnswer.Rate);
                break;
            case ScaleAnswerResultResponseModel scaleAnswer:
                writer.WriteNumber("scale", scaleAnswer.Scale);
                break;
            default:
                throw new JsonException();
        }
        
        writer.WriteEndObject();
    }
}