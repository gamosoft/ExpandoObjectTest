using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public static class ExpandoObjectHelper
{
    public static ExpandoObject CreateDynamicObject(string propertyName, object propertyValue)
    {
        dynamic exp = new ExpandoObject();
        ((IDictionary<string, object>)exp)[propertyName] = propertyValue;
        return exp;
    }

    public static void GetProperties(ExpandoObject exp)
    {
        foreach (KeyValuePair<string, object> kvp in ((IDictionary<string, object>)exp))
        {
            string propertyWithValue = kvp.Key + ": " + kvp.Value.ToString();
            System.Console.WriteLine(propertyWithValue);
        }

        // foreach (KeyValuePair<string, object> kvp in ((IDictionary<string, object>)exp))
        // {
        //     if (!kvp.Value.GetType().Name.Contains("Action"))
        //     {
        //         string propertyWithValue = kvp.Key + ": " + kvp.Value.ToString();
        //         System.Console.WriteLine(propertyWithValue);
        //     }
        // }
    }

    public static bool PropertyExists(ExpandoObject exp, string propertyName)
    {
        // Cast to IDictionary to check if properties exist
        return ((IDictionary<string, object>)exp).ContainsKey(propertyName);
        // object o;
        // System.Console.WriteLine(((IDictionary<string, object>) exp).TryGetValue("FullName", out o));
    }

    public static string Serialize(ExpandoObject exp)
    {
        // Serialize to store
        // dotnet add package NewtonSoft.Json
        // If Action or Func present serialization will blow up, as it references itself within the lambda expression
        return JsonConvert.SerializeObject(exp, new ExpandoObjectConverter());
    }

    public static ExpandoObject Deserialize(string str)
    {
        // Deserialize to restore
        return JsonConvert.DeserializeObject<ExpandoObject>(str, new ExpandoObjectConverter());
    }
}