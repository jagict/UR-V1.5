using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Files;
using System.IO;

namespace UR_点动控制器
{
    public partial class Config : Form
    {

        //默认的配置文件的位置和文件名
        public string DefaultINIFilePath;

        FilesINI ConfigController = new FilesINI();

       public Config(string FilePath)
        {
            InitializeComponent();
            DefaultINIFilePath = FilePath;
        }

        private void Config_Load(object sender, EventArgs e)
        {
            //在这里读取配置文件的所有参数值
            LoadINI(DefaultINIFilePath);

        }

        //重置就是在当前目录下删除原有的，并新建一个Config.ini文件
        private void btnReset_Click(object sender, EventArgs e)
        {
            //如果原来有的话就删掉再来
            if (File.Exists(DefaultINIFilePath))
            {
                File.Delete(DefaultINIFilePath);
            }

            InitilizeINI(DefaultINIFilePath);
            LoadINI(DefaultINIFilePath);

        }

        //保存其实就是改写这个INI文件
        private void btnSave_Click(object sender, EventArgs e)
        {
            ConfigController.INIWrite("UR控制参数", "RemoteIP", Remote_IP.Text, DefaultINIFilePath);
            ConfigController.INIWrite("UR控制参数", "RemoteControlPort", Control_Port.Text, DefaultINIFilePath);

            ConfigController.INIWrite("UR运动参数", "BasicSpeed", Basic_Speed.Text, DefaultINIFilePath);
            ConfigController.INIWrite("UR运动参数", "BasicAcceleration", Basic_Acceleration.Text, DefaultINIFilePath);
            ConfigController.INIWrite("UR运动参数", "BasicRefreshRate", Data_RefreshRate.Text, DefaultINIFilePath);

            if (AutoConnect.Checked == true)
            {
                ConfigController.INIWrite("UR控制参数", "IfAutoConnect", "YES", DefaultINIFilePath);
            }
            else
            {
                ConfigController.INIWrite("UR控制参数", "IfAutoConnect", "NO", DefaultINIFilePath);
            }

            this.Close();


        }

        //初始化INI文件
        public void InitilizeINI(string FilePath)
        {
            ConfigController.INIWrite("UR控制参数", "RemoteIP", "192.168.1.100", FilePath);
            ConfigController.INIWrite("UR控制参数", "RemoteControlPort", "30001", FilePath);
            ConfigController.INIWrite("UR控制参数", "IfAutoConnect", "NO", FilePath);

            ConfigController.INIWrite("UR运动参数", "BasicSpeed", "0.15", FilePath);
            ConfigController.INIWrite("UR运动参数", "BasicAcceleration", "0.15", FilePath);
            ConfigController.INIWrite("UR运动参数", "BasicRefreshRate", "100", FilePath);

        }

        //加载一个INI文件，加载之后直接读取所有配置并在界面上显示
        public void LoadINI(string FilePath)
        {
            try
            {
                Remote_IP.Text = ConfigController.INIRead("UR控制参数", "RemoteIP", FilePath);
                Control_Port.Text = ConfigController.INIRead("UR控制参数", "RemoteControlPort", FilePath);

                Basic_Speed.Text = ConfigController.INIRead("UR运动参数", "BasicSpeed", FilePath);
                Basic_Acceleration.Text = ConfigController.INIRead("UR运动参数", "BasicAcceleration", FilePath);
                Data_RefreshRate.Text = ConfigController.INIRead("UR运动参数", "BasicRefreshRate", FilePath);

                string AutoConnection = ConfigController.INIRead("UR控制参数", "IfAutoConnect", FilePath);

                if (AutoConnection == "YES")
                {
                    AutoConnect.Checked = true;
                }
                else
                {
                    AutoConnect.Checked = false;
                }

            }
            catch (Exception LoadError)
            {
                MessageBox.Show("配置文件异常，请重置");
            }

        }


    }
}
