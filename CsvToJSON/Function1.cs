using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CsvToJSON
{
    public static class CSVConverter
    {
        [FunctionName("CSVConverter")]
        public static void Run([BlobTrigger("to-convert/{name}", Connection = "")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            //Only convert CSV files
            if (name.Contains(".csv"))
            {
                var json = Convert(myBlob);

                createFile(json);

                log.LogInformation(json);
            }
            else
            {
                log.LogInformation("Not a CSV");
            }
        }

        public static string Convert(Stream blob)
        {
            var sReader = new StreamReader(blob);

            var csv = new CsvReader(sReader, CultureInfo.InvariantCulture);

            csv.Read();
            csv.ReadHeader();

            var csvRecords = csv.GetRecords<object>().ToList();

            return JsonConvert.SerializeObject(csvRecords);
        }

        private static void createFile(string json)
        {
            throw new NotImplementedException();
        }

    }
}
