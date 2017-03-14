[![Falkonry Logo](https://service.falkonry.io/img/logo.png)](http://falkonry.com/)


Falkonry C# Client to access [Falkonry Condition Prediction](falkonry.com) APIs

[Releases](https://github.com/Falkonry/falkonry-csharp-client/releases)

## Features

    * Create Datastream
    * Create Assessment
    * Retrieve Datastream
    * Retrieve Assessment
    * Add data to Datastream (json, stream)
    * Retrieve output of a Assessment
    * Add facts data to Assessment
	* Add Entity Meta to DataStream
	* Get Entity Meta of DataStream
	* Generate Output Data for Historical Data
    
## Quick Start

    * To create Datastream
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;
    
    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);
    
    var time = new Time();
	time.Zone = "GMT";
	time.Identifier = "time";
	time.Format = "iso_8601";
	Field field = new Field();
    Datasource datasource = new Datasource();
    datasource.type = "STANDALONE"; 

    DatastreamRequest ds = new DatastreamRequest();
    ds.name = "datastream name here";
    field.time = time;
	ds.Field = field;
    
    ds.dataSource = datasource;
    Datastream datastream = falkonry.createDatastream(ds);
```
    * To create Datastream with entityIdentifier set
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;
    
    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);
    var time = new Time();
	time.Zone = "GMT";
	time.Identifier = "time";
	time.Format = "iso_8601";
	Field field = new Field();

    Datasource datasource = new Datasource();
    datasource.type = "STANDALONE"; 

    DatastreamRequest ds = new DatastreamRequest();
    ds.name = "datastream name here";
	
	field.time = time;
	field.entityIdentifier = "nameOfEntityIdentifer";
	ds.Field = field;
    ds.dataSource = datasource;
    
    Datastream datastream = falkonry.createDatastream(ds);
```
    * To create Assessment
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);

    var time = new Time();
	time.Zone = "GMT";
	time.Identifier = "time";
	time.Format = "iso_8601";
	Field field = new Field();
    Datasource datasource = new Datasource();
    datasource.type = "STANDALONE"; 

    DatastreamRequest ds = new DatastreamRequest();
    ds.name = "datastream name here";
	field.Time = time;
	Var Signal = new Siganl();
	Signal.ValueIdentifier = "value";
    Signal.TagIdentifier = "tag";
    Signal.IsSignalPrefix = true;
    Signal.Delimiter = "_";

    field.Signal = Signal;
	ds.Field = field;
    
    ds.dataSource = datasource;
    Datastream datastream = falkonry.createDatastream(ds);
    
    AssessmentRequest asmt = new AssessmentRequest();
    asmt.name = "assessment name here";
    Assessment assessment = falkonry.createAssessment(asmt);

```
    * To get all Datastreams
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);
    List<Datastream> datastreams = new List<Datastream>();
```

    * To get all assessments
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);
    List<Assessment> assessmentList = falkonry.getAssessments();
```

    * To add data in Datastream
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);

    //Creating a datastream to add data to later
    string name="data stream name here";
    var time = new Time();
	time.Zone = "GMT";
	time.Identifier = "time";
	time.Format = "iso_8601";
	Field field = new Field();

    Datasource datasource = new Datasource();
    datasource.type = "STANDALONE"; 

    DatastreamRequest ds = new DatastreamRequest();
    ds.name = "datastream name here";
	field.Time = time;
	Field.EntityIdentifier = "Unit";
	ds.Field = field;
   
    ds.dataSource = datasource;
    Datastream datastream = falkonry.createDatastream(ds);
    
    
    string data = "{\"time\" :\"2016-03-01 01:01:01\", \"current\" : 12.4, \"vibration\" : 3.4, \"state\" : \"On\"}";
    
    SortedDictionary<string, string> options = new SortedDictionary<string, string>();
    options.Add("timeIdentifier", "time");
    options.Add("timeFormat", "iso_8601");
    options.Add("fileFormat", "json");
    InputStatus inputstatus = falkonry.addInput(datastream.id, data, options);
```

    * To add data from a stream in Datastream
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);

    //Creating a datastream to add data to later
    string name="data stream name here";
    var time = new Time();
	time.Zone = "GMT";
	time.Identifier = "time";
	time.Format = "iso_8601";
	Field field = new Field();
	Field.EntityIdentifier = "Unit";
    Datasource datasource = new Datasource();
    datasource.type = "STANDALONE"; 

    DatastreamRequest ds = new DatastreamRequest();
    ds.name = "datastream name here";
    field.Time = time;
	ds.Field = field;
    
    ds.dataSource = datasource;
    Datastream datastream = falkonry.createDatastream(ds);
    
    /*This particular example will read data from a AddData.json file in debug folder in bin*/
    
    SortedDictionary<string, string> options = new SortedDictionary<string, string>();
    options.Add("timeIdentifier", "time");
    options.Add("timeFormat", "iso_8601");
    
    string folder_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

    string path = folder_path + "/AddData.json";
    //Alternatively, you can directly specify the folder path in the "folder_path" variable

    byte[] bytes = System.IO.File.ReadAllBytes(path);

    InputStatus inputstatus = falkonry.addInputStream(datastream.id, bytes, options);
    //The updated datastream
    datastream = falkonry.getDatastream(datastream.id);
```

    * To get output of a Assessment
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);

    string assessment_id ="assessment ID here";
    System.IO.Stream streamrecieved = falkonry.getOutput(assessment_id, null, null);
    //The folder path below by default is debug in bin.
    string folder_path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
    //Alternatively, you can also specify the path of the folder in thr folder_path variable
    //The outflow will get saved in an outflow.txt file there
    string path = folder_path + "/outflow.txt";

    System.IO.StreamReader streamreader = new System.IO.StreamReader(streamrecieved);
    System.IO.StreamWriter streamwriter = new System.IO.StreamWriter(path);
    string line;
    using (streamwriter)
    {
        while ((line = streamreader.ReadLine()) != null)
        {
            streamwriter.WriteLine(line);
        }
    }
```
    * To add facts data
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token = "Add your token here";   
    SortedDictionary<string, string> options = new SortedDictionary<string, string>();
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);
    string data = "time,end,car,Health\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,IL9753,Normal\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,HI3821,Normal";
    string response = falkonry.addFacts(<assessment_id>,data, options);

```
   * To add facts data from stream
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);
    string path = "Insert the path to your file here";
    byte[] bytes = System.IO.File.ReadAllBytes(path);
    string response = falkonry.addFactsStream(<assessment_id>, bytes, null);

```     

```
	* Add Entity Meta to datastream, Get Entity Meta of datastream
```
	using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);
	
	// Create Datastream
	datastream = falkonry.getDatastream(datastream.id);

	// Create EntityMetaRequest
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

	// Get entitymeta
	entityMetaResponseList = falkonry.getEntityMeta(datastream);
```

```
   * Generate Output Data for Historical Data
```
	using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);
	
	// Create Datastream

	// create Assessment

	// Add Data To dataStream

	// From Falkonry UI, run a model revision.

	// Fetch Historical output data for given assessment, startTime , endtime
	SortedDictionary<string, string> options = new SortedDictionary<string, string>();
	options.Add("startTime", "2011-01-01T01:00:00.000Z"); // in the format YYYY-MM-DDTHH:mm:ss.SSSZ
	options.Add("endTime", "2011-06-01T01:00:00.000Z");  // in the format YYYY-MM-DDTHH:mm:ss.SSSZ
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

		// use this tracker for checking the status of the process.
		options = new SortedDictionary<string, string>();
		options.Add("tarckerId", __id);
		options.Add("responseFormat", "application/json");

		httpResponse = falkonry.getHistoricalOutput(assessment, options);

		// if status is 202 call the same request again

		// if statsu is 200, output data will be present in httpResponse.response field
	}
	if (httpResponse.statusCode > 400)
	{
		// Some Error has occured. Please httpResponse.response for detail message
	}
```

## Docs

    [Falkonry APIs](https://service.falkonry.io/api)
     

## License

  Available under [MIT License](LICENSE)