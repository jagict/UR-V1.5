using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Qzeim.ThrdPrint.BroadCast.Common;

//调用外部类
using URDate;
using URControl;
using Files;
using Newtonsoft.Json;

namespace UR_点动控制器
{
    public partial class Form1 : Form, IBroadCastHandler
    {
        public Form1()
        {
            InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;
            robotComm = new RobotComm(this);
        }


        //主程序不应该关心细枝末节，只要知道问谁要到数据，还有要把数据给谁
        URDateHandle URDateCollector = new URDateHandle();
        URControlHandle URController = new URControlHandle();

        //声明全局的速度和加速度控制条
        public double SpeedRate;
        public double AccelerationRate;

        //这五个参数做成全局的会比较好用
        public double BasicSpeed;
        public double BasicAcceleration;
        public string Target_IP;
        public int Control_Port;
        public int DataRefreshRate;
        string s_str;
        RobotComm robotComm = null;
        XYZ xyz = new XYZ(1, 2, 3);

        //声明默认的配置文件路径
        public string DefaultINIPath = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory)  + "Config.ini";

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //执行委托的绑定
            URDateCollector.OnGetPositionSuccess += new URDateHandle.GetPositionSuccess(UpdatePositionsValue);
            URDateCollector.OnGetAngleSuccess += new URDateHandle.GetAngleSuccess(UpdateAnglesValue);
            URDateCollector.OnGetRobotStateSuccess += new URDateHandle.GetRobotStateSuccess(UpdateRobotState);


            //这里直接读取配置文件是否启用了自动连接
            FilesINI ConfigController = new FilesINI();
            string AutoConnection = ConfigController.INIRead("UR控制参数", "IfAutoConnect", DefaultINIPath);


            robotComm.StartClient();

            //如果启用了自动连接，则直接获取所有自动连接参数，并运行连接方法
            if (AutoConnection == "YES")
            {
                Do_Initilize();
            }

            



        }

        //不管用户是否勾选自动连接，手动连接都是执行这个方法，区别只是改好了配置文件再连接还是不用改就可以连接
        private void Do_Initilize()
        {
            FilesINI ConfigController = new FilesINI();

            Target_IP = ConfigController.INIRead("UR控制参数", "RemoteIP", DefaultINIPath);
            Control_Port = Convert.ToInt32(ConfigController.INIRead("UR控制参数", "RemoteControlPort", DefaultINIPath));
            DataRefreshRate = Convert.ToInt32(ConfigController.INIRead("UR运动参数", "BasicRefreshRate", DefaultINIPath));

            BasicSpeed = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "BasicSpeed", DefaultINIPath));
            BasicAcceleration = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "BasicAcceleration", DefaultINIPath));


            //我在URDateHandle中定义了刷新速度是静态的，所以可以直接赋值(先赋值，后实例化对象，否则直接运行就报错)
            URDateHandle.ScanRate = DataRefreshRate;

            //初始化URDateCollector，开始实时采集UR数据(需要提供要采集UR的IP地址)
            URDateCollector.InitialMoniter(Target_IP);

            //初始化URControlHandle，生成一个clientSocket
            URController.Creat_client(Target_IP, Control_Port);

            //初始化速度和加速度(基准速度0.15 最高变成2倍即0.2，最低变成0.1倍即0.01)
            SpeedRate = BasicSpeed * SpeedBar.Value / 10;
            AccelerationRate = BasicAcceleration * AccelerationBar.Value / 10;
        }

        private void SpeedChange(object sender, EventArgs e)
        {
            SpeedRate = BasicSpeed * SpeedBar.Value / 10;
        }

        private void AccelerationChange(object sender, EventArgs e)
        {
            AccelerationRate = BasicAcceleration * AccelerationBar.Value / 10;
        }

        //退出程序要把所有都释放掉
        private void QuitApp(object sender, FormClosingEventArgs e)
        {
            URController.Close_client();
            URController = null;
            //robotComm.FinishClient();
        }

        //将取到的数据放入文本框(当需要被通知时候触发)
        void UpdatePositionsValue(float[] Positions)
        { 

            X_Position.Text = Positions[0].ToString("0.0");
            Y_Position.Text = Positions[1].ToString("0.0");
            Z_Position.Text = Positions[2].ToString("0.0");
            U_Position.Text = Positions[3].ToString("0.000");
            V_Position.Text = Positions[4].ToString("0.000");
            W_Position.Text = Positions[5].ToString("0.000");
        }

        void UpdateAnglesValue(double[] Angles)
        {

            int[] AngleBar_Values = new int[6];

            //由于Angle已经取到的是正负360度，所以正负要做区分
            for (int i = 0; i < Angles.Length; i++)
            {
                if (Angles[i] < 0)
                {
                    AngleBar_Values[i] = 360 - Math.Abs(Convert.ToInt32(Angles[i]));
                }
                else
                {
                    AngleBar_Values[i] = 360 + Math.Abs(Convert.ToInt32(Angles[i]));
                }
            }

            //这里使用了自定义控件，所以不再是Value属性
            /*
            AngleBarX.Value = AngleBar_Values[0];
            AngleBarY.Value = AngleBar_Values[1];
            AngleBarZ.Value = AngleBar_Values[2];
            AngleBarU.Value = AngleBar_Values[3];
            AngleBarV.Value = AngleBar_Values[4];
            AngleBarW.Value = AngleBar_Values[5];
            */

            AngleBarX.Position = AngleBar_Values[0];
            AngleBarY.Position = AngleBar_Values[1];
            AngleBarZ.Position = AngleBar_Values[2];
            AngleBarU.Position = AngleBar_Values[3];
            AngleBarV.Position = AngleBar_Values[4];
            AngleBarW.Position = AngleBar_Values[5];

            /*不再需要六个文本框占地方
            X_Angle.Text = Angles[0].ToString("0.00") + "  °";
            Y_Angle.Text = Angles[1].ToString("0.00") + "  °";
            Z_Angle.Text = Angles[2].ToString("0.00") + "  °";
            U_Angle.Text = Angles[3].ToString("0.00") + "  °";
            V_Angle.Text = Angles[4].ToString("0.00") + "  °";
            W_Angle.Text = Angles[5].ToString("0.00") + "  °";
            */

            AngleBarX.Text = Angles[0].ToString("0.00") + "  °";
            AngleBarY.Text = Angles[1].ToString("0.00") + "  °";
            AngleBarZ.Text = Angles[2].ToString("0.00") + "  °";
            AngleBarU.Text = Angles[3].ToString("0.00") + "  °";
            AngleBarV.Text = Angles[4].ToString("0.00") + "  °";
            AngleBarW.Text = Angles[5].ToString("0.00") + "  °";

        }

        void UpdateRobotState(int[] RobotState)
        { 
            //只要能取到第一笔数据，就说明连接成功了
            //你也可以检测258寄存器的数据 RobotMode有很多定义，一个口的不同数字表示了多种状态
            if (RobotState[0] ==1)
            {
                string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
                string PicDir = DebugDir + "\\Button\\BtnReady.png";
                this.RobotStatusPic.Image = Image.FromFile(PicDir);
                this.RobotStatusLabel.Text = "已连接";
            }

            if (RobotState[1] == 1)
            {
                string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
                string PicDir = DebugDir + "\\Button\\BtnSecurityStopped.png";
                this.RobotStatusPic.Image = Image.FromFile(PicDir);
                this.RobotStatusLabel.Text = "安全停机";
            }

            if (RobotState[2] == 1)
            {
                string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
                string PicDir = DebugDir + "\\Button\\BtnEmergencyStoped.png";
                this.RobotStatusPic.Image = Image.FromFile(PicDir);
                this.RobotStatusLabel.Text = "紧急停机";
            }

            if (RobotState[3] == 1)
            {
                string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
                string PicDir = DebugDir + "\\Button\\BtnTeaching.png";
                this.RobotStatusPic.Image = Image.FromFile(PicDir);
                this.RobotStatusLabel.Text = "示教模式";
            }

            //最后一个判断UR是不是在运行程序就不做了，很简单，通过258寄存器取值


        }




        //对于XYZ的线性移动，需要定义一个方法，只需要传入要移动的轴和移动方向(方向就是1和-1)，返回移动的命令
        string GetLinearMovementCommand(string whatAxis,int direction)
        { 
            //不管怎么样都要获取当前的坐标值
            double new_X = URDateHandle.Positions_X;
            double new_Y = URDateHandle.Positions_Y;
            double new_Z = URDateHandle.Positions_Z;
            double new_U = URDateHandle.Positions_U;
            double new_V = URDateHandle.Positions_V;
            double new_W = URDateHandle.Positions_W;

            //然后根据点动的按钮，判断要改哪个值(这里不是旋转，只有X,Y,Z三种可能)，直接覆盖到真实的当前XYZ值
            if (whatAxis == "X")
            {
                new_X = ((new_X + 10) * direction);
            }
            else if (whatAxis == "Y")
            {
                new_Y = ((new_Y + 10) * direction);
            }
            else if (whatAxis == "Z")
            {
                new_Z = ((new_Z + 10) * direction);
            }
            else
            { 
                //也有可能我不要移动，只是要看指令
            }


            //最后把方向运动的指令发送出去
            string command = "movel(p[" + new_X.ToString() + "," + new_Y.ToString() + "," + new_Z.ToString() + "," + new_U.ToString() + "," + new_V.ToString() + "," + new_W.ToString() + "], a = " + AccelerationRate.ToString() + ", v = " + SpeedRate.ToString() + ")";
            CustomCommand.Text = command;
            return command;
        }

        //对于六轴转动，跟前面类似
        string GetRotationMovementCommand(string whatAxis,int direction)
        {
            //不管怎么样都要获取当前的六个关节值
            double new_X = URDateHandle.Angles_X;
            double new_Y = URDateHandle.Angles_Y;
            double new_Z = URDateHandle.Angles_Z;
            double new_U = URDateHandle.Angles_U;
            double new_V = URDateHandle.Angles_V;
            double new_W = URDateHandle.Angles_W;

            if (whatAxis == "X")
            {
                new_X = ((new_X + 100) * direction);
            }
            else if (whatAxis == "Y")
            {
                new_Y = ((new_Y + 100) * direction);
            }
            else if (whatAxis == "Z")
            {
                new_Z = ((new_Z + 100) * direction);
            }
            else if (whatAxis == "U")
            {
                new_U = ((new_U + 100) * direction);
            }
            else if (whatAxis == "V")
            {
                new_V = ((new_V + 100) * direction);
            }
            else if (whatAxis == "W")
            {
                new_W = ((new_W + 100) * direction);
            }
            else
            {
                //也有可能我不要移动，只是要看指令
            }
            //最后把方向运动的指令发送出去
            string command = "movej([" + new_X.ToString() + "," + new_Y.ToString() + "," + new_Z.ToString() + "," + new_U.ToString() + "," + new_V.ToString() + "," + new_W.ToString() + "], a = " + AccelerationRate.ToString() + ", v = " + SpeedRate.ToString() + ")";
            CustomCommand.Text = command;
            return command;

        }

        //发送停止命令则很简单了，都是发送stopl(1)
        string GetStopCommand()
        {
            string StopCommand = "stopl(1)";
            CustomCommand.Text = StopCommand;
            return StopCommand;
        }

        # region XYZ平移区域
        //XYZ左移按钮按下
        private void Move_Left_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\ArrowXYZLeft_click.png";
            this.Move_Left.Image = Image.FromFile(PicDir);

            string str = GetLinearMovementCommand("X", 1);
            URController.Send_command(str);
        }
        //XYZ左移按钮松开
        private void Move_Left_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\ArrowXYZLeft_normal.png";
            this.Move_Left.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);

        }
        //XYZ右移按钮按下
        private void Move_Right_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\ArrowXYZRight_click.png";
            this.Move_Right.Image = Image.FromFile(PicDir);

            string str = GetLinearMovementCommand("X", -1);
            URController.Send_command(str);

        }
        //XYZ右移按钮松开
        private void Move_Right_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\ArrowXYZRight_normal.png";
            this.Move_Right.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);

        }
        //XYZ后移按钮按下
        private void Move_Back_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\ArrowXYZBack_click.png";
            this.Move_Back.Image = Image.FromFile(PicDir);

            string str = GetLinearMovementCommand("Y", -1);
            URController.Send_command(str);

        }
        //XYZ后移按钮松开
        private void Move_Back_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\ArrowXYZBack_normal.png";
            this.Move_Back.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);
        }
        //XYZ前移按钮按下
        private void Move_Forward_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\ArrowXYZForward_click.png";
            this.Move_Forward.Image = Image.FromFile(PicDir);

            string str = GetLinearMovementCommand("Y", 1);
            URController.Send_command(str);
        
        }
        //XYZ前移按钮松开
        private void Move_Forward_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\ArrowXYZForward_normal.png";
            this.Move_Forward.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);

        }
        //XYZ上移按钮按下
        private void Move_Up_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\ArrowXYZUp_click.png";
            this.Move_Up.Image = Image.FromFile(PicDir);

            string str = GetLinearMovementCommand("Z", 1);
            URController.Send_command(str);

        }
        //XYZ上移按钮松开
        private void Move_Up_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\ArrowXYZUp_normal.png";
            this.Move_Up.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);

        }
        //XYZ下移按钮按下
        private void Move_Down_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\ArrowXYZDown_click.png";
            this.Move_Down.Image = Image.FromFile(PicDir);

            string str = GetLinearMovementCommand("Z", -1);
            URController.Send_command(str);

        }
        //XYZ下移按钮松开
        private void Move_Down_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\ArrowXYZDown_normal.png";
            this.Move_Down.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);
        }

        #endregion

        #region X左右旋转

        //六轴旋转（X向左转按下）
        private void X_Left_Rotate_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_left_click.png";
            this.X_Left_Rotate.Image = Image.FromFile(PicDir);

            string str = GetRotationMovementCommand("X", -1);
            URController.Send_command(str);

        }
        //六轴旋转（X向左转松开）
        private void X_Left_Rotate_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_left_normal.png";
            this.X_Left_Rotate.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);

        }
        //六轴旋转（X向右转按下）
        private void X_Right_Rotate_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_right_click.png";
            this.X_Right_Rotate.Image = Image.FromFile(PicDir);

            string str = GetRotationMovementCommand("X", 1);
            URController.Send_command(str);
        }
        //六轴旋转（X向右转松开）
        private void X_Right_Rotate_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_right_normal.png";
            this.X_Right_Rotate.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);
        }

        

        //六轴旋转（Y向左转按下）
        private void Y_Left_Rotate_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_left_click.png";
            this.Y_Left_Rotate.Image = Image.FromFile(PicDir);

            string str = GetRotationMovementCommand("Y", -1);
            URController.Send_command(str);
        }
        //六轴旋转（Y向左转松开）
        private void Y_Left_Rotate_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_left_normal.png";
            this.Y_Left_Rotate.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);
        }

        //六轴旋转（Y向右转按下）
        private void Y_Right_Rotate_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_right_click.png";
            this.Y_Right_Rotate.Image = Image.FromFile(PicDir);

            string str = GetRotationMovementCommand("Y", 1);
            URController.Send_command(str);
        }
        //六轴旋转（Y向右转松开）
        private void Y_Right_Rotate_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_right_normal.png";
            this.Y_Right_Rotate.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);
        }


        //六轴旋转（Z向左转按下）
        private void Z_Left_Rotate_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_left_click.png";
            this.Z_Left_Rotate.Image = Image.FromFile(PicDir);

            string str = GetRotationMovementCommand("Z", -1);
            URController.Send_command(str);
        }
        //六轴旋转（Z向左转松开）
        private void Z_Left_Rotate_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_left_normal.png";
            this.Z_Left_Rotate.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);
        }
        //六轴旋转（Z向右转按下）
        private void Z_Right_Rotate_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_right_click.png";
            this.Z_Right_Rotate.Image = Image.FromFile(PicDir);

            string str = GetRotationMovementCommand("Z", 1);
            URController.Send_command(str);
        }
        //六轴旋转（Z向右转松开）
        private void Z_Right_Rotate_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_right_normal.png";
            this.Z_Right_Rotate.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);
        }

        //六轴旋转（U向左转按下）
        private void U_Left_Rotate_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_left_click.png";
            this.U_Left_Rotate.Image = Image.FromFile(PicDir);

            string str = GetRotationMovementCommand("U", -1);
            URController.Send_command(str);
        }
        //六轴旋转（U向左转松开）
        private void U_Left_Rotate_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_left_normal.png";
            this.U_Left_Rotate.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);
        }
        //六轴旋转（U向右转按下）
        private void U_Right_Rotate_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_right_click.png";
            this.U_Right_Rotate.Image = Image.FromFile(PicDir);

            string str = GetRotationMovementCommand("U", 1);
            URController.Send_command(str);
        }
        //六轴旋转（U向右转松开）
        private void U_Right_Rotate_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_right_normal.png";
            this.U_Right_Rotate.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);
        }

        //六轴旋转（V向左转按下）
        private void V_Left_Rotate_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_left_click.png";
            this.V_Left_Rotate.Image = Image.FromFile(PicDir);

            string str = GetRotationMovementCommand("V", -1);
            URController.Send_command(str);
        }
        //六轴旋转（V向左转松开）
        private void V_Left_Rotate_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_left_normal.png";
            this.V_Left_Rotate.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);
        }
        //六轴旋转（V向右转按下）
        private void V_Right_Rotate_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_right_click.png";
            this.V_Right_Rotate.Image = Image.FromFile(PicDir);

            string str = GetRotationMovementCommand("V",1);
            URController.Send_command(str);
        }
        //六轴旋转（V向右转松开）
        private void V_Right_Rotate_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_right_normal.png";
            this.V_Right_Rotate.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);
        }
        //六轴旋转（W向左转按下）
        private void W_Left_Rotate_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_left_click.png";
            this.W_Left_Rotate.Image = Image.FromFile(PicDir);

            string str = GetRotationMovementCommand("W", -1);
            URController.Send_command(str);
        }
        //六轴旋转（W向左转松开）
        private void W_Left_Rotate_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_left_normal.png";
            this.W_Left_Rotate.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);
        }
        //六轴旋转（W向右转按下）
        private void W_Right_Rotate_Down(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_right_click.png";
            this.W_Right_Rotate.Image = Image.FromFile(PicDir);

            string str = GetRotationMovementCommand("W",1);
            URController.Send_command(str);
        }
        //六轴旋转（W向右转松开）
        private void W_Right_Rotate_Up(object sender, MouseEventArgs e)
        {
            string DebugDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            string PicDir = DebugDir + "\\Arrow\\Arrow_right_normal.png";
            this.W_Right_Rotate.Image = Image.FromFile(PicDir);

            string str = GetStopCommand();
            URController.Send_command(str);
        }
        # endregion


        #region 顶部菜单栏

        //文件-参数设置
        private void File_SetParameter_Click(object sender, EventArgs e)
        {

            //我决定还是少用一点华而不实的功能，不就是设置参数嘛，何必搞一大堆配置文件，又不是很多参数，直接打开这个窗口
            Config ConfigWindow = new Config(DefaultINIPath);
            ConfigWindow.ShowDialog();
        }
        
        //文件-手动连接
        private void File_Connect_Click(object sender, EventArgs e)
        {
            //用户没有勾选自动连接，则是每次修改好了的配置文件去读取并执行连接方法
            Do_Initilize();
        }

        //文件-断开连接
        private void File_Disconnect_Click(object sender, EventArgs e)
        {
            //用户点击断开连接，则
            URDateCollector = null;
            URController.Close_client();
        }


        //帮助-所有版本
        private void Help_AllVersion_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://pan.baidu.com/s/1i3KBSDf");
        }

        //帮助-关于本软件
        private void Help_Hint_Click(object sender, EventArgs e)
        {
            //生成一个实例
            AboutMe About = new AboutMe();
            About.ShowDialog();
        }

        //帮助-问题反馈
        private void Help_Feedback_Click(object sender, EventArgs e)
        {
            //生成一个实例
            Feedback Feed = new Feedback();
            Feed.ShowDialog();
        }

        

        //常用工具-IP修改
        private void Tools_IPChange_Click(object sender, EventArgs e)
        {
            //生成一个实例
            IPChange IPWindow = new IPChange();
            IPWindow.ShowDialog();
        }

        //常用工具-自定义命令面板(单击一次显示，再单击一次隐藏)
        private void Tools_PersonalCommand_Click(object sender, EventArgs e)
        {

            //由于点击按钮之后都会触发，所以只需要判断这三个控件的可见性即可反复的显示或隐藏
            if (CustomLabel.Visible == false)
            {
                CustomLabel.Visible = true;
                CustomCommand.Visible = true;
                btnCustomSend.Visible = true;
                Change_All_Position.Visible = true;
                Change_All_Angles.Visible = true;

                Tools_PersonalCommand.Text = "隐藏自定义命令";
            }
            else
            {
                CustomLabel.Visible = false;
                CustomCommand.Visible = false;
                btnCustomSend.Visible = false;
                Change_All_Position.Visible = false;
                Change_All_Angles.Visible = false;

                Tools_PersonalCommand.Text = "显示自定义命令";
            }
        }

        //这就是自定义命令的三个控件，只要控制他们显示与隐藏即可（btnCustomSend,CustomCommand,CustomLabel）
        private void btnCustomSend_Click(object sender, EventArgs e)
        {
            //我在测试的框子中可以放任意命令
            string str = CustomCommand.Text;
            URController.Send_command(str);
        }

        //常用工具-G代码转换面板
        private void Tools_Gcode_Click(object sender, EventArgs e)
        {
            GCode GcodeWindow = new GCode(DefaultINIPath);
            GcodeWindow.Show();
        }

        //常用工具-增强示教面板
        private void Tools_Teach_Click(object sender, EventArgs e)
        {
            Teach TeachWindow = new Teach(DefaultINIPath);
            TeachWindow.Show();
        }

        //常用工具-相机标定及特征识别面板
        private void Tools_CameraCalibrate_Click(object sender, EventArgs e)
        {
            CameraCalibration CameraWindow = new CameraCalibration(DefaultINIPath);
            CameraWindow.Show();
        }

        //常用工具-相机标定及特征追踪面板
        private void Tools_CameraTracking_Click(object sender, EventArgs e)
        {

        }

        //常用工具，相机标定及视觉分拣面板
        private void Tools_CameraSorting_Click(object sender, EventArgs e)
        {

        }


        //测试工具：寄存器读写测试
        private void Tools_RegisterTest_Click(object sender, EventArgs e)
        {
            //还是要把配置文件的地址传过去
            Register RegisterWindow = new Register(DefaultINIPath);
            RegisterWindow.Show();
        }

        //测试工具：绘图工具测试
        private void Tools_DrawingTest_Click(object sender, EventArgs e)
        {
            Painting PaintWindow = new Painting();
            PaintWindow.Show();
        }

        //测试工具：图像轮廓拟合测试
        private void Tools_ImageProfileTest_Click(object sender, EventArgs e)
        {

        }



        //测试工具：Dashboard
        private void Tools_DashboardTest_Click(object sender, EventArgs e)
        {
            Dashboard DashboardWindow = new Dashboard(DefaultINIPath);
            DashboardWindow.Show();
        }



        #endregion

        //有时候我就是要往X方向走1mm，则直接修改坐标即可
        private void Change_All_Position_Click(object sender, EventArgs e)
        {
            //获取下面六个值，然后发送(并没有ABC这个轴，我只是不作处理)
            string str = GetLinearMovementCommand("ABC", 1);
            CustomCommand.Text = str;

        }

        private void Change_All_Angles_Click(object sender, EventArgs e)
        {
            //获取下面六个值，然后发送(并没有ABC这个轴，我只是不作处理)
            string str = GetRotationMovementCommand("ABC", 1);
            CustomCommand.Text = str;
        }

        private void Help_UpdateHistory_Click(object sender, EventArgs e)
        {
            //获取当前目录(我把发布方式改成Release就是Release而不是Debug了)
            //string ReleaseDir = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);
            System.Diagnostics.Process.Start("Document\\history.doc");
        }

        private void HelpDocument_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Document\\readme.doc");
        }

        private void Xplus1_Click(object sender, EventArgs e)
        {
            //string str = LinearMovementPlus("X", 0.001);
            //URController.Send_command(str);
            
        }

        string LinearMovementPlus(string axis, double distance)
        {
            //不管怎么样都要获取当前的坐标值
            double new_X = URDateHandle.Positions_X;
            double new_Y = URDateHandle.Positions_Y;
            double new_Z = URDateHandle.Positions_Z;
            double new_U = URDateHandle.Positions_U;
            double new_V = URDateHandle.Positions_V;
            double new_W = URDateHandle.Positions_W;

            if (axis == "X")
                new_X = new_X + distance;
            else if (axis == "Y")
                new_Y = new_Y + distance;
            else if (axis == "Z")
                new_Z = new_Z + distance;
            else
            { }

            //最后把方向运动的指令发送出去
            string command = "movel(p[" + new_X.ToString() + "," + new_Y.ToString() + "," + new_Z.ToString() + "," + new_U.ToString() + "," + new_V.ToString() + "," + new_W.ToString() + "], a = " + AccelerationRate.ToString() + ", v = " + SpeedRate.ToString() + ")";
            CustomCommand.Text = command;
            return command;
        }

        private void Xsub1_Click(object sender, EventArgs e)
        {
            string str = LinearMovementSub("X", 0.001);
            URController.Send_command(str);
        }
        string LinearMovementSub(string axis, double distance)
        {
            //不管怎么样都要获取当前的坐标值
            double new_X = URDateHandle.Positions_X;
            double new_Y = URDateHandle.Positions_Y;
            double new_Z = URDateHandle.Positions_Z;
            double new_U = URDateHandle.Positions_U;
            double new_V = URDateHandle.Positions_V;
            double new_W = URDateHandle.Positions_W;

            if (axis == "X")
                new_X = new_X - distance;
            else if (axis == "Y")
                new_Y = new_Y - distance;
            else if (axis == "Z")
                new_Z = new_Z - distance;
            else
            { }

            //最后把方向运动的指令发送出去
            string command = "movel(p[" + new_X.ToString() + "," + new_Y.ToString() + "," + new_Z.ToString() + "," + new_U.ToString() + "," + new_V.ToString() + "," + new_W.ToString() + "], a = " + AccelerationRate.ToString() + ", v = " + SpeedRate.ToString() + ")";
            CustomCommand.Text = command;
            return command;
        }

        private void Yplus1_Click(object sender, EventArgs e)
        {
            string str = LinearMovementPlus("Y", 0.001);
            URController.Send_command(str);
        }//Y方向移动+1mm

        private void Ysub1_Click(object sender, EventArgs e)
        {
            string str = LinearMovementSub("Y", 0.001);
            URController.Send_command(str);
        }//Y方向移动-1mm

        private void GoPoint_Click(object sender, EventArgs e)
        {            
            double x = 0.15;
            double y = 0.172;
            double z = 0.586;//第一层起始位置
            
            s_str = "movel(p[0.15,0.172,0.555,0.005,-0.018,4.215],a=0.1,v=0.1)";//home
            URController.Send_command(s_str);
            Thread.Sleep(2000);

            #region 连续打印

            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.05,t=3)";//打印起始位置
            URController.Send_command(s_str);
            Thread.Sleep(3000);

            for (int k = 1; k < 6; k = k + 1)//k决定层数
            {            
                movesquare_cons(x, y, z);//正方形路径控制
                z = z - (double)1.6 / 1000;//每两层高度差
            }
            #endregion
        }

        private void movesquare(double x,double y,double z)
        {
            double tx = x;
            double ty = y;
            double tz = z;
            int j = 0;

            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.1,t=1.2)";//每一层起始位置
            URController.Send_command(s_str);
            Thread.Sleep(1200);          

            for (int i = 1; i < 20; i++)
            {
                if (i % 2 != 0)//移动y
                    if (j % 2 == 0)//判断+y还是-y
                    {
                        y = y - 0.01;//每次走20mm
                        j++;
                        s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.01,t=1)";
                        URController.Send_command(s_str);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        y = y + 0.01;
                        j++;
                        s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.01,t=1)";
                        URController.Send_command(s_str);
                        Thread.Sleep(1000);
                    }
                else//移动x
                {
                    x = x - 0.001;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.01,t=0.15)";
                    URController.Send_command(s_str);
                    Thread.Sleep(150);
                }
            }

            s_str = "movel(p[0.16372,-0.042,0.5116,0.0216,0.0216,-3.3468],a=0.1,v=0.1)";//home
            URController.Send_command(s_str);
            Thread.Sleep(2000);

            x = tx;
            y = ty;
            z = tz;
            j = 0;

            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.1,t=1.2)";//每一层起始位置
            URController.Send_command(s_str);
            Thread.Sleep(1200);
                
            for (int i = 1; i < 20; i++)
            {
                if (i % 2 != 0)//移动x
                    if (j % 2 == 0)//判断+x还是-x
                    {
                        x = x - 0.01;
                        j++;
                        s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.01,t=1)";
                        URController.Send_command(s_str);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        x = x + 0.01;
                        j++;
                        s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.01,t=1)";
                        URController.Send_command(s_str);
                        Thread.Sleep(1000);
                    }
                else//移动y
                {
                    y = y - 0.001;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.01,t=0.15)";
                    URController.Send_command(s_str);
                    Thread.Sleep(150);
                }
            }

            s_str = "movel(p[0.16372,-0.042,0.5116,0.0216,0.0216,-3.3468],a=0.1,v=0.1)";//home
            URController.Send_command(s_str);
            Thread.Sleep(2000);
        }

        private void btn_GoTxtPoint_Click(object sender, EventArgs e)
        {
            s_str = "movel(p[-0.1246,0.183,0.624,-0.007,0.39,0.001],a=0.15,v=0.15)";//路径起始位置
            URController.Send_command(s_str);
            Thread.Sleep(3000);

            String path = Application.StartupPath + "\\path2.txt";
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line = null;

            while ((line = sr.ReadLine()) != null)
            {
                String[] str = line.Split(' ');
                s_str = "movel(p[" + str[0] + "," + str[1] + "," + str[2] + "," + str[3] + "," + str[4] + "," + str[5] + "],a=0.1,v=0.1,t=0.6)";
                URController.Send_command(s_str);
                Thread.Sleep(600);

                xyz.SetXYZ(Convert.ToDouble(str[0]), Convert.ToDouble(str[1]), Convert.ToDouble(str[2]));
                SendStatusMsg(sender, e);
                Thread.Sleep(6000);
            }

            s_str = "movel(p[-0.1246,0.183,0.624,0,0,0],a=0.15,v=0.15)";//home
            URController.Send_command(s_str);
            //Thread.Sleep(2000);
        }

        private void stop_Click(object sender, EventArgs e)
        {
            string Stopstr = "stopl(1)";
            URController.Send_command(Stopstr);
        }

        private void GoSpiral_Click(object sender, EventArgs e)
        {
            double x = 0.16372;
            double y = -0.042;
            double z = 0.5015;//第一层起始位置

            s_str = "movel(p[0.16372,-0.042,0.4806,0.0216,0.0216,-3.3468],a=0.1,v=0.1)";//home
            URController.Send_command(s_str);
            Thread.Sleep(2000);

            for (int k = 1; k < 11; k = k + 1)//k决定层数
            {
                z = z - (double)1 / 1000;//每层高度差
                movespiral(x, y, z);//螺旋形路径控制，正方
            }
        }

        private void movespiral(double x, double y, double z)
        {
            double j = 1,ts;
            int t0 = 150,t;
            int k=1;

            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.1)";//每一层起始位置
            URController.Send_command(s_str);
            Thread.Sleep(1000);

            for (int i = 1; i <= 40; i++)  //循环次数i决定宽度i/2
            {
                if (i % 2 != 0)//移动y还是移动x
                    if (k % 2 != 0)//判断+y还是-y
                    {
                        y = y - j / 1000;//每次走j mm
                        t = Convert.ToInt16(j * t0);
                        ts = Convert.ToDouble(t) / 1000;
                        //j++;
                        s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.01," + "t=" + ts.ToString() + ")";
                        URController.Send_command(s_str);
                        Thread.Sleep(t);
                    }
                    else
                    {
                        y = y + j / 1000;//每次走j mm
                        t = Convert.ToInt16(j * t0);
                        ts = Convert.ToDouble(t) / 1000;
                        //s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.01)";
                        s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.01," + "t=" + ts.ToString() + ")";
                        URController.Send_command(s_str);
                        Thread.Sleep(t);
                    }             
                else//移动x
                {
                    if (k % 2 != 0)//判断+x还是-x
                    {
                        x = x - j / 1000;//每次走j mm
                        t = Convert.ToInt16(j * t0);
                        ts = Convert.ToDouble(t) / 1000;
                        s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.01," + "t=" + ts.ToString() + ")";
                        URController.Send_command(s_str);
                        Thread.Sleep(t);
                    }
                    else
                    {
                        x = x + j / 1000;//每次走j mm
                        t = Convert.ToInt16(j * t0);
                        ts = Convert.ToDouble(t) / 1000;
                        s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.01," + "t=" + ts.ToString() + ")";
                        URController.Send_command(s_str);
                        Thread.Sleep(t);
                    } 
                    k = k + 1;
                }
                if (i % 2 == 0)//走两次增加距离一次
                    j = j + 1;
            }


        }

        private void movecircle(double x, double y, double z)
        {
            double bx,by,vx,vy,px,py;
            double r = 1;
            int t0 = 300, t;

            //s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.1)";//home
            //URController.Send_command(s_str);
            //Thread.Sleep(1000);

            for (r = 1; r <= 10; r++)
            {
                s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + (z-0.01).ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.1)";//每一圈走完停泊位置,(x,y,z)圆心
                URController.Send_command(s_str);
                Thread.Sleep(1000);

                bx = x - r / 1000; by = y;
                s_str = "movel(p[" + bx.ToString() + "," + by.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468],a=0.1,v=0.1)";//每一圈起始位置
                URController.Send_command(s_str);
                Thread.Sleep(1000);

                t = Convert.ToInt16(r * t0);
                vx = x; vy = y + r / 1000;
                px = x + r / 1000; py = y;
                //s_str = "movec(p[" + vx.ToString() + "," + vy.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468]," +
                //    "p[" + px.ToString() + "," + py.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468]," + "a=0.1,v=0.01)";
                s_str = "movec(p[" + vx.ToString() + "," + vy.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468]," +
                        "p[" + px.ToString() + "," + py.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468]," + "a=0.1,v=0.01)";
                URController.Send_command(s_str);
                Thread.Sleep(t);

                vx = x; vy = y - r / 1000;
                px = bx; py = by;
                s_str = "movec(p[" + vx.ToString() + "," + vy.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468]," +
                        "p[" + px.ToString() + "," + py.ToString() + "," + z.ToString() + ",0.0216,0.0216,-3.3468]," + "a=0.1,v=0.01)";
                URController.Send_command(s_str);
                Thread.Sleep(t);
            } 

        }

        private void GoCircle_Click(object sender, EventArgs e)
        {
            double x = 0.16372;
            double y = -0.042;
            double z = 0.5015;//第一层起始位置

            s_str = "movel(p[0.16372,-0.042,0.4806,0.0216,0.0216,-3.3468],a=0.1,v=0.1)";//home
            URController.Send_command(s_str);
            Thread.Sleep(2000);

            for (int k = 1; k < 11; k = k + 1)//k决定层数
            {
                z = z - (double)1 / 1000;//每层高度差
                movecircle(x, y, z);//环形路径控制
            }
        }

        private void movesquare_con(double x,double y,double z)
        {
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=0.08)";
            URController.Send_command(s_str);
            Thread.Sleep(80);
            xyz = new XYZ(x,y,z);
            SendStatusMsg(null, null);

            for (int i = 1; i < 21; i++)
            {              
                if (i % 2 == 0)//判断+y还是-y
                {
                    y = y + 0.02;//每次走20mm
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=2.5)";
                    URController.Send_command(s_str);
                    Thread.Sleep(2500);
                }
                else
                {
                    y = y - 0.02;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=2.5)";
                    URController.Send_command(s_str);
                    Thread.Sleep(2500);
                }

                x = x - 0.001;
                s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=0.2)";
                URController.Send_command(s_str);
                Thread.Sleep(200);
                
            }

            y = y - 0.02;
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=2.5)";
            URController.Send_command(s_str);
            Thread.Sleep(2500);

            z = z - 0.0008;
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=0.08)";
            URController.Send_command(s_str);
            Thread.Sleep(80);

            for (int i = 1; i < 21; i++)
            {             
                if (i % 2 == 0)//判断+x还是-x
                {
                    x = x - 0.02;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=2.5)";
                    URController.Send_command(s_str);
                    Thread.Sleep(2500);
                }
                else
                {
                    x = x + 0.02;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=2.5)";
                    URController.Send_command(s_str);
                    Thread.Sleep(2500);
                }
                
                y = y + 0.001;
                s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=0.2)";
                URController.Send_command(s_str);
                Thread.Sleep(200);            
            }

            x = x + 0.02;
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=2.5)";
            URController.Send_command(s_str);
            Thread.Sleep(2500);

        }//速度较快

        private void movesquare_cons(double x, double y, double z)//速度慢
        {
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.05,v=0.01,t=0.6)";
            URController.Send_command(s_str);
            Thread.Sleep(600);

            for (int i = 1; i < 11; i++)
            {
                if (i % 2 == 0)//判断+y还是-y
                {
                    y = y + 0.01;//每次走10mm
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=6)";
                    URController.Send_command(s_str);
                    Thread.Sleep(6000);
                }
                else
                {
                    y = y - 0.01;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=6)";
                    URController.Send_command(s_str);
                    Thread.Sleep(6000);
                }

                x = x - 0.001;
                s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.05,v=0.01,t=0.6)";
                URController.Send_command(s_str);
                Thread.Sleep(600);


            }

            y = y - 0.01;
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=6)";
            URController.Send_command(s_str);
            Thread.Sleep(6000);

            z = z - 0.0008;
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.05,v=0.01,t=0.6)";
            URController.Send_command(s_str);
            Thread.Sleep(600);

            for (int i = 1; i < 11; i++)
            {
                if (i % 2 == 0)//判断+x还是-x
                {
                    x = x - 0.01;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=6)";
                    URController.Send_command(s_str);
                    Thread.Sleep(6000);
                }
                else
                {
                    x = x + 0.01;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=6)";
                    URController.Send_command(s_str);
                    Thread.Sleep(6000);
                }

                y = y + 0.001;
                s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.05,v=0.01,t=0.6)";
                URController.Send_command(s_str);
                Thread.Sleep(600);
            }

            x = x + 0.01;
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=6)";
            URController.Send_command(s_str);
            Thread.Sleep(6000);

        }

        private void movesquare_newcon(double x,double y,double z)//每个点间隔1mm
        {

            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=0.08)";
            URController.Send_command(s_str);
            Thread.Sleep(80);//走到指定层高

            for (int j = 1; j < 21; j++)
            {
                if (j % 2 != 0)
                {
                    for (int i = 1; i < 21; i++)
                    {
                        x = x + 0.001;
                        s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=0.25)";
                        URController.Send_command(s_str);
                        Thread.Sleep(250);
                    }
                }
                else
                {
                    for (int i = 1; i < 21; i++)
                    {
                        x = x - 0.001;
                        s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=0.25)";
                        URController.Send_command(s_str);
                        Thread.Sleep(250);
                    }
                }

                y = y + 0.001;
                s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=0.2)";
                URController.Send_command(s_str);
                Thread.Sleep(200);
            }

            for (int i = 1; i < 21; i++)  //补齐正方形
            {
                x = x + 0.001;
                s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=0.2)";
                URController.Send_command(s_str);
                Thread.Sleep(200);
            }

            z = z - 0.0006;
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=0.08)";
            URController.Send_command(s_str);
            Thread.Sleep(80);//抬高一层连续打印

            //回到上一层起始点
            for (int j = 1; j < 21; j++) 
            {
                if (j % 2 != 0)
                {
                    for (int i = 1; i < 21; i++)
                    {
                        y = y - 0.001;
                        s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=0.2)";
                        URController.Send_command(s_str);
                        Thread.Sleep(200);
                    }
                }
                else
                {
                    for (int i = 1; i < 21; i++)
                    {
                        y = y + 0.001;
                        s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=0.2)";
                        URController.Send_command(s_str);
                        Thread.Sleep(200);
                    }
                }

                x = x - 0.001;
                s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=0.2)";
                URController.Send_command(s_str);
                Thread.Sleep(200);
            }

            for (int i = 1; i < 21; i++)  //补齐正方形
            {
                y = y - 0.001;
                s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0.005,-0.018,4.215],a=0.1,v=0.01,t=0.2)";
                URController.Send_command(s_str);
                Thread.Sleep(200);
            }
        }

        public void OnBroadCastingInfo(string message)
        {
            //MessageBox.Show(message);
        }

        public class XYZ
        {
            public XYZ(double _x,double _y,double _z)
            {
                x = _x;
                y = _y;
                z = _z;
            }

            private double x = 0;
            private double y = 0;
            private double z = 0;

            public double X
            {
                get { return x; }
                set { x = value; }
            }

            public double Y
            {
                get { return y; }
                set { y = value; }
            }

            public double Z
            {
                get { return z; }
                set { z = value; }
            }

            public  void SetXYZ(double _x,double _y,double _z)
            {
                x = _x;
                y = _y;
                z = _z;
            }
        }

        private void SendStatusMsg(object sender, EventArgs e)
        {
            string xyzStr = JsonConvert.SerializeObject((object)xyz);

            CommObj commObj = new CommObj();
            commObj.SrcId = 0x00000002;
            commObj.DestId = 0x00000000;
            commObj.SendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            commObj.DataType = "XYZ";
            commObj.DataBody = xyzStr;
            commObj.DataCmd = "ID";

            string json = CommObj.ToJson(commObj);
            robotComm.SendToServer(json);
        }

        private void 通信测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xyz.SetXYZ(3.0, 4.0, 5.0);

            SendStatusMsg(sender,e);
        }

        private void movesquare_newpath(double x, double y, double z)//速度慢
        {
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=0.6)";
            URController.Send_command(s_str);
            Thread.Sleep(600);

            y = y + 0.01;//每次走10mm，外围一圈
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=6)";
            URController.Send_command(s_str);
            Thread.Sleep(6000);

            x = x + 0.01;//每次走10mm
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=6)";
            URController.Send_command(s_str);
            Thread.Sleep(6000);

            y = y - 0.01;//每次走10mm
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=6)";
            URController.Send_command(s_str);
            Thread.Sleep(6000);

            x = x - 0.01;//每次走10mm
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=6)";
            URController.Send_command(s_str);
            Thread.Sleep(6000);

            for (int i = 1; i < 9; i++)//中间路径填充
            {
                if (i % 2 == 0)//判断+x还是-x
                {
                    y = y + 0.001; x = x + 0.001;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=0.6)";
                    URController.Send_command(s_str);
                    Thread.Sleep(600);

                    x = x + 0.009;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=5.4)";
                    URController.Send_command(s_str);
                    Thread.Sleep(5400);
                }
                else
                {
                    y = y + 0.001; x = x - 0.001;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=0.6)";
                    URController.Send_command(s_str);
                    Thread.Sleep(600);

                    x = x - 0.009;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=5.4)";
                    URController.Send_command(s_str);
                    Thread.Sleep(5400);
                }
            }

            y = y + 0.001; x = x + 0.001;
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=0.6)";
            URController.Send_command(s_str);
            Thread.Sleep(600);

            x = x + 0.008;
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=4.8)";
            URController.Send_command(s_str);
            Thread.Sleep(4800);

            y = y + 0.001; x = x + 0.001;//走到对角点
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=0.6)";
            URController.Send_command(s_str);
            Thread.Sleep(600);

            z = z - 0.0008;//往上一层
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=0.6)";
            URController.Send_command(s_str);
            Thread.Sleep(600);

            y = y - 0.01;//每次走10mm，外围一圈
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=6)";
            URController.Send_command(s_str);
            Thread.Sleep(6000);

            x = x - 0.01;//每次走10mm
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=6)";
            URController.Send_command(s_str);
            Thread.Sleep(6000);

            y = y + 0.01;//每次走10mm
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=6)";
            URController.Send_command(s_str);
            Thread.Sleep(6000);

            x = x + 0.01;//每次走10mm
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=6)";
            URController.Send_command(s_str);
            Thread.Sleep(6000);

            for (int i = 1; i < 9; i++)
            {
                if (i % 2 == 0)//判断+x还是-x
                {
                    x = x - 0.001; y = y - 0.001;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=0.6)";
                    URController.Send_command(s_str);
                    Thread.Sleep(600);

                    y = y - 0.009;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=5.4)";
                    URController.Send_command(s_str);
                    Thread.Sleep(5400);
                }
                else
                {
                    x = x - 0.001; y = y + 0.001;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=0.6)";
                    URController.Send_command(s_str);
                    Thread.Sleep(600);

                    y = y - 0.009;
                    s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=5.4)";
                    URController.Send_command(s_str);
                    Thread.Sleep(5400);
                }
            }

            x = x - 0.001; y = y - 0.001;
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=0.6)";
            URController.Send_command(s_str);
            Thread.Sleep(600);

            y = y - 0.008;
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=4.8)";
            URController.Send_command(s_str);
            Thread.Sleep(4800);

            x = x - 0.001; y = y - 0.001;
            s_str = "movel(p[" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ",0,0,0],a=0.1,v=0.01,t=0.6)";
            URController.Send_command(s_str);
            Thread.Sleep(600);//回到起始点

        }

    }
}
