using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using falkonry_csharp_client.helper.models;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.IO;
using falkonry_csharp_client;
/*INSTRUCTIONS: TO RUN ANY TESTS, SIMPLY UNCOMMENT THE '//[TestClass()] ' header before every class of tests to run that particular class of tests. 
* You should try executing method by method in case class wise tests take too long or fail */

/* Also insert your URL and your token in the: 
 * Falkonry falkonry = new Falkonry("https://localhost:8080", "");
 *  fields*/

namespace falkonry_csharp_client.Tests
{
    [TestClass()]
    public class TestsDatastream
    {

        //Falkonry _falkonry = new Falkonry("https://localhost:8080", "auth-token");
        Falkonry _falkonry = new Falkonry("https://192.168.2.243:8080", "npp766l2hghmhrc7ygrbldjnkb9rn7mg");
        List<Datastream> _datastreams = new List<Datastream>();

        // Create StandAlone Datastream with Wide format
        [TestMethod()]
        public void CreateStandaloneDatastream()
        {
            var time = new Time();
            time.Zone = "Asia/Kolkata";
            time.Identifier = "time";

            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDS" + randomNumber;
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

        // Create Datastream for narrow/historian style data from a single entity
        [TestMethod()]
        public void createDatastreamNarrowFormatSingleEntity()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "PI";
            var ds = new DatastreamRequest();
            var Field = new Field();
            var Signal = new Signal();
            Signal.ValueIdentifier = "value";
            Signal.SignalIdentifier = "signal";
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
                Assert.AreEqual(ds.Field.Signal.ValueIdentifier, datastream.Field.Signal.ValueIdentifier);
                Assert.AreEqual(ds.Field.Signal.SignalIdentifier, datastream.Field.Signal.SignalIdentifier);
                Assert.AreEqual(datastream.Field.EntityName, datastream.Name);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false); ;
            }
        }

        // Create Datastream for narrow/historian style data from a Multi entity
        [TestMethod()]
        public void createDatastreamNarrowFormatMultipleEntity()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "PI";
            var ds = new DatastreamRequest();
            var Field = new Field();
            var Signal = new Signal();
            Signal.ValueIdentifier = "value";
            Signal.SignalIdentifier = "signal";
            Field.Signal = Signal;
            Field.Time = time;
            Field.EntityIdentifier = "entity";
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
                Assert.AreEqual(ds.Field.Signal.ValueIdentifier, datastream.Field.Signal.ValueIdentifier);
                Assert.AreEqual(ds.Field.Signal.SignalIdentifier, datastream.Field.Signal.SignalIdentifier);
                Assert.AreEqual(datastream.Field.EntityIdentifier, "entity");
                Assert.AreEqual(datastream.Field.EntityName, null);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false); ;
            }
        }

        // Create Datastream for wide style data from a single entity
        //[TestMethod()]
        public void createDatastreamWideFormatSingleEntity()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "PI";
            var ds = new DatastreamRequest();
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
            var Field = new Field();
            var Signal = new Signal();
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
                Assert.AreEqual(datastream.Field.EntityIdentifier, null);
                Assert.AreEqual(datastream.Field.EntityName, datastream.Name);
                Assert.AreEqual(datastream.InputList.Count, 3);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false); ;
            }
        }

        // Create Datastream for wide style data from a multiple entities
        [TestMethod()]
        public void createDatastreamWideFormatMultipleEntities()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "PI";
            var ds = new DatastreamRequest();
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
            var Field = new Field();
            var Signal = new Signal();
            Field.Signal = Signal;
            Field.Time = time;
            Field.EntityIdentifier = "car";
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
                Assert.AreEqual(ds.Field.EntityIdentifier, datastream.Field.EntityIdentifier);
                Assert.AreEqual(datastream.Field.EntityName, null);
                Assert.AreEqual(datastream.InputList.Count, 3);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false); ;
            }
        }

        //Can not create datastream without name
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
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, "Missing name.", false);
            }
        }

        //Can not create datastream without time identifier
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
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, "Missing time identifier.", false);
            }


        }

        //Can not create datastream without time format
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

        //Create Standalone datastream with entityIdentifier
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
            Assert.AreEqual(ds.Field.EntityIdentifier, datastream.Field.EntityIdentifier);
            Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);
            _falkonry.DeleteDatastream(datastream.Id);
        }

        //Create Standalone datastream with batchIdentifier
        [TestMethod()]
        public void CreateDatastreamWithBatchIdentifierTest()
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

            Field.BatchIdentifier = "Batch";
            ds.Field = Field;
            ds.DataSource = datasource;
            ds.Field.Time = time;
            var datastream = _falkonry.CreateDatastream(ds);
            Assert.AreEqual(ds.Name, datastream.Name, false);
            Assert.AreNotEqual(null, datastream.Id);
            Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
            Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
            Assert.AreEqual(ds.Field.BatchIdentifier, datastream.Field.BatchIdentifier);
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
            Signal.SignalIdentifier = "signal";
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

        [TestMethod()]
        // Retrieve Datastreams
        public void GetDatastreamsTest()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "Time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDS" + randomNumber;
            var Field = new Field();
            Field.Time = time;

            ds.Field = Field;
            ds.DataSource = datasource;
            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);

                // get datastreams list
                List<Datastream> datastreamList = _falkonry.GetDatastreams();
                Assert.AreEqual(datastreamList.Count > 0, true);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, null, false); ;
            }
        }

        // Retrieve Datastreams by id
        [TestMethod()]
        public void GetDatastreamByIdTest()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "Time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDS" + randomNumber;
            var Field = new Field();
            Field.Time = time;

            ds.Field = Field;
            ds.DataSource = datasource;
            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);

                // get datastream by id
                Datastream datastreamFetched = _falkonry.GetDatastream(datastream.Id);
                Assert.AreEqual(ds.Name, datastreamFetched.Name, false);
                Assert.AreNotEqual(null, datastreamFetched.Id);
                _falkonry.DeleteDatastream(datastreamFetched.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, null, false); ;
            }
        }

        // Delete Datastream by id
        [TestMethod()]
        public void DeleteDatastreamByIdTest()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "Time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDS" + randomNumber;
            var Field = new Field();
            Field.Time = time;

            ds.Field = Field;
            ds.DataSource = datasource;
            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, null, false); ;
            }
        }

        // Create StandAlone Datastream with microsecond precision
        [TestMethod()]
        public void CreateMicrosecondsDatastream()
        {
            var time = new Time();
            time.Zone = "Asia/Kolkata";
            time.Identifier = "time";

            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDS" + randomNumber;
            ds.TimePrecision = "micro";
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
                Assert.AreEqual(ds.TimePrecision, datastream.TimePrecision);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false); ;
            }
        }

    }

    [TestClass()]
    public class TestAddEntityMeta
    {
        Falkonry _falkonry = new Falkonry("https://192.168.2.243:8080", "npp766l2hghmhrc7ygrbldjnkb9rn7mg");

        [TestMethod()]
        // Add EntityMeta to a Datastream, Get EntityMeta of a Datastream
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
            Signal.SignalIdentifier = "signal";
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
                entityMetaRequest1.Label = "User readable label";
                entityMetaRequest1.SourceId = "1234-21342134";
                entityMetaRequest1.Path = "//root/branch1/";

                var entityMetaRequest2 = new EntityMetaRequest();
                entityMetaRequest2.Label = "User readable label2";
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

    [TestClass()]
    public class TestAssessment
    {
        Falkonry _falkonry = new Falkonry("https://192.168.2.243:8080", "npp766l2hghmhrc7ygrbldjnkb9rn7mg");

        [TestMethod()]
        // Test Add assessment
        public void TestAssessmentCRUD()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "Time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDS" + randomNumber;
            var Field = new Field();
            Field.Time = time;

            ds.Field = Field;
            ds.DataSource = datasource;
            try
            {
                var datastream = _falkonry.CreateDatastream(ds);
                Assert.AreEqual(ds.Name, datastream.Name, false);
                Assert.AreNotEqual(null, datastream.Id);
                // create assessment
                AssessmentRequest asmtRequest = new AssessmentRequest();
                asmtRequest.Name = "TestAsmt";
                asmtRequest.Datastream = datastream.Id;

                var assessmentCreated = _falkonry.CreateAssessment(asmtRequest);
                Assert.AreEqual(assessmentCreated.Name, asmtRequest.Name);
                Assert.AreNotEqual(null, assessmentCreated.Id);

                // get Assessment List
                List<Assessment> assessmnetList = _falkonry.GetAssessments();
                Assert.AreEqual(assessmnetList.Count > 0, true);

                // get assessment by id
                Assessment fetchedassessment = _falkonry.GetAssessment("assessment-id");
                Assert.AreEqual(assessmentCreated.Name, asmtRequest.Name);
                Assert.AreNotEqual(null, fetchedassessment.Id);

                // check for aprioricondition list
                Assert.AreEqual(fetchedassessment.AprioriConditionList.Length == 0, true);

                // Delete assessment by id
                _falkonry.DeleteAssessment(fetchedassessment.Id);

                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false); ;
            }
        }
    }

    [TestClass()]
    public class TestAddHistoricalData
    {
        //Falkonry _falkonry = new Falkonry("https://localhost:8080", "auth-token");
        Falkonry _falkonry = new Falkonry("https://192.168.2.243:8080", "npp766l2hghmhrc7ygrbldjnkb9rn7mg");

        // Add historical narrow input data (json format) to multi entity Datastreamm(Used for model revision)
        [TestMethod()]
        public void AddDataNarrowFormatJson()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "YYYY-MM-DD HH:mm:ss";

            ds.Name = "TestDSPI" + randomNumber;

            var Field = new Field();
            var Signal = new Signal();
            Signal.SignalIdentifier = "signal";
            Signal.ValueIdentifier = "value";
            Field.EntityIdentifier = "car";
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
                var data = "{\"time\" :\"2016-03-01 01:01:01\", \"signal\":\"current\",\"value\" : 12.4,\"car\" : \"car1\"}";
                var options = new SortedDictionary<string, string>();
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                options.Add("timeIdentifier", "time");
                options.Add("timeZone", time.Zone);
                options.Add("timeFormat", time.Format);
                options.Add("signalIdentifier", "signal");
                options.Add("valueIdentifier", "value");
                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                datastream = _falkonry.GetDatastream(datastream.Id);
                //_falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, null, false);
            }

        }

        // Add historical narrow input data (csv format) single entity to Datastream (Used for model revision)
        [TestMethod()]
        public void AddDataNarrowFormatCsv()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "YYYY-MM-DD HH:mm:ss";

            ds.Name = "TestDSPI" + randomNumber;

            var Field = new Field();
            var Signal = new Signal();
            Signal.SignalIdentifier = "signal";
            Signal.ValueIdentifier = "value";
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
                Assert.AreEqual(ds.Name, datastream.Field.EntityName);
                var data = "time,signal,value\n" + "2016-05-05 12:00:00,current,12.4\n2016-03-01 01:01:01,vibration,20.4";
                var options = new SortedDictionary<string, string>();

                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");

                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                datastream = _falkonry.GetDatastream(datastream.Id);
                //_falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, null, false);
            }

        }

        // Add historical wide input data (json format) to single entity Datastream (Used for model revision)
        [TestMethod()]
        public void AddDataWideFormatJson()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDSJSON" + randomNumber;
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "YYYY-MM-DD HH:mm:ss";

            var Field = new Field();
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
                Assert.AreEqual(ds.Name, datastream.Field.EntityName);
                var data = "{\"time\" :\"2016-03-01 01:01:01\", \"current\" : 12.4, \"vibration\" : 3.4, \"state\" : \"On\"}";
                var options = new SortedDictionary<string, string>();
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

        [TestMethod()]
        // Add historical wide input data (csv format) to muti entity Datastreamn (Used for model revision)
        public void AddDataWideFormatCsv()
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
                var data = "time,Unit,current,vibration,state\n 2016-05-05T12:00:00.000Z,Unit1,12.4,3.4,On";
                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                options.Add("timeZone", time.Zone);
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                options.Add("entityIdentifier", "Unit");

                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, null, false);
            }

        }

        // Add historical narrow input data (json format) to multi entity Batch Datastream
        [TestMethod()]
        public void AddDataNarrowFormatJsonBatch()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "YYYY-MM-DD HH:mm:ss";

            ds.Name = "TestDSPI" + randomNumber;

            var Field = new Field();
            var Signal = new Signal();
            Signal.SignalIdentifier = "signal";
            Signal.ValueIdentifier = "value";
            Field.Signal = Signal;
            Field.Time = time;
            Field.EntityIdentifier = "car";
            Field.BatchIdentifier = "batch";
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
                var data = "{\"time\" :\"2016-03-01 01:01:01\", \"signal\":\"current\",\"value\" : 12.4,\"car\" : \"car1\", \"batch\" : \"batch1\"}";
                var options = new SortedDictionary<string, string>();
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                options.Add("timeIdentifier", "time");
                options.Add("timeZone", time.Zone);
                options.Add("timeFormat", time.Format);
                options.Add("signalIdentifier", "signal");
                options.Add("valueIdentifier", "value");
                options.Add("batchIdentifier", "batch");
                options.Add("entityIdentifier", "car");
                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                datastream = _falkonry.GetDatastream(datastream.Id);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, null, false);
            }

        }

        // Add historical narrow input data (csv format) single entity to Batch Datastream
        [TestMethod()]
        public void AddDataNarrowFormatCsvBatch()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "YYYY-MM-DD HH:mm:ss";

            ds.Name = "TestDSPI" + randomNumber;

            var Field = new Field();
            var Signal = new Signal();
            Signal.SignalIdentifier = "signal";
            Signal.ValueIdentifier = "value";
            Field.BatchIdentifier = "batch";
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
                Assert.AreEqual(ds.Name, datastream.Field.EntityName);
                Assert.AreEqual(datastream.Field.EntityIdentifier, null);
                var data = "time,signal,value,batch\n" + "2016-05-05 12:00:00,current,12.4,batch1\n2016-03-01 01:01:01,vibration,20.4,batch2";
                var options = new SortedDictionary<string, string>();

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

        // Add historical wide input data (json format) to single entity Batch Datastream
        [TestMethod()]
        public void AddDataWideFormatJsonBatch()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDSJSON" + randomNumber;
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "YYYY-MM-DD HH:mm:ss";

            var Field = new Field();
            Field.Time = time;
            Field.BatchIdentifier = "batch";
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
                Assert.AreEqual(ds.Name, datastream.Field.EntityName);
                Assert.AreEqual(datastream.Field.EntityName, null);
                var data = "{\"time\" :\"2016-03-01 01:01:01\", \"current\" : 12.4, \"vibration\" : 3.4, \"state\" : \"On\", \"batch\" : \"batch1\"}";
                var options = new SortedDictionary<string, string>();
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

        [TestMethod()]
        // Add historical wide input data(csv format) to Batch Datastream (Used for model revision)
        public void AddDataWideFormatCsvBatch()
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
            Field.BatchIdentifier = "batch";
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
                var data = "time,Unit,current,vibration,state,batch\n 2016-05-05T12:00:00.000Z,Unit1,12.4,3.4,On,batch1";
                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                options.Add("timeZone", time.Zone);
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                options.Add("entityIdentifier", "Unit");
                options.Add("batchIdentifier", "batch");

                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, null, false);
            }

        }

        [TestMethod()]
        // Cannot add historical input data(csv format) to Datastream with time identifier missing
        public void AddDataCsvMissingTimeIdentifier()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var time = new Time();
            //time.Zone = "GMT";
            time.Identifier = " Timestamp";
            time.Format = "iso_8601";
            ds.Name = "Test " + randomNumber;

            var Field = new Field();

            Field.Time = time;
            ds.Field = Field;
            Field.EntityIdentifier = "signal";
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
                var data = "time,signal,value\n" + "2016-05-05 12:00:00,current,12.4\n2016-03-01 01:01:01,vibration,20.4";
                SortedDictionary<string, string> options = new SortedDictionary<string, string>();
                //options.Add("timeIdentifier", "time");
                options.Add("timeZone", "GMT");
                options.Add("timeFormat", "YYYY-MM-DD HH:mm:ss");
                options.Add("fileFormat", "csv");
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                options.Add("entityIdentifier", "signal");

                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, "Missing time identifier.", true);
            }

        }

        [TestMethod()]
        // Cannot add historical input data(csv format) to Datastream with time zone missing
        public void AddDataCsvMissingTimeZone()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var time = new Time();
            //time.Zone = "GMT";
            time.Identifier = " Timestamp";
            time.Format = "iso_8601";
            ds.Name = "Test " + randomNumber;

            var Field = new Field();

            Field.Time = time;
            ds.Field = Field;
            Field.EntityIdentifier = "signal";
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
                var data = "time,signal,value\n" + "2016-05-05 12:00:00,current,12.4\n2016-03-01 01:01:01,vibration,20.4";
                SortedDictionary<string, string> options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                //options.Add("timeZone", "GMT");
                options.Add("timeFormat", "YYYY-MM-DD HH:mm:ss");
                options.Add("fileFormat", "csv");
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                options.Add("entityIdentifier", "signal");

                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, "Missing time zone.", true);
            }

        }

        [TestMethod()]
        // Cannot add historical input data(csv format) to Datastream with time format missing
        public void AddDataCsvMissingTimeFormat()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var time = new Time();
            //time.Zone = "GMT";
            time.Identifier = " Timestamp";
            time.Format = "iso_8601";
            ds.Name = "Test " + randomNumber;

            var Field = new Field();

            Field.Time = time;
            ds.Field = Field;
            Field.EntityIdentifier = "signal";
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
                var data = "time,signal,value\n" + "2016-05-05 12:00:00,current,12.4\n2016-03-01 01:01:01,vibration,20.4";
                SortedDictionary<string, string> options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeZone", "GMT");
                options.Add("fileFormat", "csv");
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                options.Add("entityIdentifier", "signal");

                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, "Missing time format.", true);
            }

        }

        [TestMethod()]
        // anAdd historical input data(csv format) to Datastream with entity identifier missing
        public void AddDataCsvMissingEntityIdentifier()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var time = new Time();
            //time.Zone = "GMT";
            time.Identifier = " Timestamp";
            time.Format = "iso_8601";
            ds.Name = "Test " + randomNumber;

            var Field = new Field();

            Field.Time = time;
            ds.Field = Field;
            Field.EntityIdentifier = "signal";
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
                var data = "time,signal,value\n" + "2016-05-05 12:00:00,current,12.4\n2016-03-01 01:01:01,vibration,20.4";
                SortedDictionary<string, string> options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeZone", "GMT");
                options.Add("timeFormat", "YYYY-MM-DD HH:mm:ss");
                options.Add("fileFormat", "csv");
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                //options.Add("entityIdentifier", "signal");

                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, "Missing entity Identifier.", true);
            }

        }

        [TestMethod()]
        // Add historical input data(csv format) with time format different than the time format while creating datastream
        public void AddDataCsvDifferentTimeFormat()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var time = new Time();
            //time.Zone = "GMT";
            time.Identifier = " Timestamp";
            time.Format = "iso_8601";
            ds.Name = "Test " + randomNumber;

            var Field = new Field();

            Field.Time = time;
            ds.Field = Field;
            Field.EntityIdentifier = "signal";
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
                var data = "time,signal,value\n" + "2016-05-05 12:00:00,current,12.4\n2016-03-01 01:01:01,vibration,20.4";
                SortedDictionary<string, string> options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeZone", "GMT");
                options.Add("timeFormat", "YYYY-MM-DD HH:mm:ss");
                options.Add("fileFormat", "csv");
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                options.Add("entityIdentifier", "signal");

                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, null, false);
            }

        }


        [TestMethod()]
        // Cannot add historical wide input data(csv format) to Batch Datastream because of missing batch identifier
        public void CannotAddDataMissingBatch()
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
            Field.BatchIdentifier = "batch";
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
                var data = "time,Unit,current,vibration,state,batch_changed\n 2016-05-05T12:00:00.000Z,Unit1,12.4,3.4,On,batch1";
                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                options.Add("timeZone", time.Zone);
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                options.Add("entityIdentifier", "Unit");
                //options.Add("batchIdentifier", "batch");

                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                Assert.AreEqual(0, 1);
                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, "Missing batch identifier", true);
            }

        }


    }

    [TestClass()]
    public class TestAddHistorianDataFromStream
    {

        Falkonry _falkonry = new Falkonry("https://192.168.2.243:8080", "npp766l2hghmhrc7ygrbldjnkb9rn7mg");


        [TestMethod()]
        //Add historical input data (json format) from a stream to Datastream (Used for model revision) 
        public void AddDataFromStreamJson()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "Time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDS" + randomNumber;
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
                var folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                var path = folder + "/addData.json";

                var bytes = File.ReadAllBytes(path);

                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "YYYY-MM-DD HH:mm:ss");
                options.Add("timeZone", "GMT");
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");


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
        //Add historical input data (csv format) from a stream to Datastream (Used for model revision) 
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
            ds.Field = Field;
            ds.DataSource = datasource;
            ds.Field.Time = time;
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

        // Create PI Datastream (Narrow Format)
        [TestMethod()]
        public void CreatePiDatastreamTest()
        {
            var time = new Time();
            time.Zone = "Asia/Kolkata";
            time.Identifier = "time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "PI";
            datasource.Host = "https://test.piserver.com/piwebapi";
            datasource.ElementTemplateName = "SampleElementTempalte";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));

            var ds = new DatastreamRequest();
            ds.Name = "TestDatastreamStreaming" + randomNumber;
            var Field = new Field();
            var Signal = new Signal();
            Signal.ValueIdentifier = "value";
            Signal.SignalIdentifier = "signal";
            Field.Signal = Signal;

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
                options.Add("timeFormat", "YYYY-MM-DD HH:mm:ss");
                options.Add("timeZone", "GMT");
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
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

    [TestClass()]
    public class TestAddStreamingData
    {
        Falkonry _falkonry = new Falkonry("https://192.168.2.243:8080", "npp766l2hghmhrc7ygrbldjnkb9rn7mg");

        // Add live input data (json format) to Datastream (Used for live monitoring) 
        [TestMethod()]
        public void AddDataJson()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDSJSON" + randomNumber;
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "YYYY-MM-DD HH:mm:ss";

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

        [TestMethod()]
        // Add live input data (csv format) to Datastream (Used for live monitoring) 
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
            Field.EntityIdentifier = "Unit";
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
                var data = "time,Unit,current,vibration,state\n2016-05-05T12:00:00.000Z,Unit1,12.4,3.4,On\n2016-05-06T12:00:00.000Z,Unit1,12.4,3.4,On";
                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                options.Add("timeZone", "GMT");
                options.Add("entityIdentifier", "Unit");
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);

                options["streaming"] = "true";
               
                inputstatus = _falkonry.AddInput(datastream.Id, data, options);
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
            time.Format = "YYYY-MM-DD HH:mm:ss";

            ds.Name = "TestDSPI" + randomNumber;

            var Field = new Field();
            var Signal = new Signal();
            Signal.SignalIdentifier = "signal";
            Signal.ValueIdentifier = "value";
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
                options.Add("timeZone", "GMT");
                options.Add("streaming", "true");
                options.Add("hasMoreData", "false");
                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
                datastream = _falkonry.GetDatastream(datastream.Id);
                //_falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {
                Assert.AreEqual(exception.Message, null, false);
            }

        }


    }

    //[TestClass]
    public class AddDataFromStream

    {

        Falkonry _falkonry = new Falkonry("https://192.168.2.243:8080", "npp766l2hghmhrc7ygrbldjnkb9rn7mg");


        [TestMethod()]
        //Add live input data (json format) from a stream to Datastream (Used for live monitoring) 
        public void AddDataFromStreamJson()
        {
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "Time";
            time.Format = "iso_8601";

            var datasource = new Datasource();
            datasource.Type = "STANDALONE";
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            ds.Name = "TestDS" + randomNumber;
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
                var folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                var path = folder + "/addData.json";

                var bytes = File.ReadAllBytes(path);

                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                options.Add("timeZone", "GMT");
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");


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
        //Add live input data (csv format) from a stream to Datastream (Used for live monitoring) 
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
                options.Add("timeZone", "GMT");
                options.Add("streaming", "true");
                options.Add("hasMoreData", "false");
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

    [TestClass()]
    public class AddFacts
    {
        Falkonry _falkonry = new Falkonry("https://192.168.2.243:8080", "npp766l2hghmhrc7ygrbldjnkb9rn7mg");

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
            Signal.SignalIdentifier = "signal";
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
                var options = new SortedDictionary<string, string>
                     {
                        {"startTimeIdentifier", "time"},
                        {"endTimeIdentifier", "end"},
                        {"timeFormat", "iso_8601"},
                        {"timeZone", time.Zone },
                        { "entityIdentifier", datastream.Field.EntityIdentifier},
                        { "valueIdentifier" , "Health"}
                    };
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

        [TestMethod()]
        public void addFactsForBatchDatastream()
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
            Signal.SignalIdentifier = "signal";
            Field.Signal = Signal;
            Field.Time = time;
            Field.EntityIdentifier = "Unit";
            Field.BatchIdentifier = "batch";
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

                //add data
                var data = "time,Unit,current,vibration,state,batch\n 2016-05-05T12:00:00.000Z,Unit1,12.4,3.4,On,batch1";
                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                options.Add("timeZone", time.Zone);
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                options.Add("entityIdentifier", "Unit");
                options.Add("batchIdentifier", "batch");
                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);


                // add assessment
                var asmt = new AssessmentRequest();
                var randomNumber1 = System.Convert.ToString(rnd.Next(1, 10000));
                asmt.Name = "TestAssessment" + randomNumber1;
                asmt.Datastream = datastream.Id;
                var optionsFacts = new SortedDictionary<string, string>
                     {
                        {"startTimeIdentifier", "time"},
                        {"endTimeIdentifier", "end"},
                        {"timeFormat", "iso_8601"},
                        {"timeZone", time.Zone },
                        { "entityIdentifier", datastream.Field.EntityIdentifier},
                        { "valueIdentifier" , "Health"},
                        { "batchIdentifier" , "Batch"}
                    };
                var assessment = _falkonry.CreateAssessment(asmt);

                var data1 = "time,end," + datastream.Field.EntityIdentifier
              + ",Health,Batch\n2011-03-31T00:00:00.000Z,2011-04-01T00:00:00.000Z,Unit1,Normal,batch1\n2011-03-31T00:00:00.000Z,2011-04-01T00:00:00.000Z,Unit1,Normal,batch2";
                var response = _falkonry.AddFacts(assessment.Id, data1, optionsFacts);

                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }
        }

        // [TestMethod()]
        public void addFactsWithTag()
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
            Signal.SignalIdentifier = "signal";
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
                var options = new SortedDictionary<string, string>
                     {
                        {"startTimeIdentifier", "time"},
                        {"endTimeIdentifier", "end"},
                        {"timeFormat", "iso_8601"},
                        {"timeZone", time.Zone },
                        { "entityIdentifier", datastream.Field.EntityIdentifier},
                        { "valueIdentifier" , "Health"},
                        { "tagIdentifier", "Tag"}
                    };
                var assessment = _falkonry.CreateAssessment(asmt);

                var data1 = "time,end," + datastream.Field.EntityIdentifier
              + ",Health,Tag\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,Unit1,Normal\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,Unit1,Normal,testTag1";
                var response = _falkonry.AddFacts(assessment.Id, data1, options);

                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }
        }

        // [TestMethod()]
        public void addFactsWithAdditionalTag()
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
            Signal.SignalIdentifier = "signal";
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
                var options = new SortedDictionary<string, string>
                     {
                        {"startTimeIdentifier", "time"},
                        {"endTimeIdentifier", "end"},
                        {"timeFormat", "iso_8601"},
                        {"timeZone", time.Zone },
                        { "entityIdentifier", datastream.Field.EntityIdentifier},
                        { "valueIdentifier" , "Health"},
                        { "additionalTag", "testTag"}
                    };
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

        //[TestMethod()]
        public void addFactsForSingleEntity()
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
            Signal.SignalIdentifier = "signal";
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
                Assert.AreEqual(ds.Name, datastream.Field.EntityName);
                Assert.AreEqual(datastream.Field.EntityName, null);
                datastream = _falkonry.GetDatastream(datastream.Id);

                // add assessment
                var asmt = new AssessmentRequest();
                var randomNumber1 = System.Convert.ToString(rnd.Next(1, 10000));
                asmt.Name = "TestAssessment" + randomNumber1;
                asmt.Datastream = datastream.Id;
                var options = new SortedDictionary<string, string>
                     {
                        {"startTimeIdentifier", "time"},
                        {"endTimeIdentifier", "end"},
                        {"timeFormat", "iso_8601"},
                        {"timeZone", time.Zone },
                        { "valueIdentifier" , "Health"}
                    };
                var assessment = _falkonry.CreateAssessment(asmt);

                var data1 = "time,end,Health" + "\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,Normal\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,Unit1,Normal";
                var response = _falkonry.AddFacts(assessment.Id, data1, options);

                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }
        }

        [TestMethod()]
        public void addFactsFromStream()
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
            Signal.SignalIdentifier = "signal";
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
                var options = new SortedDictionary<string, string>
                    {
                        {"startTimeIdentifier", "time"},
                        {"endTimeIdentifier", "end"},
                        {"timeFormat", "iso_8601"},
                        {"timeZone", time.Zone },
                        {"entityIdentifier", "car"},
                        {"valueIdentifier" , "Health"}
                    };
                var assessment = _falkonry.CreateAssessment(asmt);

                var folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                var path = folder + "/factsData.json";

                var bytes = File.ReadAllBytes(path);

                var response = _falkonry.AddFactsStream(assessment.Id, bytes, options);

                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }
        }

        [TestMethod()]
        public void addFactsWithTagsFromStream()
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
            Signal.SignalIdentifier = "signal";
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
                var options = new SortedDictionary<string, string>
                    {
                        {"startTimeIdentifier", "time"},
                        {"endTimeIdentifier", "end"},
                        {"timeFormat", "iso_8601"},
                        {"timeZone", time.Zone },
                        {"entityIdentifier", "car"},
                        {"valueIdentifier" , "Health"},
                        {"tagIdentifier", "Tags"}
                    };
                var assessment = _falkonry.CreateAssessment(asmt);

                var folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                var path = folder + "/factsDataWithTags.json";

                var bytes = File.ReadAllBytes(path);

                var response = _falkonry.AddFactsStream(assessment.Id, bytes, options);

                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }
        }

        [TestMethod()]
        public void addFactsWithAdditionalTagFromStream()
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
            Signal.SignalIdentifier = "signal";
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
                var options = new SortedDictionary<string, string>
                    {
                        {"startTimeIdentifier", "time"},
                        {"endTimeIdentifier", "end"},
                        {"timeFormat", "iso_8601"},
                        {"timeZone", time.Zone },
                        {"entityIdentifier", "car"},
                        {"valueIdentifier" , "Health"},
                        {"additionalTag" , "testTag"}
                    };
                var assessment = _falkonry.CreateAssessment(asmt);

                var folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                var path = folder + "/factsData.json";

                var bytes = File.ReadAllBytes(path);

                var response = _falkonry.AddFactsStream(assessment.Id, bytes, options);

                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }
        }

    }

    [TestClass()]
    public class GetFacts
    {
        Falkonry _falkonry = new Falkonry("https://192.168.2.243:8080", "npp766l2hghmhrc7ygrbldjnkb9rn7mg");


        [TestMethod()]
        public void getFacts()
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
            Signal.SignalIdentifier = "signal";
            Field.Signal = Signal;
            Field.Time = time;
            Field.EntityIdentifier = "Unit";
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

                //add data
                var data = "time,Unit,current,vibration,state\n 2016-05-05T12:00:00.000Z,Unit1,12.4,3.4,On";
                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                options.Add("timeZone", time.Zone);
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                options.Add("entityIdentifier", "Unit");
                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);



                // add assessment
                var asmt = new AssessmentRequest();
                var randomNumber1 = System.Convert.ToString(rnd.Next(1, 10000));
                asmt.Name = "TestAssessment" + randomNumber1;
                asmt.Datastream = datastream.Id;
                var assessment = _falkonry.CreateAssessment(asmt);

                var optionsFacts = new SortedDictionary<string, string>
                     {
                        {"startTimeIdentifier", "time"},
                        {"endTimeIdentifier", "end"},
                        {"timeFormat", "iso_8601"},
                        {"timeZone", time.Zone },
                        { "entityIdentifier", datastream.Field.EntityIdentifier},
                        { "valueIdentifier" , "Health"}
                    };

                var data1 = "time,end," + datastream.Field.EntityIdentifier
              + ",Health\n2011-03-31T00:00:00.000Z,2011-04-01T00:00:00.000Z,Unit1,Normal\n2011-03-31T00:00:00.000Z,2011-04-01T00:00:00.000Z,Unit1,Normal";
                var response = _falkonry.AddFacts(assessment.Id, data1, optionsFacts);

                /// Get Facts
                var factsData = _falkonry.getFacts(assessment.Id, options);
                Assert.AreEqual(factsData.Response.Length > 0, true);

                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }
        }

        //Get Facts for batch datastream
        [TestMethod()]
        public void getFactsForBatchDatastream()
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
            Signal.SignalIdentifier = "signal";
            Field.Signal = Signal;
            Field.Time = time;
            Field.EntityIdentifier = "Unit";
            Field.BatchIdentifier = "Batch";
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

                //add data
                var data = "time,Unit,current,vibration,state,batch\n 2016-05-05T12:00:00.000Z,Unit1,12.4,3.4,On,batch1";
                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                options.Add("timeZone", time.Zone);
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                options.Add("entityIdentifier", "Unit");
                options.Add("batchIdentifier", "batch");
                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);



                // add assessment
                var asmt = new AssessmentRequest();
                var randomNumber1 = System.Convert.ToString(rnd.Next(1, 10000));
                asmt.Name = "TestAssessment" + randomNumber1;
                asmt.Datastream = datastream.Id;
                var assessment = _falkonry.CreateAssessment(asmt);

                var optionsFacts = new SortedDictionary<string, string>
                     {
                        {"startTimeIdentifier", "time"},
                        {"endTimeIdentifier", "end"},
                        {"timeFormat", "iso_8601"},
                        {"timeZone", time.Zone },
                        { "entityIdentifier", datastream.Field.EntityIdentifier},
                        { "valueIdentifier" , "Health"},
                        { "batchIdentifier" , "Batch"}
                    };

                var data1 = "time,end," + datastream.Field.EntityIdentifier
              + ",Health,Batch\n2011-03-31T00:00:00.000Z,2011-04-01T00:00:00.000Z,Unit1,Normal,batch1\n2011-03-31T00:00:00.000Z,2011-04-01T00:00:00.000Z,Unit1,Normal,batch2";
                var response = _falkonry.AddFacts(assessment.Id, data1, optionsFacts);

                /// Get Facts
                var factsData = _falkonry.getFacts(assessment.Id, options);
                Assert.AreEqual(factsData.Response.Length > 0, true);

                _falkonry.DeleteDatastream(datastream.Id);
            }
            catch (System.Exception exception)
            {

                Assert.AreEqual(exception.Message, null, false);
            }
        }
    }

    [TestClass()]
    public class GetDatastreamData
    {
        Falkonry _falkonry = new Falkonry("https://192.168.2.243:8080", "npp766l2hghmhrc7ygrbldjnkb9rn7mg");

        [TestMethod()]
        public void getData()
        {
            var rnd = new System.Random();
            var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
            var ds = new DatastreamRequest();
            var time = new Time();
            time.Zone = "GMT";
            time.Identifier = "time";
            time.Format = "YYYY-MM-DD HH:mm:ss";

            ds.Name = "TestDSPI" + randomNumber;

            var Field = new Field();
            var Signal = new Signal();
            Signal.SignalIdentifier = "signal";
            Signal.ValueIdentifier = "value";
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
                var data = "time, tag, value \n" + "2016-05-05T12:00:00Z, Unit1_current, 12.4 \n 2016-03-01T01:01:01Z, Unit1_vibration, 20.4";
                var options = new SortedDictionary<string, string>();
                options.Add("timeIdentifier", "time");
                options.Add("timeFormat", "iso_8601");
                options.Add("timeZone", "GMT");
                options.Add("streaming", "false");
                options.Add("hasMoreData", "false");
                var inputstatus = _falkonry.AddInput(datastream.Id, data, options);

                // Get Input data
                var responseData = _falkonry.GetDatastreamData(datastream.Id, options);
                Assert.AreEqual(responseData.StatusCode, 200);
                Assert.AreEqual(responseData.Response.Length > 0, true);
                datastream = _falkonry.GetDatastream(datastream.Id);
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
        Falkonry _falkonry = new Falkonry("https://192.168.2.243:8080", "npp766l2hghmhrc7ygrbldjnkb9rn7mg");

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
            Signal.SignalIdentifier = "signal";

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
                options.Add("responseFormat", "application/json");  // also available options 1. text/csv 2. application/json

                var httpResponse = _falkonry.GetHistoricalOutput(assessment, options);
                // If data is not readily available then, a tracker id will be sent with 202 status code. While falkonry will generate output data
                // Client should do timely pooling on the using same method, sending tracker id (__id) in the query params
                // Once data is available server will response with 200 status code and data in json/csv format.

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

                    // if status is 200, output data will be present in httpResponse.response field
                }
                if (httpResponse.StatusCode > 400)
                {
                    // Some Error has occurred. Please httpResponse.response for detail message
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
        Falkonry _falkonry = new Falkonry("https://192.168.2.243:8080", "npp766l2hghmhrc7ygrbldjnkb9rn7mg");

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
            Signal.SignalIdentifier = "signal";

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

