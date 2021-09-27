using CandidateTesting.LucasVicenteLeite.ConsoleApplication.Model;
using System;
using System.IO;

namespace CandidateTesting.LucasVicenteLeite.ConsoleApplication.Services
{
    public class AgoraTemplateService
    {
        private static readonly string Version = "1.0";
        private static readonly string TituleFields = "provider http-method status-code uri-path time-taken response-size cache-status";
        private readonly string Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        public bool GenerateAgoraTemplateFile(string outPath, AgoraModel model)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(outPath))
                {
                    writer.WriteLine($"#{nameof(Version)}: {Version}");
                    writer.WriteLine($"#{nameof(Date)}: {Date}");
                    writer.WriteLine($"#{nameof(TituleFields)}: {TituleFields}");
                    foreach (var item in model.Fields)
                        writer.WriteLine($"\"{item.Provider}\" {item.HttpMethod} {item.StatusCode} {item.UriPath} {item.TimeTaken} {item.ResponseSize} {item.CacheStatus}");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
