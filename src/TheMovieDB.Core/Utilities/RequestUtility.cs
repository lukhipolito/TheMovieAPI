using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TheMovieDB.Core.Utilities
{
    public static class RequestUtility
    {
        public static string Post(string urlService, string data = "", Hashtable header = null, ICredentials credentials = null, Encoding codificacao = null, string contentType = "application/json; charset=utf-8")
        {
            return send(urlService, header, credentials, "POST", data, codificacao ?? Encoding.UTF8, contentType);
        }
        public static string Get(string url, Hashtable header = null, ICredentials credentials = null, Encoding codificacao = null, string contentType = "application/json; charset=utf-8")
        {
            return send(url, header, credentials, "GET", null, codificacao ?? Encoding.UTF8, contentType);
        }
        public static string Put(string urlService, string data = "", Hashtable header = null, ICredentials credentials = null, Encoding codificacao = null, string contentType = "application/json; charset=utf-8")
        {
            return send(urlService, header, credentials, "PUT", data, codificacao ?? Encoding.UTF8, contentType);
        }

        public static string Delete(string url, Hashtable header = null, ICredentials credentials = null, Encoding codificacao = null, string contentType = "application/json; charset=utf-8")
        {
            return send(url, header, credentials, "DELETE", null, codificacao ?? Encoding.UTF8, contentType);
        }

        private static string send(string url, Hashtable header, ICredentials credentials, string method, string data, Encoding codificacao, string contentType)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            HttpWebRequest requisicao = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            requisicao.Method = method;
            requisicao.ContentType = contentType;
            requisicao.Headers.Add("Accept-Encoding", "gzip,deflate");
            if (credentials != null)
                requisicao.Credentials = credentials;
            requisicao.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            if (header != null)
            {
                foreach (DictionaryEntry hash in header)
                {
                    if (hash.Key.ToString() == "ContentType")
                    {
                        requisicao.ContentType = hash.Value.ToString();
                        continue;
                    }
                    requisicao.Headers.Add(hash.Key.ToString(), hash.Value.ToString());
                }
            }
            bool hasData = (method == "POST" || method == "PATCH" || method == "PUT");
            byte[] bytes = null;
            if (hasData)
            {
                if (data == null)
                    data = "";
                string postData = data;
                bytes = codificacao.GetBytes(postData);
                requisicao.ContentLength = bytes.Length;
            }

            if (!hasData)
            {
                using (var response = requisicao.GetResponse())
                {
                    using (var content = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(content, codificacao))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            else
            {
                using (var reqstream = requisicao.GetRequestStream())
                {
                    reqstream.Write(bytes, 0, bytes.Length);
                    var httpResponse = (HttpWebResponse)requisicao.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream(), codificacao))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }


    }
}
