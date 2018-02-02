using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    public class OutputState
    {
        public string id
        {
            get;
            set;
        }

        public string sourceId
        {
            get;
            set;
        }

        public string tenant
        {
            get;
            set;
        }

        public string createdBy
        {
            get;
            set;
        }

        public string updatedBy
        {
            get;
            set;
        }

        public number createTime
        {
            get;
            set;
        }

        public number updateTime
        {
            get;
            set;
        }

        public string type
        {
            get;
            set;
        }

        public string datastream
        {
            get;
            set;
        }

        public List<object> processList
        {
            get;
            set;
        }

    }
}
