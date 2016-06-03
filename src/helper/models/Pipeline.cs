using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    class Pipeline
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
        public int createTime
        {
            get;
            set;
        }
        public string createdBy
        {
            get;
            set;
        }
        public int updateTime
        {
            get;
            set;
        }
        public string updatedBy
        {
            get;
            set;
        }
        public object input
        {
            get;
            set;
        }
        public string inputMeasurement
        {
            get;
            set;
        }
        public string thingIdentifier
        {
            get;
            set;
        }
        public string singleThingID
        {
            get;
            set;
        }
        public List<Signal> inputList
        {
            get;
            set
            {
                List<Signal> singnalL = new List<Signal>();
                foreach (object eachSignal in value)
                {
                    if (eachSignal is Signal)
                    {
                        singnalL.Add((Signal)eachSignal);
                    }
                }
                this.inputList = singnalL;
            }
        }
        public List<Assessment> assessmentList
        {
            get;
            set
            {
                List<Assessment> assessmentL = new List<Assessment>();
                foreach (object eachAssessment in value)
                {
                    if (eachAssessment is Assessment)
                    {
                        assessmentL.Add((Assessment)eachAssessment);
                    }
                }
                this.assessmentList = assessmentL;
            }
        }

        public List<Publication> publicationList
        {
            get;
            set
            {
                List<Publication> publicationL = new List<Publication>();
                foreach (object eachPublication in value)
                {
                    if (eachPublication is Publication)
                    {
                        publicationL.Add((Publication)eachPublication);
                    }
                }
                this.publicationList = publicationL;
            }
        }
        public string status
        {
            get;
            set;
        }
        public string outflowstatus
        {
            get;
            set;
        }

        public Dictionary<string, string> interval
        {
            get;
            set;
        }

        public int earliestDataPoint
        {
            get;
            set;
        }

        public int latestDataPoint
        {
            get;
            set;
        }
        public string[]  modelRevisionList
        {
            get;
            set;
        }

        public string[] outflowHistory
        {
            get;
            set;
        }
        public string toJSON()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
    }
}
