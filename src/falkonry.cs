///
/// falkonry-csharp-client
/// Copyright(c) 2016 Falkonry Inc
/// MIT Licensed
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using falkonry_csharp_client.helper.models;
using falkonry_csharp_client.service;
using falkonry_csharp_client.src.helper.models;

namespace falkonry_csharp_client    
{
    public class Falkonry
    {
        private FalkonryService falkonryService;

        public Falkonry(string host, string token)
        {
        this.falkonryService = new FalkonryService(host, token);
        }

        public Datastream createDatastream(DatastreamRequest datastream)
        {
        return falkonryService.createDatastream(datastream);
        }

        public List<Datastream> getDatastreams()
        {
        return falkonryService.getDatastream();
        }

        public void deleteDatastream(string datastream)
        {
            falkonryService.deleteDatastream(datastream);
        }

        public Assessment createAssessment(AssessmentRequest assessment)
        {
        return falkonryService.createAssessment(assessment);
        }

        public List<Assessment> getAssessments()
        {
        return falkonryService.getAssessment();
        }

        public void deleteAssessment(string assessment)
        {
            falkonryService.deleteAssessment(assessment);
        }

        public InputStatus addInput(string datastream, string data, SortedDictionary<string, string> options)
        {
        return this.falkonryService.addInputData(datastream, data, options);
        }

        public InputStatus addInputStream(string datastream, byte[] stream, SortedDictionary<string, string> options)
        {
        return this.falkonryService.addInputFromStream(datastream, stream, options);
        }

        public EventSource getOutput(string assessment, long? start, long? end)
        {
        return this.falkonryService.GetOutput(assessment, start, end);
        }

        public static void Main()
        {

        }
        public Datastream getDatastream(string id)
        {
            return falkonryService.getDatastream(id);
        }
        public string addFacts(string assessment, string data, SortedDictionary<string, string> options)
        {
            return this.falkonryService.addFacts(assessment, data, options);
        }
        public string addFactsStream(string assessment, byte[] stream, SortedDictionary<string, string> options)
        {
            return this.falkonryService.addFactsStream(assessment, stream,options);
        }

    }

}
