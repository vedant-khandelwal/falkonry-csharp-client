using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    class Assessment
    {
   // public Assessment(Assessment assessment) {
     
   //// _this.raw = assessment || {};
   // }
    public string key
    {
        get; set;
    }
    public string name
    {
        get; set;
    }
    public List<string> inputList
    {
        get; set;
    }
    public List<string> aprioriConditionList
    {
        get; set;
    }
    public string toJSON()
    {
        return new JavaScriptSerializer().Serialize(this);
    }

    }
}
