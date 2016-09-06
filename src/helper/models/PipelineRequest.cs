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
    public class PipelineRequest
    {
        public string name
        {
            get;
            set;
        }
        public string input
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
        public List<SignalRequest> inputList
        {
            get;
            set;
        }
        public List<AssessmentRequest> assessmentList
        {
            get;
            set;
        }
        public Interval interval
        {
            get;
            set;
        }

    }

}