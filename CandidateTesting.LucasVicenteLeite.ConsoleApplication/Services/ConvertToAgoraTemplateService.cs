using CandidateTesting.LucasVicenteLeite.ConsoleApplication.Model;
using CandidateTesting.LucasVicenteLeite.ConsoleApplication.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace CandidateTesting.LucasVicenteLeite.ConsoleApplication.Services
{
    public class ConvertToAgoraTemplateService
    {
        private static string TypeLogFile = "MINHA CDN";

        private readonly DownloadFile Util;
        private readonly AgoraTemplateService Service;

        public ConvertToAgoraTemplateService()
        {
            Util = new DownloadFile();
            Service = new AgoraTemplateService();
        }

        public bool ConvertMinhaCdnToAgoraModel(string urlFile, string outPathFile)
        {
            try
            {
                string tempFile = Util.FileDownloadAsync(urlFile);
                AgoraModel model = ConvertFileMinhaCdnToAgoraModel(tempFile);
                if (tempFile == null || model.Fields?.Count <= 0)
                    return false;

                return Service.GenerateAgoraTemplateFile(outPathFile, model);
            }
            catch
            {
                return false;
            }
        }

        private AgoraModel ConvertFileMinhaCdnToAgoraModel(string tempFile)
        {
            var result = new AgoraModel { Fields = new List<AgoraFieldModel>() };
            try
            {
                string[] lines = File.ReadAllLines(tempFile);
                foreach (var item in lines)
                {
                    var line = item.Split("|");
                    var elements = line[3].Split(" ");

                    result.Fields.Add(new AgoraFieldModel
                    {
                        CacheStatus = line[2],
                        HttpMethod = elements[0].Remove(0, 1),
                        Provider = TypeLogFile,
                        ResponseSize = line[0],
                        StatusCode = line[1],
                        TimeTaken = Convert.ToInt32(Convert.ToDouble(line[4])),
                        UriPath = elements[1]
                    });
                }
                File.Delete(tempFile);

                return result;
            }
            catch
            {
                return new AgoraModel();
            }
        }
    }
}
