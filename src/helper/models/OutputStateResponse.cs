using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    public class OutputStateResponse
    {
        public string inputUrl
        {
            get;
            set;
        }

        public string stopUrl
        {
            get;
            set;
        }

        public List<object> outputUrl
        {
            get;
            set;
        }
    }
}
