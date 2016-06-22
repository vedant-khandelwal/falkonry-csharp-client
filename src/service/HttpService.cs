///
/// falkonry-csharp-client
/// Copyright(c) 2016 Falkonry Inc
/// MIT Licensed
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;

namespace falkonry_csharp_client.service
{
    public class HttpService
    {
        private string host;
        private string token;
        private string user_agent="falkonry/csharp-client";

        public HttpService(string host, string token)
        {
          
          
            this.host = host == null ? "https://service.falkonry.io" : host;
            
            this.token = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
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
                
                string resp = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if ( Convert.ToInt32(response.StatusCode) == 401 )
                {
                    return "Unauthorized : Invalid token " + Convert.ToString(response.StatusCode);
                }
                else if ( Convert.ToInt32(response.StatusCode) >= 400 )
                {
                    return Convert.ToString(response.StatusDescription);
                }
                else
                {
                    return resp;
                }
                
                 
            
                }
            catch ( Exception E)
            {
                return E.Message.ToString();
            }
        
       }

        public string post (string path,object data)
        {
            var resp = "";
            try 
            {
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token "+this.token);
			    request.Method = "POST";
                request.ContentType = "application/json";
                
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    
                    string restoWrite = serializer.Serialize(data);
                    
                    streamWriter.Write(restoWrite);
                    
                    streamWriter.Flush();
                    
                    streamWriter.Close();
                }
                
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                
                resp = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (Convert.ToInt32(response.StatusCode) == 401)
                {
                    return "Unauthorized : Invalid token " + Convert.ToString(response.StatusCode);
                }
                else if (Convert.ToInt32(response.StatusCode) >= 400)
                {
                    return Convert.ToString(response.StatusDescription);
                }
                else
                {
                    return resp;
                }
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
                
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    //initiate the request
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string restoWrite = serializer.Serialize(pipeline);
                    streamWriter.Write(restoWrite);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                
                var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
                
                
                if (Convert.ToInt32(response.StatusCode) == 401)
                {
                    return "Unauthorized : Invalid token " + Convert.ToString(response.StatusCode);
                }
                else if (Convert.ToInt32(response.StatusCode) >= 400)
                {
                    return Convert.ToString(response.StatusDescription);
                }
                else
                {
                    return resp;
                }
            }
            catch ( Exception E)
            {
                return E.Message.ToString();
            }
        }
        /*
        public string sfpost (string path,string data)
        {   //done
            try 
            {
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token "+this.token);
			    request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] byteArray = Encoding.UTF8.GetBytes(data);
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

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
        */
        async public Task<string> fpost (string path,SortedDictionary<string,string> options,byte[] stream)
        {
            try 
            {
                //Debug.WriteLine("Token is+++++="+this.token);
                Random rnd = new Random();
                string random_number = Convert.ToString(rnd.Next(1, 200));
                //HttpClient httpClient = new HttpClient();
                string temp_file_name = "";
                var url = this.host + path;
                
                
                string sd = "";
                HttpClient client = new HttpClient();               
                    Debug.WriteLine("Print 1");
                
                client.DefaultRequestHeaders.Add("Authorization", "Token "+this.token);
                using (MultipartFormDataContent form = new MultipartFormDataContent())
                    {
                        
                        
                        form.Add(new StringContent(options["name"]), "name");
                        
                        form.Add(new StringContent(options["timeIdentifier"]), "timeIdentifier");
                        
                        form.Add(new StringContent(options["timeFormat"]), "timeFormat");
                        
                        if (stream != null)
                        {
                        
                        temp_file_name = "input" + random_number + "." + options["fileFormat"];
                        
                        ByteArrayContent bytearraycontent = new ByteArrayContent(stream);
                        bytearraycontent.Headers.Add("Content-Type", "text/"+options["fileFormat"]);
                        form.Add(bytearraycontent, "data", temp_file_name);
                        }
                        
                        var result = client.PostAsync(url, form).Result;
                        Debug.WriteLine("Print 9");
                        sd =await result.Content.ReadAsStringAsync();
                        

                    }

                    return sd; 
            
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
                
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
                if (Convert.ToInt32(response.StatusCode) == 401)
                {
                    return "Unauthorized : Invalid token " + Convert.ToString(response.StatusCode);
                }
                else if (Convert.ToInt32(response.StatusCode) >= 400)
                {
                    return Convert.ToString(response.StatusDescription);
                }
                else
                {
                    return resp;
                }
            }
            catch ( Exception E)
            {
                return E.Message.ToString();
            }
        }

        public string upstream(string path,byte[] data)
        {
            try
            {
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);

                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method="POST";
                request.Headers.Add("Authorization", "Token " + this.token);
                request.ContentType = "text/plain";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = data.Length;
                // Get the request stream.
               
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
               
                dataStream.Write(data, 0, data.Length);
                // Close the Stream object.
               
                dataStream.Close();
                // Get the response.
               
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                
                var resp = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Debug.WriteLine(resp);
                if (Convert.ToInt32(response.StatusCode) == 401)
                {
                    return "Unauthorized : Invalid token " + Convert.ToString(response.StatusCode);
                }
                else if (Convert.ToInt32(response.StatusCode) >= 400)
                {
                    return Convert.ToString(response.StatusDescription);
                }
                else
                {
                    return resp;
                }
            }
            catch (Exception E)
            {
                return E.Message.ToString();
            }
        }

        public Stream downstream(string path)
        {
            try
            {
                var url = this.host + path;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token " + this.token);
                
                request.Method = "GET";
                request.ContentType = "application/x-json-stream";
               
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var resp = response.GetResponseStream();
                
                return resp; 
            }
            catch (Exception E)
            {
                Debug.WriteLine("Error aa gaya downstream mein");
                return null;
            }
        }
        
        public string postData(string path, string data)
        {
            string resp = "";
            try { 
                var url = this.host + path;
                
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers.Add("Authorization", "Token " + this.token);
                request.Method = "POST";
                request.ContentType = "text/plain";
                
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    //initiate the request
                    streamWriter.Write(data);
                    
                    streamWriter.Flush();
                    
                    streamWriter.Close();
                }
                
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                
                resp = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (Convert.ToInt32(response.StatusCode) == 401)
                {
                    return "Unauthorized : Invalid token " + Convert.ToString(response.StatusCode);
                }
                else if (Convert.ToInt32(response.StatusCode) >= 400)
                {
                    return Convert.ToString(response.StatusDescription);
                }
                else
                {
                    return resp;
                }
            }
            catch (Exception E)
            {
                

                
                return E.Message.ToString();
            }
        



        }
    }
}
