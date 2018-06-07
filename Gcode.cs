using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Files;
using ModbusCommunication;
using URControl;

namespace UR_点动控制器
{
    public partial class GCode : Form
    {
        public string DefaultINIPath;
        public string DefaultScriptPath = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory) + "Script.txt";

        //读取配置文件的IP
        FilesINI ConfigController = new FilesINI();

        public GCode(string ConfigFilePath)
        {
            InitializeComponent();
            DefaultINIPath = ConfigFilePath;

        }

        //其实Pose差值面板只要读取主界面的值就可以了（限定为只读六轴的坐标值）
        //但是界面的数值有时候会假死，尤其是当刷新频率太高，机械手示教器的屏幕就不正常显示了，甚至会重启
        //所以还是老老实实的再做一个实例读取寄存器值
        ModbusSocket URRegisterHandle = new ModbusSocket();
        URControlHandle URController = new URControlHandle();

        //定义两个全局的数组，当点击记录的时候，直接把整数放到整数数组中，方便计算，在界面上显示则转成字符串即可
        double[] PosInt1 = new double[6];
        double[] PosInt2 = new double[6];
        double[] PosInt3 = new double[6];
        double[] PosDiff = new double[6];

        //当前的六维姿态量
        double[] CurrentPosInt = new double[6];
        double[] NewPosInt = new double[6];
        double[] CurrentActiveOA = new double[6];
        double[] CurrentActiveOB = new double[6];
        double[] CurrentActiveOC = new double[6];

        //定义初始的保存G代码的字符串和字符串数组
        string SourceGCodeStr;
        string FilteredGCodeStr;
        string[] txtRows;//这是没过滤的字符串数组
        string[] txtRowsFilter;//这是过滤之后的字符串数组（只剩XYZ相关的数据）

        //设定三组分量，分别存放当前O，A，B三点的三组坐标值
        #region
        double HomeX = 0;
        double HomeY = 0;
        double HomeZ = 0;
        double HomeU = 0;
        double HomeV = 0;
        double HomeW = 0;

        double HomeX_A = 0;
        double HomeY_A = 0;
        double HomeZ_A = 0;
        double HomeU_A = 0;
        double HomeV_A = 0;
        double HomeW_A = 0;

        double HomeX_B = 0;
        double HomeY_B = 0;
        double HomeZ_B = 0;
        double HomeU_B = 0;
        double HomeV_B = 0;
        double HomeW_B = 0;

        #endregion

        double CurrentX = 0;
        double CurrentY = 0;
        double CurrentZ = 0;

        //最大的Z起始值设为很小，最小的Z起始值设为很大
        double GlobalBiggestZ = -10000000;
        double GlobalSmallestZ = 10000000;

        
        string MoveType = "";
        string HomeVector = "";//HomeVector是指在开始写字的上方的点
        string HomeVectorTruely = "";//HomeVectorTruely是指在G代码转换得到最高的Z和最小的Z之后的真实的向量
        string HomeRecordVector = "";//HomeRecordVector是指开始写字的点，即尝试触摸的点
        string HomeRecordVectorX_Direction = "";//HomeRecordVectorX_Direction是指以HomeRecordVector为起始点，绘制的X方向的向量
        string HomeRecordVectorY_Direction = "";//HomeRecordVectorY_Direction是指以HomeRecordVector为起始点，绘制的Y方向的向量

        //定义三个数组，分别存放所有得到的G代码的点位
        double[] TotalPointX;
        double[] TotalPointY;
        double[] TotalPointZ;
        double[] TotalPointSpeed;
        double[] TotalPointRadius;

        //定义三个数组，分别存放所有转换成单位向量之后的G代码数据
        double[] TotalPointX_Converted;
        double[] TotalPointY_Converted;
        double[] TotalPointZ_Converted;
        double[] TotalPointXYZ_Converted;

        double AcclerationFast = 0;
        double SpeedFast = 0;
        double AcclerationSlow = 0;
        double SpeedSlow = 0;
        double Radius = 0;

        string CurrentIP;
        string Control_Port;

        string RefPos1;
        string RefPos2;
        string RefPos3;

        bool ifConnected = false;

        private void PoseDifference_Load(object sender, EventArgs e)
        {

            FilesINI ConfigController = new FilesINI();
            CurrentIP = ConfigController.INIRead("UR控制参数", "RemoteIP", DefaultINIPath);
            Control_Port = ConfigController.INIRead("UR控制参数", "RemoteControlPort", DefaultINIPath); ;

            URRegisterHandle.RemoteIP = CurrentIP;
            URRegisterHandle.RemotePort = 502;
            URRegisterHandle.SocketTimeOut = 1000;

            RefPos1 = ConfigController.INIRead("UR运动参数", "BasePos1", DefaultINIPath);
            RefPos2 = ConfigController.INIRead("UR运动参数", "BasePos2", DefaultINIPath);
            RefPos3 = ConfigController.INIRead("UR运动参数", "BasePos3", DefaultINIPath);

            txtPose1.Text = RefPos1;
            txtPose2.Text = RefPos2;
            txtPose3.Text = RefPos3;

            HomeVector = RefPos1;

            //尝试连接，如果不能连接，则某些按钮不可用
            try
            {
                URController.Creat_client(CurrentIP, Convert.ToInt32(Control_Port));
                ifConnected = true;
            }
            catch
            {
                ifConnected = false;
                MessageBox.Show("未连接UR，某些功能不可用");
            }

        }



        //当要记录点位的时候，把此时的点位置记录下来（标准的格式，不做转换，保留小数点）
        public double[] GetPose()
        {
            URRegisterHandle.connectServer();

            int ReadNum = 6;
            int ReadStartAddress = 400;

            int[] SixPositions = new int[6];
            SixPositions = URRegisterHandle.ReadMultipleRegister(ReadNum, ReadStartAddress);

            double[] PositionsFiltered = new double[6];

            for (int i = 0; i < 6; i++)
            {
                if (SixPositions[i] > 32768)
                {
                    SixPositions[i] = SixPositions[i] - 65535;
                }
                //这里全部用浮点值
                if (i < 3)
                {
                    PositionsFiltered[i] = (float)SixPositions[i] / 10000;
                }
                else
                {
                    PositionsFiltered[i] = (float)SixPositions[i] / 1000;
                }
            }

            return PositionsFiltered;

        }

        //简化一下代码，写一个把X,X,X,X,X,X的数组转换为p[X,X,X,X,X,X]的方法
        public string GetPoseString(double[] pose)
        {
            string PoseTXT = "p[";

            //全部保留4位小数
            for (int i = 0; i < pose.Length; i++)
            {
                if (i != (pose.Length - 1))
                {
                    PoseTXT = PoseTXT + pose[i].ToString("0.0000") + ",";
                }
                else
                {
                    PoseTXT = PoseTXT + pose[i].ToString("0.0000");
                }
            }

            PoseTXT = PoseTXT + "]";

            return PoseTXT;
        }


        //载入G代码
        private void btnLoadGCode_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "所有文件(*.*)|*.*";

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string FileName = this.openFileDialog1.FileName;

                //避免中文乱码，使用GB2312编码方式（得到SourceGCodeStr字符串）
                StreamReader SourceGCode = new StreamReader(FileName, System.Text.Encoding.Default);
                SourceGCodeStr = SourceGCode.ReadToEnd();

                //文本区域清空并保存当前读取到的数据
                this.textBoxCodeList.Text = "";
                this.textBoxCodeList.Text = SourceGCodeStr;
            }
        }

        //过滤G代码
        private void btnFilterGCode_Click(object sender, EventArgs e)
        {
            //根据换行符获取一共多少行(把每一行放到string数组中)
            txtRows = SourceGCodeStr.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            //MessageBox.Show(txtRows.Length.ToString());

            //文本区域清空并保存当前读取到的数据
            this.textBoxCodeList.Text = "";

            for (int i = 0; i < txtRows.Length;i++ )
            {
                //只要该行存在X或者Y或者Z任意一个字符，则认为该行有用
                if (txtRows[i].Contains('X') || txtRows[i].Contains('Y') || txtRows[i].Contains('Z'))
                {
                    //如果该行存在不应出现的字符，则要被踢掉（比如“（”）
                    if (!txtRows[i].Contains('('))
                    {
                        //最后还要不存在"X0 Y0"，因为我一定是从原点开始走的，所以这个直接可以踢掉
                        int FilterZeroPoint = txtRows[i].IndexOf("X0 Y0");
                        //如果IndexOf在父字符串中找不到X0 Y0，才可以添加，则IndexOf的结果为-1
                        if (FilterZeroPoint == -1)
                        {
                            this.textBoxCodeList.Text += txtRows[i] + "\r\n";
                        }

                    }
                }
            }
            
        }

        //转成UR代码
        private void btnConvertCode_Click(object sender, EventArgs e)
        {

            //这里明确移动方式（movej，还是movel，还是movep）
            #region
                if(radioMoveJ.Checked)
                {
                    MoveType = "movej";
                }
                else if (radioMoveL.Checked)
                {
                    MoveType = "movel";
                }
                else
                {
                    MoveType = "movep";
                }

            #endregion

            //这里获取其他的参数（速度，交融半径）
            #region
                try
                {
                    AcclerationFast = Convert.ToDouble(txtAcclerationFast.Text) / 1000;
                    SpeedFast = Convert.ToDouble(txtSpeedFast.Text) / 1000;
                    AcclerationSlow = Convert.ToDouble(txtAcclerationSlow.Text) / 1000;
                    SpeedSlow = Convert.ToDouble(txtSpeedSlow.Text) / 1000;
                    Radius = Convert.ToDouble(txtRadius.Text) / 1000;
                }
                catch
                {
                    MessageBox.Show("参数设置有误，请检查");
                }


            #endregion

           //这里先把过滤之后的G代码放到数组中
            FilteredGCodeStr = textBoxCodeList.Text;
            //已经到了Richtextbox中，则划分每一行的分隔符是“\n”
            txtRowsFilter = FilteredGCodeStr.Split(new string[] { "\n" }, StringSplitOptions.None);

            this.textBoxCodeList.Text = "";
            //这里必须在为五个容器赋值之前实例化
            TotalPointX = new double[txtRowsFilter.Length];
            TotalPointY = new double[txtRowsFilter.Length];
            TotalPointZ = new double[txtRowsFilter.Length];
            TotalPointSpeed = new double[txtRowsFilter.Length];
            TotalPointRadius = new double[txtRowsFilter.Length];

            TotalPointX_Converted = new double[txtRowsFilter.Length];
            TotalPointY_Converted = new double[txtRowsFilter.Length];
            TotalPointZ_Converted = new double[txtRowsFilter.Length];
            TotalPointXYZ_Converted = new double[txtRowsFilter.Length];

            //此时就不能用简单的字符串替换的方式了，而是要先取到所有的点位到数组(以后还得用)
            for (int i = 0; i < txtRowsFilter.Length; i++)
            {
                ConvertGCodeToURCode(txtRowsFilter[i],i);
            }

            //然后再用一次for循环来把数据导出到textbox中(注意这是原始的XYZ数据)
            /*
            for (int i = 0; i < txtRowsFilter.Length; i++)
            {
                this.textBoxCodeList.Text += "[" + TotalPointX[i].ToString("0.00") + "|" + TotalPointY[i].ToString("0.00") + "|" + TotalPointZ[i].ToString("0.00") + "|" + TotalPointSpeed[i].ToString("0.00") + "|" + TotalPointRadius[i].ToString("0.00") + "]" + "\r\n";
            }
             */

            //得到被三个方向的单位向量运算之后的新的点位
            for (int i = 0; i < txtRowsFilter.Length; i++)
            {
                //我们要把这个值累加到三个向量上去
                TotalPointX_Converted[i] = CurrentActiveOA[0] * TotalPointX[i] + CurrentActiveOB[0] * TotalPointY[i] + CurrentActiveOC[0] * TotalPointZ[i];
                TotalPointY_Converted[i] = CurrentActiveOA[1] * TotalPointX[i] + CurrentActiveOB[1] * TotalPointY[i] + CurrentActiveOC[1] * TotalPointZ[i];
                TotalPointZ_Converted[i] = CurrentActiveOA[2] * TotalPointX[i] + CurrentActiveOB[2] * TotalPointY[i] + CurrentActiveOC[2] * TotalPointZ[i];
            }

            //再用一次for循环得到最后的结果
            for (int i = 0; i < (txtRowsFilter.Length-1); i++)
            {
                string CurrentMoveCommand = MoveType + "(pose_add(HomeVectorTruely, p[" + TotalPointX_Converted[i].ToString("0.00000") + "," + TotalPointY_Converted[i].ToString("0.00000") + "," + TotalPointZ_Converted[i].ToString("0.00000") + ",0,0,0]),v = " + TotalPointSpeed[i].ToString() + ", r = " + TotalPointRadius[i].ToString() +")";
                this.textBoxCodeList.Text += CurrentMoveCommand + "\r\n";
            }

        }

        //该方法的功能是获取一行G代码（已经过滤之后的G代码，只剩下XYZ相关数据）并转换成UR脚本代码
        public void ConvertGCodeToURCode(string SourceGCode,int CurrentIndex)
        {

            //如果既存在G0 又存在Z，则说明是快速回原点(但是Z的高度在改变)
            #region
            if (SourceGCode.Contains("G0") && SourceGCode.Contains("Z"))
            {

               //先把当前的字符串按空格进行拆分(可能除了XYZ还有AC等)
                string[] CurrentRowGCodeSplitted = SourceGCode.Split(' ');
                for (int i = 0; i < CurrentRowGCodeSplitted.Length; i++)
                {
                    if (CurrentRowGCodeSplitted[i].Contains("X"))
                    {
                        //把这个X替换成空
                        CurrentX = Convert.ToDouble(CurrentRowGCodeSplitted[i].Replace("X", "")) / 1000;
                    }
                    if (CurrentRowGCodeSplitted[i].Contains("Y"))
                    {
                        //把这个Y替换成空
                        CurrentY = Convert.ToDouble(CurrentRowGCodeSplitted[i].Replace("Y", "")) / 1000;
                    }
                    if (CurrentRowGCodeSplitted[i].Contains("Z"))
                    {
                        //把这个Z替换成空
                        CurrentZ = Convert.ToDouble(CurrentRowGCodeSplitted[i].Replace("Z", "")) / 1000;
                    
                        if(CurrentZ>GlobalBiggestZ)
                        {
                            GlobalBiggestZ = CurrentZ;
                        }
                        if (CurrentZ < GlobalSmallestZ)
                        {
                            GlobalSmallestZ = CurrentZ;
                        }
                    }
                }

                //此时已经完全刷新了XYZ的关系
                TotalPointX[CurrentIndex] = CurrentX;
                TotalPointY[CurrentIndex] = CurrentY;
                TotalPointZ[CurrentIndex] = CurrentZ;
                TotalPointSpeed[CurrentIndex] = SpeedFast;
                TotalPointRadius[CurrentIndex] = 0;

            }
            #endregion

            //如果XYZ三个任意存在一个，则认为不存在的就套用上面的，存在的则更新当前的
            #region
            else if (SourceGCode.Contains("X") || SourceGCode.Contains("Y") || SourceGCode.Contains("Z"))
            {
                //先把当前的字符串按空格进行拆分(可能除了XYZ还有AC等)
                string[] CurrentRowGCodeSplitted = SourceGCode.Split(' ');
                for (int i = 0; i < CurrentRowGCodeSplitted.Length; i++)
                {
                    //MessageBox.Show(CurrentRowGCodeSplitted[i]);
                    //如果被拆分的数组中存在X，则把当前的数组元素的X踢掉，并保留数字
                    //MessageBox.Show(CurrentX.ToString());
                    if (CurrentRowGCodeSplitted[i].Contains("X"))
                    {
                        //把这个X替换成空
                        CurrentX = Convert.ToDouble(CurrentRowGCodeSplitted[i].Replace("X", "")) / 1000;
                    }
                    if (CurrentRowGCodeSplitted[i].Contains("Y"))
                    {
                        //把这个Y替换成空
                        CurrentY = Convert.ToDouble(CurrentRowGCodeSplitted[i].Replace("Y", "")) / 1000;
                    }
                    if (CurrentRowGCodeSplitted[i].Contains("Z"))
                    {
                        //把这个Z替换成空
                        CurrentZ = Convert.ToDouble(CurrentRowGCodeSplitted[i].Replace("Z", "")) / 1000;

                        if (CurrentZ > GlobalBiggestZ)
                        {
                            GlobalBiggestZ = CurrentZ;
                        }
                        if (CurrentZ < GlobalSmallestZ)
                        {
                            GlobalSmallestZ = CurrentZ;
                        }
                    }
                }
                //此时已经完全刷新了XYZ的关系(由于我知道是实际加工，所以tempData[3] = SpeedSlow，tempData[4] = Radius)
                //此时已经完全刷新了XYZ的关系
                TotalPointX[CurrentIndex] = CurrentX;
                TotalPointY[CurrentIndex] = CurrentY;
                TotalPointZ[CurrentIndex] = CurrentZ;
                TotalPointSpeed[CurrentIndex] = SpeedSlow;
                TotalPointRadius[CurrentIndex] = Radius;

            }
            #endregion
            else
            {
                //MessageBox.Show("这一行代码无效！！！");
                //如果这一行没有任何用途，则所有的相对量都是0就可以了，最后还是根据原点合成的
                TotalPointX[CurrentIndex] = 0;
                TotalPointY[CurrentIndex] = 0;
                TotalPointZ[CurrentIndex] = 0;
                TotalPointSpeed[CurrentIndex] = 0;
                TotalPointRadius[CurrentIndex] = 0;
            }

        }

        //把Pose1记录并作为相对参考原点
        private void btnPos1_Relative_Click(object sender, EventArgs e)
        {
            if (ifConnected)
            {
                PosInt1 = GetPose();
                txtPose1.Text = GetPoseString(PosInt1);
                HomeVector = txtPose1.Text;

                HomeX = PosInt1[0];
                HomeY = PosInt1[1];
                HomeZ = PosInt1[2];
                HomeU = PosInt1[3];
                HomeV = PosInt1[4];
                HomeW = PosInt1[5];

                ConfigController.INIWrite("UR运动参数", "BasePos1", txtPose1.Text, DefaultINIPath);
            }
            else
            {
                MessageBox.Show("请确认目标UR已连接");
            }

        }

        private void btnPos2_Relative_Click(object sender, EventArgs e)
        {

            if (ifConnected)
            {
                PosInt2 = GetPose();
                txtPose2.Text = GetPoseString(PosInt2);

                HomeX_A = PosInt2[0];
                HomeY_A = PosInt2[1];
                HomeZ_A = PosInt2[2];
                HomeU_A = PosInt2[3];
                HomeV_A = PosInt2[4];
                HomeW_A = PosInt2[5];

                ConfigController.INIWrite("UR运动参数", "BasePos2", txtPose2.Text, DefaultINIPath);
            }
            else
            {
                MessageBox.Show("请确认目标UR已连接");
            }

        }

        private void btnPos3_Relative_Click(object sender, EventArgs e)
        {
            if (ifConnected)
            {
                PosInt3 = GetPose();
                txtPose3.Text = GetPoseString(PosInt3);

                HomeX_B = PosInt3[0];
                HomeY_B = PosInt3[1];
                HomeZ_B = PosInt3[2];
                HomeU_B = PosInt3[3];
                HomeV_B = PosInt3[4];
                HomeW_B = PosInt3[5];

                ConfigController.INIWrite("UR运动参数", "BasePos3", txtPose3.Text, DefaultINIPath);
            }
            else
            {
                MessageBox.Show("请确认目标UR已连接");
            }

        }

        //加壳UR代码是指对G代码一对一的翻译进行最后的包装
        private void btnEmbedCode_Click(object sender, EventArgs e)
        {
            //加壳的头和尾
            string Embed_head = "def " + MoveType + "_test(): \r\n";

            //加壳有很重要的一步，就是把这个原始的HomeVector减去G代码中最高的Z，然后合成的时候如果合成Z方向就不会抬起来再往下走了
            //第一步，先找到G代码中最高的Z和最低的Z是多少，已经在遍历G代码的时候找到了最高的和最低的Z
            //第二步，将当前的HomeVector的Z值减去当前的量
            HomeVector = "p[" + HomeX.ToString("0.0000") + "," + HomeY.ToString("0.0000") + "," + HomeZ.ToString("0.0000") + "," + HomeU.ToString("0.0000") + "," + HomeV.ToString("0.0000") + "," + HomeW.ToString("0.0000") + "]";
            HomeVectorTruely = "p[" + HomeX.ToString("0.0000") + "," + HomeY.ToString("0.0000") + "," + (HomeZ - GlobalBiggestZ).ToString("0.0000") + "," + HomeU.ToString("0.0000") + "," + HomeV.ToString("0.0000") + "," + HomeW.ToString("0.0000") + "]";
            //第三步，在URScript中声明当前的HomeVector
            string Embed_Vector = "HomeVector = " + HomeVector + "\r\n";
            string Embed_VectorTruely = "HomeVectorTruely = " + HomeVectorTruely + "\r\n";

            //最后加一条手工回原点的代码(速度用这个速度表示只是为了区分之前的快进速度和加工速度，以此表示这行代码是手工加上去的)
            string Embed_GoZeroPoint = "movel(HomeVector,v = 0.1234567,r = 0) \r\n";
            string Embed_bottom = "end \r\n";
            string Embed_Run = MoveType + "_test() \r\n";

            //剔除空行
            string CurrentCode = (textBoxCodeList.Text).Replace("\n\n","\n");
            textBoxCodeList.Text = "";
            textBoxCodeList.Text = Embed_head + Embed_Vector + Embed_VectorTruely + CurrentCode + Embed_GoZeroPoint + Embed_bottom + Embed_Run;

        }

        //发送当前G代码
        private void btnSendCode_Click(object sender, EventArgs e)
        {
            if (ifConnected)
            {
                string CMD = textBoxCodeList.Text;
                URController.Send_command(CMD);
            }
            else
            {
                MessageBox.Show("请确认目标UR已连接");
            }

        }

        //保存当前G代码
        private void btnSaveCode_Click(object sender, EventArgs e)
        {
            DefaultScriptPath = "FromGCode.script";
            FileStream fs1 = new FileStream(DefaultScriptPath, FileMode.Append, FileAccess.Write);//创建写入文件 
            StreamWriter sw = new StreamWriter(fs1);
            string str = textBoxCodeList.Text;
            sw.Write(str);
            sw.Close();
            fs1.Close();
        }

        //回到原点
        private void btnGoZeroPoint_Click(object sender, EventArgs e)
        {

            if (ifConnected)
            {
                HomeVector = txtPose1.Text;
                //用一个比较慢的速度回原点(设置回原点的速度为v =0.1)
                string CMD = "movel(" + HomeVector + ",v = 0.2)";
                URController.Send_command(CMD);
            }
            else
            {
                MessageBox.Show("请确认目标UR已连接");
            }

            
        }

        private void btnGoAPoint_Click(object sender, EventArgs e)
        {
            if (ifConnected)
            {
                string PointA_Vector = txtPose2.Text;
                //用一个比较慢的速度回原点(设置回原点的速度为v =0.1)
                string CMD = "movel(" + PointA_Vector + ",v = 0.2)";
                URController.Send_command(CMD);
            }
            else
            {
                MessageBox.Show("请确认目标UR已连接");
            }
        }

        private void btnGoBPoint_Click(object sender, EventArgs e)
        {
            if (ifConnected)
            {
                string PointB_Vector = txtPose3.Text;
                //用一个比较慢的速度回原点(设置回原点的速度为v =0.1)
                string CMD = "movel(" + PointB_Vector + ",v = 0.2)";
                URController.Send_command(CMD);
            }
            else
            {
                MessageBox.Show("请确认目标UR已连接");
            }

        }

        //尝试触摸的意思是从HomeVector走到最低的Z值上去，看笔尖是否正好碰到，如果不是，则微调O点，把O点调下来，则这个低点也会下来了
        private void btnTryTouch_Click(object sender, EventArgs e)
        {
            //为了防止用户直接点击尝试触摸（默认的HomeX如果没有被修改，则会有问题），先模拟点击回到O点按钮,将当前位置设置为O点按钮
            //this.btnGoZeroPoint.PerformClick();
            //MessageBox.Show("已经回到O点");
            //this.btnPos1_Relative.PerformClick();
            //MessageBox.Show("已将当前位置设置为O点");

            //先获取当前位置
            double[] PosIntCurrent = new double[6];
            PosIntCurrent = GetPose();

            //再得到Z方向（不是Base的Z方向）的进给量（根据G代码的最大值最小值得到Z方向的进给数值）
            double[] TouchZ_Vector = new double[6];
            double SafeZ_TouchZ = GlobalSmallestZ - GlobalBiggestZ;
            TouchZ_Vector[0] = CurrentActiveOC[0] * SafeZ_TouchZ;
            TouchZ_Vector[1] = CurrentActiveOC[1] * SafeZ_TouchZ;
            TouchZ_Vector[2] = CurrentActiveOC[2] * SafeZ_TouchZ;
            TouchZ_Vector[3] = 0;
            TouchZ_Vector[4] = 0;
            TouchZ_Vector[5] = 0;

            //再得到最终的合成向量
            double[] TotalTouch_Vector = new double[6];
            TotalTouch_Vector[0] = PosIntCurrent[0] + TouchZ_Vector[0];
            TotalTouch_Vector[1] = PosIntCurrent[1] + TouchZ_Vector[1];
            TotalTouch_Vector[2] = PosIntCurrent[2] + TouchZ_Vector[2];
            TotalTouch_Vector[3] = PosIntCurrent[3];
            TotalTouch_Vector[4] = PosIntCurrent[4];
            TotalTouch_Vector[5] = PosIntCurrent[5];

            HomeRecordVector = "p[" + TotalTouch_Vector[0].ToString("0.0000") + "," + TotalTouch_Vector[1].ToString("0.0000") + "," + TotalTouch_Vector[2].ToString("0.0000") + "," + TotalTouch_Vector[3].ToString("0.0000") + "," + TotalTouch_Vector[4].ToString("0.0000") + "," + TotalTouch_Vector[5].ToString("0.0000") + "]";
            
            //假定X方向就是X增加50mm的效果，别的不变
            HomeRecordVectorX_Direction = "p[" + (HomeX + 0.05).ToString("0.0000") + "," + HomeY.ToString("0.0000") + "," + (HomeZ - GlobalSmallestZ).ToString("0.0000") + "," + HomeU.ToString("0.0000") + "," + HomeV.ToString("0.0000") + "," + HomeW.ToString("0.0000") + "]";

            //假定Y方向就是Y增加50mm的效果，别的不变
            HomeRecordVectorY_Direction = "p[" + HomeX.ToString("0.0000") + "," + (HomeY + 0.05).ToString("0.0000") + "," + (HomeZ - GlobalSmallestZ).ToString("0.0000") + "," + HomeU.ToString("0.0000") + "," + HomeV.ToString("0.0000") + "," + HomeW.ToString("0.0000") + "]";


            if (ifConnected)
            {
                //用一个比较慢的速度回原点(设置回原点的速度为v =0.1)
                string CMD = "movel(" + HomeRecordVector + ",v = 0.03)";
                URController.Send_command(CMD);
            }
            else
            {
                MessageBox.Show("请确认目标UR已连接");
            }

        }

        //刷新三个向量（其实就是相机的XY方向向量）
        private void btnRefreshVectors_Click(object sender, EventArgs e)
        {
            //此时已经确认了一个基准位置和两个基准方向
            ConfigController.INIWrite("UR运动参数", "BasePos1", txtPose1.Text, DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "BasePos2", txtPose2.Text, DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "BasePos3", txtPose3.Text, DefaultINIPath);

            //写入三组点位坐标，我就不后面用提取的方法了，烦人
            ConfigController.INIWrite("UR运动参数", "HomeX", PosInt1[0].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeY", PosInt1[1].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeZ", PosInt1[2].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeU", PosInt1[3].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeV", PosInt1[4].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeW", PosInt1[5].ToString("0.0000"), DefaultINIPath);

            ConfigController.INIWrite("UR运动参数", "HomeX_A", PosInt2[0].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeY_A", PosInt2[1].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeZ_A", PosInt2[2].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeU_A", PosInt2[3].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeV_A", PosInt2[4].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeW_A", PosInt2[5].ToString("0.0000"), DefaultINIPath);

            ConfigController.INIWrite("UR运动参数", "HomeX_B", PosInt3[0].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeY_B", PosInt3[1].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeZ_B", PosInt3[2].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeU_B", PosInt3[3].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeV_B", PosInt3[4].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeW_B", PosInt3[5].ToString("0.0000"), DefaultINIPath);

            //顺便把计算出来的东西也写入配置文件
            //计算OA和OB的长度(空间两点的距离公式)
            double OALength = Math.Sqrt(Math.Pow((PosInt1[0] - PosInt2[0]), 2) + Math.Pow((PosInt1[1] - PosInt2[1]), 2) + Math.Pow((PosInt1[2] - PosInt2[2]), 2));
            double OBLength = Math.Sqrt(Math.Pow((PosInt1[0] - PosInt3[0]), 2) + Math.Pow((PosInt1[1] - PosInt3[1]), 2) + Math.Pow((PosInt1[2] - PosInt3[2]), 2));
            textBoxCodeList.Text = "OALength = " + OALength.ToString("0.0000") + "\r\n" + "OBLength = " + OBLength.ToString("0.0000") + "\r\n";

            CurrentActiveOA[0] = ((PosInt2[0] - PosInt1[0]) / OALength);
            CurrentActiveOA[1] = ((PosInt2[1] - PosInt1[1]) / OALength);
            CurrentActiveOA[2] = ((PosInt2[2] - PosInt1[2]) / OALength);
            CurrentActiveOA[3] = ((PosInt2[3] - PosInt1[3]) / OALength);
            CurrentActiveOA[4] = ((PosInt2[4] - PosInt1[4]) / OALength);
            CurrentActiveOA[5] = ((PosInt2[5] - PosInt1[5]) / OALength);

            CurrentActiveOB[0] = ((PosInt3[0] - PosInt1[0]) / OBLength);
            CurrentActiveOB[1] = ((PosInt3[1] - PosInt1[1]) / OBLength);
            CurrentActiveOB[2] = ((PosInt3[2] - PosInt1[2]) / OBLength);
            CurrentActiveOB[3] = ((PosInt3[3] - PosInt1[3]) / OBLength);
            CurrentActiveOB[4] = ((PosInt3[4] - PosInt1[4]) / OBLength);
            CurrentActiveOB[5] = ((PosInt3[5] - PosInt1[5]) / OBLength);


            //计算OA和OB的单位向量（只关心XYZ，不关心UVW）
            string BaseDirctionOA = "p[" + CurrentActiveOA[0].ToString("0.0000") + "," + CurrentActiveOA[1].ToString("0.0000") + "," + CurrentActiveOA[2].ToString("0.0000") + ",0,0,0]";
            string BaseDirctionOB = "p[" + CurrentActiveOB[0].ToString("0.0000") + "," + CurrentActiveOB[1].ToString("0.0000") + "," + CurrentActiveOB[2].ToString("0.0000") + ",0,0,0]";

            ConfigController.INIWrite("UR运动参数", "BaseDirctionOA", BaseDirctionOA, DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "BaseDirctionOB", BaseDirctionOB, DefaultINIPath);

            txtPosOA.Text = BaseDirctionOA;
            txtPosOB.Text = BaseDirctionOB;

            //除了要知道OA,OB两个向量，还可以知道与OA,OB垂直的向量OC向量
            //这里核心是假定新的OC向量[X,Y,Z]的Z是1，先算出来OC的某个向量，再求OC方向的单位向量
            double TempOCX = ((CurrentActiveOB[1] * CurrentActiveOA[2]) - (CurrentActiveOA[1] * CurrentActiveOB[2])) / ((CurrentActiveOB[0] * CurrentActiveOA[1]) - (CurrentActiveOA[0] * CurrentActiveOB[1]));
            double TempOCY = ((CurrentActiveOA[2] * CurrentActiveOB[0]) - (CurrentActiveOA[0] * CurrentActiveOB[2])) / ((CurrentActiveOB[1] * CurrentActiveOA[0]) - (CurrentActiveOA[1] * CurrentActiveOB[0]));
            double TempOCZ = 1;

            //然后根据这三个向量算出OC方向的单位向量
            double OCLength = Math.Sqrt(Math.Pow(TempOCX, 2) + Math.Pow(TempOCY, 2) + Math.Pow(TempOCZ, 2));

            CurrentActiveOC[0] = TempOCX / OCLength;
            CurrentActiveOC[1] = TempOCY / OCLength;
            CurrentActiveOC[2] = TempOCZ / OCLength;

            string BaseDirctionOC = "p[" + CurrentActiveOC[0].ToString("0.0000") + "," + CurrentActiveOC[1].ToString("0.0000") + "," + CurrentActiveOC[2].ToString("0.0000") + ",0,0,0]";
            txtPosOC.Text = BaseDirctionOC;

        }

        //验证三个向量
        private void btnVerifyVectors_Click(object sender, EventArgs e)
        {
            //先验证三个向量是否是单位向量
            double ValidateOALength = Math.Pow(CurrentActiveOA[0], 2) + Math.Pow(CurrentActiveOA[1], 2) + Math.Pow(CurrentActiveOA[2], 2);
            double ValidateOBLength = Math.Pow(CurrentActiveOB[0], 2) + Math.Pow(CurrentActiveOB[1], 2) + Math.Pow(CurrentActiveOB[2], 2);
            double ValidateOCLength = Math.Pow(CurrentActiveOC[0], 2) + Math.Pow(CurrentActiveOC[1], 2) + Math.Pow(CurrentActiveOC[2], 2);
        
            //再验证向量两两是否垂直
            double ValidateRelation_OAOB = CurrentActiveOA[0] * CurrentActiveOB[0] + CurrentActiveOA[1] * CurrentActiveOB[1] + CurrentActiveOA[2] * CurrentActiveOB[2];
            double ValidateRelation_OAOC = CurrentActiveOA[0] * CurrentActiveOC[0] + CurrentActiveOA[1] * CurrentActiveOC[1] + CurrentActiveOA[2] * CurrentActiveOC[2];
            double ValidateRelation_OBOC = CurrentActiveOB[0] * CurrentActiveOC[0] + CurrentActiveOB[1] * CurrentActiveOC[1] + CurrentActiveOB[2] * CurrentActiveOC[2];

            textBoxCodeList.Text = "";
            textBoxCodeList.Text += "ValidateOALength = " + ValidateOALength.ToString("0.000000") + " \r\n";
            textBoxCodeList.Text += "ValidateOBLength = " + ValidateOBLength.ToString("0.000000") + " \r\n";
            textBoxCodeList.Text += "ValidateOCLength = " + ValidateOCLength.ToString("0.000000") + " \r\n";
            textBoxCodeList.Text += "ValidateRelation_OAOB = " + ValidateRelation_OAOB.ToString("0.000000") + " \r\n";
            textBoxCodeList.Text += "ValidateRelation_OAOC = " + ValidateRelation_OAOC.ToString("0.000000") + " \r\n";
            textBoxCodeList.Text += "ValidateRelation_OBOC = " + ValidateRelation_OBOC.ToString("0.000000") + " \r\n";
        }


        //将当前的三个配置位置坐标值写入相机的配置文件（假定相机用到的基准三个点与G代码用到的不一样），但是我又可以共用一套面板
        private void btnWriteConfigFile_Click(object sender, EventArgs e)
        {
            //写入三组点位坐标，我就不后面用提取的方法了，烦人
            ConfigController.INIWrite("UR运动参数", "HomeX_Camera", PosInt1[0].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeY_Camera", PosInt1[1].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeZ_Camera", PosInt1[2].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeU_Camera", PosInt1[3].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeV_Camera", PosInt1[4].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeW_Camera", PosInt1[5].ToString("0.0000"), DefaultINIPath);

            ConfigController.INIWrite("UR运动参数", "HomeX_A_Camera", PosInt2[0].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeY_A_Camera", PosInt2[1].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeZ_A_Camera", PosInt2[2].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeU_A_Camera", PosInt2[3].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeV_A_Camera", PosInt2[4].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeW_A_Camera", PosInt2[5].ToString("0.0000"), DefaultINIPath);

            ConfigController.INIWrite("UR运动参数", "HomeX_B_Camera", PosInt3[0].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeY_B_Camera", PosInt3[1].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeZ_B_Camera", PosInt3[2].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeU_B_Camera", PosInt3[3].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeV_B_Camera", PosInt3[4].ToString("0.0000"), DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeW_B_Camera", PosInt3[5].ToString("0.0000"), DefaultINIPath);

            double OALength = Math.Sqrt(Math.Pow((PosInt1[0] - PosInt2[0]), 2) + Math.Pow((PosInt1[1] - PosInt2[1]), 2) + Math.Pow((PosInt1[2] - PosInt2[2]), 2));
            double OBLength = Math.Sqrt(Math.Pow((PosInt1[0] - PosInt3[0]), 2) + Math.Pow((PosInt1[1] - PosInt3[1]), 2) + Math.Pow((PosInt1[2] - PosInt3[2]), 2));

            string BaseDirctionOA = "p[" + CurrentActiveOA[0].ToString("0.0000") + "," + CurrentActiveOA[1].ToString("0.0000") + "," + CurrentActiveOA[2].ToString("0.0000") + ",0,0,0]";
            string BaseDirctionOB = "p[" + CurrentActiveOB[0].ToString("0.0000") + "," + CurrentActiveOB[1].ToString("0.0000") + "," + CurrentActiveOB[2].ToString("0.0000") + ",0,0,0]";

            ConfigController.INIWrite("UR运动参数", "BaseDirctionOA_Camera", BaseDirctionOA, DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "BaseDirctionOB_Camera", BaseDirctionOB, DefaultINIPath);


        }

        //我们以当前测试的尝试触摸点为基准，绘制当前Base坐标系
        private void btnDrawCoordinateSystem_Click(object sender, EventArgs e)
        {
            if (ifConnected)
            {
                //用一个比较慢的速度回原点(设置回原点的速度为v =0.1)
                string CMD_1 = "def movel_test(): \r\n";
                string CMD_2 = "movel(" + HomeRecordVector + ",v = 0.08) \r\n";
                string CMD_3 = "movel(" + HomeRecordVectorX_Direction + ",v = 0.08) \r\n";
                string CMD_4 = "movel(" + HomeRecordVector + ",v = 0.08) \r\n";
                string CMD_5 = "movel(" + HomeRecordVectorY_Direction + ",v = 0.08) \r\n";
                string CMD_6 = "movel(" + HomeRecordVector + ",v = 0.08) \r\n";
                string CMD_7 = "end  \r\nmovel_test()";

                string TotalCMD = CMD_1 + CMD_2 + CMD_3 + CMD_4 + CMD_5 + CMD_6 + CMD_7;

                //MessageBox.Show(TotalCMD);
                URController.Send_command(TotalCMD);
            }
            else
            {
                MessageBox.Show("请确认目标UR已连接");
            }
        }

        //设置当前速度
        private void btnSetSpeed_Click(object sender, EventArgs e)
        {

        }

        //重置当前速度
        private void btnResetSpeed_Click(object sender, EventArgs e)
        {

        }

        //获取当前坐标值，此时我不管UR在哪里，保持当前姿态往OA向量方向移动，松开按钮则停止运动
        private void DriveOA_Down(object sender, MouseEventArgs e)
        {
            //当前方向向量的整数倍（比如1000倍，认为需要往该方向移动很远很远的距离）
            for(int i=0;i<CurrentActiveOA.Length;i++)
            {
                NewPosInt[i] = (CurrentActiveOA[i] * 1000);
            }

            string str = "movel(p[" + NewPosInt[0].ToString("0.0000") + "," + NewPosInt[1].ToString("0.0000") + "," + NewPosInt[2].ToString("0.0000") + ",0,0,0],a=0.1,v=0.1)";
            URController.Send_command(str);
        }

        private void DriveOA_Up(object sender, MouseEventArgs e)
        {
            string str = "stopl(1)";
            URController.Send_command(str);
        }

        private void DriveAO_Down(object sender, MouseEventArgs e)
        {
            //当前方向向量的整数倍（比如1000倍，认为需要往该方向移动很远很远的距离）
            for (int i = 0; i < CurrentActiveOA.Length; i++)
            {
                NewPosInt[i] = (CurrentActiveOA[i] * (-1000));
            }

            string str = "movel(p[" + NewPosInt[0].ToString("0.0000") + "," + NewPosInt[1].ToString("0.0000") + "," + NewPosInt[2].ToString("0.0000") + ",0,0,0],a=0.1,v=0.1)";
            URController.Send_command(str);
        }

        private void DriveAO_Up(object sender, MouseEventArgs e)
        {
            string str = "stopl(1)";
            URController.Send_command(str);
        }

        //与OA类似
        private void DriveOB_Down(object sender, MouseEventArgs e)
        {
            //当前方向向量的整数倍（比如1000倍，认为需要往该方向移动很远很远的距离）
            for (int i = 0; i < CurrentActiveOB.Length; i++)
            {
                NewPosInt[i] = (CurrentActiveOB[i] * 1000);
            }

            string str = "movel(p[" + NewPosInt[0].ToString("0.0000") + "," + NewPosInt[1].ToString("0.0000") + "," + NewPosInt[2].ToString("0.0000") + ",0,0,0],a=0.1,v=0.1)";
            URController.Send_command(str);
        }

        private void DriveOB_Up(object sender, MouseEventArgs e)
        {
            string str = "stopl(1)";
            URController.Send_command(str);
        }



        private void DriveBO_Down(object sender, MouseEventArgs e)
        {
            //累加当前方向向量的整数倍（比如1000倍，认为需要往该方向移动很远很远的距离）
            for (int i = 0; i < CurrentActiveOB.Length; i++)
            {
                NewPosInt[i] = (CurrentActiveOB[i] * (-1000));
            }

            string str = "movel(p[" + NewPosInt[0].ToString("0.0000") + "," + NewPosInt[1].ToString("0.0000") + "," + NewPosInt[2].ToString("0.0000") + ",0,0,0],a=0.1,v=0.1)";
            URController.Send_command(str);
        }

        private void DriveBO_Up(object sender, MouseEventArgs e)
        {
            string str = "stopl(1)";
            URController.Send_command(str);
        }

        //CO是往下运动
        private void DriveCO_Down(object sender, MouseEventArgs e)
        {
            //当前方向向量的整数倍（比如1000倍，认为需要往该方向移动很远很远的距离）
            for (int i = 0; i < CurrentActiveOC.Length; i++)
            {
                NewPosInt[i] = (CurrentActiveOC[i] * (-1000));
            }

            string str = "movel(p[" + NewPosInt[0].ToString("0.0000") + "," + NewPosInt[1].ToString("0.0000") + "," + NewPosInt[2].ToString("0.0000") + ",0,0,0],a=0.1,v=0.1)";
            URController.Send_command(str);
        }
        //CO是往下运动
        private void DriveCO_Up(object sender, MouseEventArgs e)
        {
            string str = "stopl(1)";
            URController.Send_command(str);
        }
        //OC是往上运动
        private void DriveOC_Down(object sender, MouseEventArgs e)
        {
            //当前方向向量的整数倍（比如1000倍，认为需要往该方向移动很远很远的距离）
            for (int i = 0; i < CurrentActiveOC.Length; i++)
            {
                NewPosInt[i] = (CurrentActiveOC[i] * (1000));
            }

            string str = "movel(p[" + NewPosInt[0].ToString("0.0000") + "," + NewPosInt[1].ToString("0.0000") + "," + NewPosInt[2].ToString("0.0000") + ",0,0,0],a=0.1,v=0.1)";
            URController.Send_command(str);
        }
        //OC是往上运动
        private void DriveOC_Up(object sender, MouseEventArgs e)
        {
            string str = "stopl(1)";
            URController.Send_command(str);
        }








    }
}
