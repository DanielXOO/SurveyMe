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

        var dictionary = new Dictionary<string, string>();

        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
        {
            AddProperty(property, source, dictionary);
        }
        
        var query = string.Join("&", dictionary.Select(p => $"{p.Key}={p.Value}"));
        
        return query;
    }


    private static void AddProperty(PropertyDescriptor property, object source, IDictionary<string, string> dictionary)
    {
        var value = property.GetValue(source)?.ToString();
        var key = property.Name;

        dictionary.Add(key, value);
    }
}