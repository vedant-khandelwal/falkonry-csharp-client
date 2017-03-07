using System;
using System.Collections.Generic;
using falkonry_csharp_client.helper.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace falkonry_csharp_client.service
{
    internal class FalkonryService
    {
        public string Host { get; }
        public string Token { get; }
        private readonly HttpService _http;
        
         public FalkonryService(string host, string token)
        {
            Host = host;
            Token = token;
            _http = new HttpService(host, token);
            
        }

        // Create Datastream
        public Datastream CreateDatastream(DatastreamRequest datastream)
        {
            var data = JsonConvert.SerializeObject(datastream, Formatting.Indented,
                new JsonSerializerSettings() {ContractResolver = new CamelCasePropertyNamesContractResolver()});

            var datastreamJson = _http.Post("/datastream", data);

            return JsonConvert.DeserializeObject<Datastream>(datastreamJson);
        }

        // List Datastream
        public List<Datastream> GetDatastream()
        {
            var datastreamJson = _http.Get("/datastream");
            return JsonConvert.DeserializeObject<List<Datastream>>(datastreamJson);
        }

        // Get Datastream by id
        public Datastream GetDatastream(string id)
        {
            var url = "/datastream/" + id;

            var datastreamJson = _http.Get(url);
            return JsonConvert.DeserializeObject<Datastream>(datastreamJson);
        }
        
        // Add data to DataStream
        public InputStatus AddInputData(string datastream, string data, SortedDictionary<string, string> options)
        {
            string streamingValue;
            string hasMoreDataValue;
            var url = "/datastream/" + datastream;
            if (options.TryGetValue("streaming", out streamingValue))
            {
                url += "?streaming=" +Uri.EscapeDataString(streamingValue);
            }
            if (options.TryGetValue("hasMoreData", out hasMoreDataValue))
            {
                url += "&hasMoreData=" + Uri.EscapeDataString(hasMoreDataValue);
            }

            var status = _http.PostData(url, data);
            return JsonConvert.DeserializeObject<InputStatus>(status);
        }

        public InputStatus AddInputFromStream(string datastream, byte[] data, SortedDictionary<string, string> options)
        {
            var url = "/datastream/" + datastream;
            string streamingValue;
            string hasMoreDataValue;
            if (options.TryGetValue("streaming", out streamingValue))
            {
                
                url += "?streaming=" + Uri.EscapeDataString(streamingValue);
            }
            if (options.TryGetValue("hasMoreData", out hasMoreDataValue))
            {
                url += "&hasMoreData=" + Uri.EscapeDataString(hasMoreDataValue);
            }
            var status = _http.Upstream(url, data);
            return JsonConvert.DeserializeObject<InputStatus>(status);
        }

        // Delete Datastream
        public void DeleteDatastream(string datastream)
        {
            _http.Delete("/datastream/" + datastream);
        }

        // Create Assessment
        public Assessment CreateAssessment(AssessmentRequest assessment)
        {
            var data = JsonConvert.SerializeObject(assessment, Formatting.Indented,
                new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            var assessmentJson = _http.Post("/assessment", data);

            return JsonConvert.DeserializeObject<Assessment>(assessmentJson);
        }

        // List Assessment
        public List<Assessment> GetAssessment()
        {
            var assessmentJson = _http.Get("/assessment");
            return JsonConvert.DeserializeObject<List<Assessment>>(assessmentJson);
        }

        // delete Assessment
        public void DeleteAssessment(string assessment)
        {
            _http.Delete("/assessment/" + assessment);
        }

        // Add Facts to assessment
        public string AddFacts(string assessment, string data, SortedDictionary<string, string> options)
        {
            var url = "/assessment/" + assessment + "/facts";
            return _http.PostData(url, data);
        }

        public string AddFactsStream(string assessment, byte[] stream, SortedDictionary<string, string> options)
        {
            var url = "/assessment/" + assessment + "/facts";
            return _http.Upstream(url, stream);
        }

        // Stream Output
        public EventSource GetOutput(string assessmentId, long? start, long? end)
        {
            var url = "/assessment/" + assessmentId + "/output";

            var starttemp=start;
            var endtemp = end;

            if (endtemp.HasValue)
            {
                url += "?lastTime=" + end.Value;
                if (starttemp.HasValue) url += "&startTime=" + start.Value;
            }
            else
            {
                if (starttemp.HasValue) url += "?startTime=" + start.Value;
            }

            return _http.Downstream(url);
        }

        //Stream historical output
        public HttpResponse GetHistoricalOutput(Assessment assessment, SortedDictionary<string, string> options)
        {
            var url = "/assessment/" + assessment.Id + "/output?";
            string trackerId;
            string modelIndex;
            string startTime;
            string endTime;
            var firstReqParam = true;

            if (options.TryGetValue("trackerId", out trackerId))
            {
                firstReqParam = false;
                url += "trackerId=" + Uri.EscapeDataString(trackerId);
            }
            if (options.TryGetValue("modelIndex", out modelIndex))
            {
                if (firstReqParam)
                {
                    firstReqParam = false;
                    url += "model=" + Uri.EscapeDataString(modelIndex);
                }
                else
                    url += "&model=" + Uri.EscapeDataString(modelIndex);

            }
            if (options.TryGetValue("startTime", out startTime))
            {
                if (firstReqParam)
                {
                    firstReqParam = false;
                    url += "startTime=" + Uri.EscapeDataString(startTime);
                }
                else
                    url += "&startTime=" + Uri.EscapeDataString(startTime);

            }
            if (options.TryGetValue("endTime", out endTime))
            {
                if (firstReqParam)
                {
                    url += "endTime=" + Uri.EscapeDataString(endTime);
                }
                else
                    url += "&endTime=" + Uri.EscapeDataString(endTime);

            }
            string format;
            var responseFromat = "application/json";
            if (options.TryGetValue("responseFromat", out format))
            {
                if (format.Equals("text/csv"))
                {
                    responseFromat = "text/csv";
                }
            }

            var outputData = _http.GetOutput(url, responseFromat);
            return outputData;
        }

        // Post EntityMeta
        public List<EntityMeta> PostEntityMeta(List<EntityMetaRequest> entityMetaRequest, Datastream datastream)
        {
            var data = JsonConvert.SerializeObject(entityMetaRequest, Formatting.Indented,
                new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var response = _http.Post("/datastream/"+ datastream.Id+"/entityMeta", data);
            return JsonConvert.DeserializeObject<List<EntityMeta>>(response);
        }

        // Get EntityMeta
        public List<EntityMeta> GetEntityMeta(Datastream datastream)
        {
            var response = _http.Get("/datastream/" + datastream.Id + "/entityMeta");
            return JsonConvert.DeserializeObject<List<EntityMeta>>(response);
        }

    }
}

