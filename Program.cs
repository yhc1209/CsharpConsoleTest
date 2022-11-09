using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using CommandLine;

namespace testCons
{
    class Program
    {
        static void Main(string[] args)
        {
            // try
            // {
            //     Console.WriteLine("args: " + string.Join(", ", args));
            //     func0818(args);
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine("catched by Main(). " + e.Message);
            // }
            func1108();
            // func1109(0.5f);
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
        }

        static void func1104()
        {
            J4T j4t = new J4T();
            j4t.CheckId();
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
