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
            // dynamic exp = new ExpandoObject();
            // exp.FullName = "Joe Satriani";

            dynamic exp = ExpandoObjectHelper.CreateDynamicObject("FullName", "Joseph Satriani");
            exp.DOB = new DateTime(1956, 7, 15);
            System.Console.WriteLine($"Name: {exp.FullName}; Date of birth: {exp.DOB.ToShortDateString()}");

            // #############################################################################################
            // public void ChangeName(string newName)
            // exp.ChangeName = (Action<string>)((string newName) =>
            // {
            //     exp.FullName = newName;
            // });
            // exp.ChangeName("Joe Satriani");
            // System.Console.WriteLine($"Name: {exp.FullName}; Date of birth: {exp.DOB.ToShortDateString()}");
            // #############################################################################################
            // public int Age()
            // exp.Age = (Func<int>)(() =>
            // {
            //     var today = DateTime.Today;
            //     var age = today.Year - exp.DOB.Year;
            //     // Go back to the year the person was born in case of a leap year
            //     if (exp.DOB.Date > today.AddYears(-age)) age--;
            //     return age;
            // });
            // System.Console.WriteLine($"Age: {exp.Age()}");
            // #############################################################################################
            // int ChangeNameGetLength(string newName)
            // exp.ChangeNameGetLength = (Func<string, int>)((string newName) =>
            // {
            //     exp.FullName = newName;
            //     return newName.Length;
            // });
            // System.Console.WriteLine(exp.ChangeNameGetLength("Joe 'Satch' Satriani"));
            // #############################################################################################


            System.Console.WriteLine("Serialized Contents:");
            var str = ExpandoObjectHelper.Serialize(exp);
            System.Console.WriteLine(str);

            System.Console.WriteLine("Deserialized Contents:");
            dynamic exp2 = ExpandoObjectHelper.Deserialize(str);
            ExpandoObjectHelper.GetProperties(exp2);
        }
    }
}