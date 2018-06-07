using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;

//需要在项目中添加对System.Management的引用
using System.Management;



namespace NetConnection
{
    class AboutIP
    {

        string strIP = "0.0.0.0";
        string strSubnet = "0.0.0.0";

        //获取本机IP的方法
        public string GetMyIP()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"])
                {
                    continue;
                }
                if ((mo["IPAddress"] as String[]).Length > 0 && strIP == "0.0.0.0")
                {
                    strIP = (mo["IPAddress"] as String[])[0];
                }
            }

            return strIP;

        }

        //获取本机子网掩码的方法
        public string GetMySubMask()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"])
                {
                    continue;
                }
                if ((mo["IPSubnet"] as String[]).Length > 0 && strSubnet == "0.0.0.0")
                {
                    strSubnet = (mo["IPSubnet"] as String[])[0];
                }
            }
            return strSubnet;

        }

        //获取某个网址的IP（比如www.baidu.com到底是XX.XX.XX.XX）
        public string GetWebsiteIP(string website)
        {
            IPHostEntry Website_IP = Dns.GetHostEntry(website);
            IPAddress Website_IP_Address = Website_IP.AddressList[0];
            return Website_IP_Address.ToString();
        }

        //检测远程IP是否可以PING通的方法
        public bool CheckConnectionStatus(string RemoteIP)
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(RemoteIP);

            if (reply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                  return false;
            }

        }
        
        //注意：修改IP调用系统的方法，如果你一边开着“本地连接”-“属性”-“Internet协议版本 4”然后看效果，你是看不到效果的，必须关掉界面只在这上面做

        //修改本机IP和SubMask的方法
        public void SetMyIPAndSubMask(String NewIP, String NewSubMask)
        {
            ManagementBaseObject inPar = null;
            ManagementBaseObject outPar = null;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"])
                {
                    continue;
                }

                inPar = mo.GetMethodParameters("EnableStatic");
                inPar["IPAddress"] = new string[] { NewIP };
                inPar["SubnetMask"] = new string[] { NewSubMask };
                outPar = mo.InvokeMethod("EnableStatic", inPar, null);
            }


        }

        //修改本机IP和SubMask为自动获取的方法
        public void SetMyIPAndSubMaskToAutoMode()
        {
            ManagementBaseObject inPar = null;
            ManagementBaseObject outPar = null;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"]) continue;

                if (!(bool)mo["DHCPEnabled"])
                {
                    inPar = mo.GetMethodParameters("EnableDHCP");
                    outPar = mo.InvokeMethod("EnableDHCP", inPar, null);
                }

            }



        }

        



    }
}
