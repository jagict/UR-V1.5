using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using URDate;
using URControl;
using Files;
using System.IO;

namespace UR_点动控制器
{
    public partial class Teach : Form
    {
        public string DefaultINIPath;
        public string DefaultScriptPath = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory);

        public Teach(string ConfigFilePath)
        {
            InitializeComponent();
            DefaultINIPath = ConfigFilePath;
        }

        URControlHandle URController = new URControlHandle();

        //定义全局的记录点位数量
        public static int Global_Tick = 0;
        //定义全局的记录频率
        public static int Global_Tick_Time = 100;
        //定义最大的存储点位数量（因为这里是根据时间取样的，所以能记录的时间就是总的时间*采样间隔）
        public static int Total_Counts = 1000;
        //定义存储的点位数组
        public static double[,] Total_PointRecord;
        //定义存储的点位数组的字符串形式
        public static string[] Total_PointRecordString;
        //默认不与任何东西做合成（如果是与Base合成，则该值为1，如果是与Tool合成，则该值为2）
        public static int Add_What = 0;

        //定义存储关节的数组
        public static double[,] Total_AngleRecord;
        //定义存储的关节数组的字符串形式
        public static string[] Total_AngleRecordString;
        //定义存储的时候是否保存三个参数的布尔值
        public static bool Include_Accleration = true;
        public static bool Include_Speed = true;
        public static bool Include_Radius = true;

        private void Teach_Load(object sender, EventArgs e)
        {
            FilesINI ConfigController = new FilesINI();
            string Target_IP = ConfigController.INIRead("UR控制参数", "RemoteIP", DefaultINIPath);
            string Control_Port = ConfigController.INIRead("UR控制参数", "RemoteControlPort", DefaultINIPath); ;

            URController.Creat_client(Target_IP, Convert.ToInt32(Control_Port));
            //MessageBox.Show(Target_IP + "|" + Control_Port);
        }

        //每隔这个时间段执行定时
        private void TeachTimer_Tick(object sender, EventArgs e)
        {
            //如果采集到的数据不为零(至少有一个不为零)
            if (URDateHandle.Positions_X != 0 || URDateHandle.Positions_Y != 0 || URDateHandle.Positions_Z != 0)
            {
                if (Global_Tick < Total_Counts)
                {
                    //每次定时器被触发都修改label(Global_Tick是指)
                    this.labelCurrent.Text = Global_Tick.ToString() + "/" + Total_Counts.ToString();

                    //每次定时器被触发都保存到数组(double和string都要)
                    Total_PointRecord[Global_Tick, 0] = URDateHandle.Positions_X;
                    Total_PointRecord[Global_Tick, 1] = URDateHandle.Positions_Y;
                    Total_PointRecord[Global_Tick, 2] = URDateHandle.Positions_Z;
                    Total_PointRecord[Global_Tick, 3] = URDateHandle.Positions_U;
                    Total_PointRecord[Global_Tick, 4] = URDateHandle.Positions_V;
                    Total_PointRecord[Global_Tick, 5] = URDateHandle.Positions_W;
                    Total_PointRecordString[Global_Tick] = "p[" + URDateHandle.Positions_X.ToString("0.0000") + "," + URDateHandle.Positions_Y.ToString("0.0000") + "," + URDateHandle.Positions_Z.ToString("0.0000") + "," + URDateHandle.Positions_U.ToString("0.0000") + "," + URDateHandle.Positions_V.ToString("0.0000") + "," + URDateHandle.Positions_W.ToString("0.0000") + "]";

                    Total_AngleRecord[Global_Tick, 0] = URDateHandle.Angles_X;
                    Total_AngleRecord[Global_Tick, 1] = URDateHandle.Angles_Y;
                    Total_AngleRecord[Global_Tick, 2] = URDateHandle.Angles_Z;
                    Total_AngleRecord[Global_Tick, 3] = URDateHandle.Angles_U;
                    Total_AngleRecord[Global_Tick, 4] = URDateHandle.Angles_V;
                    Total_AngleRecord[Global_Tick, 5] = URDateHandle.Angles_W;
                    Total_AngleRecordString[Global_Tick] = "[" + URDateHandle.Angles_X.ToString("0.0000") + "," + URDateHandle.Angles_Y.ToString("0.0000") + "," + URDateHandle.Angles_Z.ToString("0.0000") + "," + URDateHandle.Angles_U.ToString("0.0000") + "," + URDateHandle.Angles_V.ToString("0.0000") + "," + URDateHandle.Angles_W.ToString("0.0000") + "]";

                    //每次定时器被触发都修改标题
                    this.Text = Total_PointRecordString[Global_Tick];
                    Global_Tick++;
                }
                else
                {
                    this.labelCurrent.Text = Global_Tick.ToString() + "/" + Total_Counts.ToString();
                }
            }

        }

        //开始记录点位
        private void btnStartTeach_Click(object sender, EventArgs e)
        {
            //手动设置时间间隔
            Global_Tick_Time = Convert.ToInt32(txtRecordTick.Text);
            TeachTimer.Interval = Global_Tick_Time;

            //初始化记录点位的数组
            Total_Counts = Convert.ToInt32(txtRecordLimit.Text);
            Total_PointRecord = new double[(Total_Counts+1), 6];
            Total_PointRecordString = new string[(Total_Counts+1)];

            Total_AngleRecord = new double[(Total_Counts+1), 6];
            Total_AngleRecordString = new string[(Total_Counts + 1)];

            TeachTimer.Start();
        }

        private void btnStopTeach_Click(object sender, EventArgs e)
        {
            TeachTimer.Stop();
        }

        private void btnShowTeach_Click(object sender, EventArgs e)
        {
            //判断是否要合成点位
            if(radioAdd_Nothing.Checked)
            {
                Add_What = 0;
            }
            else if(radioAdd_Base.Checked)
            {
                Add_What = 1;
            }
            else
            {
                Add_What = 2;
            }


            //看当前勾选了哪个选项
            if(radioMoveJ.Checked)
            {
                ShowResults_InMoveJ();
            }
            else if (radioMoveL.Checked)
            {
                ShowResults_InMoveL();
            }
            else
            {
                ShowResults_InMoveP();
            }

          
        }

        //发送只关心当前TextBox中的内容
        private void btnSendTeachResult_Click(object sender, EventArgs e)
        {
            string CMD = TextBoxPointResult.Text;
            URController.Send_command(CMD);
        }


        private void btnResetTeachResult_Click(object sender, EventArgs e)
        {
            Global_Tick = 0;
            Total_PointRecord = null;
            Total_PointRecordString = null;
            Total_AngleRecord = null;
            Total_AngleRecordString = null;

            this.labelCurrent.Text = Global_Tick.ToString() + "/" + Total_Counts.ToString();
            TeachTimer.Stop();

            this.TextBoxPointResult.Text = "";

        }

        //保存为一个Script文件（不管前面是用什么方式移动的，这里只是提取TextBox中的东西）
        private void btnSaveTeachResult_Click(object sender, EventArgs e)
        {
            //string CurrentTime = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            //DefaultScriptPath += CurrentTime + "_Ace.script";
            //不加系统时间，方便直接替换虚拟机的文件
            DefaultScriptPath = "Ace.script";
            FileStream fs1 = new FileStream(DefaultScriptPath, FileMode.Append, FileAccess.Write);//创建写入文件 
            StreamWriter sw = new StreamWriter(fs1);
            string str = TextBoxPointResult.Text;
            sw.Write(str);
            sw.Close();
            fs1.Close();

        }

        void check_Parameters()
        {
            if (txtAccleration.Text.ToString() == "")
            {
                Include_Accleration = false;
            }
            if (txtSpeed.Text.ToString() == "")
            {
                Include_Speed = false;
            }
            if (txtRadius.Text.ToString() == "")
            {
                Include_Radius = false;
            }
        
        }
 
        //貌似用MoveJ记录六个关节值还不能，因为关节值无法合成新的向量
        void ShowResults_InMoveJ()
        {
            //先清空原有的文本
            TextBoxPointResult.Text = "";

            TextBoxPointResult.AppendText("def movej_test(): \r\n");

            check_Parameters();
            #region 以下是获取六个关节数值的方式生成movej的脚本
            /*
            for (int i = 0; i < Global_Tick; i++)
            {
                string Str = "";

                Str += "movej(" + Total_AngleRecordString[i];

                if (Include_Accleration)
                {
                    Str += ", a = " + txtAccleration.Text.ToString();
                }
                if (Include_Speed)
                {
                    Str += ", v = " + txtSpeed.Text.ToString();
                }
                if (Include_Radius)
                {
                    Str += ", r = " + txtRadius.Text.ToString();
                }

                //如果三个都不包含，则
                Str += ") \r\n";

                //TextBoxPointResult.AppendText("movel(" + Total_PointRecordString[i] + ",a = " + txtAccleration.Text.ToString() + ",v = " + txtSpeed.Text.ToString() + ",r = " + txtRadius.Text.ToString() + ") \r\n");
                TextBoxPointResult.AppendText(Str);
            }
            */
            #endregion

            #region 以下是切换成点位的方式生成movej的脚本
            for (int i = 0; i < Global_Tick; i++)
            {
                string Str = "movej(";

                //如果勾选了与Base合成，则用Pose_add合成上去
                if (Add_What ==1  )
                {
                    Str += "pose_add(AddVector, ";
                }
                if (Add_What == 2)
                {
                    Str += "pose_trans(AddVector, ";
                }

                Str += Total_PointRecordString[i];

                if (Add_What == 1 || Add_What == 2)
                {
                    Str += ")";
                }

                if (Include_Accleration)
                {
                    Str += ",a = " + txtAccleration.Text.ToString();
                }
                if (Include_Speed)
                {
                    Str += ",v = " + txtSpeed.Text.ToString();
                }
                if (Include_Radius)
                {
                    Str += ",r = " + txtRadius.Text.ToString();
                }

                //如果三个都不包含，则
                Str += ") \r\n";

                TextBoxPointResult.AppendText(Str);
            }

            #endregion

            TextBoxPointResult.AppendText("end \r\n");
            TextBoxPointResult.AppendText("movej_test()  \r\n");
        }

        void ShowResults_InMoveL()
        {
            //先清空原有的文本
            TextBoxPointResult.Text = "";

            TextBoxPointResult.AppendText("def movel_test(): \r\n");

            check_Parameters();

            for (int i = 0; i < Global_Tick; i++)
            {
                string Str = "movel(";

                //如果勾选了与Base合成，则用Pose_add合成上去
                if (Add_What == 1)
                {
                    Str += "pose_add(AddVector, ";
                }
                if (Add_What == 2)
                {
                    Str += "pose_trans(AddVector, ";
                }

                Str += Total_PointRecordString[i];

                if (Add_What == 1 || Add_What == 2)
                {
                    Str += ")";
                }

                if (Include_Accleration)
                {
                    Str += ",a = " + txtAccleration.Text.ToString();
                }
                if (Include_Speed)
                {
                    Str += ",v = " + txtSpeed.Text.ToString();
                }
                if (Include_Radius)
                {
                    Str += ",r = " + txtRadius.Text.ToString();
                }

                //如果三个都不包含，则
                Str += ") \r\n";

                TextBoxPointResult.AppendText(Str);
            }

            TextBoxPointResult.AppendText("end \r\n");
            TextBoxPointResult.AppendText("movel_test()  \r\n");
        }

        void ShowResults_InMoveP()
        {
            //先清空原有的文本
            TextBoxPointResult.Text = "";

            TextBoxPointResult.AppendText("def movep_test(): \r\n");

            check_Parameters();

            for (int i = 0; i < Global_Tick; i++)
            {
                string Str = "movep(";

                //如果勾选了与Base合成，则用Pose_add合成上去
                if (Add_What == 1)
                {
                    Str += "pose_add(AddVector, ";
                }
                if (Add_What == 2)
                {
                    Str += "pose_trans(AddVector, ";
                }

                Str += Total_PointRecordString[i];

                if (Add_What == 1 || Add_What == 2)
                {
                    Str += ")";
                }
                
                if (Include_Accleration)
                {
                    Str += ",a = " + txtAccleration.Text.ToString();
                }
                if (Include_Speed)
                {
                    Str += ",v = " + txtSpeed.Text.ToString();
                }
                if (Include_Radius)
                {
                    Str += ",r = " + txtRadius.Text.ToString();
                }

                //如果三个都不包含，则
                Str += ") \r\n";

                TextBoxPointResult.AppendText(Str);
            }

            TextBoxPointResult.AppendText("end \r\n");
            TextBoxPointResult.AppendText("movep_test()  \r\n");

        }

    }
}
