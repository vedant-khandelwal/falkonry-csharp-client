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

    public class HttpResponse
    {
        public int statusCode
        {
            get;
            set;

        }

        // if the status code is 202 then response will contain Tracker object
        // if status code is 200 then response will contain output data
        // if status code is more than 400 then response will contain error messgae
        public String response
        {
            get;
            set;

        }
    }
}