using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace testCons
{
    class Program
    {
        static void Main(string[] args)
        {
            func0707();
        }

        static void func0707()
        {
            NTPClient client = new NTPClient();
            Console.WriteLine(client.GetNtpTime(8).ToString("yyyy/MM/dd-HH:mm:ss.ffff"));
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss.ffff"));
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
