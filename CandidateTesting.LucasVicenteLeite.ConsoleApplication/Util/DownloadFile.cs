using System;
using System.IO;
using System.Net;

namespace CandidateTesting.LucasVicenteLeite.ConsoleApplication.Util
{
    public class DownloadFile
    {
        private static readonly string TemporaryFolder = "./temp/";

        public string FileDownloadAsync(string urlFile)
        {
            try
            {
                string tempPath = string.Concat(TemporaryFolder, urlFile.GetHashCode(), ".txt");
                if (!Directory.Exists(TemporaryFolder))
                    Directory.CreateDirectory(TemporaryFolder);

                Uri uriFile = new Uri(urlFile);
                WebClient webClient = new WebClient();
                webClient.DownloadFile(uriFile, tempPath);

                return tempPath;
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteFile(string urlFile)
        {
            try
            {
                File.Delete(urlFile);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
