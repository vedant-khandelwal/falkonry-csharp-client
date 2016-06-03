using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;


namespace falkonry_csharp_client.service
{
    public class HttpService
    {
        private string host;
        private string token;
        public HttpService(string host, string token)
        {
          this.token = Convert.ToBase64String(Encoding.UTF8.GetBytes((token == null ? "" : token)));
          this.host = host == null ? "https://service.falkonry.io" : host;
        }

        public string get (string path)
        {
            try 
            {
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token "+this.token);
			    request.Method = "GET";
                request.ContentType = "application/json";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                String obj = js.Deserialize<string>(resp);
                //dynamic obj = JsonConvert.DeserializeObject(resp);
                //body = JSON.parse(body);
                if ( Convert.ToInt32(response.StatusCode) == 401 )
                {
                    return "Unauthorized : Invalid token " + response.StatusCode;
                }
                else if ( Convert.ToInt32(response.StatusCode) >= 400 )
                {
                    // return obj.error.message + response.StatusCode;
                    //return done(body.message, null, response.statusCode);
                }
                else
                {
                   return obj + response.StatusCode;
                //    return done(null, body, response.statusCode);
                }
                 return request.ToString(); 
            }
            catch ( Exception E)
            {
                return E.Message.ToString();
            }
        }

        public string post (string path,object pipeline)
        {
            try 
            {
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token "+this.token);
			    request.Method = "POST";
                request.ContentType = "application/json";
                //request.body = JSON.stringify(pipeline);
                //body: JSON.stringify(pipeline);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //dynamic obj = JsonConvert.DeserializeObject(resp);
                //body = JSON.parse(body);
                if ( Convert.ToInt32(response.StatusCode) == 401 )
                {
                    return "Unauthorized : Invalid token " + response.StatusCode;
                }
                else if ( Convert.ToInt32(response.StatusCode) >= 400 )
                {
                    // return obj.error.message + response.StatusCode;
                    //return done(body.message, null, response.statusCode);
                }
                else
                {
                //    return obj. + response.StatusCode;
                //    return done(null, body, response.statusCode);
                }
                 return request.ToString(); 
            }
            catch ( Exception E)
            {
                return E.Message.ToString();
            }
        }

        public string put (string path,object pipeline)
        {
            try 
            {
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token "+this.token);
			    request.Method = "PUT";
                request.ContentType = "application/json";
                //request.body = JSON.stringify(pipeline);
                //body: JSON.stringify(pipeline);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //dynamic obj = JsonConvert.DeserializeObject(resp);
                //body = JSON.parse(body);
                if ( Convert.ToInt32(response.StatusCode) == 401 )
                {
                    return "Unauthorized : Invalid token " + response.StatusCode;
                }
                else if ( Convert.ToInt32(response.StatusCode) >= 400 )
                {
                    // return obj.error.message + response.StatusCode;
                    //return done(body.message, null, response.statusCode);
                }
                else
                {
                //    return obj. + response.StatusCode;
                //    return done(null, body, response.statusCode);
                }
                 return request.ToString(); 
            }
            catch ( Exception E)
            {
                return E.Message.ToString();
            }
        }

        public string sfpost (string path,object data)
        {
            try 
            {
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token "+this.token);
			    request.Method = "POST";
                //form   : {'name' : 'hello'},
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //dynamic obj = JsonConvert.DeserializeObject(resp);
                //body = JSON.parse(body);
                if ( Convert.ToInt32(response.StatusCode) == 401 )
                {
                    return "Unauthorized : Invalid token " + response.StatusCode;
                }
                else if ( Convert.ToInt32(response.StatusCode) >= 400 )
                {
                    // return obj.error.message + response.StatusCode;
                    //return done(body.message, null, response.statusCode);
                }
                else
                {
                //    return obj. + response.StatusCode;
                //    return done(null, body, response.statusCode);
                }
                 return request.ToString(); 
            }
            catch ( Exception E)
            {
                return E.Message.ToString();
            }
        }

        public string fpost (string path,object data)
        {
            try 
            {
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token "+this.token);
			    request.Method = "POST";
                 request.ContentType = "multipart/form-data";
                //request.formdata : data,
                //formData: data,
                //request.body = "";
                //body: "";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //dynamic obj = JsonConvert.DeserializeObject(resp);
                //body = JSON.parse(body);
                if ( Convert.ToInt32(response.StatusCode) == 401 )
                {
                    return "Unauthorized : Invalid token " + response.StatusCode;
                }
                else if ( Convert.ToInt32(response.StatusCode) >= 400 )
                {
                    // return obj.error.message + response.StatusCode;
                    //return done(body.message, null, response.statusCode);
                }
                else
                {
                //    return obj. + response.StatusCode;
                //    return done(null, body, response.statusCode);
                }
                 return request.ToString(); 
            }
            catch ( Exception E)
            {
                return E.Message.ToString();
            }
        }

        public string delete (string path)
        {
            try 
            {
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token "+this.token);
			    request.Method = "DELETE";
                request.ContentType = "application/json";
                //request.body = "";
                //body: "";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //dynamic obj = JsonConvert.DeserializeObject(resp);
                //body = JSON.parse(body);
                if ( Convert.ToInt32(response.StatusCode) == 401 )
                {
                    return "Unauthorized : Invalid token " + response.StatusCode;
                }
                else if ( Convert.ToInt32(response.StatusCode) >= 400 )
                {
                    // return obj.error.message + response.StatusCode;
                    //return done(body.message, null, response.statusCode);
                }
                else
                {
                //    return obj. + response.StatusCode;
                //    return done(null, body, response.statusCode);
                }
                 return request.ToString(); 
            }
            catch ( Exception E)
            {
                return E.Message.ToString();
            }
        }

        public string upstream(string path,string datatype,Stream stream)
        {
            try
            {
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);

                //  var formData = {
                //    data: {
                //      value: stream,
                //      options: {
                //        filename: ('input-'+Utils.randomString(6) + (dataType === "csv" ? ".csv" : ".json"))
                //      }
                //    }
                //  };
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token " + this.token);
                request.Method = "POST";
                request.ContentType = "multipart/form-data";
                //    formData: formData,
                //    body: ''
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //dynamic obj = JsonConvert.DeserializeObject(resp);
                //body = JSON.parse(body);
                if (Convert.ToInt32(response.StatusCode) == 401)
                {
                    return "Unauthorized : Invalid token " + response.StatusCode;
                }
                else if (Convert.ToInt32(response.StatusCode) >= 400)
                {
                    // return obj.error.message + response.StatusCode;
                    //return done(body.message, null, response.statusCode);
                }
                else
                {
                    //    return obj. + response.StatusCode;
                    //    return done(null, body, response.statusCode);
                }
                return request.ToString();
            }
            catch (Exception E)
            {
                return E.Message.ToString();
            }
        }

        public string downstream(string path)
        {
            try
            {
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token " + this.token);
                request.Headers.Add("Accept", "application/json");
                request.Method = "GET";
                request.ContentType = "application/x-json-stream";
                //    body: ''
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //dynamic obj = JsonConvert.DeserializeObject(resp);
                //body = JSON.parse(body);
                if (Convert.ToInt32(response.StatusCode) == 401)
                {
                    return "Unauthorized : Invalid token " + response.StatusCode;
                }
                else if (Convert.ToInt32(response.StatusCode) >= 400)
                {
                    // return obj.error.message + response.StatusCode;
                    //return done(body.message, null, response.statusCode);
                }
                else
                {
                    //    return obj. + response.StatusCode;
                    //    return done(null, body, response.statusCode);
                }
                return request.ToString();
            }
            catch (Exception E)
            {
                return E.Message.ToString();
            }
        }
    //module.exports = HttpService;
    }
}
