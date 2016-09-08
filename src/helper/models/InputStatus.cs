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
    public class InputStatus
    {
        public string status
        {
            get;
            set;
        }
        public int requestPending
        {
            get;
            set;
        }
        public int requestCompleted
        {
            get;
            set;
        }
        public string id
        {
            get;
            set;
        }
        public string tenant
        {
            get;
            set;
        }
        public long createTime
        {
            get;
            set;
        }
        public string eventBuffer
        {
            get;
            set;
        }
        public string action
        {
            get;
            set;
        }







    }
}
