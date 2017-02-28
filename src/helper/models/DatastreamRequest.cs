using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    public class DatastreamRequest
    {
        

        public string Name
        {
            get;
            set;
        }

        public string ToJson()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
       
        public Timezone Timezone
        {
            get;
            set;
        }

        public string SignalsTagField
        {
            get;
            set;
        }

        public string SignalsDelimiter
        {
            get;
            set;
        }

        public string ValueColumn
        {
            get;
            set;
        }

        public string SignalsLocation
        {
            get;
            set;
        }

        public string EntityIdentifier
        {
            get;
            set;
        }

        public string EntityName
        {
            get;
            set;
        }

        public string TimeIdentifier
        {
            get;
            set;
        }

        public string TimeFormat
        {
            get;
            set;
        }

        public Datasource DataSource
        {
            get;
            set;
        }

        public List<Input> InputList
        {
            get;
            set;
        }

    }
}
