using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace testCons
{
    public class Cat
    {
        public string Name {get; set;}
        public int Age {get; set;}

        public Cat()
        {
            Age = 1;
        }
        public Cat(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public static Cat Load(string json)
        {
            return JsonSerializer.Deserialize<Cat>(json);
        }

        public virtual void Roar()
        {
            Console.WriteLine($"I am a Cat, my name is {Name}, meow.");
        }

        // public override bool Equals(object obj)
        // {
        //     //Check for null and compare run-time types.
        //     if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
        //     {
        //         return false;
        //     }
        //     else
        //     {
        //         Cat cat = (Cat)obj;
        //         return (this.Age == cat.Age && this.Name.ToLower() == cat.Name.ToLower());
        //     }
        // }
        public override bool Equals(object obj)
        {
            try
            {
                Cat cat = (Cat)obj;
                return (this.Age == cat.Age && this.Name.ToLower() == cat.Name.ToLower());
            }
            catch (Exception) {}
 
            return false;
        }

        public override string ToString()
        {
            return $"{Name} is a {Age}-year-old Cat.";
        }

        public static T Produce<T>(string name) where T: Cat, new()
        {
            T cat = new T();
            cat.Name = name;
            return cat;
        }
    }

    public class FlyingCat: Cat
    {
        public string wing {get; set;} 
        public override void Roar()
        {
            Console.WriteLine($"I am a flying cat, my name is {Name}.");
        }
    }
    public class CampingCat: Cat
    {
        public string paw {get; set;}
        public override void Roar()
        {
            Console.WriteLine($"I am a camping Cat, my name is {Name}.");
        }
    }
    
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

    // NTP test
    public class NTPClient
    {
        protected internal DateTime GetNtpTime(int iUTCAdd)
        {
            //const string ntpServer = "time.nist.gov";
            const string ntpServer = "tick.stdtime.gov.tw";

            byte[] ntpData = new byte[48];
            
            //LeapIndicator = 0 (no warning), VersionNum = 3 (IPv4 only), Mode = 3 (Client Mode)
            ntpData[0] = 0x1B;
            
            IPAddress[] addresses = Dns.GetHostEntry(ntpServer).AddressList;
            IPEndPoint ipEndPoint = new IPEndPoint(addresses[0], 123);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            
            socket.Connect(ipEndPoint);
            socket.Send(ntpData);
            socket.Receive(ntpData);
            socket.Close();
            
            const byte serverReplyTime = 40;
            //Get the seconds part
            ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);
            
            //Get the seconds fraction
            ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);
            
            //Convert From big-endian to little-endian
            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);
            
            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            
            //UTC time + 8 
            DateTime networkDateTime = (new DateTime(1900, 1, 1))
                .AddMilliseconds((long)milliseconds).AddHours(iUTCAdd);

            //UnityEngine.Debug.Log(networkDateTime);

            return networkDateTime;
        }
        
        // stackoverflow.com/a/3294698/162671
        uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                          ((x & 0x0000ff00) << 8) +
                          ((x & 0x00ff0000) >> 8) +
                          ((x & 0xff000000) >> 24));
        }
    }
}