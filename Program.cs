using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ExpandoObjectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic expando = new ExpandoObject();

            // property
            expando.FullName = "Joe Satriani";

            // void ChangeName1 (string newName)
            expando.ChangeName1 = (Action<string>)((string newName) =>
            {
                expando.FullName = newName;
            });

            // int ChangeName2 (string newName)
            expando.ChangeName2 = (Func<string, int>)((string newName) =>
            {
                expando.FullName = newName;
                return newName.Length;
            });

            // int NameLength ()
            expando.NameLength = (Func<int>)(() =>
            {
                return expando.FullName.Length;
            });

            // System.Console.WriteLine(expando.FullName);
            // System.Console.WriteLine(String.Format("Length of name: {0}", expando.NameLength()));
            // System.Console.WriteLine(expando.ChangeName2("Steve Vai"));
            // System.Console.WriteLine(expando.FullName);


            dynamic cust = CreateDynamicCustomer("FullName", "Eric Johnson");
            string fname = cust.FullName;
            cust.Age = 25;
            // cust.ChangeName1 = (Action<string>)((string newName) =>
            // {
            //     cust.FullName = newName;
            // });
            System.Console.WriteLine(fname);

            foreach (KeyValuePair<string, object> kvp in ((IDictionary<string, object>)cust))
            {
                string propertyWithValue = kvp.Key + ": " + kvp.Value.ToString();
                System.Console.WriteLine(propertyWithValue);
            }

            foreach (KeyValuePair<string, object> kvp in ((IDictionary<string, object>)cust))
            {
                if (!kvp.Value.GetType().Name.Contains("Action"))
                {
                    string propertyWithValue = kvp.Key + ": " + kvp.Value.ToString();
                    System.Console.WriteLine(propertyWithValue);
                }
            }

            // Cast to IDictionary to check if properties exist
            System.Console.WriteLine(((IDictionary<string, object>)cust).ContainsKey("FullName"));
            object o;
            System.Console.WriteLine(((IDictionary<string, object>)cust).TryGetValue("FullName", out o));

            // Serialize to store
            // dotnet add package NewtonSoft.Json
            // If Action or Func present serialization will blow up, as it references itself within the lambda expression
            string strCust = JsonConvert.SerializeObject(cust, new ExpandoObjectConverter());

            System.Console.WriteLine(String.Format("Serialized: {0}", strCust));
            // Deserialize to restore
            cust = JsonConvert.DeserializeObject<ExpandoObject>(strCust, new ExpandoObjectConverter());
            System.Console.WriteLine(cust.FullName);

            System.Console.WriteLine("End!");
        }

        public static ExpandoObject CreateDynamicCustomer(string propertyName, string PropertyValue)
        {
            dynamic cust = new ExpandoObject();
            ((IDictionary<string, object>)cust)[propertyName] = PropertyValue;
            return cust;
        }
    }
}
