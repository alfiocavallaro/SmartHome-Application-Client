using System;
using System.IO;
using System.Net;

namespace cavallaro.tesi.smartedifice
{
    public class Request
    {
        public static string newRequest(String url, String method)
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create(url);

            request.Method = method;
            request.Timeout = 10000;

            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            return responseFromServer;
        }

    }
}
