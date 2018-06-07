using System;
using System.Collections;
using System.Configuration;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using System.Windows.Forms;
using Qzeim.ThrdPrint.BroadCast.Common;

namespace UR_点动控制器
{
    public class RobotComm
    {
        private IBroadCastHandler broadCastHandler;
        private IBroadCast watch = null;
        private EventWrapper wrapper = null;
        private IUpCast upCast = null;
        private string rcvMsg = "";

        public RobotComm(IBroadCastHandler _broadCastHandler)
        {
            broadCastHandler = _broadCastHandler;
        }

        public string RcvMsg
        {
            get { return rcvMsg; }
            set { rcvMsg = value; }
        }

        public void StartClient()
        {
            BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
            BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();
            serverProvider.TypeFilterLevel = TypeFilterLevel.Full;

            IDictionary props = new Hashtable();
            props["port"] = 0;
            TcpChannel channel = new TcpChannel(props, clientProvider, serverProvider);
            ChannelServices.RegisterChannel(channel);

            // 由config中读取相关数据
            string broadCastObjURL = ConfigurationManager.AppSettings["BroadCastObjURL"];
            string upCastObjURL = ConfigurationManager.AppSettings["RobotUpCastObjURL"];

            // 获取广播远程对象
            watch = (IBroadCast)Activator.GetObject(typeof(IBroadCast), broadCastObjURL);
            wrapper = new EventWrapper();
            wrapper.LocalBroadCastEvent += new BroadCastEventHandler(broadCastHandler.OnBroadCastingInfo);
            watch.BroadCastEvent += new BroadCastEventHandler(wrapper.BroadCasting);

            // upcast
            upCast = (IUpCast)Activator.GetObject(typeof(IUpCast), upCastObjURL);
        }

        public void FinishClient()
        {
            watch.BroadCastEvent -= new BroadCastEventHandler(wrapper.BroadCasting);
        }

        public void SendToServer(string json)
        {
            upCast.SendMsg(json);
        }

       
    }
}