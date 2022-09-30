using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace testCons
{
    /// <remarks>acknowledge HuanLinTalk</remarks>
    public class AsyncTest
    {
        private static int count = 1;
        public static void Log(int num, string msg)
        {
            Console.WriteLine("({0}|{3}) T{1}: {2}", 
                num, Thread.CurrentThread.ManagedThreadId, msg, count++);
        }

        public static async Task Main()
        {
            Log(1, "正要起始非同步工作 MyDownloadPageAsync()。");

            var task = MyDownloadPageAsync("https://www.huanlintalk.com");

            Log(4, "已從 MyDownloadPageAsync() 返回，但尚未取得工作結果。");

            string content = await task;

            Log(7, "已經取得 MyDownloadPageAsync() 的結果。");

            Console.WriteLine("網頁內容總共為 {0} 個字元。", content.Length);
        }

        public static async Task<string> MyDownloadPageAsync(string url)
        {
            Log(2, "正要呼叫 WebClient.DownloadStringTaskAsync()。");

            using (var webClient = new WebClient())
            {
                var task = webClient.DownloadStringTaskAsync(url);

                Log(3, "已起始非同步工作 DownloadStringTaskAsync()。");

                string content = await task;

                Log(5, "已經取得 DownloadStringTaskAsync() 的結果。");

                // await FuncAsync();

                // Log(6, "out out結束");

                return content;
            }
        }

        public static async Task<int> FuncAsync()
        {
            await Task.Run(
                () => Console.WriteLine("out out")
            );
            await Task.Delay(500);
            Log(7, "out2");
            return 123;
        }
    }
}