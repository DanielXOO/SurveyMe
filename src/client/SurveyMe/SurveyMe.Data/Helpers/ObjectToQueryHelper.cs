using System.ComponentModel;

namespace SurveyMe.Data.Helpers;

public static class ObjectToQueryHelper
{
    public static string ToQuery(this object source)
    {
        if (source == null)
        {
            throw new NullReferenceException();
        }

        var queries = new List<string>();

        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
        {
            var value = property.GetValue(source)?.ToString();
            var key = property.Name;
            
            queries.Add($"{key}={value}");
        }

        var query = string.Join("&", queries);
        
        return query;
    }
}