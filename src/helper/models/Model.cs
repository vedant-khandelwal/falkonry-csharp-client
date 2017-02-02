using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    public class Model
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

        public string name
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

        public string createdBy
        {
            get;
            set;
        }

        public long updateTime
        {
            get;
            set;
        }

        public string updatedBy
        {
            get;
            set;
        }
        public string toJSON()
        {
            return new JavaScriptSerializer().Serialize(this);
        }

        public string type
        {
            get;
            set;
        }

        public string message
        {
            get;
            set;
        }

        public string status
        {
            get;
            set;
        }

        public string pid
        {
            get;
            set;
        }

        public long index
        {
            get;
            set;
        }
        
        public string model
        {
            get;
            set;
        }
    }
}