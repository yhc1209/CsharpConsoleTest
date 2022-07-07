using System;
using System.IO;
using System.Text.Json;

namespace testCons
{
    class OBJ1
    {
        public int id {get; set;}
        public bool bCheck2 {get; set;}
        public bool bCheck {get; set;}

        public static string currentTime {
            get { return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff"); }
        }
        public static string[] currentTimes {
            get { return new string[] {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff"), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff")}; }
        }
    }
}