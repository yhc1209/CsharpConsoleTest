using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace testCons
{
    // NTP test
    public class NTPClient1
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

    public class NTPClient2
    {
        public static DateTime GetNetworkTime()
        {
            //default Windows time server
            const string ntpServer = "time.windows.com";

            // NTP message size - 16 bytes of the digest (RFC 2030)
            var ntpData = new byte[48];

            //Setting the Leap Indicator, Version Number and Mode values
            ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;

            //The UDP port number assigned to NTP is 123
            var ipEndPoint = new IPEndPoint(addresses[0], 123);
            //NTP uses UDP

            using(var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.Connect(ipEndPoint);

                //Stops code hang if NTP is blocked
                socket.ReceiveTimeout = 3000;     

                socket.Send(ntpData);
                socket.Receive(ntpData);
                socket.Close();
            }

            //Offset to get to the "Transmit Timestamp" field (time at which the reply 
            //departed the server for the client, in 64-bit timestamp format."
            const byte serverReplyTime = 40;

            //Get the seconds part
            ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

            //Get the seconds fraction
            ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);
            Debug.WriteLine($"intPart: {intPart} | fractPart: {fractPart}");

            //Convert From big-endian to little-endian
            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);
            Debug.WriteLine($"intPart: {intPart} | fractPart: {fractPart}");

            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            Debug.WriteLine($"long value: {milliseconds}");

            //**UTC** time
            var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

            return networkDateTime.ToLocalTime();
        }

        // stackoverflow.com/a/3294698/162671
        static uint SwapEndianness(ulong x)
        {
            return (uint) (((x & 0x000000ff) << 24) +
                        ((x & 0x0000ff00) << 8) +
                        ((x & 0x00ff0000) >> 8) +
                        ((x & 0xff000000) >> 24));
        }
    }

    /// <summary>用於向NTP server取得時間的類別。</summary>
    public class NTPClient
    {
        private string serverUrl;
        private int port;

        public NTPClient(string NTPserver)
        {
            serverUrl = NTPserver;
            port = 123;
        }

        public void ShowAllIps()
        {
            IPAddress[] addresses = Dns.GetHostAddresses(serverUrl);
            foreach (IPAddress ip in addresses)
            {
                Console.WriteLine($" - [{ip.AddressFamily}] {string.Join('.', ip.GetAddressBytes())}");
            }
        }

        public byte[] GetNtpPackage(int version = 3)
        {
            byte[] ntpData = new byte[48];

            int LI = 0;
            int VN = version;
            int Mode = 3;
            ntpData[0] = (byte)(
                ((LI & 0x03) << 6) +
                ((VN & 0x0B) << 3) +
                ((Mode & 0x0B))
            );
            Debug.WriteLine($"{ntpData:X02}");

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.SendTimeout = 500;
                socket.ReceiveTimeout = 500;
                socket.Connect(serverUrl, port);

                int c;
                c = socket.Send(ntpData);
                Debug.WriteLine($"Send {c} byte(s) to {serverUrl}:{port}");
                // showNtpData(ntpData);
                c = socket.Receive(ntpData);
                Debug.WriteLine($"Receive {c} byte(s) from {serverUrl}:{port}");
                showNtpData(ntpData);
            }

            return ntpData;
        }

        private void showNtpData(byte[] data)
        {
            // LI (Leap Indicator) :
            // A 2-bit leap indicator. When set to 11, it warns of an alarm condition (clock unsynchronized); 
            // when set to any other value, it is not to be processed by NTP.
            int LI = ((data[0] & 0xC0) >> 6);

            // VN (Version Number) :
            // A 3-bit version number that indicates the version of NTP. The latest version is version 4.
            int VN = ((data[0] & 0x38) >> 3);

            // Mode :
            // A 3-bit code that indicates the work mode of NTP.
            int Mode = (data[0] & 0x07);

            // Stratum :
            // An 8-bit integer that indicates the stratum level of the local clock, with the value ranging from 1 to 16. 
            // Clock precision decreases from stratum 1 through stratum 16. A stratum 1 clock has the highest precision, and a stratum 
            // 16 clock is not synchronized and cannot be used as a reference clock.
            int stratum = (data[1] & 0x0F);

            // Poll :
            // An 8-bit signed integer that indicates the maximum interval between successive messages, which is called the poll interval.
            int poll = (data[2] & 0x0F);

            // Precision :
            // An 8-bit signed integer that indicates the precision of the local clock.
            int precision = (data[3] & 0x0F);

            // Root Delay :
            // Roundtrip delay to the primary reference source.
            int rootDelay = BitConverter.ToInt32(data, 4);

            // Root Dispersion :
            // The maximum error of the local clock relative to the primary reference source.
            int rootDispersion = BitConverter.ToInt32(data, 8);

            // Reference Identifier :
            // Identifier of the particular reference source.
            int referenceIdentifier = BitConverter.ToInt32(data, 12);

            // Reference Timestamp :
            // The local time at which the local clock was last set or corrected.
            DateTime refTS = byte2time(data[16..24]);

            // Originate Timestamp :
            // The local time at which the request departed from the client for the service host.
            DateTime orgTS = byte2time(data[24..32]);

            // Receive Timestamp :
            // The local time at which the request arrived at the service host.
            DateTime rcvTS = byte2time(data[32..40]);

            // Transmit Timestamp :
            // The local time at which the reply departed from the service host for the client.
            DateTime tsmTS = byte2time(data[40..48]);

            // Authenticator :
            // Authentication information.
            byte[] authenticator = new byte[data.Length - 48];
            if (data.Length > 48)
                authenticator = data[48..];

            Console.WriteLine($"LI: {LI} | VN: {VN} | Mode: {NtpMode[Mode]}");
            Console.WriteLine($"Stratum: {stratum} | Poll: {poll} | Precision: {precision}");
            Console.WriteLine($"Root Delay: {rootDelay} | Root Dispersion: {rootDispersion}");
            Console.WriteLine($"Reference Identifier: {referenceIdentifier}");
            Console.WriteLine($"Reference Timestamp: {refTS}");
            Console.WriteLine($"Originate Timestamp: {orgTS}");
            Console.WriteLine($"Receive Timestamp: {rcvTS}");
            Console.WriteLine($"Transmit Timestamp: {tsmTS}");
            Console.WriteLine($"Authenticator: {string.Join('|', authenticator)}");
            Console.WriteLine();
        }

        private DateTime byte2time(byte[] b)
        {
            ulong intPart = swapEndianness(BitConverter.ToUInt32(b, 0));
            ulong fractPart = swapEndianness(BitConverter.ToUInt32(b, 4));
            Debug.WriteLine($"intPart: {intPart} | fractPart: {fractPart}");

            var timeL = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            return new DateTime(1900, 1, 1).AddMilliseconds((long)timeL);
        }

        private uint swapEndianness(uint x)
        {
            return (((x & 0x000000ff) << 24) + ((x & 0x0000ff00) << 8) + ((x & 0x00ff0000) >> 8) + ((x & 0xff000000) >> 24));
        }

        private string[] NtpMode = {
            "reserved",
            "symmetric active",
            "symmetric passive",
            "client",
            "server",
            "broadcast or multicast",
            "NTP control message",
            "reserved for private use.",
        };
    }
}