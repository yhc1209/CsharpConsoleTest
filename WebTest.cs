using System;
using System.Net;
using System.Net.Http;

namespace testCons
{
    public class WebTest
    {
        public static string LastErrPhrase {get; private set;}
        public static int LastErrCode {get; private set;}
        public static bool Test(string url)
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage resp = client.GetAsync(url).Result;
                LastErrCode = ((int)resp.StatusCode);
                LastErrPhrase = resp.ReasonPhrase;
                return resp.IsSuccessStatusCode;
            }
            catch (AggregateException e)
            {
                if (e.InnerExceptions.Count == 1)
                {
                    LastErrPhrase = e.InnerExceptions[0].Message;
                    LastErrCode = e.InnerExceptions[0].HResult;
                }
                else
                {
                    Console.WriteLine("multi!!");
                    LastErrPhrase = e.InnerException.Message;
                    LastErrCode = e.InnerException.HResult;
                }
            }
            catch (Exception e)
            {
                LastErrPhrase = e.Message;
                LastErrCode = e.HResult;
            }
            return false;
        }
    }
}