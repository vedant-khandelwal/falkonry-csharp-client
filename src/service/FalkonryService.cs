///
/// falkonry-csharp-client
/// Copyright(c) 2016 Falkonry Inc
/// MIT Licensed
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using falkonry_csharp_client.helper.models;
using System.IO;
using falkonry_csharp_client.service;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
namespace falkonry_csharp_client.service
{
    class FalkonryService
    {
        private string host;
        private string token;
        private HttpService http;
        
         public FalkonryService(string host, string token)
        {
            this.host = host;
            this.token = token;
            this.http = new HttpService(host, token);
            
        }

        // Create Datastream
        public Datastream createDatastream(DatastreamRequest datastream)
        {
            JavaScriptSerializer javascript = new JavaScriptSerializer();

            string data = javascript.Serialize(datastream);

            string datastream_json = http.post("/datastream", data);

            return javascript.Deserialize<Datastream>(datastream_json);
        }

        // List Datastream
        public List<Datastream> getDatastream()
        {
            JavaScriptSerializer javascript = new JavaScriptSerializer();
            string datastream_json = http.get("/datastream");
            return javascript.Deserialize<List<Datastream>>(datastream_json);
        }

        // Get Datastream by id
        public Datastream getDatastream(string id)
        {
            JavaScriptSerializer javascript = new JavaScriptSerializer();
            string url = "/datastream/" + id;

            string datastream_json = http.get(url);
            return javascript.Deserialize<Datastream>(datastream_json);
        }
        // Add data to DataStream

        public InputStatus addInputData(string datastream, string data, SortedDictionary<string, string> options)
        {
            JavaScriptSerializer javascript = new JavaScriptSerializer();
            string url = "/datastream/" + datastream;
            string status = this.http.postData(url, data);
            return javascript.Deserialize<InputStatus>(status);
        }

        public InputStatus addInputFromStream(string datastream, byte[] data, SortedDictionary<string, string> options)
        {
            JavaScriptSerializer javascript = new JavaScriptSerializer();
            string url = "/datastream/" + datastream;
            string status = this.http.upstream(url, data);
            return javascript.Deserialize<InputStatus>(status);
        }

        // Delete Datastream
        public void deleteDatastream(string datastream)
        {
            http.delete("/datastream/" + datastream);
        }

        // Create Assessment

        public Assessment createAssessment(Assessment assessment)
        {
            JavaScriptSerializer javascript = new JavaScriptSerializer();

            string data = javascript.Serialize(assessment);

            string assessment_json = http.post("/assessment", data);

            return javascript.Deserialize<Assessment>(assessment_json);
        }

        // List Assessment
        public List<Assessment> getAssessment()
        {
            JavaScriptSerializer javascript = new JavaScriptSerializer();
            string assessment_json = http.get("/assessment");
            return javascript.Deserialize<List<Assessment>>(assessment_json);

        }

        // delete Assessment
        public void deleteAssessment(string assessment)
        {
            http.delete("/assessment/" + assessment);
        }

        // Add Facts to assessment
        public string addFacts(string assessment, string data, SortedDictionary<string, string> options)
        {
            string url = "/assessment/" + assessment + "/facts";
            return http.postData(url, data);
        }
        public string addFactsStream(string assessment, byte[] stream, SortedDictionary<string, string> options)
        {
            string url = "/assessment/" + assessment + "/facts";
            //byte[] data_bytes = IOUtils.toByteArray(stream);
            return http.upstream(url, stream);
        }

        // Stream Output

        public Stream getOutput(string pipeline, long? start, long? end)
        {
            string url = "/pipeline/"+pipeline+"/output?";
            long? starttemp=start;
            long? endtemp = end;

            if (endtemp != null) {
                url += "lastTime=" + end;
                if (starttemp!= null)
                    url += "&startTime=" + start;
                            }
            else {
                if (starttemp != null)
                    url += "startTime=" + start;
                }
            return http.downstream(url);
        }

        byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}

