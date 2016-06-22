///
/// falkonry-csharp-client
/// Copyright(c) 2016 Falkonry Inc
/// MIT Licensed
///


using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
namespace falkonry_csharp_client.helper.models
{
    public class SignalRequest
    {
    
        public string name
        {

            get;
            set;
        }
        public ValueType valueType
        {

            get;
            set;
        }
        public EventType eventType
        {

            get;
            set;
        }

    }
}