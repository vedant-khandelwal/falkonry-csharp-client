using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace falkonry_csharp_client.service
{
    class FalkonryService
    {
        private string host;
        private string token;
        private HttpService http;
        private void FalkonryService (string host,string token) 
        {
            this.host=host;
            this.token=token;
            this.http  = new HttpService(host, token);
        }
    }
}

