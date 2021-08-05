using ApiService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiService
{
    public class Api
    {
        private CRateModel rates;

        public Api(){}

        public CRateModel ApiCall()
        {
            var uri = "http://api.exchangeratesapi.io/v1/latest?access_key=63d197c01c5a7a1c5b26b5794c326ee0";
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        if (stream == null) return null;
                        using (StreamReader streamReader = new StreamReader(stream))
                        {
                            string responseBody = streamReader.ReadToEnd();
                            JObject json = JObject.Parse(responseBody);
                            rates = JsonConvert.DeserializeObject<CRateModel>(responseBody);
                            return rates;
                        }
                    }
                }
            }
            catch (WebException) { return null; }
        }

    }
}
