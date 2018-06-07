using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NetConnection;

namespace UR_点动控制器
{
    public partial class IPChange : Form
    {
        public IPChange()
        {
            InitializeComponent();
        }

        AboutIP AIP = new AboutIP();

        private void IPChange_Load(object sender, EventArgs e)
        {
            //获取当前本机IP和子网掩码
            string TempIP = AIP.GetMyIP();
            CurrentIP.Text = TempIP;

            string TempSubMask = AIP.GetMySubMask();
            CurrentSubMask.Text = TempSubMask;

        }

        private void btnChangeIP_Click(object sender, EventArgs e)
        {
            //获取要设置的IP和SubMask
            string TempIP = CurrentIP.Text;
            string TempSubMask = CurrentSubMask.Text;

            AIP.SetMyIPAndSubMask(TempIP,TempSubMask);
        }

        //本来只在Form Load的时候加载一次，这里做一个按钮，方便查看我切换了自动获取和手动设置之后到底IP变成啥了
        private void btnRefreshIP_Click(object sender, EventArgs e)
        {
            CurrentIP.Text = AIP.GetMyIP();
            CurrentSubMask.Text = AIP.GetMySubMask();
        }

        private void btnAutoGetIP_Click(object sender, EventArgs e)
        {
            AIP.SetMyIPAndSubMaskToAutoMode();
        }

        //调用PING方法，获取返回值
        private void btnPingIP_Click(object sender, EventArgs e)
        {
            bool ConnectionState = AIP.CheckConnectionStatus(OtherIP.Text);

            if (ConnectionState)
            {
                btnPingIP.BackColor = Color.Green;
                btnPingIP.Text = "Ping的通";
            }
            else
            {
                btnPingIP.BackColor = Color.Red;
                btnPingIP.Text = "Ping不通";
            }

        }




    }
}
