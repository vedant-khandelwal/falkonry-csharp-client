///
/// falkonry-csharp-client
/// Copyright(c) 2016 Falkonry Inc
/// MIT Licensed
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    public class DatastreamRequest
    {
        

        public string name
        {
            get;
            set;
        }

        public string toJSON()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
       
        public Timezone timezone
        {
            get;
            set;
        }

        public string signalsTagField
        {
            get;
            set;
        }

        public string signalsDelimiter
        {
            get;
            set;
        }

        public string valueColumn
        {
            get;
            set;
        }

        public string signalsLocation
        {
            get;
            set;
        }

        public string entityIdentifier
        {
            get;
            set;
        }

        public string entityName
        {
            get;
            set;
        }

        public string timeIdentifier
        {
            get;
            set;
        }

        public string timeFormat
        {
            get;
            set;
        }

        public Datasource dataSource
        {
            get;
            set;
        }

        public List<Input> inputList
        {
            get;
            set;
        }

    }
}
