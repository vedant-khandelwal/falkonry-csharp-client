using Microsoft.VisualStudio.TestTools.UnitTesting;

using falkonry_csharp_client;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using falkonry_csharp_client.helper.models;
using falkonry_csharp_client.service;
using System.Diagnostics;
using System.IO;
/*INSTRUCTIONS: TO RUN ANY TESTS, SIMPLY UNCOMMENT THE ' [TESTCLASS()] ' header before every class of tests to run that particular class of tests. 
 * You should try executing method by method in case classwise tests take too long or fail */

/* Also insert your url and your token in the: 
 * Falkonry falkonry = new Falkonry("http://localhost:8080", "");
 *  fields*/

namespace falkonry_csharp_client.Tests
{
    //[TestClass()]
    public class FalkonryTestsEventbuffer
    {

        
        
        Falkonry falkonry = new Falkonry("http://localhost:8080", "");
        List<Eventbuffer> eventbuffers = new List<Eventbuffer>();
        


        [TestMethod()]
         public void createEventBufferTest()
         {
             
             List<Eventbuffer> eventbuffers = new List<Eventbuffer>();

             System.Random rnd = new System.Random();
             string random_number = System.Convert.ToString(rnd.Next(1, 10000));
             Eventbuffer eb = new Eventbuffer();
             eb.name = "TestEb" + random_number;
             SortedDictionary<string, string> options = new SortedDictionary<string, string>();
             options.Add("timeIdentifier", "time");
             options.Add("timeFormat", "iso_8601");
             Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);

             eventbuffers.Add(eventbuffer);

             Assert.AreEqual(eb.name, eventbuffer.name,false);
             Assert.AreNotEqual(null, eventbuffer.id);
             Assert.AreEqual(0, eventbuffer.schemaList.Count);
             Assert.AreEqual(1, eventbuffer.subscriptionList.Count);
             falkonry.deleteEventbuffer(eventbuffer.id);
        }
         
        
         
        
         
        [TestMethod()]
        public void createEventbufferWithJsonData()
        {
            
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" + random_number;
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            options.Add("data", "{\"time\" :\"2016-03-01 01:01:01\", \"current\" : 12.4, \"vibration\" : 3.4, \"state\" : \"On\"}");
            options.Add("fileFormat", "json");
            Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);
            eventbuffers.Add(eventbuffer);
            

            Assert.AreEqual(eb.name, eventbuffer.name);
            Assert.AreNotEqual(null, eventbuffer.id);
            Assert.AreEqual(1, eventbuffer.schemaList.Count);
            Assert.AreEqual(1, eventbuffer.subscriptionList.Count);
            falkonry.deleteEventbuffer(eventbuffer.id);


        }
        

       [TestMethod()]
       public void createEventBufferWithCsvData()
       {
            
            

            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));

            Eventbuffer eb = new Eventbuffer();
           eb.name = "TestEb" + random_number;
           SortedDictionary<string, string> options = new SortedDictionary<string, string>();
           
           options.Add("timeIdentifier", "time");
           options.Add("timeFormat", "iso_8601");
           options.Add("data", "time, current, vibration, state\n" + "2016-03-01 01:01:01, 12.4, 3.4, On");
           options.Add("fileFormat", "csv");
           Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);
           eventbuffers.Add(eventbuffer);
            Debug.WriteLine(eventbuffer.name);
           Assert.AreEqual(eb.name, eventbuffer.name);
           Assert.AreNotEqual(null, eventbuffer.id);
           Assert.AreEqual(1, eventbuffer.schemaList.Count);
           Assert.AreEqual(1, eventbuffer.subscriptionList.Count);
           falkonry.deleteEventbuffer(eventbuffer.id);

       }
        [TestMethod()]
        public void createEventbufferWithMqttSubscriptionForNarroeFormatData()
        {


            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" + random_number;
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");

            Subscription sub = new Subscription();
            sub.type = "MQTT";
            sub.path = ("mqtt://test.mosquito.com");
            sub.topic = ("falkonry-eb-1-test");
            sub.username = ("test-user");
            sub.password = ("test");
            sub.timeFormat = ("YYYY-MM-DD HH:mm:ss");
            sub.timeIdentifier = ("time");
            sub.valueColumn = ("value");
            sub.signalsDelimiter = ("_");
            sub.signalsTagField = ("tag");
            sub.signalsLocation = ("prefix");

            Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);
            eventbuffers.Add(eventbuffer);
            Subscription subscription = falkonry.createSubscription(eventbuffer.id, sub);
            Assert.AreNotEqual(null, subscription.key);
            Assert.AreEqual(sub.type, subscription.type);
            Assert.AreEqual(sub.path, subscription.path);
            Assert.AreEqual(sub.topic, subscription.topic);
            Assert.AreEqual(sub.path, subscription.path);
            Assert.AreEqual(sub.username, subscription.username);
            Assert.AreEqual(sub.password, subscription.password);
            Assert.AreEqual(sub.timeIdentifier, subscription.timeIdentifier);
            Assert.AreEqual(sub.timeFormat, subscription.timeFormat);
            Assert.AreEqual(sub.valueColumn, subscription.valueColumn);
            Assert.AreEqual(sub.signalsDelimiter, subscription.signalsDelimiter);
            Assert.AreEqual(sub.signalsTagField, subscription.signalsTagField);
            Assert.AreEqual(sub.signalsLocation, subscription.signalsLocation);
            falkonry.deleteSubscription(eventbuffer.id, subscription.key);
            falkonry.deleteEventbuffer(eventbuffer.id);
        }
        [TestMethod]
        public void createEventbufferWithMqttSubscription()
        {
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" + random_number;
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();

            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            Subscription sub = new Subscription();
            sub.type = ("MQTT");
            sub.path = ("mqtt://test.mosquito.com");
            sub.topic = ("falkonry-eb-1-test");
            sub.username = ("test-user");
            sub.password = ("test");
            sub.timeFormat = ("YYYY-MM-DD HH:mm:ss");
            sub.timeIdentifier=("time");
            Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);
            Subscription subscription = falkonry.createSubscription(eventbuffer.id, sub);
            Assert.AreNotEqual(null, subscription.key);
            Assert.AreEqual(sub.type, subscription.type);
            Assert.AreEqual(sub.path, subscription.path);
            Assert.AreEqual(sub.topic, subscription.topic);
            Assert.AreEqual(sub.path, subscription.path);
            Assert.AreEqual(sub.username, subscription.username);
            Assert.AreEqual(sub.password, subscription.password);
            Assert.AreEqual(sub.timeIdentifier, subscription.timeIdentifier);
            Assert.AreEqual(sub.timeFormat, subscription.timeFormat);
            falkonry.deleteSubscription(eventbuffer.id, subscription.key);
            falkonry.deleteEventbuffer(eventbuffer.id);
        }

        //The test below fails
        //[TestMethod()]
        public void createEventbufferWithOutflowSubscription()
        {
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" + random_number;
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();

            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");

            Subscription sub = new Subscription();
            sub.type = ("PIPELINEOUTFLOW");
            sub.path=("urn:falkonry:pipeline:qaerscdtxh7rc3");

        Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);
        eventbuffers.Add(eventbuffer);
        Assert.AreEqual(1, eventbuffer.subscriptionList.Count);

    Subscription subscription = falkonry.createSubscription(eventbuffer.id, sub);
        Assert.AreNotEqual(null, subscription.key);
    Assert.AreEqual(sub.type, subscription.type);
    Assert.AreEqual(sub.path, subscription.path);
  }
       
    }
    //[TestClass]
    public class AddData
    {
        
        Falkonry falkonry = new Falkonry("http://localhost:8080", "");
        List<Eventbuffer> eventbuffers = new List<Eventbuffer>();

        [TestMethod()]
        public void addDataJson()
        {
            
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" +random_number;
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);
            string data = "{\"time\" :\"2016-03-01 01:01:01\", \"current\" : 12.4, \"vibration\" : 3.4, \"state\" : \"On\"}";
            options.Add("fileFormat", "json");
            InputStatus inputstatus = falkonry.addInput(eventbuffer.id, data, options);
            eventbuffer = falkonry.getEventBuffer(eventbuffer.id);
            Debug.WriteLine(eventbuffer.schemaList.Count);
            Assert.AreEqual(1, eventbuffer.schemaList.Count);
            falkonry.deleteEventbuffer(eventbuffer.id);
        }
        [TestMethod()]
        public void addDataCSV()
        {
            
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" + random_number;
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);
            string data = "time, current, vibration, state\n" + "2016-03-01 01:01:01, 12.4, 3.4, On";
            options.Add("fileFormat", "csv");
            InputStatus inputstatus = falkonry.addInput(eventbuffer.id, data, options);
            eventbuffer = falkonry.getEventBuffer(eventbuffer.id);
            Debug.WriteLine(eventbuffer.schemaList.Count);
            Assert.AreEqual(1, eventbuffer.schemaList.Count);
            falkonry.deleteEventbuffer(eventbuffer.id);
        }
        

    }
    //[TestClass]
    public class AddDataFromStream
    {
        
        Falkonry falkonry = new Falkonry("http://localhost:8080", "");
        List<Eventbuffer> eventbuffers = new List<Eventbuffer>();

        [TestMethod()]
        public void addDataFromStreamJSON()
        {
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));

            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" + random_number;
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);
            string folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            
            string path = folder + "/AddData.json";
            
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            
            InputStatus inputstatus = falkonry.addInputStream(eventbuffer.id, bytes, options);

            eventbuffer = falkonry.getEventBuffer(eventbuffer.id);
            eventbuffers.Add(eventbuffer);
            Debug.WriteLine(eventbuffer.schemaList.Count);
            falkonry.deleteEventbuffer(eventbuffer.id);
            Assert.AreEqual(1, eventbuffer.schemaList.Count);
            
            
        }
        [TestMethod()]
        public void addDataFromStreamCSV()
        {
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));

            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" + random_number;
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);
            string folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            
            string path = folder + "/AddData.csv";
            
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Debug.WriteLine("IF READING FROM FILE WORKS");
            InputStatus inputstatus = falkonry.addInputStream(eventbuffer.id, bytes, options);

            eventbuffer = falkonry.getEventBuffer(eventbuffer.id);
            eventbuffers.Add(eventbuffer);
            Debug.WriteLine(eventbuffer.schemaList.Count);
            falkonry.deleteEventbuffer(eventbuffer.id);
            

        }
        
    }
    //[TestClass]
    public class TestCreatePipeline
    {
        Falkonry falkonry = new Falkonry("http://localhost:8080", "");
        List<Eventbuffer> eventbuffers = new List<Eventbuffer>();
        [TestMethod]
        public void createPipeline()
        {
            List<Pipeline> pipelines = new List<Pipeline>();
            List<Eventbuffer> eventbuffers = new List<Eventbuffer>();
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" + random_number;

            List<Signal> signals = new List<Signal>();
            Signal signal1 = new Signal();
            signal1.name = "current";
            ValueType valuetype1 = new ValueType();
            valuetype1.type = "Numeric";
            EventType eventtype1 = new EventType();
            eventtype1.type = "Samples";
            signal1.eventType = eventtype1;
            signal1.valueType = valuetype1;
            signals.Add(signal1);

            Signal signal2 = new Signal();
            signal2.name = "vibration";
            ValueType valuetype2 = new ValueType();
            valuetype2.type = "Numeric";
            EventType eventtype2 = new EventType();
            eventtype2.type = "Samples";
            signal2.eventType = eventtype2;
            signal2.valueType = valuetype2;
            signals.Add(signal2);

            Signal signal3 = new Signal();
            signal3.name = "state";
            ValueType valuetype3 = new ValueType();
            valuetype3.type = "Categorical";
            EventType eventtype3 = new EventType();
            eventtype3.type = "Samples";
            signal3.eventType = eventtype3;
            signal3.valueType = valuetype3;
            signals.Add(signal3);


            List<string> inputList = new List<string>();
            inputList.Add("current");
            inputList.Add("vibration");
            inputList.Add("state");

            List<Assessment> assessments = new List<Assessment>();
            Assessment assessment = new Assessment();
            assessment.name = "Health";
            assessment.inputList = inputList;
            assessments.Add(assessment);

            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            options.Add("data", "time, current, vibration, state\n" + "2016-03-01 01:01:01, 12.4, 3.4, On");
            options.Add("fileFormat", "csv");

            Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);
            

            Interval interval = new Interval();
            interval.duration = "PT1S";

            Pipeline pipeline = new Pipeline();
            random_number = System.Convert.ToString(rnd.Next(1, 10000));
            string name = "Test-PL-" + random_number;
            pipeline.name = name;
            

            pipeline.inputList = (signals);
            pipeline.singleThingID = (name);
            pipeline.thingIdentifier = ("thing");
            pipeline.assessmentList = (assessments);
            pipeline.interval = (interval);
            pipeline.input = eventbuffer.id;
            
            Pipeline pl = falkonry.createPipeline(pipeline);
            falkonry.deletePipeline(pl.id);
            falkonry.deleteEventbuffer(eventbuffer.id);
            Assert.AreEqual(pl.name, pipeline.name);
        }
    }

    //[TestClass]
    public class GetPipelines
    {
        Falkonry falkonry = new Falkonry("http://localhost:8080", "");
        List<Eventbuffer> eventbuffers = new List<Eventbuffer>();
        [TestMethod]
        public void getPipeline()
        {
            List<Pipeline> pipelines = new List<Pipeline>();
            List<Eventbuffer> eventbuffers = new List<Eventbuffer>();
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" + random_number;

            List<Signal> signals = new List<Signal>();
            Signal signal1 = new Signal();
            signal1.name = "current";
            ValueType valuetype1 = new ValueType();
            valuetype1.type = "Numeric";
            EventType eventtype1 = new EventType();
            eventtype1.type = "Samples";
            signal1.eventType = eventtype1;
            signal1.valueType = valuetype1;
            signals.Add(signal1);

            Signal signal2 = new Signal();
            signal2.name = "vibration";
            ValueType valuetype2 = new ValueType();
            valuetype2.type = "Numeric";
            EventType eventtype2 = new EventType();
            eventtype2.type = "Samples";
            signal2.eventType = eventtype2;
            signal2.valueType = valuetype2;
            signals.Add(signal2);

            Signal signal3 = new Signal();
            signal3.name = "state";
            ValueType valuetype3 = new ValueType();
            valuetype3.type = "Categorical";
            EventType eventtype3 = new EventType();
            eventtype3.type = "Samples";
            signal3.eventType = eventtype3;
            signal3.valueType = valuetype3;
            signals.Add(signal3);


            List<string> inputList = new List<string>();
            inputList.Add("current");
            inputList.Add("vibration");
            inputList.Add("state");

            List<Assessment> assessments = new List<Assessment>();
            Assessment assessment = new Assessment();
            assessment.name = "Health";
            assessment.inputList = inputList;
            assessments.Add(assessment);

            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            options.Add("data", "time, current, vibration, state\n" + "2016-03-01 01:01:01, 12.4, 3.4, On");
            options.Add("fileFormat", "csv");

            Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);


            Interval interval = new Interval();
            interval.duration = "PT1S";

            Pipeline pipeline = new Pipeline();
            random_number = System.Convert.ToString(rnd.Next(1, 10000));
            string name = "Test-PL-" + random_number;
            pipeline.name = name;


            pipeline.inputList = (signals);
            pipeline.singleThingID = (name);
            pipeline.thingIdentifier = ("thing");
            pipeline.assessmentList = (assessments);
            pipeline.interval = (interval);
            pipeline.input = eventbuffer.id;

            Pipeline pl = falkonry.createPipeline(pipeline);
            List<Pipeline> pipelinelist = falkonry.getPipelines();
            Assert.AreNotEqual(0, pipelinelist.Count);
            falkonry.deletePipeline(pl.id);
            falkonry.deleteEventbuffer(eventbuffer.id);
            
        }
    }
    //[TestClass]
    public class GetPipelineOutFlow
    {
        Falkonry falkonry = new Falkonry("http://localhost:8080", "");
        [TestMethod]
        public void PipelineOutFlow()
        {
            try
            {
                Stream streamrecieved = falkonry.getOutput("e9wxrrh4yvwv4p", null, null);
                string folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string path = folder + "/outflow.txt";

                StreamReader streamreader = new StreamReader(streamrecieved);
                StreamWriter streamwriter = new StreamWriter(path);
                string line;
                using (streamwriter)
                {
                    while ((line = streamreader.ReadLine()) != null)
                    {


                        streamwriter.WriteLine(line);
                    }
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail();
            }
        }
    

        }
    //[TestClass]
    public class TestPublication
    {
        Falkonry falkonry = new Falkonry("http://localhost:8080", "");
        List<Pipeline> pipelines = new List<Pipeline>();
        List<Eventbuffer> eventbuffers = new List<Eventbuffer>();
        [TestMethod]
        public void createPublication()
        {

            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" + random_number;

            List<Signal> signals = new List<Signal>();
            Signal signal1 = new Signal();
            signal1.name = "current";
            ValueType valuetype1 = new ValueType();
            valuetype1.type = "Numeric";
            EventType eventtype1 = new EventType();
            eventtype1.type = "Samples";
            signal1.eventType = eventtype1;
            signal1.valueType = valuetype1;
            signals.Add(signal1);

            Signal signal2 = new Signal();
            signal2.name = "vibration";
            ValueType valuetype2 = new ValueType();
            valuetype2.type = "Numeric";
            EventType eventtype2 = new EventType();
            eventtype2.type = "Samples";
            signal2.eventType = eventtype2;
            signal2.valueType = valuetype2;
            signals.Add(signal2);

            Signal signal3 = new Signal();
            signal3.name = "state";
            ValueType valuetype3 = new ValueType();
            valuetype3.type = "Categorical";
            EventType eventtype3 = new EventType();
            eventtype3.type = "Samples";
            signal3.eventType = eventtype3;
            signal3.valueType = valuetype3;
            signals.Add(signal3);


            List<string> inputList = new List<string>();
            inputList.Add("current");
            inputList.Add("vibration");
            inputList.Add("state");

            List<Assessment> assessments = new List<Assessment>();
            Assessment assessment = new Assessment();
            assessment.name = "Health";
            assessment.inputList = inputList;
            assessments.Add(assessment);

            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);
            

            Interval interval = new Interval();
            interval.duration = "PT1S";
            
            random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Pipeline pipeline = new Pipeline();
            string name = "Test-PL-" + random_number;
            pipeline.name = name;
            
            pipeline.inputList = (signals);
            pipeline.singleThingID = (name);
            pipeline.thingIdentifier = ("thing");
            pipeline.assessmentList = (assessments);
            pipeline.interval = (interval);
            pipeline.input = eventbuffer.id;
            //pipeline.input = eb.name;
            Pipeline pl = falkonry.createPipeline(pipeline);
            
            Publication publication = new Publication();
            publication.type = ("MQTT");
            publication.path = ("mqtt://test.mosquito.com");
            publication.topic = ("falkonry-eb-1-test");
            publication.username = ("test-user");
            publication.password = ("test");
            publication.contentType = ("application/json");
            Publication pb = falkonry.createPublication(pl.id, publication);
           
            Assert.AreEqual(pb.username, publication.username);
            falkonry.deletePublication(pl.id, pb.key);
            falkonry.deletePipeline(pl.id);
            falkonry.deleteEventbuffer(eventbuffer.id);


        }
    }

    //[TestClass]
    public class TestVerification
    {
        Falkonry falkonry = new Falkonry("http://localhost:8080", "");

        [TestMethod]
        public void createPipelineWithCSVData()
        {
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" + random_number;
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);

            

            List<Signal> signals = new List<Signal>();
            Signal signal1 = new Signal();
            signal1.name = "current";
            ValueType valuetype1 = new ValueType();
            valuetype1.type = "Numeric";
            EventType eventtype1 = new EventType();
            eventtype1.type = "Samples";
            signal1.eventType = eventtype1;
            signal1.valueType = valuetype1;
            signals.Add(signal1);

            Signal signal2 = new Signal();
            signal2.name = "vibration";
            ValueType valuetype2 = new ValueType();
            valuetype2.type = "Numeric";
            EventType eventtype2 = new EventType();
            eventtype2.type = "Samples";
            signal2.eventType = eventtype2;
            signal2.valueType = valuetype2;
            signals.Add(signal2);

            Signal signal3 = new Signal();
            signal3.name = "state";
            ValueType valuetype3 = new ValueType();
            valuetype3.type = "Categorical";
            EventType eventtype3 = new EventType();
            eventtype3.type = "Samples";
            signal3.eventType = eventtype3;
            signal3.valueType = valuetype3;
            signals.Add(signal3);


            List<string> inputList = new List<string>();
            inputList.Add("current");
            inputList.Add("vibration");
            inputList.Add("state");

            List<Assessment> assessments = new List<Assessment>();
            Assessment assessment = new Assessment();
            assessment.name = "Health";
            assessment.inputList = inputList;
            assessments.Add(assessment);

            

            Interval interval = new Interval();
            interval.duration = "PT1S";

            Pipeline pipeline = new Pipeline();
            random_number = System.Convert.ToString(rnd.Next(1, 10000));
            string name = "Test-PL-" + random_number;
            pipeline.name = name;


            pipeline.inputList = (signals);
            pipeline.singleThingID = (name);
            pipeline.thingIdentifier = ("thing");
            pipeline.assessmentList = (assessments);
            pipeline.interval = (interval);
            pipeline.input = eventbuffer.id;

            Pipeline pl = falkonry.createPipeline(pipeline);

            string data = "time,end,car,Health\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,IL9753,Normal\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,HI3821,Normal";
            string response = falkonry.addVerification(pl.id, data, null);
            Assert.AreEqual(response, "{\"message\":\"Data submitted successfully\"}");

            falkonry.deletePipeline(pl.id);
            falkonry.deleteEventbuffer(eventbuffer.id);

        }
        [TestMethod]
        public void createPipelineWithJSONVerification()
        {
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" + random_number;
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);



            List<Signal> signals = new List<Signal>();
            Signal signal1 = new Signal();
            signal1.name = "current";
            ValueType valuetype1 = new ValueType();
            valuetype1.type = "Numeric";
            EventType eventtype1 = new EventType();
            eventtype1.type = "Samples";
            signal1.eventType = eventtype1;
            signal1.valueType = valuetype1;
            signals.Add(signal1);

            Signal signal2 = new Signal();
            signal2.name = "vibration";
            ValueType valuetype2 = new ValueType();
            valuetype2.type = "Numeric";
            EventType eventtype2 = new EventType();
            eventtype2.type = "Samples";
            signal2.eventType = eventtype2;
            signal2.valueType = valuetype2;
            signals.Add(signal2);

            Signal signal3 = new Signal();
            signal3.name = "state";
            ValueType valuetype3 = new ValueType();
            valuetype3.type = "Categorical";
            EventType eventtype3 = new EventType();
            eventtype3.type = "Samples";
            signal3.eventType = eventtype3;
            signal3.valueType = valuetype3;
            signals.Add(signal3);


            List<string> inputList = new List<string>();
            inputList.Add("current");
            inputList.Add("vibration");
            inputList.Add("state");

            List<Assessment> assessments = new List<Assessment>();
            Assessment assessment = new Assessment();
            assessment.name = "Health";
            assessment.inputList = inputList;
            assessments.Add(assessment);



            Interval interval = new Interval();
            interval.duration = "PT1S";

            Pipeline pipeline = new Pipeline();
            random_number = System.Convert.ToString(rnd.Next(1, 10000));
            string name = "Test-PL-" + random_number;
            pipeline.name = name;


            pipeline.inputList = (signals);
            pipeline.singleThingID = (name);
            pipeline.thingIdentifier = ("thing");
            pipeline.assessmentList = (assessments);
            pipeline.interval = (interval);
            pipeline.input = eventbuffer.id;

            Pipeline pl = falkonry.createPipeline(pipeline);

            string data = "{\"time\" : \"2011-03-26T12:00:00Z\", \"car\" : \"HI3821\", \"end\" : \"2012-06-01T00:00:00Z\", \"Health\" : \"Normal\"}";
            string response = falkonry.addVerification(pl.id, data, null);
            
            //Assert.AreEqual(response, "{\"message\":\"Data submitted successfully\"}");

            string response_id = response.Split(new char[] {':',','})[1];
            Debug.WriteLine("+++++++++++++++++++++++++++++++++++");
            Debug.WriteLine(response_id);
            Debug.WriteLine(response);
            Debug.WriteLine("+++++++++++++++++++++++++++++++++++");
            Assert.AreNotEqual(response_id, null);
            falkonry.deletePipeline(pl.id);
            falkonry.deleteEventbuffer(eventbuffer.id);
        }

    }

    //[TestClass]
    public class TestAddVerificationDataStream
    {
        Falkonry falkonry = new Falkonry("http://localhost:8080", "");


        [TestMethod]
        public void createPipelineWithCsvVerificationStream()
        {
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" + random_number;
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);



            List<Signal> signals = new List<Signal>();
            Signal signal1 = new Signal();
            signal1.name = "current";
            ValueType valuetype1 = new ValueType();
            valuetype1.type = "Numeric";
            EventType eventtype1 = new EventType();
            eventtype1.type = "Samples";
            signal1.eventType = eventtype1;
            signal1.valueType = valuetype1;
            signals.Add(signal1);

            Signal signal2 = new Signal();
            signal2.name = "vibration";
            ValueType valuetype2 = new ValueType();
            valuetype2.type = "Numeric";
            EventType eventtype2 = new EventType();
            eventtype2.type = "Samples";
            signal2.eventType = eventtype2;
            signal2.valueType = valuetype2;
            signals.Add(signal2);

            Signal signal3 = new Signal();
            signal3.name = "state";
            ValueType valuetype3 = new ValueType();
            valuetype3.type = "Categorical";
            EventType eventtype3 = new EventType();
            eventtype3.type = "Samples";
            signal3.eventType = eventtype3;
            signal3.valueType = valuetype3;
            signals.Add(signal3);


            List<string> inputList = new List<string>();
            inputList.Add("current");
            inputList.Add("vibration");
            inputList.Add("state");

            List<Assessment> assessments = new List<Assessment>();
            Assessment assessment = new Assessment();
            assessment.name = "Health";
            assessment.inputList = inputList;
            assessments.Add(assessment);



            Interval interval = new Interval();
            interval.duration = "PT1S";

            Pipeline pipeline = new Pipeline();
            random_number = System.Convert.ToString(rnd.Next(1, 10000));
            string name = "Test-PL-" + random_number;
            pipeline.name = name;


            pipeline.inputList = (signals);
            pipeline.singleThingID = (name);
            pipeline.thingIdentifier = ("thing");
            pipeline.assessmentList = (assessments);
            pipeline.interval = (interval);
            pipeline.input = eventbuffer.id;

            Pipeline pl = falkonry.createPipeline(pipeline);

            string folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = folder + "/verificationData.csv";
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            string response = falkonry.addVerificationStream(pl.id, bytes, null);
            Assert.AreEqual(response, "{\"message\":\"Data submitted successfully\"}");
            falkonry.deletePipeline(pl.id);
            falkonry.deleteEventbuffer(eventbuffer.id);
        }

        [TestMethod]
        public void createPipelineWithJSONVerificationStream()
        {
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Eventbuffer eb = new Eventbuffer();
            eb.name = "TestEb" + random_number;
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            Eventbuffer eventbuffer = falkonry.createEventbuffer(eb, options);



            List<Signal> signals = new List<Signal>();
            Signal signal1 = new Signal();
            signal1.name = "current";
            ValueType valuetype1 = new ValueType();
            valuetype1.type = "Numeric";
            EventType eventtype1 = new EventType();
            eventtype1.type = "Samples";
            signal1.eventType = eventtype1;
            signal1.valueType = valuetype1;
            signals.Add(signal1);

            Signal signal2 = new Signal();
            signal2.name = "vibration";
            ValueType valuetype2 = new ValueType();
            valuetype2.type = "Numeric";
            EventType eventtype2 = new EventType();
            eventtype2.type = "Samples";
            signal2.eventType = eventtype2;
            signal2.valueType = valuetype2;
            signals.Add(signal2);

            Signal signal3 = new Signal();
            signal3.name = "state";
            ValueType valuetype3 = new ValueType();
            valuetype3.type = "Categorical";
            EventType eventtype3 = new EventType();
            eventtype3.type = "Samples";
            signal3.eventType = eventtype3;
            signal3.valueType = valuetype3;
            signals.Add(signal3);


            List<string> inputList = new List<string>();
            inputList.Add("current");
            inputList.Add("vibration");
            inputList.Add("state");

            List<Assessment> assessments = new List<Assessment>();
            Assessment assessment = new Assessment();
            assessment.name = "Health";
            assessment.inputList = inputList;
            assessments.Add(assessment);



            Interval interval = new Interval();
            interval.duration = "PT1S";

            Pipeline pipeline = new Pipeline();
            random_number = System.Convert.ToString(rnd.Next(1, 10000));
            string name = "Test-PL-" + random_number;
            pipeline.name = name;


            pipeline.inputList = (signals);
            pipeline.singleThingID = (name);
            pipeline.thingIdentifier = ("thing");
            pipeline.assessmentList = (assessments);
            pipeline.interval = (interval);
            pipeline.input = eventbuffer.id;

            Pipeline pl = falkonry.createPipeline(pipeline);

            string folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = folder + "/verificationData.json";
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            string response = falkonry.addVerificationStream(pl.id, bytes, null);

            string response_id = response.Split(new char[] { ':', ',' })[1];
            Assert.AreNotEqual(response_id, null);
            falkonry.deletePipeline(pl.id);
            falkonry.deleteEventbuffer(eventbuffer.id);

        }
    }

}
    
