using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using MongoDB.Bson;

namespace YPIConnect
{
    public class APIRequestHelper
    {
        public static JObject GetTokenMessage(string userName, string password, string authenticatorToken)
        {
            return new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "authenticationRequest"),
                new JProperty("id", ObjectId.GenerateNewId().ToString()),
                new JProperty("params", new JArray(
                    new JObject(
                        new JProperty("authenticationRequest", new JObject(
                            new JProperty("target", "webServiceAccount"),
                            new JProperty("method", "getToken"),
                            new JProperty("userName", userName),
                            new JProperty("password", password),
                            new JProperty("authenticatorToken", authenticatorToken)
                            ))
                    )
                ))
            );
        }

        public static JObject GetQRCodeMessage(string userName, string password)
        {
            return new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "authenticationRequest"),
                new JProperty("id", ObjectId.GenerateNewId().ToString()),
                new JProperty("params", new JArray(
                    new JObject(                        
                        new JProperty("authenticationRequest", new JObject(
                            new JProperty("target", "webServiceAccount"),
                            new JProperty("method", "getQRCode"),
                            new JProperty("userName", userName),
                            new JProperty("password", password)
                            ))
                    )
                ))
            );
        }

        public static JObject SubmitAPIRequestMessage(JObject requestMessage)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.1.2.90:3000");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string msg = requestMessage.ToString();
                streamWriter.Write(requestMessage.ToString());
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                JObject apiResult = JObject.Parse(streamReader.ReadToEnd());
                return apiResult;
            }
        }
    }
}
