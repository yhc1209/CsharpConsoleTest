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
        private string _serverUrl;
        private int _port;
        private int _locate;

        public NTPClient(string NTPserver, int port = 123, int locate = 8)
        {
            _serverUrl = NTPserver;
            _port = port;
            _locate = locate;
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

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.SendTimeout = 500;
                socket.ReceiveTimeout = 500;
                socket.Connect(_serverUrl, _port);

                int c;
                c = socket.Send(ntpData);
                Debug.WriteLine($"Send {c} byte(s) to {_serverUrl}:{_port}");
                // showNtpData(ntpData);
                c = socket.Receive(ntpData);
                Debug.WriteLine($"Receive {c} byte(s) from {_serverUrl}:{_port}");
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
            int rootDelay = BitConverter.ToInt32(swapEndianness(data[4..8]));

            // Root Dispersion :
            // The maximum error of the local clock relative to the primary reference source.
            int rootDispersion = BitConverter.ToInt32(swapEndianness(data[8..12]));

            // Reference Identifier :
            // Identifier of the particular reference source.
            int referenceIdentifier = BitConverter.ToInt32(swapEndianness(data[12..16]));

            // Reference Timestamp :
            // The local time at which the local clock was last set or corrected.
            DateTime refTS = NtpFormat2DateTime(data[16..24]);

            // Originate Timestamp :
            // The local time at which the request departed from the client for the service host.
            DateTime orgTS = NtpFormat2DateTime(data[24..32]);

            // Receive Timestamp :
            // The local time at which the request arrived at the service host.
            DateTime rcvTS = NtpFormat2DateTime(data[32..40]);

            // Transmit Timestamp :
            // The local time at which the reply departed from the service host for the client.
            DateTime tsmTS = NtpFormat2DateTime(data[40..48]);

            // Authenticator :
            // Authentication information.
            byte[] authenticator = new byte[data.Length - 48];
            if (data.Length > 48)
                authenticator = data[48..];

            Console.WriteLine($"LI: {LI} | VN: {VN} | Mode: {NtpMode[Mode]}");
            Console.WriteLine($"Stratum: {stratum} | Poll: {poll} | Precision: {precision}");
            Console.WriteLine($"Root Delay: {rootDelay} | Root Dispersion: {rootDispersion}");
            Console.WriteLine($"Reference Identifier: 0x{referenceIdentifier:X04}");
            Console.WriteLine($"Reference Timestamp: {refTS.ToString("yyyy/MM/dd - HH:mm:ss.ffff")}");
            Console.WriteLine($"Originate Timestamp: {orgTS.ToString("yyyy/MM/dd - HH:mm:ss.ffff")}");
            Console.WriteLine($"Receive Timestamp: {rcvTS.ToString("yyyy/MM/dd - HH:mm:ss.ffff")}");
            Console.WriteLine($"Transmit Timestamp: {tsmTS.ToString("yyyy/MM/dd - HH:mm:ss.ffff")}");
            Console.WriteLine($"Authenticator: {string.Join('|', authenticator)}");
            Console.WriteLine();
        }

        private DateTime NtpFormat2DateTime(byte[] b)
        {
            ulong intPart = BitConverter.ToUInt32(swapEndianness(b[0..4]));
            ulong fractPart = BitConverter.ToUInt32(swapEndianness(b[4..8]));

            ulong ms = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            return new DateTime(1900, 1, 1).AddMilliseconds((long)ms).AddHours(_locate);
        }

        private byte[] swapEndianness(byte[] x)
        {
            byte[] y = new byte[x.Length];
            for (int i = 0; i < x.Length; i++)
                y[i] = x[x.Length - i - 1];
            
            return y;
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
    public class NTPClient1109
    {
        public static NTPmsg GetNtpMsg(string ntpsvr, int port, int timeloc)
        {
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.SendTimeout = 500;
                socket.ReceiveTimeout = 500;
                socket.Connect(ntpsvr, port);

                byte[] ntpdata = new byte[48];
                int LI = 0, VN = 3, Mode = 3;
                ntpdata[0] = (byte)(((LI & 0x03) << 6) + ((VN & 0x0B) << 3) + (Mode & 0x0B));

                DateTime clnsend = DateTime.Now;
                socket.Send(ntpdata);
                socket.Receive(ntpdata);
                DateTime clnrecv = DateTime.Now;

                return new NTPmsg(clnsend, clnrecv, ntpdata, timeloc);
            }
            catch (SocketException se)
            {
                Debug.WriteLine($"[NTP] 取得NTP資訊失敗！ {se.GetType().Name} - {se.Message}");
            }
            catch (Exception excp)
            {
                Debug.WriteLine($"[NTP] 取得NTP資訊失敗！ {excp.GetType().Name} - {excp.Message}\n{excp.StackTrace}");
            }
            return null;
        }
    }

    public class NTPmsg
    {
        #region members
        /// <summary>NTP client傳出訊息的時間。</summary>
        public DateTime TSclientSend;
        /// <summary>NTP server收到訊息的時間。</summary>
        public DateTime TSserverRecv;
        /// <summary>NTP server傳出訊息的時間。</summary>
        public DateTime TSserverSend;
        /// <summary>NTP client收到訊息的時間。</summary>
        public DateTime TSclientRecv;

        /// <summary>Leap Indicator</summary>
        /// <remarks>
        /// A 2-bit leap indicator. When set to 11, it warns of an alarm condition (clock unsynchronized); 
        /// when set to any other value, it is not to be processed by NTP.
        /// </remarks>
        public int LI;
        /// <summary>Version Number</summary>
        /// <remarks>A 3-bit version number that indicates the version of NTP. The latest version is version 4.</remarks>
        public int VN;
        /// <summary>Mode</summary>
        /// <remarks>A 3-bit code that indicates the work mode of NTP.</remarks>
        public int Mode;
        /// <summary>Stratum</summary>
        /// <remarks>
        /// An 8-bit integer that indicates the stratum level of the local clock, with the value ranging from 1 to 16. 
        /// Clock precision decreases from stratum 1 through stratum 16. A stratum 1 clock has the highest precision, and a stratum 
        /// 16 clock is not synchronized and cannot be used as a reference clock.
        /// </remarks>
        public int Stratum;
        /// <summary>Poll</summary>
        /// <remarks>An 8-bit signed integer that indicates the maximum interval between successive messages, which is called the poll interval.</remarks>
        public int Poll;
        /// <summary>Precision</summary>
        /// <remarks>An 8-bit signed integer that indicates the precision of the local clock.</remarks>
        public int Precision;
        /// <summary>Root Delay</summary>
        /// <remarks>Roundtrip delay to the primary reference source.</remarks>
        public int RootDelay;
        /// <summary>Root Dispersion</summary>
        /// <remarks>The maximum error of the local clock relative to the primary reference source.</remarks>
        public int RootDispersion;
        /// <summary>Reference Identifier</summary>
        /// <remarks>Identifier of the particular reference source.</remarks>
        public int ReferenceIdentifier;

        /// <summary>Reference Timestamp</summary>
        /// <remarks>The local time at which the local clock was last set or corrected.</remarks>
        public DateTime RefTS;
        /// <summary>Originate Timestamp</summary>
        /// <remarks>The local time at which the request departed from the client for the service host.</remarks>
        public DateTime OrgTS;
        /// <summary>>Authenticator</summary>
        /// <remarks>Authentication information.</remarks>
        public byte[] Authenticator;
        /// <summary>時區。</summary>
        /// <remarks>以台為為例時區為UTC+8，所以值為8。</remarks>
        public int TimeLoc;

        public TimeSpan offset;
        public TimeSpan delay;
        public string NtpMode
        {
            get { return ntpMode[Mode]; }
        }

        /// <summary>計算後的那個時間。</summary>
        public DateTime TheTime
        {
            get { return (TSserverSend + delay - offset); }
        }
        #endregion

        /// <summary>解析NTP package。</summary>
        public NTPmsg(DateTime ts_send, DateTime ts_receive, byte[] ntppackage, int timeloc)
        {
            TimeLoc = timeloc;
            
            LI = ((ntppackage[0] & 0xC0) >> 6);
            VN = ((ntppackage[0] & 0x38) >> 3);
            Mode = (ntppackage[0] & 0x07);
            Stratum = (ntppackage[1] & 0x0F);
            Poll = (ntppackage[2] & 0x0F);
            Precision = (ntppackage[3] & 0x0F);
            RootDelay = BitConverter.ToInt32(swapEndianness(ntppackage[4..8]));
            RootDispersion = BitConverter.ToInt32(swapEndianness(ntppackage[8..12]));
            ReferenceIdentifier = BitConverter.ToInt32(swapEndianness(ntppackage[12..16]));
            RefTS = NtpFormat2DateTime(ntppackage[16..24]);
            OrgTS = NtpFormat2DateTime(ntppackage[24..32]);

            TSclientSend = ts_send;                                 // t1
            TSserverRecv = NtpFormat2DateTime(ntppackage[32..40]);  // t2
            TSserverSend = NtpFormat2DateTime(ntppackage[40..48]);  // t3
            TSclientRecv = ts_receive;                              // t4
            Debug.WriteLine(
                string.Format(
                    "t1={0}, t2={1}, t3={2}, t4={3}",
                    TSclientSend.ToString("yyyy/MM/dd-HH:mm:ss.fff"),
                    TSserverRecv.ToString("yyyy/MM/dd-HH:mm:ss.fff"),
                    TSserverSend.ToString("yyyy/MM/dd-HH:mm:ss.fff"),
                    TSclientRecv.ToString("yyyy/MM/dd-HH:mm:ss.fff")
                )
            );

            offset = 0.5 * ((TSserverRecv - TSclientSend) + (TSserverSend - TSclientRecv));
            delay = (TSclientRecv - TSclientSend) - (TSserverRecv - TSserverSend);
            Debug.WriteLine($"offset=((t2-t1)+(t3-t4))/2={offset}");
            Debug.WriteLine($"delay=((t4-t1)-(t3-t2))={delay}");

            if (ntppackage.Length > 48)
                Authenticator = ntppackage[48..];
        }
        
        private DateTime NtpFormat2DateTime(byte[] b)
        {
            ulong intPart = BitConverter.ToUInt32(swapEndianness(b[0..4]));
            ulong fractPart = BitConverter.ToUInt32(swapEndianness(b[4..8]));

            ulong ms = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            return new DateTime(1900, 1, 1).AddMilliseconds((long)ms).AddHours(TimeLoc);
        }

        private byte[] swapEndianness(byte[] x)
        {
            byte[] y = new byte[x.Length];
            for (int i = 0; i < x.Length; i++)
                y[i] = x[x.Length - i - 1];
            
            return y;
        }

        private string[] ntpMode = {
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