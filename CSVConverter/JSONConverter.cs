using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;

namespace CSVConverter
{
    public static class JSONConverter
    {
        [FunctionName("JSONConverter")]
        public static void Run([BlobTrigger("to-convert/{name}", Connection = "")]Stream myBlob, string name, TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            //Only convert CSV files
            if (name.Contains(".csv"))
            {
                var json = Convert(myBlob);
                log.Info(json);
            }
            else
            {
                log.Info("Not a CSV");
            }
        }

        public static string Convert(Stream blob)
        {
            string[] strCSV;

            using (StreamReader reader = new StreamReader(blob))
            {
                strCSV = reader.ReadToEnd().Split(Environment.NewLine.ToCharArray());
            }

            //Assume first row is object properties/csv header
            var properties = strCSV[0].Split(',');
            var jsonList = new List<dynamic>();
           
            foreach (var item in strCSV.Skip(1))
            {
                dynamic expando = new ExpandoObject();

                var values = item.Split(',');
                for (int i = 0; i < properties.Length; i++)
                {
                    ((IDictionary<string, object>)expando)[properties[i]] = values[i] ?? "";
                }
                jsonList.Add(expando);
            }

            return JsonConvert.SerializeObject(jsonList);
        }
    }
}