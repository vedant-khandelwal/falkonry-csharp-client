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
    
## Quick Start

    * To create Datastream
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;
    
    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);
    
    Timezone timezone = new Timezone();
    timezone.zone = "GMT";
    timezone.offset = 0;

    Datasource datasource = new Datasource();
    datasource.type = "STANDALONE"; 

    DatastreamRequest ds = new DatastreamRequest();
    ds.name = "datastream name here";
    ds.timeIdentifier = "time";
    ds.timeFormat = "iso_8601";
    ds.timezone = timezone;
    ds.dataSource = datasource;
    Datastream datastream = falkonry.createDatastream(ds);
```
    * To create Datastream with entityIdentifier set
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;
    
    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);
    Timezone timezone = new Timezone();
    timezone.zone = "GMT";
    timezone.offset = 0;

    Datasource datasource = new Datasource();
    datasource.type = "STANDALONE"; 

    DatastreamRequest ds = new DatastreamRequest();
    ds.name = "datastream name here";
    ds.timeIdentifier = "time";
    ds.timeFormat = "iso_8601";
    ds.timezone = timezone;
    ds.dataSource = datasource;
    ds.entityIdentifier = "nameOfEntityIdentifer";
    Datastream datastream = falkonry.createDatastream(ds);
```
    * To create Assessment
    
```
    using falkonry_csharp_client;
    using falkonry_csharp_client.helper.models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("http://localhost:8080", token);

    Timezone timezone = new Timezone();
    timezone.zone = "GMT";
    timezone.offset = 0;

    Datasource datasource = new Datasource();
    datasource.type = "STANDALONE"; 

    DatastreamRequest ds = new DatastreamRequest();
    ds.name = "datastream name here";
    ds.timeIdentifier = "time";
    ds.timeFormat = "iso_8601";
    ds.timezone = timezone;
    ds.dataSource = datasource;
    Datastream datastream = falkonry.createDatastream(ds);
    
    AssessmentRequest asmt = new Assessment();
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
    Timezone timezone = new Timezone();
    timezone.zone = "GMT";
    timezone.offset = 0;

    Datasource datasource = new Datasource();
    datasource.type = "STANDALONE"; 

    DatastreamRequest ds = new DatastreamRequest();
    ds.name = "datastream name here";
    ds.timeIdentifier = "time";
    ds.timeFormat = "iso_8601";
    ds.timezone = timezone;
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
    Timezone timezone = new Timezone();
    timezone.zone = "GMT";
    timezone.offset = 0;

    Datasource datasource = new Datasource();
    datasource.type = "STANDALONE"; 

    DatastreamRequest ds = new DatastreamRequest();
    ds.name = "datastream name here";
    ds.timeIdentifier = "time";
    ds.timeFormat = "iso_8601";
    ds.timezone = timezone;
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


## Docs

    [Falkonry APIs](https://service.falkonry.io/api)
     

## License

  Available under [MIT License](LICENSE)