using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using falkonry_csharp_client.helper.models;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.IO;
/*INSTRUCTIONS: TO RUN ANY TESTS, SIMPLY UNCOMMENT THE '//[TestCLASS()] ' header before every class of tests to run that particular class of tests. 
* You should try executing method by method in case classwise tests take too long or fail */

/* Also insert your url and your token in the: 
 * Falkonry falkonry = new Falkonry("http://localhost:8080", "");
 *  fields*/

namespace falkonry_csharp_client.Tests
{
    // [TestClass()]
    public class FalkonryTestsDatastream
    {
        Falkonry _falkonry = new Falkonry("https://localhost:8080", "9qhoa1se6qzhrs1556kegrnh1vzc6aj2");
        List<Datastream> _datastreams = new List<Datastream>();

        // Create StandAlone Datrastream with Wide format
        [TestMethod()]
        public void CreateStandaloneDatastream()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDatastream" + randomNumber;
            var Field = new Field();
            Field.Time = time;

            ds.Field = Field;
            ds.DataSource = datasource;
            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
                Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
                Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false); ;
            }
        }


        //Cretate datastream without name
        [TestMethod()]
        public void CreateDatastreamWithoutName()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            //ds.Name = "TestDatastream" + randomNumber;
            var Field = new Field();
            Field.Time = time;

            ds.Field = Field;
            ds.DataSource = datasource;
            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, "Missing name.", false);
            }
            //_falkonry.DeleteDatastream(datastream.Id);
        }


        //Cretate datastream without time identifier
        [TestMethod()]
        public void CreateDatastreamWithoutTimeIdentifier()
        {
            var time = new Time();
            time.Zone = "GMT";
            //time.Identifier = "time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDatastream" + randomNumber;
            var Field = new Field();
            Field.Time = time;

            ds.Field = Field;
            ds.DataSource = datasource;
            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, "Missing time identifier.", false);
            }

            //_falkonry.DeleteDatastream(datastream.Id);
        }

        //Cretate datastream without time format
        [TestMethod()]
        public void CreateDatastreamWithoutTimeFormat()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            //time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDatastream" + randomNumber;
            var Field = new Field();
            Field.Time = time;

            ds.Field = Field;
            ds.DataSource = datasource;
            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, "Missing time format.", false);
            }
        }


        // Create Standalone datastream with entityIdentifier
        [TestMethod()]
        public void CreateDatastreamWithEntityIdentifierTest()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";

            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            ds.Name = "TestDatastream" + randomNumber;
            var Field = new Field();

            Field.EntityIdentifier = "Unit";
            ds.Field = Field;
            ds.DataSource = datasource;
            ds.Field.Time = time;
            var datastream = _falkonry.CreateDatastream(ds);
            Assert.AreEqual(ds.Name, datastream.Name, false);
            Assert.AreNotEqual(null, datastream.Id);
            Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
            Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
            Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
            _falkonry.DeleteDatastream(datastream.Id);
        }

        // Create PI Datastream (Narrow Format)
        [TestMethod()]
        public void CreatePiDatastreamTest()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "PI";
            datasource.Host = "https://test.piserver.com/piwebapi";
            datasource.ElementTemplateName = "SampleElementTempalte";
            var ds = new DatastreamRequest();
            var Field = new Field();
            var Signal = new Signal();
            Signal.ValueIdentifier = "value";
            Signal.TagIdentifier = "tag";
            Signal.IsSignalPrefix = true;
            Signal.Delimiter = "_";
            Field.Signal = Signal;
            Field.Time = time;
            ds.Field = Field;
            ds.DataSource = datasource;
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            ds.Name = "TestDS" + randomNumber;
            ds.Field.Time = time;
            ds.DataSource = datasource;
            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
                Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
                Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, null, false);
            }

        }

    }

    // [TestClass()]
    public class AddData
    {

        Falkonry _falkonry = new Falkonry("https://localhost:8080", "9qhoa1se6qzhrs1556kegrnh1vzc6aj1");

        //[TestMethod()]
        public void AddDataJson()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDSJSON" + randomNumber;
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";

            var Field = new Field();
            Field.Time = time;
            Field.EntityIdentifier = "Unit";
            ds.Field = Field;
            var datasource = new Datasource();
            datasource.Type = "PI";
            datasource.Host = "https://test.piserver.com/piwebapi";
            datasource.ElementTemplateName = "SampleElementTempalte";
            ds.DataSource = datasource;

            // Input List
            var inputList = new List<Input>();
            var currents = new Input();
            currents.Name = "current";
            currents.ValueType = new ValueType();
            currents.EventType = new EventType();
            currents.ValueType.Type = "Numeric";
            currents.EventType.Type = "Samples";
            inputList.Add(currents);

            var vibration = new Input();
            vibration.Name = "vibration";
            vibration.ValueType = new ValueType();
            vibration.EventType = new EventType();
            vibration.ValueType.Type = "Numeric";
            vibration.EventType.Type = "Samples";
            inputList.Add(vibration);

            var state = new Input();
            state.Name = "state";
            state.ValueType = new ValueType();
            state.EventType = new EventType();
            state.ValueType.Type = "Categorical";
            state.EventType.Type = "Samples";
            inputList.Add(state);

            ds.InputList = inputList;

            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
                Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
                Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
                var data = "{\"time\" :\"2016-03-01 01:01:01\",\"Unit\":\"Unit1\", \"current\" : 12.4, \"vibration\" : 3.4, \"state\" : \"On\"}";
                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                options.Add("fileFormat", "json");
                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                datastream = _falkonry.GetDatastream(datastream.Id);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }

        }

        [TestMethod()]
        public void AddDataCsv()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";
            ds.Name = "TestDSCSV" + randomNumber;

            var Field = new Field();
            Field.EntityIdentifier = "Unit";
            Field.Time = time;
            ds.Field = Field;
            var datasource = new Datasource();
            datasource.Type = "PI";
            datasource.Host = "https://test.piserver.com/piwebapi";
            datasource.ElementTemplateName = "SampleElementTempalte";
            ds.DataSource = datasource;

            // Input List
            var inputList = new List<Input>();
            var currents = new Input();
            currents.Name = "current";
            currents.ValueType = new ValueType();
            currents.EventType = new EventType();
            currents.ValueType.Type = "Numeric";
            currents.EventType.Type = "Samples";
            inputList.Add(currents);

            var vibration = new Input();
            vibration.Name = "vibration";
            vibration.ValueType = new ValueType();
            vibration.EventType = new EventType();
            vibration.ValueType.Type = "Numeric";
            vibration.EventType.Type = "Samples";
            inputList.Add(vibration);

            var state = new Input();
            state.Name = "state";
            state.ValueType = new ValueType();
            state.EventType = new EventType();
            state.ValueType.Type = "Categorical";
            state.EventType.Type = "Samples";
            inputList.Add(state);

            ds.InputList = inputList;

            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
                Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
                Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
                var data = "time, Unit, current, vibration, state\n 2016-05-05T12:00:00Z, Unit1, 12.4, 3.4, On";
                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                options.Add("fileFormat", "csv");
                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, null, false);
            }

        }

        [TestMethod()]
        public void AddDataNarrowFormatCsv()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";

            ds.Name = "TestDSPI" + randomNumber;

            var Field = new Field();
            var Signal = new Signal();
            Signal.TagIdentifier = "tag";
            Signal.ValueIdentifier = "value";
            Signal.Delimiter = "_";
            Signal.IsSignalPrefix = false;
            Field.Signal = Signal;
            Field.Time = time;
            ds.Field = Field;
            var datasource = new Datasource();
            datasource.Type = "PI";
            datasource.Host = "https://test.piserver.com/piwebapi";
            datasource.ElementTemplateName = "SampleElementTempalte";
            ds.DataSource = datasource;

            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
                Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
                Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
                var data = "time, tag, value \n" + "2016-05-05T12:00:00Z, Unit1_current, 12.4 \n 2016-03-01 01:01:01, Unit1_vibration, 20.4";
                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                options.Add("fileFormat", "csv");
                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                datastream = _falkonry.GetDatastream(datastream.Id);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, null, false);
            }

        }


    }

    // [TestClass]
    public class AddDataFromStream
    {

        Falkonry _falkonry = new Falkonry("https://localhost:8080", "9qhoa1se6qzhrs1556kegrnh1vzc6aj1");


        [TestMethod()]
        public void AddDataFromStreamJson()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            ds.Name = "TestDatastreamStreaming" + randomNumber;
            var Field = new Field();
            Field.EntityIdentifier = "Unit";

            Field.Time = time;
            ds.Field = Field;
            ds.DataSource = datasource;

            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
                Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
                Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
                var folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                var path = folder + "/AddData.json";

                var bytes = File.ReadAllBytes(path);

                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");

                var inputstatus = _falkonry.AddInputStream(datastream.Id, bytes, options);

                datastream = _falkonry.GetDatastream(datastream.Id);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false); ;
            }

        }
        [TestMethod()]
        public void AddDataFromStreamCsv()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            ds.Name = "TestDatastreamStreaming" + randomNumber;
            var Field = new Field();

            Field.EntityIdentifier = "Unit";
            Field.Time = time;
            ds.Field = Field;
            ds.DataSource = datasource;

            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
                Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
                Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
                var folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                var path = folder + "/AddData.csv";

                var bytes = File.ReadAllBytes(path);
                Debug.WriteLine("IF READING FROM FILE WORKS");
                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                var inputstatus = _falkonry.AddInputStream(datastream.Id, bytes, options);

                datastream = _falkonry.GetDatastream(datastream.Id);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }

        }
    }

    // [TestClass()]
    public class AddHistorainData
    {

        Falkonry _falkonry = new Falkonry("https://localhost:8080", "9qhoa1se6qzhrs1556kegrnh1vzc6aj1");

        [TestMethod()]
        public void AddDataNarrowFormatCsvForLearning()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";
            ds.Name = "TestDSPILearning" + randomNumber;

            var Field = new Field();
            var Signal = new Signal();
            Signal.TagIdentifier = "tag";
            Signal.ValueIdentifier = "value";
            Signal.Delimiter = "_";
            Signal.IsSignalPrefix = false;
            Field.Signal = Signal;
            Field.Time = time;
            ds.Field = Field;
            var datasource = new Datasource();
            datasource.Type = "PI";
            datasource.Host = "https://test.piserver.com/piwebapi";
            datasource.ElementTemplateName = "SampleElementTempalte";
            ds.DataSource = datasource;

            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
                Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
                Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);


                var data = "time, tag, value \n" + "2016-05-05T12:00:00Z, Unit1_current, 12.4 \n 2016-03-01 01:01:01, Unit1_vibration, 20.4";
                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                options.Add("fileFormat", "csv");
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");

                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                datastream = _falkonry.GetDatastream(datastream.Id);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }

        }


    }

    // [TestClass()]
    public class AddStreamingData
    {

        Falkonry _falkonry = new Falkonry("https://localhost:8080", "9qhoa1se6qzhrs1556kegrnh1vzc6aj1");

        [TestMethod()]
        public void AddDataNarrowFormatCsvForStreaming()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";
            ds.Name = "TestDSPIStreaming" + randomNumber;

            var Field = new Field();
            var Signal = new Signal();
            Signal.TagIdentifier = "tag";
            Signal.ValueIdentifier = "value";
            Signal.Delimiter = "_";
            Signal.IsSignalPrefix = false;
            Field.Signal = Signal;
            Field.Time = time;
            ds.Field = Field;
            var datasource = new Datasource();
            datasource.Type = "PI";
            datasource.Host = "https://test.piserver.com/piwebapi";
            datasource.ElementTemplateName = "SampleElementTempalte";
            ds.DataSource = datasource;

            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
                Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
                Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
                var data = "time, tag, value \n" + "2016-05-05T12:00:00Z, Unit1_current, 12.4 \n 2016-03-01 01:01:01, Unit1_vibration, 20.4";
                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                options.Add("fileFormat", "csv");
                options.Add("streaming", "true");
                options.Add("hasMoreData", "false");
                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                datastream = _falkonry.GetDatastream(datastream.Id);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }

        }


    }

    // [TestClass]
    public class AddFacts
    {
        Falkonry _falkonry = new Falkonry("https://localhost:8080", "9qhoa1se6qzhrs1556kegrnh1vzc6aj1");

        [TestMethod()]
        public void addFacts()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";


            var datasource = new Datasource();
            datasource.Type = "PI";
            datasource.Host = "https://test.piserver.com/piwebapi";
            datasource.ElementTemplateName = "SampleElementTempalte";
            var ds = new DatastreamRequest();

            var Field = new Field();
            var Signal = new Signal();
            Signal.ValueIdentifier = "value";
            Signal.TagIdentifier = "tag";
            Signal.IsSignalPrefix = true;
            Signal.Delimiter = "_";
            Field.Signal = Signal;
            Field.Time = time;
            ds.Field = Field;
            ds.DataSource = datasource;
            ds.Name = "TestDS" + randomNumber;
            ds.DataSource = datasource;
            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
                Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
                Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);

                datastream = _falkonry.GetDatastream(datastream.Id);

                // add assessment
                var asmt = new AssessmentRequest();
                var randomNumber1 = System.Convert.ToString(rnd.Next(1, 10000));
                asmt.Name = "TestAssessment" + randomNumber1;
                asmt.Datastream = datastream.Id;
                var options = new SortedDictionary<string, string>();
                var assessment = _falkonry.CreateAssessment(asmt);

                var data1 = "time,end," + datastream.Field.EntityIdentifier
              + ",Health\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,Unit1,Normal\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,Unit1,Normal";
                var response = _falkonry.AddFacts(assessment.Id, data1, options);

                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }
        }

    }

    // [TestClass]
    public class AddEntityMeta
    {
        Falkonry _falkonry = new Falkonry("https://localhost:8080", "9qhoa1se6qzhrs1556kegrnh1vzc6aj1");

        [TestMethod()]
        public void addEntityMeta()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601"; ;


            var datasource = new Datasource();
            datasource.Type = "PI";
            datasource.Host = "https://test.piserver.com/piwebapi";
            datasource.ElementTemplateName = "SampleElementTempalte";
            var ds = new DatastreamRequest();
            var Field = new Field();
            var Signal = new Signal();
            Signal.ValueIdentifier = "value";
            Signal.TagIdentifier = "tag";
            Signal.IsSignalPrefix = true;
            Signal.Delimiter = "_";
            Field.Signal = Signal;
            Field.Time = time;
            ds.Field = Field;
            ds.DataSource = datasource;
            ds.Name = "TestDS" + randomNumber;

            ds.DataSource = datasource;
            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
                Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
                Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);

                datastream = _falkonry.GetDatastream(datastream.Id);
                var entityMetaRequestList = new List<EntityMetaRequest>();
                var entityMetaRequest1 = new EntityMetaRequest();
                entityMetaRequest1.Label = "User readbale label";
                entityMetaRequest1.SourceId = "1234-21342134";
                entityMetaRequest1.Path = "//root/branch1/";

                var entityMetaRequest2 = new EntityMetaRequest();
                entityMetaRequest2.Label = "User readbale label2";
                entityMetaRequest2.SourceId = "1234-213421rawef";
                entityMetaRequest2.Path = "//root/branch2/";

                entityMetaRequestList.Add(entityMetaRequest1);
                entityMetaRequestList.Add(entityMetaRequest2);

                var entityMetaResponseList = _falkonry.PostEntityMeta(entityMetaRequestList, datastream);
                Assert.AreEqual(2, entityMetaResponseList.Count);

                // Get entitymeta
                entityMetaResponseList = _falkonry.GetEntityMeta(datastream);
                Assert.AreEqual(2, entityMetaResponseList.Count);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }
        }

    }

    // [TestClass]
    public class FetchHistoricalOutput
    {
        Falkonry _falkonry = new Falkonry("https://localhost:8080", "9qhoa1se6qzhrs1556kegrnh1vzc6aj1");

        [TestMethod()]
        public void TestHistoricalOutput()
        {
            var javascript = new JavaScriptSerializer();
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";

            var Field = new Field();
            var Signal = new Signal();
            var datasource = new Datasource();
            datasource.Type = "PI";
            datasource.Host = "https://test.piserver.com/piwebapi";
            datasource.ElementTemplateName = "SampleElementTempalte";
            var ds = new DatastreamRequest();

            Signal.ValueIdentifier = "value";
            Signal.TagIdentifier = "tag";
            Signal.IsSignalPrefix = true;
            Signal.Delimiter = "_";

            Field.Signal = Signal;
            Field.Time = time;
            ds.Field = Field;
            ds.DataSource = datasource;
            ds.Name = "TestDS" + randomNumber;
            ds.Field.Time = time;
            ds.DataSource = datasource;
            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
                Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
                Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);

                // Create Assessment
                var asmt = new AssessmentRequest();
                var randomNumber1 = System.Convert.ToString(rnd.Next(1, 10000));
                asmt.Name = "TestAssessment" + randomNumber1;
                asmt.Datastream = datastream.Id;
                asmt.Rate = "PT0S";
                var assessment = _falkonry.CreateAssessment(asmt);

                //assessment.id = "lqv606xtcxnlca";
                // Got TO Falkonry UI and run a model revision

                // Fetch Historical output data for given assessment, startTime , endtime
                var options = new SortedDictionary<string, string>();
                options.Add("startTime", "2011-04-04T01:00:00.000Z"); // in the format YYYY-MM-DDTHH:mm:ss.SSSZ
                options.Add("endTime", "2011-05-05T01:00:00.000Z");  // in the format YYYY-MM-DDTHH:mm:ss.SSSZ
                options.Add("responseFormat", "application/json");  // also avaibale options 1. text/csv 2. application/json

                var httpResponse = _falkonry.GetHistoricalOutput(assessment, options);
                // If data is not readily avaiable then, a tracker id will be sent with 202 status code. While falkonry will genrate ouptut data
                // Client should do timely pooling on the using same method, sending tracker id (__id) in the query params
                // Once data is avaiable server will response with 200 status code and data in json/csv format.

                if (httpResponse.StatusCode == 202)
                {
                    var trackerResponse = javascript.Deserialize<Tracker>(httpResponse.Response);
                    // get id from the tracker
                    var id = trackerResponse.__Id;
                    //string __id = "phzpfmvwsgiy7ojc";


                    // use this tracker for checking the status of the process.
                    options = new SortedDictionary<string, string>();
                    options.Add("trackerId", id);
                    options.Add("responseFormat", "application/json");

                    httpResponse = _falkonry.GetHistoricalOutput(assessment, options);

                    // if status is 202 call the same request again

                    // if statsu is 200, output data will be present in httpResponse.response field
                }
                if (httpResponse.StatusCode > 400)
                {
                    // Some Error has occured. Please httpResponse.response for detail message
                }
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }
        }
    }



    // [TestClass]
    public class DatastreamLiveMonitoring
    {
        Falkonry _falkonry = new Falkonry("https://localhost:8080", "9qhoa1se6qzhrs1556kegrnh1vzc6aj1");

        //[TestMethod()]
        public void DatastreamLiveMonitoringOn()
        {
            var javascript = new JavaScriptSerializer();
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";

            var Field = new Field();
            var Signal = new Signal();
            var datasource = new Datasource();
            datasource.Type = "PI";
            datasource.Host = "https://test.piserver.com/piwebapi";
            datasource.ElementTemplateName = "SampleElementTempalte";
            var ds = new DatastreamRequest();

            Signal.ValueIdentifier = "value";
            Signal.TagIdentifier = "tag";
            Signal.IsSignalPrefix = true;
            Signal.Delimiter = "_";

            Field.Signal = Signal;
            Field.Time = time;
            ds.Field = Field;
            ds.DataSource = datasource;
            ds.Name = "TestDS" + randomNumber;
            ds.Field.Time = time;
            ds.DataSource = datasource;
            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
                Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
                Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);

                // Create Assessment
                var asmt = new AssessmentRequest();
                var randomNumber1 = System.Convert.ToString(rnd.Next(1, 10000));
                asmt.Name = "TestAssessment" + randomNumber1;
                asmt.Datastream = datastream.Id;
                asmt.Rate = "PT0S";
                var assessment = _falkonry.CreateAssessment(asmt);

                //assessment.id = "lqv606xtcxnlca";
                // Got TO Falkonry UI and run a model revision
                try
                {
                    _falkonry.onDatastream(datastream.Id);
                }
                catch (System.Exception exception)
                {
                    //Some error  in turning datastream live monitoring on
                    Assert.AreEqual(exception.Message, null, false);
                }
                Assert.AreNotEqual(null, datastream.Id);

                try
                {
                    _falkonry.offDatastream(datastream.Id);
                }
                catch (System.Exception exception)
                {
                    //Some error  in turning datastream live monitoring off
                    Assert.AreEqual(exception.Message, null, false);
                }
                Assert.AreNotEqual(null, datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }
        }

    }
}

