using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace CandidateTesting.LucasVicenteLeite.ConsoleApplication
{
    class Program
    {
        private static string TypeLogFile = "MINHA CDN";
        private static string TemporaryFolder = "./temp/";

        static void Main(string[] args)
        {
            Console.WriteLine("Commands: ");
            string readCommands = Console.ReadLine();
            var commands = readCommands.Split(" ");

            if (commands.Length == 3 && commands[0] == "convert")
            {
                string urlFile = commands[1];
                string outPath = commands[2];
                string tempPath = FileDownload(urlFile);
                AgoraModel model = FileProcessing(tempPath);
                FileResultWrite(outPath, model);
            }
            else
            {
                Console.WriteLine("Error Commands");
            }
        }

        private static void FileResultWrite(string outPath, AgoraModel result)
        {
            using (StreamWriter writer = new StreamWriter(outPath))
            {
                writer.WriteLine($"#{nameof(AgoraModel.Version)}: {result.Version}");
                writer.WriteLine($"{nameof(AgoraModel.Date)}: {result.Date}");
                writer.WriteLine($"{nameof(AgoraModel.TituleFields)}: {result.TituleFields}");
                foreach (var item in result.Fields)
                    writer.WriteLine($"\"{item.Provider}\" {item.HttpMethod} {item.StatusCode} {item.UriPath} {item.TimeTaken} {item.ResponseSize} {item.CacheStatus}");
            }
        }

        private static AgoraModel FileProcessing(string tempPath)
        {
            var result = new AgoraModel()
            {
                Version = "1.0",
                Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                TituleFields = "provider http-method status-code uri-path time-taken response-size cache-status",
                Fields = new List<FieldModel>()
            };

            string[] lines = File.ReadAllLines(tempPath);
            foreach (var item in lines)
            {
                var line = item.Split("|");
                var elements = line[3].Split(" ");

                result.Fields.Add(new FieldModel
                {
                    CacheStatus = line[2],
                    HttpMethod = elements[0],
                    Provider = TypeLogFile,
                    ResponseSize = line[0],
                    StatusCode = line[1],
                    TimeTaken = Convert.ToInt32(Convert.ToDouble(line[4])),
                    UriPath = elements[1]
                });
            }
            File.Delete(tempPath);

            return result;
        }

        private static string FileDownload(string urlFile)
        {
            string tempPath = string.Concat(TemporaryFolder, urlFile.GetHashCode(), ".txt");

            WebClient webClient = new WebClient();
            webClient.DownloadFile(urlFile, tempPath);
            return tempPath;
        }
    }
}
