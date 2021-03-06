using System.Text.Json;
using System.Text.Json.Serialization;
using SurveyMe.DomainModels.Request.Answers;

namespace SurveyMe.WebApplication.Converters;

public sealed class AnswerRequestJsonConverter : JsonConverter<BaseAnswerRequestModel>
{
    public override BaseAnswerRequestModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, BaseAnswerRequestModel value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        
        writer.WriteNumber("questionType", (int)value.QuestionType);
        writer.WriteString("questionId", value.QuestionId);

        if (value is TextAnswerRequestModel textAnswer)
        {
            writer.WriteString("textAnswer", textAnswer.TextAnswer);
        }
        else if (value is FileAnswerRequestModel fileAnswer)
        {
            writer.WritePropertyName("fileInfoId");
            writer.WriteStringValue(fileAnswer.FileInfoId);
        }
        else if (value is CheckboxAnswerRequestModel checkboxAnswer)
        {
            writer.WritePropertyName("optionIds");
            writer.WriteStartArray();
            foreach (var id in checkboxAnswer.OptionIds)
            {
                writer.WriteStringValue(id);
            }
            writer.WriteEndArray();
        }
        else if (value is RadioAnswerRequestModel radioAnswer)
        {
            writer.WriteString("optionId", radioAnswer.OptionId);
        }
        else if (value is RateAnswerRequestModel rateAnswer)
        {
            writer.WriteNumber("rateAnswer", rateAnswer.RateAnswer);
        }
        else if (value is ScaleAnswerRequestModel scaleAnswer)
        {
            writer.WriteNumber("scaleAnswer", scaleAnswer.ScaleAnswer);
        }
        
        writer.WriteEndObject();
    }
}