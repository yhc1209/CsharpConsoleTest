using System;
using System.Text;
using System.IO.Pipes;
using System.Security.Principal;
using System.Security.AccessControl;

namespace testCons
{
    public class ROSlistener
    {
        private const int MAX_PIPE_NUM = 1;
        public void Listen(string pipename)
        {
            // PipeSecurity ps = new PipeSecurity();
            // ps.AddAccessRule(
            //     new PipeAccessRule(
            //         "Everyone",
            //         PipeAccessRights.ReadWrite,
            //         AccessControlType.Allow
            //     )
            // );
            // ps.AddAccessRule(
            //     new PipeAccessRule(
            //         WindowsIdentity.GetCurrent().Owner,
            //         PipeAccessRights.FullControl,
            //         AccessControlType.Allow
            //     )
            // );
            // NamedPipeServerStream pipe =
            //     NamedPipeServerStreamConstructors.New(
            //         pipename,
            //         PipeDirection.In,
            //         MAX_PIPE_NUM,
            //         PipeTransmissionMode.Byte,
            //         PipeOptions.None,
            //         0, 0, ps
            //     );
            NamedPipeServerStream pipe = new NamedPipeServerStream("thisisnotcorrect");
            pipe.WaitForConnection();
            byte[] buf = new byte[4];
            pipe.Read(buf, 0, 4);
            int len = BitConverter.ToInt32(buf, 0);
            Array.Resize<byte>(ref buf, len);
            pipe.Read(buf, 0, len);
            ushort type = BitConverter.ToUInt16(buf, 0);
            string folder = Encoding.UTF8.GetString(buf, 2, len - 2);
            string sender = null;
            ROSstateChangedEventArgs args = new ROSstateChangedEventArgs();
            switch ((ROSL_TYPE)type)
            {
                case ROSL_TYPE.ROS_SVC_SET:
                {
                    sender = "sdservice";
                    args.FolderName = folder;
                    args.IsReadOnly = true;
                    break;
                }
                case ROSL_TYPE.ROS_SVC_MODIFY:
                {
                    sender = "sdservice";
                    args.FolderName = folder;
                    args.IsReadOnly = false;
                    break;
                }
                case ROSL_TYPE.ROS_CM_SET:
                {
                    sender = "sdcontextmenu";
                    args.FolderName = folder;
                    args.IsReadOnly = true;
                    break;
                }
                case ROSL_TYPE.ROS_CM_CANCEL:
                {
                    sender = "sdcontextmenu";
                    args.FolderName = folder;
                    args.IsReadOnly = false;
                    break;
                }
            }
            OnStateChanged(sender, args);
        }

        protected virtual void OnStateChanged(string sender, ROSstateChangedEventArgs e)
        {
            ROSstateChangedEventHandler handler = StateChanged;
            if (handler != null)
                handler(sender, e);
        }

        public event ROSstateChangedEventHandler StateChanged;
    }

    public class ROSstateChangedEventArgs: EventArgs
    {
        public string FolderName {get; set;}
        public bool IsReadOnly {get; set;}
    }

    public delegate void ROSstateChangedEventHandler(string sender, ROSstateChangedEventArgs e);

    public enum ROSL_TYPE: ushort
    {
        /// <summary>SD service設定為唯讀隱藏區。</summary>
        ROS_SVC_SET     = 0,
        /// <summary>SD service修改唯讀隱藏區為普通隱藏區。</summary>
        ROS_SVC_MODIFY  = 1,
        /// <summary>SD context menu設定為唯讀隱藏區。</summary>
        ROS_CM_SET      = 2,
        /// <summary>SD context menu取消設定唯讀隱藏區。</summary>
        ROS_CM_CANCEL   = 3,
    }
}