using System.Text.Json;
using System.Text.Json.Serialization;
using SurveyMe.WebApplication.Models.RequestModels;

namespace SurveyMe.WebApplication.Converters;

public class SurveyJsonConverter : JsonConverter<SurveyRequestModel>
{
    public override SurveyRequestModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, SurveyRequestModel value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}