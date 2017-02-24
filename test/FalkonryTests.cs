using Microsoft.VisualStudio.TestTools.UnitTesting;

using falkonry_csharp_client;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using falkonry_csharp_client.helper.models;
using falkonry_csharp_client.service;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.IO;
/*INSTRUCTIONS: TO RUN ANY TESTS, SIMPLY UNCOMMENT THE ' [TESTCLASS()] ' header before every class of tests to run that particular class of tests. 
 * You should try executing method by method in case classwise tests take too long or fail */

/* Also insert your url and your token in the: 
 * Falkonry falkonry = new Falkonry("http://localhost:8080", "");
 *  fields*/

namespace falkonry_csharp_client.Tests
{
    // [TestClass()]
    public class FalkonryTestsDatastream
    {
        Falkonry falkonry = new Falkonry("https://dev.falkonry.ai", "kvtsfp2z9qoggpndf8p5jhk7w0woi580");
        List<Datastream> datastreams = new List<Datastream>();
        
        // Create StandAlone Datrastream with Wide format
       [TestMethod()]
         public void createStandaloneDatastream()
         {
             Timezone timezone = new Timezone();
             timezone.zone = "GMT";
             timezone.offset = 0;
             Datasource datasource = new Datasource();
             datasource.type = "STANDALONE"; 
             System.Random rnd = new System.Random();
             string random_number = System.Convert.ToString(rnd.Next(1, 10000));
             DatastreamRequest ds = new DatastreamRequest();
             ds.name = "TestDatastream" + random_number;
             ds.timeIdentifier = "time";
             ds.timeFormat = "iso_8601";
             ds.timezone = timezone;
             ds.dataSource = datasource;
             Datastream datastream = falkonry.createDatastream(ds);
             Assert.AreEqual(ds.name, datastream.name,false);
             Assert.AreNotEqual(null, datastream.id);
             Assert.AreEqual(ds.timeFormat, datastream.dataTransformation.timeFormat);
             Assert.AreEqual(ds.timeIdentifier, datastream.dataTransformation.timeIdentifier);
             Assert.AreEqual(ds.dataSource.type, datastream.dataSource.type);
             falkonry.deleteDatastream(datastream.id);
        }

        // Create Standalone datastream with entityIdentifier
       [TestMethod()]
        public void createDatastreamWithEntityIdentifierTest()
        {
            Timezone timezone = new Timezone();
            timezone.zone = "GMT";
            timezone.offset = 0;
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            DatastreamRequest ds = new DatastreamRequest();
            Datasource datasource = new Datasource();
            datasource.type = "STANDALONE";
            ds.name = "TestDatastream" + random_number;
            ds.timeIdentifier = "time";
            ds.timeFormat = "iso_8601";
            ds.entityIdentifier = "Unit";
            ds.dataSource = datasource;
            ds.timezone = timezone;
            Datastream datastream = falkonry.createDatastream(ds);
            Assert.AreEqual(ds.name, datastream.name, false);
            Assert.AreNotEqual(null, datastream.id);
            Assert.AreEqual(ds.timeFormat, datastream.dataTransformation.timeFormat);
            Assert.AreEqual(ds.timeIdentifier, datastream.dataTransformation.timeIdentifier);
            Assert.AreEqual(ds.dataSource.type, datastream.dataSource.type);
            falkonry.deleteDatastream(datastream.id);
        }

        // Create PI Datastream (Narrow Format)
        [TestMethod()]
        public void createPIDatastreamTest()
        {
            Timezone timezone = new Timezone();
            timezone.zone = "GMT";
            timezone.offset = 0;

            Datasource datasource = new Datasource();
            datasource.type = "PI";
            datasource.host = "https://test.piserver.com/piwebapi";
            datasource.elementTemplateName = "SampleElementTempalte";
            DatastreamRequest ds = new DatastreamRequest();
            ds.timeIdentifier = "time";
            ds.timeFormat = "iso_8601";
            ds.valueColumn = "value";
            ds.signalsTagField = "tag";
            ds.signalsLocation = "prefix";
            ds.signalsDelimiter = "_";
            ds.dataSource = datasource;
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            ds.name = "TestDS" + random_number;
            ds.timezone = timezone;
            ds.dataSource = datasource;
            Datastream datastream = falkonry.createDatastream(ds);
            Assert.AreEqual(ds.name, datastream.name, false);
            Assert.AreNotEqual(null, datastream.id);
            Assert.AreEqual(ds.timeFormat, datastream.dataTransformation.timeFormat);
            Assert.AreEqual(ds.timeIdentifier, datastream.dataTransformation.timeIdentifier);
            Assert.AreEqual(ds.dataSource.type, datastream.dataSource.type);
            falkonry.deleteDatastream(datastream.id);
        }

    }

   // [TestClass()]
    public class AddData
    {

        Falkonry falkonry = new Falkonry("https://dev.falkonry.ai", "kvtsfp2z9qoggpndf8p5jhk7w0woi580");
        
        [TestMethod()]
        public void addDataJson()
        {
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            DatastreamRequest ds = new DatastreamRequest();
            ds.name = "TestDSJSON" + random_number;
            ds.timeIdentifier = "time";
            ds.timeFormat = "iso_8601";
            ds.entityIdentifier = "Unit";
            Datasource datasource = new Datasource();
            datasource.type = "PI";
            datasource.host = "https://test.piserver.com/piwebapi";
            datasource.elementTemplateName = "SampleElementTempalte";
            ds.dataSource = datasource;

            // Input List
            List<Input> inputList = new List<Input>();
            Input currents = new Input();
            currents.name = "current";
            currents.valueType = new ValueType();
            currents.eventType = new EventType();
            currents.valueType.type = "Numeric";
            currents.eventType.type = "Samples";
            inputList.Add(currents);

            Input vibration = new Input();
            vibration.name = "vibration";
            vibration.valueType = new ValueType();
            vibration.eventType = new EventType();
            vibration.valueType.type = "Numeric";
            vibration.eventType.type = "Samples";
            inputList.Add(vibration);

            Input state = new Input();
            state.name = "state";
            state.valueType = new ValueType();
            state.eventType = new EventType();
            state.valueType.type = "Categorical";
            state.eventType.type = "Samples";
            inputList.Add(state);

            ds.inputList = inputList;

            Datastream datastream = falkonry.createDatastream(ds);
            Assert.AreEqual(ds.name, datastream.name, false);
            Assert.AreNotEqual(null, datastream.id);
            Assert.AreEqual(ds.timeFormat, datastream.dataTransformation.timeFormat);
            Assert.AreEqual(ds.timeIdentifier, datastream.dataTransformation.timeIdentifier);
            Assert.AreEqual(ds.dataSource.type, datastream.dataSource.type);



            string data = "{\"time\" :\"2016-03-01 01:01:01\",\"Unit\":\"Unit1\", \"current\" : 12.4, \"vibration\" : 3.4, \"state\" : \"On\"}";
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            options.Add("fileFormat", "json");
            InputStatus inputstatus = falkonry.addInput(datastream.id, data, options);
            datastream = falkonry.getDatastream(datastream.id);
            falkonry.deleteDatastream(datastream.id);
        }

        [TestMethod()]
        public void addDataCSV()
          {
              System.Random rnd = new System.Random();
              string random_number = System.Convert.ToString(rnd.Next(1, 10000));
              DatastreamRequest ds = new DatastreamRequest();
              Timezone timezone = new Timezone();
              timezone.zone = "GMT";
              timezone.offset = 0;
              ds.name = "TestDSCSV" + random_number;
              ds.timeIdentifier = "time";
              ds.timeFormat = "iso_8601";
              ds.entityIdentifier = "Unit";
              ds.timezone = timezone;
              Datasource datasource = new Datasource();
              datasource.type = "PI";
              datasource.host = "https://test.piserver.com/piwebapi";
              datasource.elementTemplateName = "SampleElementTempalte";
              ds.dataSource = datasource;

              // Input List
              List<Input> inputList = new List<Input>();
              Input currents = new Input();
              currents.name = "current";
              currents.valueType = new ValueType();
              currents.eventType = new EventType();
              currents.valueType.type = "Numeric";
              currents.eventType.type = "Samples";
              inputList.Add(currents);

              Input vibration = new Input();
              vibration.name = "vibration";
              vibration.valueType = new ValueType();
              vibration.eventType = new EventType();
              vibration.valueType.type = "Numeric";
              vibration.eventType.type = "Samples";
              inputList.Add(vibration);

              Input state = new Input();
              state.name = "state";
              state.valueType = new ValueType();
              state.eventType = new EventType();
              state.valueType.type = "Categorical";
              state.eventType.type = "Samples";
              inputList.Add(state);

              ds.inputList = inputList;

              Datastream datastream = falkonry.createDatastream(ds);
            Assert.AreEqual(ds.name, datastream.name, false);
            Assert.AreNotEqual(null, datastream.id);
            Assert.AreEqual(ds.timeFormat, datastream.dataTransformation.timeFormat);
            Assert.AreEqual(ds.timeIdentifier, datastream.dataTransformation.timeIdentifier);
            Assert.AreEqual(ds.dataSource.type, datastream.dataSource.type);
            string data = "time, Unit, current, vibration, state\n 2016-05-05T12:00:00Z, Unit1, 12.4, 3.4, On";
              SortedDictionary<string, string> options = new SortedDictionary<string, string>();
              options.Add("timeIdentifier", "time");
              options.Add("timeFormat", "iso_8601");
              options.Add("fileFormat", "csv");
              InputStatus inputstatus = falkonry.addInput(datastream.id, data, options);
              falkonry.deleteDatastream(datastream.id);
          }

        [TestMethod()]
        public void addDataNarrowFormatCSV()
        {
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            DatastreamRequest ds = new DatastreamRequest();
            Timezone timezone = new Timezone();
            timezone.zone = "GMT";
            timezone.offset = 0;
            ds.timezone = timezone;
            ds.name = "TestDSPI" + random_number;
            ds.timeIdentifier = "time";
            ds.timeFormat = "iso_8601";
            ds.signalsTagField = "tag";
            ds.valueColumn = "value";
            ds.signalsDelimiter = "_";
            ds.signalsLocation = "suffix";
            Datasource datasource = new Datasource();
            datasource.type = "PI";
            datasource.host = "https://test.piserver.com/piwebapi";
            datasource.elementTemplateName = "SampleElementTempalte";
            ds.dataSource = datasource;

            Datastream datastream = falkonry.createDatastream(ds);
            Assert.AreEqual(ds.name, datastream.name, false);
            Assert.AreNotEqual(null, datastream.id);
            Assert.AreEqual(ds.timeFormat, datastream.dataTransformation.timeFormat);
            Assert.AreEqual(ds.timeIdentifier, datastream.dataTransformation.timeIdentifier);
            Assert.AreEqual(ds.dataSource.type, datastream.dataSource.type);
            string data = "time, tag, value \n" + "2016-05-05T12:00:00Z, Unit1_current, 12.4 \n 2016-03-01 01:01:01, Unit1_vibration, 20.4";
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            options.Add("fileFormat", "csv");
            InputStatus inputstatus = falkonry.addInput(datastream.id, data, options);
            datastream = falkonry.getDatastream(datastream.id);
            falkonry.deleteDatastream(datastream.id);
        }


    }

    //[TestClass]
    public class AddDataFromStream
    {

    Falkonry falkonry = new Falkonry("https://dev.falkonry.ai", "kvtsfp2z9qoggpndf8p5jhk7w0woi580");


    [TestMethod()]
        public void addDataFromStreamJSON()
        {
            Timezone timezone = new Timezone();
            timezone.zone = "GMT";
            timezone.offset = 0;
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            DatastreamRequest ds = new DatastreamRequest();
            Datasource datasource = new Datasource();
            datasource.type = "STANDALONE";
            ds.name = "TestDatastreamStreaming" + random_number;
            ds.timeIdentifier = "time";
            ds.timeFormat = "iso_8601";
            ds.entityIdentifier = "Unit";
            ds.dataSource = datasource;
            ds.timezone = timezone;
            Datastream datastream = falkonry.createDatastream(ds);
            Assert.AreEqual(ds.name, datastream.name, false);
            Assert.AreNotEqual(null, datastream.id);
            Assert.AreEqual(ds.timeFormat, datastream.dataTransformation.timeFormat);
            Assert.AreEqual(ds.timeIdentifier, datastream.dataTransformation.timeIdentifier);
            Assert.AreEqual(ds.dataSource.type, datastream.dataSource.type);
            string folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string path = folder + "/AddData.json";

            byte[] bytes = System.IO.File.ReadAllBytes(path);

            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");

            InputStatus inputstatus = falkonry.addInputStream(datastream.id, bytes, options);

            datastream = falkonry.getDatastream(datastream.id);
            falkonry.deleteDatastream(datastream.id);
        }
        [TestMethod()]
        public void addDataFromStreamCSV()
        {
            Timezone timezone = new Timezone();
            timezone.zone = "GMT";
            timezone.offset = 0;
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            DatastreamRequest ds = new DatastreamRequest();
            Datasource datasource = new Datasource();
            datasource.type = "STANDALONE";
            ds.name = "TestDatastreamStreaming" + random_number;
            ds.timeIdentifier = "time";
            ds.timeFormat = "iso_8601";
            ds.entityIdentifier = "Unit";
            ds.dataSource = datasource;
            ds.timezone = timezone;
            Datastream datastream = falkonry.createDatastream(ds);
            Assert.AreEqual(ds.name, datastream.name, false);
            Assert.AreNotEqual(null, datastream.id);
            Assert.AreEqual(ds.timeFormat, datastream.dataTransformation.timeFormat);
            Assert.AreEqual(ds.timeIdentifier, datastream.dataTransformation.timeIdentifier);
            Assert.AreEqual(ds.dataSource.type, datastream.dataSource.type);
            string folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string path = folder + "/AddData.csv";

            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Debug.WriteLine("IF READING FROM FILE WORKS");
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            InputStatus inputstatus = falkonry.addInputStream(datastream.id, bytes, options);

            datastream = falkonry.getDatastream(datastream.id);
            falkonry.deleteDatastream(datastream.id);
        }
    }

    //[TestClass()]
    public class AddHistorainData
    {

        Falkonry falkonry = new Falkonry("https://dev.falkonry.ai", "kvtsfp2z9qoggpndf8p5jhk7w0woi580");

        [TestMethod()]
        public void addDataNarrowFormatCSVForLearning()
        {
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            DatastreamRequest ds = new DatastreamRequest();
            Timezone timezone = new Timezone();
            timezone.zone = "GMT";
            timezone.offset = 0;
            ds.timezone = timezone;
            ds.name = "TestDSPILearning" + random_number;
            ds.timeIdentifier = "time";
            ds.timeFormat = "iso_8601";
            ds.signalsTagField = "tag";
            ds.valueColumn = "value";
            ds.signalsDelimiter = "_";
            ds.signalsLocation = "suffix";
            Datasource datasource = new Datasource();
            datasource.type = "PI";
            datasource.host = "https://test.piserver.com/piwebapi";
            datasource.elementTemplateName = "SampleElementTempalte";
            ds.dataSource = datasource;

            Datastream datastream = falkonry.createDatastream(ds);
            Assert.AreEqual(ds.name, datastream.name, false);
            Assert.AreNotEqual(null, datastream.id);
            Assert.AreEqual(ds.timeFormat, datastream.dataTransformation.timeFormat);
            Assert.AreEqual(ds.timeIdentifier, datastream.dataTransformation.timeIdentifier);
            Assert.AreEqual(ds.dataSource.type, datastream.dataSource.type);


            string data = "time, tag, value \n" + "2016-05-05T12:00:00Z, Unit1_current, 12.4 \n 2016-03-01 01:01:01, Unit1_vibration, 20.4";
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            options.Add("fileFormat", "csv");
            options.Add("streaming", "false");
            options.Add("hasMoreData", "false");

            InputStatus inputstatus = falkonry.addInput(datastream.id, data, options);
            datastream = falkonry.getDatastream(datastream.id);
            falkonry.deleteDatastream(datastream.id);
        }


    }

    //[TestClass()]
    public class AddStreamingData
    {

        Falkonry falkonry = new Falkonry("https://dev.falkonry.ai", "kvtsfp2z9qoggpndf8p5jhk7w0woi580");

        [TestMethod()]
        public void addDataNarrowFormatCSVForStreaming()
        {
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            DatastreamRequest ds = new DatastreamRequest();
            Timezone timezone = new Timezone();
            timezone.zone = "GMT";
            timezone.offset = 0;
            ds.timezone = timezone;
            ds.name = "TestDSPIStreaming" + random_number;
            ds.timeIdentifier = "time";
            ds.timeFormat = "iso_8601";
            ds.signalsTagField = "tag";
            ds.valueColumn = "value";
            ds.signalsDelimiter = "_";
            ds.signalsLocation = "suffix";
            Datasource datasource = new Datasource();
            datasource.type = "PI";
            datasource.host = "https://test.piserver.com/piwebapi";
            datasource.elementTemplateName = "SampleElementTempalte";
            ds.dataSource = datasource;

            Datastream datastream = falkonry.createDatastream(ds);
            Assert.AreEqual(ds.name, datastream.name, false);
            Assert.AreNotEqual(null, datastream.id);
            Assert.AreEqual(ds.timeFormat, datastream.dataTransformation.timeFormat);
            Assert.AreEqual(ds.timeIdentifier, datastream.dataTransformation.timeIdentifier);
            Assert.AreEqual(ds.dataSource.type, datastream.dataSource.type);
            string data = "time, tag, value \n" + "2016-05-05T12:00:00Z, Unit1_current, 12.4 \n 2016-03-01 01:01:01, Unit1_vibration, 20.4";
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            options.Add("timeIdentifier", "time");
            options.Add("timeFormat", "iso_8601");
            options.Add("fileFormat", "csv");
            options.Add("streaming", "true");
            options.Add("hasMoreData", "false");
            InputStatus inputstatus = falkonry.addInput(datastream.id, data, options);
            datastream = falkonry.getDatastream(datastream.id);
            falkonry.deleteDatastream(datastream.id);
        }


    }

    //[TestClass]
    public class AddFacts
    {
        Falkonry falkonry = new Falkonry("https://dev.falkonry.ai", "kvtsfp2z9qoggpndf8p5jhk7w0woi580");

        [TestMethod()]
        public void addFacts()
        {
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Timezone timezone = new Timezone();
            timezone.zone = "GMT";
            timezone.offset = 0;

            Datasource datasource = new Datasource();
            datasource.type = "PI";
            datasource.host = "https://test.piserver.com/piwebapi";
            datasource.elementTemplateName = "SampleElementTempalte";
            DatastreamRequest ds = new DatastreamRequest();
            ds.timeIdentifier = "time";
            ds.timeFormat = "iso_8601";
            ds.valueColumn = "value";
            ds.signalsTagField = "tag";
            ds.signalsLocation = "prefix";
            ds.signalsDelimiter = "_";
            ds.dataSource = datasource;
            ds.name = "TestDS" + random_number;
            ds.timezone = timezone;
            ds.dataSource = datasource;
            Datastream datastream = falkonry.createDatastream(ds);
            Assert.AreEqual(ds.name, datastream.name, false);
            Assert.AreNotEqual(null, datastream.id);
            Assert.AreEqual(ds.timeFormat, datastream.dataTransformation.timeFormat);
            Assert.AreEqual(ds.timeIdentifier, datastream.dataTransformation.timeIdentifier);
            Assert.AreEqual(ds.dataSource.type, datastream.dataSource.type);

            datastream = falkonry.getDatastream(datastream.id);

            // add assessment
            AssessmentRequest asmt = new AssessmentRequest();
            string random_number1 = System.Convert.ToString(rnd.Next(1, 10000));
            asmt.name = "TestAssessment"+ random_number1;
            asmt.datastream = datastream.id;
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            Assessment assessment = falkonry.createAssessment(asmt);

            string data1 = "time,end,"+ datastream.dataTransformation.entityIdentifier
          + ",Health\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,Unit1,Normal\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,Unit1,Normal";
            string response = falkonry.addFacts(assessment.id, data1, options);

            falkonry.deleteDatastream(datastream.id);
        }

    }

    //[TestClass]
    public class AddEntityMeta
    {
        Falkonry falkonry = new Falkonry("https://dev.falkonry.ai", "uunov5we8ef8zh19ipt5xvp5y9ccw8x3");

        [TestMethod()]
        public void addEntityMeta()
        {
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Timezone timezone = new Timezone();
            timezone.zone = "GMT";
            timezone.offset = 0;

            Datasource datasource = new Datasource();
            datasource.type = "PI";
            datasource.host = "https://test.piserver.com/piwebapi";
            datasource.elementTemplateName = "SampleElementTempalte";
            DatastreamRequest ds = new DatastreamRequest();
            ds.timeIdentifier = "time";
            ds.timeFormat = "iso_8601";
            ds.valueColumn = "value";
            ds.signalsTagField = "tag";
            ds.signalsLocation = "prefix";
            ds.signalsDelimiter = "_";
            ds.dataSource = datasource;
            ds.name = "TestDS" + random_number;
            ds.timezone = timezone;
            ds.dataSource = datasource;
            Datastream datastream = falkonry.createDatastream(ds);
            Assert.AreEqual(ds.name, datastream.name, false);
            Assert.AreNotEqual(null, datastream.id);
            Assert.AreEqual(ds.timeFormat, datastream.dataTransformation.timeFormat);
            Assert.AreEqual(ds.timeIdentifier, datastream.dataTransformation.timeIdentifier);
            Assert.AreEqual(ds.dataSource.type, datastream.dataSource.type);

            datastream = falkonry.getDatastream(datastream.id);
            List<EntityMetaRequest> entityMetaRequestList = new List<EntityMetaRequest>();
            EntityMetaRequest entityMetaRequest1 = new EntityMetaRequest();
            entityMetaRequest1.label = "User readbale label";
            entityMetaRequest1.sourceId = "1234-21342134";
            entityMetaRequest1.path = "//root/branch1/";

            EntityMetaRequest entityMetaRequest2 = new EntityMetaRequest();
            entityMetaRequest2.label = "User readbale label2";
            entityMetaRequest2.sourceId = "1234-213421rawef";
            entityMetaRequest2.path = "//root/branch2/";

            entityMetaRequestList.Add(entityMetaRequest1);
            entityMetaRequestList.Add(entityMetaRequest2);

            List<EntityMeta> entityMetaResponseList = falkonry.postEntityMeta(entityMetaRequestList, datastream);
            Assert.AreEqual(2,entityMetaResponseList.Count);

            // Get entitymeta
            entityMetaResponseList = falkonry.getEntityMeta(datastream);
            Assert.AreEqual(2, entityMetaResponseList.Count);
            falkonry.deleteDatastream(datastream.id);
        }

    }

   //[TestClass]
    public class fetchHistoricalOutput
    {
        Falkonry falkonry = new Falkonry("https://dev.falkonry.ai", "uunov5we8ef8zh19ipt5xvp5y9ccw8x3");

        [TestMethod()]
        public void TestHistoricalOutput()
        {
            JavaScriptSerializer javascript = new JavaScriptSerializer();
            System.Random rnd = new System.Random();
            string random_number = System.Convert.ToString(rnd.Next(1, 10000));
            Timezone timezone = new Timezone();
            timezone.zone = "GMT";
            timezone.offset = 0;

            Datasource datasource = new Datasource();
            datasource.type = "PI";
            datasource.host = "https://test.piserver.com/piwebapi";
            datasource.elementTemplateName = "SampleElementTempalte";
            DatastreamRequest ds = new DatastreamRequest();
            ds.timeIdentifier = "time";
            ds.timeFormat = "iso_8601";
            ds.valueColumn = "value";
            ds.signalsTagField = "tag";
            ds.signalsLocation = "prefix";
            ds.signalsDelimiter = "_";
            ds.dataSource = datasource;
            ds.name = "TestDS" + random_number;
            ds.timezone = timezone;
            ds.dataSource = datasource;
            Datastream datastream = falkonry.createDatastream(ds);
            Assert.AreEqual(ds.name, datastream.name, false);
            Assert.AreNotEqual(null, datastream.id);
            Assert.AreEqual(ds.timeFormat, datastream.dataTransformation.timeFormat);
            Assert.AreEqual(ds.timeIdentifier, datastream.dataTransformation.timeIdentifier);
            Assert.AreEqual(ds.dataSource.type, datastream.dataSource.type);

            // Create Assessment
            AssessmentRequest asmt = new AssessmentRequest();
            string random_number1 = System.Convert.ToString(rnd.Next(1, 10000));
            asmt.name = "TestAssessment" + random_number1;
            asmt.datastream = datastream.id;
            Assessment assessment = falkonry.createAssessment(asmt);

            //assessment.id = "lqv606xtcxnlca";
            // Got TO Falkonry UI and run a model revision

            // Fetch Historical output data for given assessment, startTime , endtime
            SortedDictionary<string, string> options = new SortedDictionary<string, string>();
            options.Add("startTime", "2011-04-04T01:00:00.000Z"); // in the format YYYY-MM-DDTHH:mm:ss.SSSZ
            options.Add("endTime", "2011-05-05T01:00:00.000Z");  // in the format YYYY-MM-DDTHH:mm:ss.SSSZ
            options.Add("responseFormat", "application/json");  // also avaibale options 1. text/csv 2. application/json

            HttpResponse httpResponse = falkonry.getHistoricalOutput(assessment, options);
            // If data is not readily avaiable then, a tracker id will be sent with 202 status code. While falkonry will genrate ouptut data
            // Client should do timely pooling on the using same method, sending tracker id (__id) in the query params
            // Once data is avaiable server will response with 200 status code and data in json/csv format.

            if (httpResponse.statusCode == 202)
            {
                Tracker trackerResponse = javascript.Deserialize<Tracker>(httpResponse.response);
                // get id from the tracker
                string __id = trackerResponse.__id;
                //string __id = "phzpfmvwsgiy7ojc";


                // use this tracker for checking the status of the process.
                options = new SortedDictionary<string, string>();
                options.Add("trackerId", __id);
                options.Add("responseFormat", "application/json");

                httpResponse = falkonry.getHistoricalOutput(assessment, options);

                // if status is 202 call the same request again

                // if statsu is 200, output data will be present in httpResponse.response field
            }
            if (httpResponse.statusCode > 400)
            {
                // Some Error has occured. Please httpResponse.response for detail message
            }
        }
    }

}
    
