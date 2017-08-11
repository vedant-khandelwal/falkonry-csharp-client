using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    public class DatastreamUpdateRequest
    {
        public string Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string ToJson()
        {
            return new JavaScriptSerializer().Serialize(this);
        }

        public List<Input> InputList
        {
            get;
            set;
        }

        public bool Streaming
        {
            get;
            set;
        }
    }
}
