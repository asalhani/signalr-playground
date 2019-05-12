using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Dynamic;
using System.Collections.Generic;

namespace JsonManipuationSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonAsString = File.ReadAllText(@"M:\POC\json-manipulation\JsonManipuationSample\appsettings.json");

            var dyConfig = JObject.Parse(jsonAsString);
            var connString = dyConfig["ConnectionStrings.DefaultConnection"];
            var connString2 = dyConfig["ConnectionStrings"].SelectToken("DefaultConnection");
            var connString3 = dyConfig.SelectToken("ConnectionStrings.DefaultConnection");


            ExpandoObject obj = JsonConvert.DeserializeObject<ExpandoObject>(jsonAsString);
            var expandoDict = obj as IDictionary<string, object>;
            dynamic dy = obj;
            dy.ConnectionStrings.DefaultConnection = "test 123";

            var updatedConfigStr = JsonConvert.SerializeObject(dy);
            //var test = connString.ToString();
        }
    }
}
