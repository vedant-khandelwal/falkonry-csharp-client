using System.Collections.Generic;
using falkonry_csharp_client.helper.models;
using falkonry_csharp_client.service;

namespace falkonry_csharp_client    
{
    public class Falkonry
    {
        private readonly FalkonryService _falkonryService;

        public Falkonry(string host, string token)
        {
        _falkonryService = new FalkonryService(host, token);
        }

        public Datastream CreateDatastream(DatastreamRequest datastream)
        {
        return _falkonryService.CreateDatastream(datastream);
        }

        public List<Datastream> GetDatastreams()
        {
        return _falkonryService.GetDatastream();
        }

        public void DeleteDatastream(string datastream)
        {
            _falkonryService.DeleteDatastream(datastream);
        }

        public Assessment CreateAssessment(AssessmentRequest assessment)
        {
        return _falkonryService.CreateAssessment(assessment);
        }

        public List<Assessment> GetAssessments()
        {
        return _falkonryService.GetAssessment();
        }

        public void DeleteAssessment(string assessment)
        {
            _falkonryService.DeleteAssessment(assessment);
        }

        public InputStatus AddInput(string datastream, string data, SortedDictionary<string, string> options)
        {
        return _falkonryService.AddInputData(datastream, data, options);
        }

        public InputStatus AddInputStream(string datastream, byte[] stream, SortedDictionary<string, string> options)
        {
        return _falkonryService.AddInputFromStream(datastream, stream, options);
        }

        public EventSource GetOutput(string assessment, long? start, long? end)
        {
        return _falkonryService.GetOutput(assessment, start, end);
        }

        public static void Main()
        {

        }
        public Datastream GetDatastream(string id)
        {
            return _falkonryService.GetDatastream(id);
        }
        public string AddFacts(string assessment, string data, SortedDictionary<string, string> options)
        {
            return _falkonryService.AddFacts(assessment, data, options);
        }
        public string AddFactsStream(string assessment, byte[] stream, SortedDictionary<string, string> options)
        {
            return _falkonryService.AddFactsStream(assessment, stream,options);
        }

        public HttpResponse GetHistoricalOutput(Assessment assessment, SortedDictionary<string, string> options)
        {
            return _falkonryService.GetHistoricalOutput(assessment, options);
        }

        public List<EntityMeta> PostEntityMeta(List<EntityMetaRequest> entityMetaRequest, Datastream datastream)
        {
            return _falkonryService.PostEntityMeta(entityMetaRequest, datastream);
        }

        public List<EntityMeta> GetEntityMeta(Datastream datastream)
        {
            return _falkonryService.GetEntityMeta(datastream);
        }

    }

}
