using Azure.Storage.Blobs;
using CsvHelper;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

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

                CreateFile(json, name.Replace(".csv", ""));

                //log.LogInformation(json);
            }
            else
            {
                log.LogInformation("Not a CSV");
            }
        }

        public static string Convert(Stream blob)
        {
            var csv = new CsvReader(new StreamReader(blob), CultureInfo.InvariantCulture);
            csv.Configuration.BadDataFound = null;
            csv.Read();
            csv.ReadHeader();

            var csvRecords = csv.GetRecords<object>().ToList();

            return JsonConvert.SerializeObject(csvRecords);
        }

        public static void CreateFile(string json, string fileName)
        {
            var c = new BlobContainerClient(System.Environment.GetEnvironmentVariable("ConnectionStrings:StorageConnString"), "to-convert");
            byte[] writeArr = Encoding.UTF8.GetBytes(json);

            using (MemoryStream stream = new MemoryStream(writeArr))
            {
                c.UploadBlob($"{fileName}.json", stream);
            }
        }

    }
}
