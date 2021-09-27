namespace CandidateTesting.LucasVicenteLeite.ConsoleApplication.Model
{
    public class AgoraFieldModel
    {
        public string Provider { get; set; }
        public string HttpMethod { get; set; }
        public string StatusCode { get; set; }
        public string UriPath { get; set; }
        public int TimeTaken { get; set; }
        public string ResponseSize { get; set; }
        public string CacheStatus { get; set; }
    }
}
