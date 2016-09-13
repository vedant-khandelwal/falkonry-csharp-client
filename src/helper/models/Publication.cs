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
    public class Publication
    {
        public string key
        {
            get;
            set;
        }
        public string type
        {
            get;
            set;
        }
        public string topic
        {
            get;
            set;
        }
        public string path
        {
            get;
            set;
        }
        public string username
        {
            get;
            set;
        }
        public string password
        {
            get;
            set;
        }
        public string contentType
        {
            get;
            set;
        }
        public Boolean streaming
        {
            get;
            set;
        }
        /*public object input
        {
            get;
            set;
        }*/
        public SortedDictionary<string,string> headers
        {
            get;
            set;
        }
        /*public string entityIdentifier
        {
            get;
            set;
        }
        public string entityName
        {
            get;
            set;
        }
        public string entityIdentifier
        {
            get;
            set;
        }
        public List<Signal> inputList
        {
            get { }
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
        public string[] modelRevisionList
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
        }*/
    }
}
