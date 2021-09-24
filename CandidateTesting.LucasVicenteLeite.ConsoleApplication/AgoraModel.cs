using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CandidateTesting.LucasVicenteLeite.ConsoleApplication
{
    public class AgoraModel
    {
        public string Version { get; set; }
        public string Date { get; set; }
        public string TituleFields { get; set; }
        public List<FieldModel> Fields { get; set; }
    }


    public class FieldModel
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
