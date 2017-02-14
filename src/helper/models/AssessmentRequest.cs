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
    public class AssessmentRequest
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
        public string datastream
        {
            get;
            set;
        }
        public string assessmentRate
        {
            get;
            set;
        }



    }
}
