using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModbusCommunication;
using Files;

namespace UR_点动控制器
{
    public partial class Register : Form
    {
        public string DefaultINIPath;

        public Register(string ConfigFilePath)
        {
            InitializeComponent();
            DefaultINIPath = ConfigFilePath;
        }

        //就是读取502端口的一个实例
        ModbusSocket URRegisterHandle = new ModbusSocket();

        private void Register_Load(object sender, EventArgs e)
        {
            //读取配置文件的IP
            FilesINI ConfigController = new FilesINI();
            string CurrentIP = ConfigController.INIRead("UR控制参数", "RemoteIP", DefaultINIPath);

            URRegisterHandle.RemoteIP = CurrentIP;
            URRegisterHandle.RemotePort = 502;
            URRegisterHandle.SocketTimeOut = 1000;

            URRegisterHandle.connectServer();
           
        }


        private void btn_RegisterWrite_Click(object sender, EventArgs e)
        {
            int WriteNum = Convert.ToInt32(txtWriteNum.Text);
            int WriteRegisterStartAddress = Convert.ToInt32(txtWriteStartAddress.Text);
            string WriteString = txtWriteValue.Text;

            URRegisterHandle.WriteMultipleRegister(WriteString, WriteNum, WriteRegisterStartAddress);
        }

        private void btn_RegisterRead_Click(object sender, EventArgs e)
        {
            int ReadNum = Convert.ToInt32(txtReadNum.Text);
            int ReadStartAddress = Convert.ToInt32(txtReadStartAddress.Text);

            int [] TempArray = URRegisterHandle.ReadMultipleRegister(ReadNum, ReadStartAddress);

            string TempStr = "";

            for (int i = 0; i < TempArray.Length; i++)
            {
                TempStr = TempStr + TempArray[i].ToString() + "|";
            }

            txtReadValues.Text = TempStr;

        }

    }
}
