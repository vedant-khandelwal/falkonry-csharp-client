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
    namespace falkonry_csharp_client    
{
    public class Falkonry
    {
        private FalkonryService falkonryService;

        public Falkonry(string host, string token)
        {
        this.falkonryService = new FalkonryService(host, token);
        }

        public Eventbuffer createEventbuffer(Eventbuffer eventbuffer, SortedDictionary<string, string> options)
        {
        return falkonryService.createEventbuffer(eventbuffer, options);
        }

        public List<Eventbuffer> getEventbuffers()
        {
        return falkonryService.getEventbuffers();
        }

        public void deleteEventbuffer(string eventbuffer)
        {
            falkonryService.deleteEventbuffer(eventbuffer);
        }

        public Pipeline createPipeline(Pipeline pipeline)
        {
        return falkonryService.createPipeline(pipeline);
        }

        public List<Pipeline> getPipelines()
        {
        return falkonryService.getPipelines();
        }

        public void deletePipeline(string pipeline)
        {
            falkonryService.deletePipeline(pipeline);
        }

        public InputStatus addInput(string eventbuffer, string data, SortedDictionary<string, string> options)
        {
        return this.falkonryService.addInputData(eventbuffer, data, options);
        }

        public InputStatus addInputStream(string eventbuffer, byte[] stream, SortedDictionary<string, string> options)
        {
        return this.falkonryService.addInputFromStream(eventbuffer, stream, options);
        }

        public Stream getOutput(string pipeline, long? start, long? end)
        {
        return this.falkonryService.getOutput(pipeline, start, end);
        }

        public Subscription createSubscription(string eventbuffer, Subscription subscription)
        {
        return falkonryService.createSubscription(eventbuffer, subscription);
        }

        public Subscription updateSubscription(string eventbuffer, Subscription subscription)
        {
        return falkonryService.updateSubscription(eventbuffer, subscription);
        }

        public void deleteEventbuffer(string eventbuffer, string subscription)
        {
            falkonryService.deleteEventbuffer(eventbuffer, subscription);
        }

        public Publication createPublication(string pipeline, Publication publication)
        {
        return falkonryService.createPublication(pipeline, publication);
        }

        public Publication updatePublication(string pipeline, Publication publication)
        {
        return falkonryService.updatePublication(pipeline, publication);
        }

        public void deletePublication(string pipeline, string publication)
        {
            falkonryService.deletePublication(pipeline, publication);
        }
        public void deleteSubscription(string eventbuffer, string subscription)
        {
            falkonryService.deleteSubscription(eventbuffer, subscription);
        }
        public static void Main()
        {

        }
        public Eventbuffer getEventBuffer(string id)
        {
            return falkonryService.getEventBuffer(id);
        }

}

}
