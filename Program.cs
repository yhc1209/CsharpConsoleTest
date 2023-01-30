using System;
using System.IO;
using System.IO.Pipes;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net;
using CommandLine;

namespace testCons
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DateTime? dt1 = null;//new DateTime(2022, 12, 09, 12, 12, 9);
                DateTime? dt2 = new DateTime(2022, 12, 09, 12, 12, 10);
                double diff = (dt1 - dt2).Value.TotalMilliseconds;
                Console.WriteLine($"dt1 - dt2 = {Math.Abs(diff)} (ms)");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType()} - {e.Message}");
                Console.WriteLine(e.StackTrace);
            }
        }

        static void func0112()
        {
            Task
        }

        static void func0112B(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Task.Delay(1000, token);
                Console.WriteLine($"[{DateTime.}]")
            }
        }

        static void func0111z()
        {
            string[] MailList = {"abcd83129@gmail.com"};
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            //收件者，以逗號分隔不同收件者 ex "test@gmail.com,test2@gmail.com"
            msg.To.Add(string.Join(",", MailList));
            msg.From = new System.Net.Mail.MailAddress("test2@gmail.com", "測試郵件", System.Text.Encoding.UTF8);
            //郵件標題 
            msg.Subject = "有收到嗎?";
            //郵件標題編碼  
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            //郵件內容
            msg.Body = "<p>Hello world.</p>";
            msg.IsBodyHtml = true;
            msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼 
            msg.Priority = System.Net.Mail.MailPriority.Normal;//郵件優先級 
            //建立 SmtpClient 物件 並設定 Gmail的smtp主機及Port 
            #region 其它 Host
            /*
            *  outlook.com smtp.live.com port:25
            *  yahoo smtp.mail.yahoo.com.tw port:465
            */
            #endregion

            System.Net.Mail.SmtpClient MySmtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            //設定你的帳號密碼
            MySmtp.Credentials = new System.Net.NetworkCredential("How@TrustONE.com.tw", "");
            //Gmial 的 smtp 使用 SSL
            MySmtp.EnableSsl = true;
            MySmtp.Send(msg);
        }
        static void func0110()
        {
            string str = "AcBbCa";
            int Times = 100;
            int tic = Environment.TickCount;
            for (int i = 0; i < Times; i++)
                str.ToLower();
            int toc = Environment.TickCount - tic;
            Console.WriteLine($"{Times} times string.toLower() costs {toc} ms.");

            // string path = "\\\\192.168.9.41";
            // string s1 = System.IO.Path.GetDirectoryName(path);
            // string s2 = System.IO.Path.GetFileName(path);
            // Console.WriteLine($"s1={s1}\ns2={s2}");
        }
        static void func0106b()
        {
            List<J4T> arr = new List<J4T> {
                new J4T {id = 3, str = "help"},
                new J4T {id = 5, str = "see"}
            };
            // J4T[] arr = {
            //     new J4T {id = 3, str = "help"},
            //     new J4T {id = 5, str = "see"}
            // };

            func0106c(arr[1]);
            Console.WriteLine(arr[1].str);
        }
        static void func0106c(J4T obj)
        {
            obj.str = "emo";
        }
        static void func0106()
        {

            ushort[] arr1 = null;
            ushort[] arr2 = {};
            // method 2
            string arr1s = (arr1==null?"":string.Join(',', arr1));
            string arr2s = (arr2==null?"":string.Join(',', arr2));
            if (arr1s == arr2s)
                Console.WriteLine("same");
            else
                Console.WriteLine($"diiferent (arr1s={arr1s}; arr2s={arr2s})");
            // method 1
            // if (arr1?.Length == arr2?.Length)
            // {
            //     Console.WriteLine("in");
            //     if (arr1?.Length > 0)
            //     {
            //         if (string.Join(',', arr1) != string.Join(',', arr2))
            //         {
            //             Console.WriteLine("different");
            //             return;
            //         }
            //     }
            //     Console.WriteLine("same");
            // }
            // else
            //     Console.WriteLine("different");

            // // string str = "alpha;bravo;charlie;delta";
            // string str = null;
            // string[] ss = str?.Split(';');
            // if (ss == null)
            //     Console.WriteLine("ss is null");
            // else
            //     Console.WriteLine($"split:[{string.Join(',', ss)}]");
                
            // int i = 0;
            // int? ii = null;
            // if (ii < i)
            //     Console.WriteLine("good");
            // else
            //     Console.WriteLine("bad");
        }

        static void func0104()
        {
            try
            {
                // Console.WriteLine("args: " + string.Join(", ", args));
                Console.WriteLine("start");
                string str = null;
                // string str = "abcd";
                int? len = str?.Length;
                Console.WriteLine($"{(len==null?"null":len)} > 3 ? {len > 3}");

                Console.WriteLine($"(int)len = {(int)len}");    // InvalidOperationException thrown while len is null

                // [conclusion] 
                // 1. null可以跟數字比大小，結果一定是false
                // 2. ?.滿好用
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType()} - {e.Message}");
            }
        }

        // arraysegment test
        static void func1230()
        {
            string[] arr1 = {"alpha", "bravo", "charlie", "delta", "echo", "foxtrot", "golf", "hotel"};
            // ArraySegment<string> seg1 = new ArraySegment<string>(arr1, 2, 6);
            ArraySegment<string> seg1 = arr1;

            foreach (string e in seg1)
                Console.WriteLine(e);

            // Console.WriteLine("slice at index 1.");
            // seg1 = seg1.Slice(1);
            // foreach (string e in seg1)
            //     Console.WriteLine(e);

            // Console.WriteLine("arr2:");
            // string[] arr2 = seg1.ToArray();
            // foreach (string e in arr2)
            //     Console.WriteLine(e);

            // Console.WriteLine("arr3':");
            // string[] arr3 = seg1.Array;
            // foreach (string e in arr3)
            //     Console.WriteLine(e);
        }

        static void func1227(string path)
        {
            if (path.Length == 0)
                path = Environment.GetEnvironmentVariable("SYSTEMDRIVE");
            Console.WriteLine($"path: {path}");
            PathNode node = PathNode.GetNodeInfo(path, false);
            foreach (PathNode pn in node.Children)
                Console.WriteLine($" - {pn.Path}\\{pn.Name}{(pn.IsFile?"":"\\")}");
        }

        static void func1226C(string ipa, string ipb)
        {
            // IPAddress ip = IPAddress.Parse(input);
            // Console.WriteLine($"{ip} =>");
            // Console.WriteLine($"<{string.Join('|', ip.GetAddressBytes())}>");

            // format check
            IPAddress ipBg, ipEd;
            if (!IPAddress.TryParse(ipa, out ipBg) || !IPAddress.TryParse(ipb, out ipEd))
            {
                Console.WriteLine("Failed to parse.");
                return;
            }

            if (ipBg.AddressFamily != ipEd.AddressFamily)
            {
                Console.WriteLine("起始位址與結束位址格式不一致。");
                return;
            }

            // range check
            byte[] ip1 = ipBg.GetAddressBytes();
            byte[] ip2 = ipEd.GetAddressBytes();
            bool bIp1IsSmaller = false;
            for (int i = 0; i < ip1.Length; i += 4)
            {
                uint ip1s = BitConverter.ToUInt32(ip1, i);
                uint ip2s = BitConverter.ToUInt32(ip2, i);
                if (bIp1IsSmaller = (ip1s < ip2s))
                    break;
            }

            if (bIp1IsSmaller)
                Console.WriteLine($"[DlgIPRang] {ipBg} to {ipEd}");
            else
                Console.WriteLine($"[DlgIPRang] {ipEd} to {ipBg}");

            Console.WriteLine($"{ipBg} =v4=> {ipBg.MapToIPv4()}");
            Console.WriteLine($"{ipBg} =v6=> {ipBg.MapToIPv6()}");
        }

        static void func1226B(string input)
        {
            Console.WriteLine($"input: {input}");
            
            string output = string.Empty;
            if (!input.Contains("://"))
            {
                if (Uri.CheckHostName(input) == UriHostNameType.IPv6)
                {
                    if (input.StartsWith('[') && input.EndsWith(']'))
                        input = $"https://{input}";
                    else
                        input = $"https://[{input}]";
                }
            }
            Console.WriteLine($"input => {input}");
            Uri uri = new UriBuilder(input).Uri;
            Console.WriteLine($"// uri scheme       : {uri.Scheme}");
            Console.WriteLine($"// uri Authority    : {uri.Authority}");
            // protocol
            if (uri.Scheme == "http" && !input.Contains("http://"))
                output += "https://";
            else
                output += $"{uri.Scheme}://";
            // hostname
            output += uri.Host;
            // port
            if (uri.IsDefaultPort && !input.Contains($":{uri.Port}"))   // 若使用者輸入port有在前面補0會錯
                output += ":9443";
            else
                output += $":{uri.Port}";
            // path
            if (uri.AbsolutePath == "/")
                output += "/api/client";
            else
                output += uri.AbsolutePath;
            
            Console.WriteLine($"output: {output}");
        }
        static void func1226(string input)
        {
            // #region 自動轉換URL
            // string[] bufs;
            // string rest, protocol = string.Empty, ip = string.Empty, port = string.Empty, path = string.Empty;
            // rest = input;

            // if (rest.Contains("://"))
            // {
            //     bufs = rest.Split("://");   // [protocol]://[ip:port/path]
            //     protocol = bufs[0];
            //     rest = rest.Substring(protocol.Length + 3);
            // }

            // if (rest.Contains('/'))
            // {
            //     bufs = rest.Split('/');     // [ip:port]/[path]
            //     path = rest.Substring(bufs[0].Length + 1);
            //     rest = bufs[0];
            // }

            // if (rest.Contains(':'))
            // {
            //     bufs = rest.Split(':');     // [ip]:[port]
            //     port = bufs[1];
            //     rest = bufs[0];   
            // }

            // ip = rest;
            // if (protocol == string.Empty)
            //     protocol = "https";
            // if (port == string.Empty)
            //     port = "9443";
            // if (path == string.Empty)
            //     path = "api/client";

            // string url = $"{protocol}://{ip}:{port}/{path}";
            // #endregion

            Console.WriteLine($"scheme ? {Uri.CheckSchemeName(input)}");
            Console.WriteLine($"HostNameType ? {Uri.CheckHostName(input)}");
            // if (Uri.CheckHostName(input) == UriHostNameType.IPv6)
            // {
            //     if (!input.Contains('['))
            //         input = $"[{input}]";
            // }
            // else
            // {
            //     int idx = input.IndexOf("://");

            // }
            // if (!Uri.CheckSchemeName(input))
            //     input = $"https://{input}";

            // Uri uri = new Uri(input);
            Uri uri = new UriBuilder(input).Uri;

            // Console.WriteLine($"uri.Fragment={uri.Fragment}");
            // Console.WriteLine($"uri.Segments={uri.Segments}");
            // Console.WriteLine($"uri.UserInfo={uri.UserInfo}");
            // Console.WriteLine($"uri.UserEscaped={uri.UserEscaped}");
            // Console.WriteLine($"uri.Authority={uri.Authority}");
            // Console.WriteLine($"uri.DnsSafeHost={uri.DnsSafeHost}");
            // Console.WriteLine($"uri.IdnHost={uri.IdnHost}");
            Console.WriteLine($"uri.Host={uri.Host}");
            Console.WriteLine($"uri.HostNameType={uri.HostNameType}");
            Console.WriteLine($"uri.Scheme={uri.Scheme}");
            Console.WriteLine($"uri.Port={uri.Port}");
            Console.WriteLine($"uri.IsDefaultPort={uri.IsDefaultPort}");
            // Console.WriteLine($"uri.IsFile={uri.IsFile}");
            // Console.WriteLine($"uri.LocalPath={uri.LocalPath}");
            Console.WriteLine($"uri.AbsolutePath={uri.AbsolutePath}");
            // Console.WriteLine($"uri.PathAndQuery={uri.PathAndQuery}");
            // Console.WriteLine($"uri.Query={uri.Query}");
            Console.WriteLine($"uri.OriginalString={uri.OriginalString}");
            Console.WriteLine($"uri.ToString()={uri.ToString()}");

            Console.WriteLine(uri.AbsoluteUri);
        }

        static void func1221C()
        {
            // DateTime dt = DateTime.UtcNow;
            // DateTime dt = DateTime.Parse("2022/12/21 04:20:00");
            DateTime dt = new DateTime(2022, 12, 21, 04, 20, 00, DateTimeKind.Local);
            Console.WriteLine($"UTC time: {dt}, kind={dt.Kind}");
            Console.WriteLine(TimeZoneInfo.Local);
            // foreach (TimeZoneInfo info in TimeZoneInfo.GetSystemTimeZones())
            //     Console.WriteLine(info);

            // Console.WriteLine(TimeZoneInfo.ConvertTimeFromUtc(dt, TimeZoneInfo.Local));
            Console.WriteLine(dt + TimeZoneInfo.Local.BaseUtcOffset);
        }
        static void func1221B()
        {
            string jstxt = "{\"id\":523,\"flag\":true,\"str\":\"just for test\",\"obj\":{\"Name\":\"apple\",\"Number\":33}}";
            try
            {
                Dictionary<string, object> dict = JsonSerializer.Deserialize<Dictionary<string, object>>(jstxt);
                foreach (string k in dict.Keys)
                {
                    Console.WriteLine($"{k,5} >> {dict[k]}");
                }
            }
            catch (Exception excp)
            {
                Console.WriteLine($"[{excp.GetType().Name}] - {excp.Message}");
            }
        }
        static void func1221()
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("apple 1");
            queue.Enqueue("apple 2");
            queue.Enqueue("apple 3");

            // // good
            // string dq;
            // while (queue.TryDequeue(out dq))
            //     Console.WriteLine($"pop: {dq}");

            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Dequeue()); // InvalidOperationException: Queue empty
        }

        static void func1213()
        {
            string jstxt = "{\"id\":523,\"flag\":true,\"str\":\"just for test\",\"obj\":{\"Name\":\"apple\",\"Number\":33}}";
            try
            {
                J4T j1 = JsonSerializer.Deserialize<J4T>(jstxt);
                // J4T j1 = Newtonsoft.Json.JsonConvert.DeserializeObject<J4T>(jstxt);

                jstxt = JsonSerializer.Serialize<J4T>(j1);
                Console.WriteLine(jstxt);
            }
            catch (Exception excp)
            {
                Console.WriteLine($"[{excp.GetType().Name}] - {excp.Message}");
            }
            // [結論] Newtonsoft.Json可以接受\t
        }

        static void func1201()
        {
            // // precondition
            // J4T j = new J4T {
            //     flag = true, obj = new C1 {Name = "apple", Number = 33}, str = "just for test"
            // };
            // string jstxt = JsonSerializer.Serialize<J4T>(j);
            // Console.WriteLine(jstxt);

            // [test] 同名不同型態的屬性做JSON parse
            // string jstxt = "{\"id\":2,\"flag\":true,\"str\":\"just for test\",\"obj\":{\"Name\":\"apple\",\"Number\":33}}";
            string jstxt = "{}";
            try
            {
                J4T j1 = JsonSerializer.Deserialize<J4T>(jstxt);
                jstxt = JsonSerializer.Serialize<J4T>(j1);
                Console.WriteLine(jstxt);
            }
            catch (Exception excp)
            {
                Console.WriteLine($"[{excp.GetType().Name}] - {excp.Message}");
            }
        }

        static void func1129A()
        {
            string strA = "hello", strB = null;
            if ((strB = strA) == null)
                Console.WriteLine($"It is null! strA={(strA==null?"null":strA)} strB={(strB==null?"null":strB)}");
            else
                Console.WriteLine($"It is not null! strA={(strA==null?"null":strA)} strB={(strB==null?"null":strB)}");
        }

        static void func1129(string path)
        {
            // PathNode root = PathNode.GetNodeInfo(path, false);
            // root.GetChildrenInfo(false);
            // Console.WriteLine($"{root.Path}\\{root.Name}{(root.IsFile?"":"\\")}");
            // if (root.IsFile)
            //     return;

            // foreach (PathNode node in root.Children)
            // {
            //     node.GetChildrenInfo(false);
            //     Console.WriteLine($"  {(node==root.Children[^1]?"└":"├")} {node.Name}{(node.IsFile?"":"\\")}");
            //     if (node.IsFile)
            //         continue;
                    
            //     foreach (PathNode nn in node.Children)
            //         Console.WriteLine($"  {(node==root.Children[^1]?" ":"│")}   {(nn==node.Children[^1]?"└":"├")} {nn.Name}{(nn.IsFile?"":"\\")}");
            // }

            // JsonSerializerOptions op = new JsonSerializerOptions { WriteIndented = true };
            // string jstxt = JsonSerializer.Serialize<PathNode>(root, op);
            // File.WriteAllText("test1129.json", jstxt);
        }

        static void func1128A()
        {
            string jsontxt = "{\"flag\":true,\"str\":null}";
            J4T obj = JsonSerializer.Deserialize<J4T>(jsontxt);
            Console.WriteLine($"id={obj.id} flag={obj.flag} str={(obj.str==null?"null":obj.str)}");
        }

        static void func1128()
        {
            byte[] package = new byte[6];
            BitConverter.GetBytes((ushort)31415).CopyTo(package, 0);
            BitConverter.GetBytes(1).CopyTo(package, 2);
            
            byte[] response;
            if (sendNreceive(package, out response))
            {
                Aes _aes = Aes.Create();
                _aes.Mode = CipherMode.CBC;
                _aes.Padding = PaddingMode.PKCS7;
                _aes.Key = Encoding.ASCII.GetBytes("w-3L49!`12AS$420=+jsHS30keJs52zd");
                _aes.IV = Encoding.ASCII.GetBytes("SDefenseCryption");
                byte[] plain = _aes.CreateDecryptor().TransformFinalBlock(response, 2, response.Length - 2);

                string jstxt = Encoding.UTF8.GetString(plain);
                Console.WriteLine(jstxt);
            }
        }

        /// <summary>發送與接收。</summary>
        /// <param name="requst">要傳送的資訊。包含型態(SDSVC_CMD)與內容。</param>
        /// <param name="response">回傳的資訊。包含型態(SDSVC_CMD)與內容，若與request型態不相符或長度錯誤則會回傳null。</param>
        /// <remarks>requst只需指定訊息型態與內容，此函式會自動加上封包長度與session ID。</remarks>
        private static bool sendNreceive(byte[] requst, out byte[] response)
        {
            string _pipename = "StealthDefense3_ServicePipe";
            int _sid = Process.GetCurrentProcess().SessionId;
            try
            {
                // header adding (package length and session id)
                byte[] packageH = new byte[requst.Length + 8];
                BitConverter.GetBytes(packageH.Length - 4).CopyTo(packageH, 0);
                BitConverter.GetBytes(_sid).CopyTo(packageH, 4);
                requst.CopyTo(packageH, 8);

                // check namedpipe existing
                if (Directory.GetFiles("\\\\.\\pipe\\", _pipename).Length == 0)
                    throw new Exception($"namedpipe \'{_pipename}\' not found.");

                using (NamedPipeClientStream pipe = new NamedPipeClientStream(_pipename))
                {
                    pipe.Connect(500);
                    // send
                    pipe.Write(packageH, 0, packageH.Length);

                    // receive
                    response = new byte[4];
                    pipe.Read(response, 0, 4);
                    int len = BitConverter.ToInt32(response, 0);
                    Array.Resize(ref response, len);
                    if (pipe.Read(response, 0, len) != len)
                        throw new Exception("Response length is not match.");
                    if (requst[0] != response[0] || requst[1] != response[1])
                        throw new Exception("Response type is not match.");
                }

                return true;
            }
            catch (Exception excp)
            {
                response = null;
                // _lastExcp = excp;
                // SDSVC_CMD type = (SDSVC_CMD)BitConverter.ToUInt16(requst, 0);
                // _log.LogExcp($"Failed to communicate with SD_service.\nMessge Type: {type}", excp, EventLogEntryType.Error);
                return false;
            }
        }

        static void func1125()
        {
            int[] arr = {1, 2, 3};

            if (arr[3] > 0)
                Console.WriteLine("ooo");
            else
                Console.WriteLine("xxx");
        }

        static void func1118()
        {
            string filename = "data\\dir1\\textfile1118.txt";
            // // 前置作業
            // using (StreamWriter sw = new StreamWriter(filename, false, Encoding.Unicode))
            // {
            //     for (int i = 0; i < 10; i++)
            //         sw.WriteLine($"text #{i}");
            //     sw.Flush();

            //     Console.WriteLine($"sw.BaseStream.Length: {sw.BaseStream.Length}, sw.BaseStream.Position: {sw.BaseStream.Position}");
            // }
            
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Unicode))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {
                    string line = sr.ReadLine();
                    Console.WriteLine($"sr reads one line: \'{line}\'");
                    Console.Write($"sr.BaseStream.Position: {sr.BaseStream.Position}, ");
                    Console.Write($"sw.BaseStream.Position: {sw.BaseStream.Position}, ");
                    Console.Write($"fs.Position: {fs.Position}");
                    Console.WriteLine(Environment.NewLine);

                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    Console.WriteLine("sw seeks to end of file.");
                    Console.Write($"sr.BaseStream.Position: {sr.BaseStream.Position}, ");
                    Console.Write($"sw.BaseStream.Position: {sw.BaseStream.Position}, ");
                    Console.Write($"fs.Position: {fs.Position}");
                    Console.WriteLine(Environment.NewLine);

                    sw.WriteLine($"hello, world.");
                    sw.Flush();
                    Console.WriteLine("sw writes a line.");
                    Console.Write($"sr.BaseStream.Position: {sr.BaseStream.Position}, ");
                    Console.Write($"sw.BaseStream.Position: {sw.BaseStream.Position}, ");
                    Console.Write($"fs.Position: {fs.Position}");
                    Console.WriteLine();
                    Console.Write($"sr.BaseStream.Length: {sr.BaseStream.Length}, ");
                    Console.Write($"sw.BaseStream.Length: {sw.BaseStream.Length}, ");
                    Console.Write($"fs.Length: {fs.Length}");
                    Console.WriteLine(Environment.NewLine);
                }
            }
        }

        static void func1116C()
        {
            string zipFile = @"data\dir1\texts.zip";
            using (FileStream fs = new FileStream(zipFile, FileMode.Open))
            {
                using (ZipArchive zipArchive = new ZipArchive(fs, ZipArchiveMode.Update))
                {
                    foreach (ZipArchiveEntry entry in zipArchive.Entries)
                    {
                        ZipArchiveEntry ee = entry;
                        ee.Delete();
                        Console.WriteLine($"{ee.Name}已刪除。");
                        break;
                        // using (StreamReader sr = new StreamReader(entry.Open()))
                        // {
                        //     Console.WriteLine($"[{entry.Name}] {entry.LastWriteTime}");
                        //     while (!sr.EndOfStream)
                        //         Console.WriteLine(sr.ReadLine());
                        //     Console.WriteLine($"{entry.FullName}");
                        // }
                        // Console.WriteLine();
                    }
                    Console.WriteLine("end");
                }
            }
        }

        static void func1116B()
        {
            FileStream fs = new FileStream("file1.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("Hello world.");
            sw.Flush();
            Console.WriteLine($"file length: {sw.BaseStream.Length}");
            sw.Close();
            sw.Dispose();
            Console.WriteLine("end");
            // [結論] sw.Close()不會關閉fs
        }

        static void func1116()
        {
            string orgFile1 = @"data\dir1\text1.txt";
            string orgFile2 = @"data\dir1\text2.txt";
            string zipFile = @"data\dir1\texts.zip";

            // 檔案建立壓縮檔(刪掉原始)
            try
            {
                string zipDir = $"{Path.GetDirectoryName(orgFile1)}\\texts";
                Directory.CreateDirectory(zipDir);
                File.Move(orgFile1, $"{zipDir}\\{Path.GetFileName(orgFile1)}");
                ZipFile.CreateFromDirectory(zipDir, zipFile);
                Directory.Delete(zipDir, true);
                Console.WriteLine("建立壓縮檔完成。");
            }
            catch (Exception e)
            {
                Console.WriteLine($"建立壓縮檔失敗！ ({e.GetType().Name} - {e.Message})");
            }

            // 檔案加入壓縮檔(刪掉原始)
            try
            {
                using (FileStream fs = new FileStream(zipFile, FileMode.Open))
                {
                    using (ZipArchive zipArchive = new ZipArchive(fs, ZipArchiveMode.Update))
                    {
                        Console.WriteLine($"entry count before: {zipArchive.Entries.Count}");
                        ZipArchiveEntry zae = zipArchive.CreateEntry(Path.GetFileName(orgFile2));
                        using (StreamWriter sw = new StreamWriter(zae.Open()))
                        {
                            using (StreamReader sr = new StreamReader(orgFile2))
                            {
                                while (!sr.EndOfStream)
                                    sw.WriteLine(sr.ReadLine());
                            }
                        }
                        Console.WriteLine($"entry count after: {zipArchive.Entries.Count}");
                    }
                }
                File.Delete(orgFile2);
                Console.WriteLine("加入壓縮檔完成。");
            }
            catch (Exception e)
            {
                Console.WriteLine($"加入壓縮檔失敗！ ({e.GetType().Name} - {e.Message})");
            }
        }

        static void func1114()
        {
            // // queue test
            // Queue<string> q = new Queue<string>();
            // q.Enqueue("Hello");
            // q.Enqueue("world");
            // q.Enqueue("123");
            // q.Enqueue("456");
            // q.Enqueue("789");

            // JsonSerializerOptions op = new JsonSerializerOptions();
            // op.WriteIndented = true;
            // string js = JsonSerializer.Serialize<Queue<string>>(q, op);
            // Console.WriteLine(js);

            // fileinfo test
            // FileInfo file = new FileInfo(@"data\dir1\file1.txt");
            // file.MoveTo(@"data\dir2\file1.txt");
            // Console.WriteLine($"file path = {file.FullName}");
            // Console.WriteLine(file.OpenText().ReadToEnd());

            DirectoryInfo di = new DirectoryInfo("data\\dir1");
            FileInfo[] files = di.GetFiles("file*.txt");
            foreach (FileInfo file in files)
                file.MoveTo($"data\\dir2\\{file.Name}");

            foreach (FileInfo file in files)
                Console.WriteLine(file.FullName);
        }

        static void func1109(float f)
        {
            Console.WriteLine($"intput: {f}");

            byte[] bf = SwapEndianness(BitConverter.GetBytes(f));
            Console.Write("Hex: 0x");
            foreach (byte b in bf)
                Console.Write($"{b:X02}");
            Console.WriteLine();

            Console.Write("Bin: ");
            string bin = string.Empty;
            foreach (byte b in bf)
            {
                bin += $"{((b & 0x80) >> 7)}{((b & 0x40) >> 6)}{((b & 0x20) >> 5)}{((b & 0x10) >> 4)}";
                bin += $"{((b & 0x08) >> 3)}{((b & 0x04) >> 2)}{((b & 0x02) >> 1)}{((b & 0x01) >> 0)}";
            }
            Console.WriteLine($"{bin.Substring(0, 1)}-{bin.Substring(1, 8)}-{bin.Substring(9)}");
        }

        static byte[] SwapEndianness(byte[] x)
        {
            byte[] y = new byte[x.Length];
            for (int i = 0; i < x.Length; i++)
                y[i] = x[x.Length - i - 1];
            
            return y;
        }

        static void func1108()
        {
            // // func0707();
            // NTPClient ntp = new NTPClient("time.windows.com");
            // // ntp.ShowAllIps();
            // ntp.GetNtpPackage();

            // Console.WriteLine($"------------\n{NTPClient2.GetNetworkTime()}");

            NTPmsg msg = NTPClient1109.GetNtpMsg("time.windows.com", 123, 8);
            Console.WriteLine($"The time: {msg.TheTime.ToString("yyyy/MM/dd-HH:mm:ss.fff")}");
            Console.WriteLine($"System time: {DateTime.Now}");
        }

        static void func1104()
        {
            // J4T j4t = new J4T();
            // j4t.CheckId();
        }

        static void func0930()
        {
            Human joe = new Human{
                Name = "Joe", Age = 27
            };
            // joe.Age = 3;

            string jstxt = JsonSerializer.Serialize<Human>(joe);
            Console.WriteLine(jstxt);
            Human joe_c =  JsonSerializer.Deserialize<Human>(jstxt);
            Console.WriteLine("name={0}, age={1}", joe_c.Name, joe_c.Age);
            Console.WriteLine($"{Environment.GetFolderPath(Environment.SpecialFolder.System)}\\SearchProtocolHost.exe");
        }

        static void func0929()
        {
            AsyncTest.Main().Wait();
        }

        static void func0928()
        {
            List<Cat> cats = new List<Cat>();
            cats.Add(new Cat("Tom", 5));
            cats.Add(new Cat("John", 3));
            cats.Add(new Cat("Tony", 5));
            Cat[] cats2 = cats.ToArray(), cats3;

            string jsontxt = JsonSerializer.Serialize<List<Cat>>(cats);
            Console.WriteLine(jsontxt);
            Console.WriteLine(JsonSerializer.Serialize<Cat[]>(cats2));

            cats3 = JsonSerializer.Deserialize<Cat[]>(jsontxt);
            Console.WriteLine("cats3:");
            foreach (Cat c in cats3)
                Console.WriteLine($" - {c}");
            // [結論] List與Array可以互通
        }

        static void func0923()
        {
            // bool b = true;
            // byte[] bb = BitConverter.GetBytes(b);
            // Console.WriteLine(string.Join<byte>(',', bb));

            // string s = string.Empty;
            // byte[] bs = Encoding.UTF8.GetBytes(s);
            // Console.WriteLine(string.Join<byte>(',', bs));

            Cat catA = new Cat("Amy", 3);
            Cat catB = new Cat("Ben", 4);
            Console.WriteLine($"{catA} -> {catA.GetHashCode()}");
            Console.WriteLine($"{catB} -> {catB.GetHashCode()}");
            Console.WriteLine($"catA.Name -> {catA.Name.GetHashCode()}");
            Console.WriteLine($"catB.Name -> {catB.Name.GetHashCode()}");

            // catB = catA;
            catB.CopyFrom(catA);

            Console.WriteLine($"{catA} -> {catA.GetHashCode()}");
            Console.WriteLine($"{catB} -> {catB.GetHashCode()}");
            Console.WriteLine($"catA.Name -> {catA.Name.GetHashCode()}");
            Console.WriteLine($"catB.Name -> {catB.Name.GetHashCode()}");
        }

        static void func0921()
        {
            string homedrive = Environment.GetEnvironmentVariable("HOMEDRIVE");       // c: 
            Console.WriteLine($"homedrive={homedrive}");
            string windir = Environment.GetEnvironmentVariable("WINDIR");             // c:\windows
            Console.WriteLine($"windir={windir}");
            string programs = Environment.GetEnvironmentVariable("PROGRAMFILES");     // c:\program files
            Console.WriteLine($"programs={programs}");
            string userprofile = Environment.GetEnvironmentVariable("USERPROFILE");   // c:\users\<name>
            Console.WriteLine($"userprofile={userprofile}");
            string appdata = (userprofile + "\\AppData");                             // c:\users\<name>\appdata
            Console.WriteLine($"appdata={appdata}");
            string appdata2 = Environment.GetEnvironmentVariable("appdata");   // c:\users\<name>
            Console.WriteLine($"appdata2={appdata2}");
            string desktop = (userprofile + "\\Desktop");                             // c:\users\<name>\desktop
            Console.WriteLine($"desktop={desktop}");
            string programdata = Environment.GetEnvironmentVariable("PROGRAMDATA");   // c:\programdata
            Console.WriteLine($"programdata={programdata}");
        }

        static void func0916()
        {
            // // string[] bs = null;
            // string[] bs = {"Nancy", "Cindy"};
            // Cat c = new Cat("Tom", 12, bs);
            // Console.WriteLine("no exception.");

            char[] splitor = {'*', '+'};
            string[] strarr = "123* 25.34+48 +".Split(splitor, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine($"strarr.Length = {strarr.Length};");
            Console.WriteLine(">>" + string.Join(',', strarr) + "<<");
        }

        static void func0914()
        {
            // // uint a = uint.MaxValue;  // Exception
            // ushort a = ushort.MaxValue;
            // // uint a = int.MaxValue;   // Exception
            // // int a = int.MaxValue;    // Exception
            // byte[] arr = new byte[a];
            // Console.WriteLine($"a={a}");
            // Console.WriteLine($"arr.Length={arr.Length}");
            
            // uint a = uint.MaxValue;
            // int b = 100;
            // Console.WriteLine($"a={a}; b={b}; a-b={a-b}");
            
            int i = int.MaxValue;
            // int i = 0;
            
            Console.WriteLine($"i={i}; ui={((uint)i)};");
            // [結論] int為正數時強行轉換為uint都可以是正確的數字
        }

        static void func0907()
        {
            // package
            ushort type = 2;
            string folder = "C:\\Path\\to\\the\\folder";
            byte[] bFolder = Encoding.UTF8.GetBytes(folder);
            byte[] package = new byte[6 + bFolder.Length];
            BitConverter.GetBytes(package.Length - 4).CopyTo(package, 0);
            BitConverter.GetBytes(type).CopyTo(package, 4);
            bFolder.CopyTo(package, 6);

            // named pipe
            // NamedPipeClientStream pipe = new NamedPipeClientStream(".", "eventhandleTest0907", PipeDirection.Out);
            NamedPipeClientStream pipe = new NamedPipeClientStream("SD3_ReadOnlySchedule");
            Console.WriteLine("start connecting.");
            try
            {
                pipe.Connect(1000);
                if (pipe.IsConnected)
                {
                    Console.WriteLine("connected!");
                    pipe.Write(package);
                    Console.WriteLine("sent!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name);
                Console.WriteLine(e.Message);
            }
            pipe.Close();
            Console.WriteLine("done.");
        }

        static void func0901()
        {
            // string output = string.Format("\"Age\":{0},\"Name\":\"{1}\"", 32, "Leo");
            // string output = "\"Age\":3,  \"Name\":\"\"";
            string output = "\"Age\":5";
            // string output = string.Empty;
            Console.WriteLine("{" + output + "}");
            Cat cat = JsonSerializer.Deserialize<Cat>("{" + output + "}");
            Console.WriteLine(cat);
        }

        static void func0818(string[] cmds)
        {
            var result = Parser.Default.ParseArguments<Verb1, Verb2>(cmds);
            Console.WriteLine("this time parse: " + result.TypeInfo.Current.Name);
            Console.WriteLine("errors: ");
            foreach (var err in result.Errors)
                Console.WriteLine(err);

            result
            .WithParsed<Verb1>(Verb1.Verb1Func)
            .WithParsed<Verb2>(Verb2.Verb2Func);
        }

        static void func0816(string content)
        {
            // ushort us16 = ushort.MaxValue;
            // // short s16 = ((short)us16);
            // short s16 = short.Parse(us16.ToString());
            // Console.WriteLine($"us16={us16}, (short)us16={s16}");
            // byte[] bus16 = BitConverter.GetBytes(us16);
            // byte[] bs16 = BitConverter.GetBytes(s16);
            // Console.WriteLine($"bus16={string.Join(", ", bus16)}, bus16={string.Join(", ", bs16)}");

            // named pipe test
            NamedPipeClientStream pipe = new NamedPipeClientStream("StealthDefenseLx_PIPE");
            byte[] buf, package = new byte[6];

            buf = BitConverter.GetBytes((ushort)0);
            Array.Copy(buf, 0, package, 4, buf.Length);
            //
            buf = Encoding.UTF8.GetBytes(content);
            Console.WriteLine($"{content}->[{string.Join('|', buf)}]");
            Array.Resize<byte>(ref package, package.Length + buf.Length);
            Array.Copy(buf, 0, package, 6, buf.Length);
            int len = package.Length - 4;
            buf = BitConverter.GetBytes(len);
            Array.Copy(buf, 0, package, 0, 4);
            Console.WriteLine($"package: <{string.Join('|', package)}>");

            string contentRec = Encoding.UTF8.GetString(package);
            Console.WriteLine("content recover: " + content);

            pipe.Connect();
            pipe.Write(package, 0, package.Length);
            pipe.Close();
            Console.WriteLine("done.");

            // // named pipe server
            // NamedPipeServerStream pipeSvr = new NamedPipeServerStream("StealthDefenseLx_PIPE", PipeDirection.InOut);
            // pipeSvr.WaitForConnection();
            // Console.WriteLine("connect!");
            // byte[] bLen = new byte[4];
            // pipeSvr.Read(bLen, 0, 4);
            // int len = BitConverter.ToInt32(bLen);
            // byte[] bContent = new byte[len];
            // pipeSvr.Read(bContent, 0, len);
            // Console.WriteLine($"package: <{string.Join('|', bContent)}>");
            // pipeSvr.Close();
        }

        static void func0809()
        {
            Cat cat = new Cat("Jerry", 31);
            
            Console.WriteLine($"{cat.GetType().GetProperty("Name")}");
        }
        static void func0719b()
        {
            try
            {
                Console.WriteLine("func0719b() run.");
                throw new Exception("test 1 excp.");
            }
            catch (Exception e)
            {
                Console.WriteLine("catched by function. " + e.Message);
            }
            throw new Exception("test 2 excp.");
        }

        static void func0719()
        {
            // Random rnd = new Random(234);
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            byte[] arrB = new byte[3];
            // Console.WriteLine($"initial array (length: {arrB.Length}): {string.Join('-', arrB)}");
            for (int i = 0; i < 5; i++)
            {
                rnd.NextBytes(arrB);
                Console.WriteLine($"#{i}  array: {string.Join('-', arrB)}");
            }
        }

        static void func0712()
        {
            if (func0712a(-1) || func0712b(1))
                Console.WriteLine("good");
            else
                Console.WriteLine("no way here.");
            // [結論] or條件時，前面成立了就不會去做後面。
        }

        static bool func0712a(int a)
        {
            Console.WriteLine($"a is {(a < 0 ? "\b":"not")} smaller than 0.");
            return (a < 0);
        }
        static bool func0712b(int b)
        {
            Console.WriteLine($"b is {(b > 0 ? "\b":"not")} greater than 0.");
            return (b > 0);
        }

        static void func0711lx()
        {
            Process.Start("https://www.google.com");
            Console.WriteLine("google good.");
            // [結論] linux不能直接開網址
        }

        static void cmdParse(string[] args)
        {
            // ParserResult<object> res = 
            //     Parser.Default.ParseArguments<HDOptions, HFOptions, SPOptions, EntryOptions, UninstOptions>(args);

            // if (res.GetType() == typeof(NotParsed<object>))
            // {
            //     Globals.OutputDebugString("not parse..");
            //     return;
            // }
            // else
            // {
            //     // Console.WriteLine($"{res.TypeInfo.Current}");
            //     if (res.TypeInfo.Current == typeof(UninstOptions))
            //     {
            //         res.WithParsed<UninstOptions>(uninstall);
            //         return;
            //     }

            //     if (init() < 0)
            //         return;
            //     else
            //     {
            //         res
            //         .WithParsed<HDOptions>(settingAction)
            //         .WithParsed<HFOptions>(hiddenFolderAction)
            //         .WithParsed<SPOptions>(safetyProcessAction)
            //         .WithParsed<EntryOptions>(openEntry);
            //     }
            // }
        }

        /// <summary>NTP test</summary>
        static void func0707()
        {
            NTPClient1 client = new NTPClient1();
            for (int i = 0; i < 5; i++)
                Console.WriteLine($"[{i}] - NTPClient: {client.GetNtpTime(8).ToString("yyyy/MM/dd-HH:mm:ss.ffff")}");
            Console.WriteLine();
            for (int i = 0; i < 5; i++)
                Console.WriteLine(DateTime.Now.ToString("C#的時間：yyyy/MM/dd-HH:mm:ss.ffff"));
            Console.WriteLine();
            for (int i = 0; i < 5; i++)
                Console.WriteLine($"[{i}] - client2：{NTPClient2.GetNetworkTime().ToString("yyyy/MM/dd-HH:mm:ss.ffff")}");
            Console.WriteLine();
        }

        static void func4PublicQuickly(string[] args)
        {
            foreach (string folder in args)
            {
                Console.WriteLine($"check \'{folder}\'");
                if (Directory.Exists($"{folder}\\runtimes"))
                {
                    Directory.Delete($"{folder}\\runtimes", true);
                    Console.WriteLine($"deleted \'{folder}\\runtimes\'");
                }

                string[] files = Directory.GetFiles(folder);
                foreach (string f in files)
                {
                    string filename = Path.GetFileName(f);
                    if (filename.Substring(0, 2) != "SD" || filename.Substring(0, 6) == "SDBase")
                    {
                        File.Delete(f);
                        Console.WriteLine($"deleted \'{f}\'");
                        continue;
                    }
                }
            }
        }

        static void func0629()
        {
            #if OP1
            Console.WriteLine("option 1");
            #elif OP2
            Console.WriteLine("option 2");
            #else
            Console.WriteLine("other else");
            #endif

            #if HEY
            Console.WriteLine("Hey, it works.");
            #endif
        }

        static void func0628()
        {
            try
            {
                Cat fc = Cat.Load("{\"Name\":\"Macro\",\"Age\":123}");
                // Cat fc = Cat.Load("{\"Name\":\"Macro\"}");
                Console.WriteLine($"cat.name = {fc.Name}");
                Console.WriteLine($"cat.age = {fc.Age}");
                // Console.WriteLine($"cat.wing: {fc.wing}");
                fc.Roar();
                Console.WriteLine("hello");
                throw new Exception("world!");
                int a = 0;
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.Message);
            }

            // [結論] json text沒有給的欄位，會保持constructor給的值。
        }

        static void func0623()
        {
            string str = "   , v  ";
            Console.WriteLine($">{str}<");
            Console.WriteLine($">{str.Trim()}<");
            string str1 = Regex.Replace(str, @"\s+", "");
            Console.WriteLine($">{str1}<");
            string[] arr = str.Split('*');
            foreach (string a in arr)
                Console.WriteLine(a);
            Console.WriteLine($"end. arr.length={arr.Length}");
        }

        static void func0622()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            DriveInfo[] driveInfos = DriveInfo.GetDrives();
            try
            {
                foreach (DriveInfo driveInfo in driveInfos)
                {
                    StringBuilder sb = new StringBuilder(1024);
                    string diskPart = driveInfo.Name.Split('\\')[0];
                    QueryDosDevice(diskPart, sb, 1024);
                    Console.WriteLine($"add [{sb.ToString()} --> {diskPart}]");
                    dict.Add(sb.ToString(), diskPart);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        static void func0617()
        {
            List<int> Lst = new List<int>() {1,2,3,4,5,6,7,8,9,0};
            Lst.ForEach(
                i => {
                    if (i < 5)
                        return;
                    Console.WriteLine(i);
                }
            );
            // test join
            Console.WriteLine(string.Join(" - ", Lst));
        }

        static void func0616()
        {
            // test 1
            // // Dictionary<string, string> dict = new Dictionary<string, string>();
            // DriveInfo[] driveInfos = DriveInfo.GetDrives();
            // foreach (DriveInfo driveInfo in driveInfos)
            // {
            //     StringBuilder sb = new StringBuilder(1024);
            //     string diskPart = driveInfo.Name.Split("\\")[0];
            //     QueryDosDevice(diskPart, sb, 1024);
            //     // dict.Add(
            //     //     sb.ToString().Split(";")[0],    // 多個名稱的話會用";"隔開，這邊取第一個就好。
            //     //     diskPart
            //     // );
            //     Console.WriteLine($"{sb.ToString()} -> {diskPart}");
            // }

            // test 2
            // Console.WriteLine(Path.GetDirectoryName("Y:\\abd"));

            // test 3
            // 掃描所有桌面所有捷徑
            DirectoryInfo dir = new DirectoryInfo(Path.GetPathRoot(Environment.SystemDirectory) + "\\Users");
            Console.WriteLine(dir.FullName);
            DirectoryInfo[] users = dir.GetDirectories();
            foreach (DirectoryInfo user in users)
            {
                Console.WriteLine($"    L {user.FullName}");
                try
                {
                    FileInfo[] linkfiles = user.GetFiles(@"desktop\*.lnk", SearchOption.TopDirectoryOnly);
                    foreach (FileInfo linkfile in linkfiles)
                        Console.WriteLine("         L> " + linkfile.FullName);
                }
                // catch (DirectoryNotFoundException) {}
                // catch (UnauthorizedAccessException) {}
                catch (Exception excp) { Console.WriteLine($"Exception thrown while scanning all .lnk files on \'{user.FullName}\\Desktop\'\n{excp.Message}"); }
                Console.WriteLine();
            }
        }
        
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, int ucchMax);

        static void func0615()
        {
            Cat cat1 = Cat.Produce<Cat>("Jerry");
            Cat cat2 = Cat.Produce<FlyingCat>("Willy");
            cat1.Roar();
            cat2.Roar();
        }

        static void func0614()
        {
            List<Cat> cathouse = new List<Cat>() {
                new Cat("Alpha", 2),
                new Cat("Bravo", 3),
                new Cat("Charlie", 11)
            };

            // test join
            Console.WriteLine(string.Join<Cat>(Environment.NewLine, cathouse));

            // Console.WriteLine("Cat House:");
            // cathouse.ForEach(
            //     cat => {
            //         Console.WriteLine($"  * {cat.Name,10} {cat.Age:D03} year-old");
            //     }
            // );
            // Console.WriteLine();

            // Cat cat = new Cat("Charlie", 11);
            // // // Cat catC = cathouse[2];
            // Console.WriteLine($"{cat.Name} is contained in the house: {cathouse.Contains(cat)}");
            // int idx = cathouse.FindIndex(
            //     c => {
            //         return (cat.Age == c.Age && cat.Name.ToLower() == c.Name.ToLower());
            //     }
            // );
            // Console.WriteLine($"{cat.Name} is found in the house: {(idx != -1)}");
            // Console.WriteLine();
            // /*
            // [結論1]
            // List<物件>沒辦法使用contain但可以用find實現，除非override Equals()。
            // */
            
            // Console.WriteLine($"remove result 1: {cathouse.Remove(cat)}");
            // Console.WriteLine("Cat House:");
            // cathouse.ForEach(
            //     cat => {
            //         Console.WriteLine($"  * {cat.Name,10} {cat.Age,03} year-old");
            //     }
            // );
            // Console.WriteLine($"remove result 2: {cathouse.Remove(cathouse[idx])}");
            // Console.WriteLine("Cat House:");
            // cathouse.ForEach(
            //     cat => {
            //         Console.WriteLine($"  * {cat.Name,10} {cat.Age,03} year-old");
            //     }
            // );
            // /*
            // [結論2]
            // List<物件>沒辦法使用Remove但可以用find實現，除非override Equals()。
            // */
        }

        static void func0610()
        {
            // List<string> lst = new List<string>();
            // string[] arr = lst.ToArray();
            // if (arr.Length == 0)
            //     Console.WriteLine("good");
            // else
            //     Console.WriteLine("bad");

            string jsontxt = "{\"Name\":\"neko\",\"Age\":3}";
            // string jsontxt = "{\"Name\":\"neko\",}";
            Cat cat = Cat.Load(jsontxt);
            Console.WriteLine($"name: {cat.Name}, age: {cat.Age}");
        }

        static void func0609()
        {
            // "https://otpsvr-a1.trustonecloud.com:1443;https://otpsvr.trustonecloud.com:1443"
            string URL = "https://otpsvr-a1.trustonecloud.com:1443";
            if (WebTest.Test(URL))
                Console.WriteLine("good");
            else
                Console.WriteLine($"bad. ({WebTest.LastErrCode} - {WebTest.LastErrPhrase})");
        }

        static void func0608()
        {
            // Process p = Process.Start("explorer.exe", "http://www.google.com"); // 會自動變成預設瀏覽器開啟
            // Console.WriteLine("start.");
            // p.WaitForExit();
            // Console.WriteLine("finish");
            // string file1 = @"\\tofs\RD\Components\HiddenDefense\Client\TRAYICON\3.0.22.0609\passportPlus\SD_TrayIcon.dll";
            // string file1 = @"C:\Users\How\Documents\permReader\consoleTest.exe";
            string file1 = @"C:\Users\How\Documents\folder";
            Console.WriteLine(Path.GetFullPath(file1));
            Console.WriteLine(Path.GetFileName(file1));
            Console.WriteLine(Path.GetDirectoryName(file1));
        }

        static void func0607()
        {
            string str = "1994/12/09 12:34:56";
            DateTime dt = DateTime.Parse(str);
            Console.WriteLine($"{dt:T}");
        }

        static void func0606()
        {
            List<char> charArr = new List<char>();
            try
            {
                for (int i = 0; i < int.MaxValue; i ++)
                    charArr.Add('g');
                Console.WriteLine($"塞滿。(length: {charArr.Count})");
            }
            catch (Exception excp)
            {
                Console.WriteLine($"{excp.Message}\n{excp.StackTrace}");
                Console.WriteLine($"塞滿。(length: {charArr.Count})");
            }
        }

        static void func0602(string[] str)
        {
            Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W)");
            foreach (string s in str)
            {
                if (regex.IsMatch(s))
                    Console.WriteLine($"{s} --> complex");
                else
                    Console.WriteLine($"{s} --> simple");
            }
        }

        static void func0512B()
        {
            string Msg = string.Empty;
            foreach (string str in getStrArr())
                Msg += $"{str} ";
            Console.WriteLine($"result: {Msg}");
            // 結論: getStrArr()只會做一次。

            string Msg2 = string.Empty;
            foreach (string str in strArr)
                Msg2 += $"{str} ";
            Console.WriteLine($"result: {Msg2}");
            // 結論: getStrArr()只會做一次。
        }

        static string[] getStrArr()
        {
            Console.WriteLine("in");
            return new string[] {"chicken", "otpus", "cat", "ox", DateTime.Now.ToString("HH:mm:ss.ffff")};
        }
        static string[] strArr {
            get {
                Console.WriteLine("in");
                return new string[] {"chicken", "otpus", "cat", "ox", DateTime.Now.ToString("HH:mm:ss.ffff")};
            }
        }

        static void func0512()
        {
            FileInfo fi = new FileInfo(@"note2.txt");
            Console.WriteLine($"file size: {fi.Length} bytes.");
            using (StreamReader sr = new StreamReader(@"note2.txt", Encoding.Unicode))
            {
                string line, recentContent = string.Empty;
                Console.WriteLine($"stream size: {sr.BaseStream.Length} bytes.");
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line == "abcd")
                    {
                        // Console.WriteLine("it is \'abcd\'.");
                    }
                    else
                    {
                        recentContent = $"{line}\n{sr.ReadToEnd()}";
                        Console.WriteLine($"rest length: {recentContent.Length} size: {2*recentContent.Length}");
                    }
                }
                // Console.WriteLine($"content:\n  {recentContent.Replace("\n", "\n  ")}");
            }
        }

        static void func0511()
        {
            string jsontxt = "[{\"id\": 1, \"bCheck\":true},{\"id\": 2},{\"id\": 3, \"bCheck\":false}]";
            Console.WriteLine(jsontxt);
            OBJ1[] objs = JsonSerializer.Deserialize<OBJ1[]>(jsontxt);
            if (objs != null)
            {
                foreach (OBJ1 o in objs)
                {
                    Console.WriteLine($"obj.id: {o.id}, obj.bCheck: {o.bCheck}, obj.bCheck2: {o.bCheck2}");
                }
            }
        }

        static void func0510()
        {
            string str1 = "this is a test.";
            Console.WriteLine(str1);
            byte[] bStrUtf8 = Encoding.UTF8.GetBytes(str1);
            // byte[] bStrUnicode = Encoding.Unicode.GetBytes(str1);

            Console.WriteLine($"bStrUtf8    : {BitConverter.ToString(bStrUtf8)}");
            // Console.WriteLine($"bStrUnicode : {BitConverter.ToString(bStrUnicode)}");

            byte[] shaA, shaB;
            shaA = SHA256.HashData(bStrUtf8);
            using (SHA256 s256 = SHA256.Create())
            {
                shaB = s256.ComputeHash(bStrUtf8);
            }

            Console.WriteLine($"shaA: {BitConverter.ToString(shaA)}");  // same
            Console.WriteLine($"shaB: {BitConverter.ToString(shaB)}");  // same
        }

        static void func0509()
        {
            Console.WriteLine($"CommonApplicationData: {Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}");
            Console.WriteLine($"ApplicationData: {Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}");
            Console.WriteLine($"CommonPrograms: {Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms)}");
            Console.WriteLine($"MyComputer: {Environment.GetFolderPath(Environment.SpecialFolder.MyComputer)}");
            Console.WriteLine($"System: {Environment.GetFolderPath(Environment.SpecialFolder.System)}");
            Console.WriteLine($"Desktop: {Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}");
            Console.WriteLine($"DesktopDirectory: {Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}");
            Console.WriteLine($"CommonDesktopDirectory: {Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory)}");
            Console.WriteLine($"SystemDirectory: {Environment.SystemDirectory}");
            Console.WriteLine($"SystemDirectory's path root: {Path.GetPathRoot(Environment.SystemDirectory)}");

            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Microsoft\\Windows Defender\\Platform\\"))
                Console.WriteLine("存在");
            string[] MsMpEngs = 
                    Directory.GetFiles(
                        Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Microsoft\\Windows Defender\\Platform\\",
                        "MsMpEng.exe", SearchOption.AllDirectories
                    );
            Console.WriteLine(@"C:\ProgramData\Microsoft\Windows Defender\Platform\<ver.>\MsMpEng.exe");
            foreach (string msmpeng in MsMpEngs)
                Console.WriteLine($"  > {msmpeng}");
        }

        static void func0506Demo(string[] args)
        {
            string[][] output = func0506(args);
            if (output == null || output.Length == 0)
            {
                Console.WriteLine("no input");
                return;
            }

            foreach (string[] strs in output)
            {
                Console.Write("str > ");
                foreach (string str in strs)
                    Console.Write(str + " ");
                Console.WriteLine();
            }
        }

        static string[][] func0506(string[] input)
        {
            List<string> strs = new List<string>();
            List<string[]> strPkg = new List<string[]>();

            foreach (string inp in input)
            {
                strs.Add(inp);
                if (strs.Count == 3)
                {
                    strPkg.Add(strs.ToArray());
                    strs.Clear();
                }
            }
            if (strs.Count > 0)
                strPkg.Add(strs.ToArray());

            return strPkg.ToArray();
        }
    }
}
