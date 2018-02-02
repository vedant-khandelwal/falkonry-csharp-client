using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    public class OutputStateRequest
    {
        public string datastream
        {
            get;
            set;
        }

        public List<string> assessment
        {
            get;
            set;
        }
    }
}
