using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Files;

//引用UR机器人相关
using ModbusCommunication;
using URControl;

//引用Halcon的代码
using HalconDotNet;


//本面板的说明：
//1 硬件为USB工业相机（京杭工业相机JHSM300）
//2 软件为Halcon 10.0 通过DirectionShow接口获取相机数据
//3 为了方便演示，除了特征匹配没有添加额外的功能，包括修改相机分辨率，修改显示模式等
//4 当前的相机分辨率设置为1600*1200，而显示窗口的尺寸为800*600，因此实际测出来的像素坐标值是实际值的一半



namespace UR_点动控制器
{
    public partial class CameraCalibration : Form
    {
        public string DefaultINIPath;
        public string DefaultScriptPath = Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory) + "Script.txt";

        //设置一些开关量和全局变量
        public static bool Running = false;//相机是否运行
        public static bool DoMatch = false;//是否进行匹配（仅在设置好了匹配图像之后该开关为true）
        public static string StaticImage = "";//打开浏览文件对话框的时候，找到的静态图像的地址字符串
        public static string StaticImagePath = "";//打开保存文件对话框的时候，得到的要保存的静态图像的地址字符串

        //这里不用相机的SDK获取相机分辨率种类了，直接手动输入
        public static int[] ResolutionWidth = new int[] { 640, 800, 1024, 1280, 1600, 2048 };
        public static int[] ResolutionHeight = new int[] { 480, 600, 768, 1024, 1200, 1536 };

        public static int CurrentSelectedResolutionWidth = 1600;
        public static int CurrentSelectedResolutionHeight = 1200;

        //实例化这个Halcon对象，这个Halcon对象涵盖了多个图像处理的方法
        public HDevelopExport HD = new HDevelopExport();

        //为了在HDevelopExport类里面跟Form1有数据交互，我还是做成静态变量(X,Y坐标和旋转角度)
        public static double[] MatchDataArray = new double[5];

        //方便记录拍照点和回拍照点等功能
        //读取配置文件的IP
        FilesINI ConfigController = new FilesINI();
        string CurrentIP;
        string Control_Port;

        ModbusSocket URRegisterHandle = new ModbusSocket();
        URControlHandle URController = new URControlHandle();

        //记录此时为拍照位置的数组
        double[] CurrentPosInt = new double[6];
        string CurrentPosStr = "";

        //我还要读取配置文件中三个参考点坐标的的实际物理位置
        double[] PosInt1 = new double[6];
        double[] PosInt2 = new double[6];
        double[] PosInt3 = new double[6];

        //写G代码的时候没想好，这里还得再复制一遍代码
        double[] CurrentActiveOA = new double[6];
        double[] CurrentActiveOB = new double[6];

        //再做几个新的向量(描述特征物体的相对位置的向量)
        double[] ObjectVectorOA = new double[6];
        double[] ObjectVectorOB = new double[6];
        double[] ObjectVectorTotal = new double[6];
        double[] ObjectVectorFinal = new double[6];

        bool ifConnected = false;

        public CameraCalibration(string ConfigFilePath)
        {
            InitializeComponent();
            DefaultINIPath = ConfigFilePath;
        }

        private void CameraCalibration_Load(object sender, EventArgs e)
        {
            CurrentIP = ConfigController.INIRead("UR控制参数", "RemoteIP", DefaultINIPath);
            Control_Port = ConfigController.INIRead("UR控制参数", "RemoteControlPort", DefaultINIPath);
            
            //读取拍照位置的各项参数
            CurrentPosStr = ConfigController.INIRead("UR运动参数", "BaseSnapPos", DefaultINIPath);
            txtCurrentSnapPos.Text = CurrentPosStr;

            //读取三个基准位置
            PosInt1[0] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeX_Camera", DefaultINIPath));
            PosInt1[1] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeY_Camera", DefaultINIPath));
            PosInt1[2] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeZ_Camera", DefaultINIPath));
            PosInt1[3] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeU_Camera", DefaultINIPath));
            PosInt1[4] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeV_Camera", DefaultINIPath));
            PosInt1[5] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeW_Camera", DefaultINIPath));

            PosInt2[0] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeX_A_Camera", DefaultINIPath));
            PosInt2[1] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeY_A_Camera", DefaultINIPath));
            PosInt2[2] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeZ_A_Camera", DefaultINIPath));
            PosInt2[3] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeU_A_Camera", DefaultINIPath));
            PosInt2[4] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeV_A_Camera", DefaultINIPath));
            PosInt2[5] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeW_A_Camera", DefaultINIPath));

            PosInt3[0] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeX_B_Camera", DefaultINIPath));
            PosInt3[1] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeY_B_Camera", DefaultINIPath));
            PosInt3[2] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeZ_B_Camera", DefaultINIPath));
            PosInt3[3] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeU_B_Camera", DefaultINIPath));
            PosInt3[4] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeV_B_Camera", DefaultINIPath));
            PosInt3[5] = Convert.ToDouble(ConfigController.INIRead("UR运动参数", "HomeW_B_Camera", DefaultINIPath));

            //计算OA和OB的长度(空间两点的距离公式)
            double OALength = Math.Sqrt(Math.Pow((PosInt1[0] - PosInt2[0]), 2) + Math.Pow((PosInt1[1] - PosInt2[1]), 2) + Math.Pow((PosInt1[2] - PosInt2[2]), 2));
            double OBLength = Math.Sqrt(Math.Pow((PosInt1[0] - PosInt3[0]), 2) + Math.Pow((PosInt1[1] - PosInt3[1]), 2) + Math.Pow((PosInt1[2] - PosInt3[2]), 2));
            
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

            //读取三个特征点的像素值
            txt_O_X.Text = ConfigController.INIRead("UR运动参数", "HomeX_Camera_Pixel", DefaultINIPath);
            txt_O_Y.Text = ConfigController.INIRead("UR运动参数", "HomeY_Camera_Pixel", DefaultINIPath);
            txt_A_X.Text = ConfigController.INIRead("UR运动参数", "HomeX_A_Camera_Pixel", DefaultINIPath);
            txt_A_Y.Text = ConfigController.INIRead("UR运动参数", "HomeY_A_Camera_Pixel", DefaultINIPath);
            txt_B_X.Text = ConfigController.INIRead("UR运动参数", "HomeX_B_Camera_Pixel", DefaultINIPath);
            txt_B_Y.Text = ConfigController.INIRead("UR运动参数", "HomeY_B_Camera_Pixel", DefaultINIPath);



            URRegisterHandle.RemoteIP = CurrentIP;
            URRegisterHandle.RemotePort = 502;
            URRegisterHandle.SocketTimeOut = 1000;

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

            //注册HD的委托方法
            HD.OnGetMatchPatternSuccess += new HDevelopExport.GetMatchPatternSuccess(UpdateMatchValue);

            //这里要在线程中处理，否则窗体就卡死了
            Thread thread = new Thread(new ThreadStart(threadMethod));
            thread.Start();
        }

        public void threadMethod()
        {
            //我们一共有了两个窗体控件的句柄，一个是hWindowControl2的，用于显示静态图像，一个是hWindowControl1的，用于显示视频图像
            HD.RunHalcon(hWindowControl1.HalconWindow);
        }

        //从Halcon导出的代码
        public partial class HDevelopExport
        {
            //这是一个用于显示抓取视频的窗口控件
            public HTuple hv_ExpDefaultWinHandle;

            //把这个ho_Image的图像公共出来(注意这个ho_Image是整个采集图像的核心，就放在这个变量里)
            public HObject ho_Image, ho_Image_Static;

            // Local iconic variables 
            public HObject ho_ImageFilled, ho_Rectangle;
            public HObject ho_ImageReduced;

            // Local control variables 
            public HTuple hv_AcqHandle;
            public HTuple hv_Width, hv_Height, hv_WindowID = new HTuple();

            public HTuple hv_pi, hv_arrowLength, hv_TemplateID, hv_Row, hv_Column;
            public HTuple hv_Angle, hv_Error, hv_Area;
            public HTuple hv_LeftTopY, hv_LeftTopX, hv_RightBottomY, hv_RightBottomX;

            //声明一个修改分辨率的字符串
            public HTuple hv_TargetResolution = "RGB24 (" + CurrentSelectedResolutionWidth + "x" + CurrentSelectedResolutionHeight + ")";

            //申明一组委托，当测试出来了特征的参数值，需要把参数值传递出去(当前需要传递就是XY坐标和偏转角度三个浮点数)
            public delegate void GetMatchPatternSuccess(double[] MatchData);
            public event GetMatchPatternSuccess OnGetMatchPatternSuccess;

            // Main procedure 
            private void action()
            {
                // Initialize local and output iconic variables 
                HOperatorSet.GenEmptyObj(out ho_Image);
                HOperatorSet.GenEmptyObj(out ho_ImageFilled);
                HOperatorSet.GenEmptyObj(out ho_Rectangle);
                HOperatorSet.GenEmptyObj(out ho_ImageReduced);

                //Code generated by Image Acquisition 01（应该就是说将相机采集到的图像句柄传递给hv_AcqHandle这个HTuple类）
                HOperatorSet.OpenFramegrabber("DirectShow", 1, 1, 0, 0, 0, 0, "default", 8, "rgb", -1, "false", hv_TargetResolution, "Jinghang JHSM300 Camera", 0, -1, out hv_AcqHandle);
                HOperatorSet.GrabImageStart(hv_AcqHandle, -1);

                //这个死循环必须始终在执行
                while (true)
                {
                    //但是内部可以做开关控制是否运行
                    if (Running == true)
                    {
                        //不知道如果不Dispose的话，是否会造成内存泄露(如果不关闭，则在开启视频抓取的时候，直接点击设置矩形框，该ho_Image为null,则会报错，因为无法把空图像放到控件里)
                        //ho_Image.Dispose();

                        //异步采集并将图像传递给ho_Image这个HObject
                        HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);

                        //采集好了之后就显示到hv_ExpDefaultWinHandle这个控件上（已经在主窗体绘制好了）
                        HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandle);

                        if (DoMatch == true)
                        {
                            MatchDynamicImage();
                        }

                    }

                }
                //由于线程初始化的时候执行了OpenFramegrabber，这里一旦关闭，则后面我还想再开启就不行了，所以这里的代码要注释掉
                //HOperatorSet.CloseFramegrabber(hv_AcqHandle);
                //ho_Image.Dispose();
            }

            public void InitHalcon()
            {
                // Default settings used in HDevelop 
                HOperatorSet.SetSystem("do_low_error", "false");
            }

            public void RunHalcon(HTuple Window)
            {
                hv_ExpDefaultWinHandle = Window;
                action();
            }

            //保存当前的静态图像
            public void SaveStaticImage()
            {
                //注意一般我保存当前静态图像之前，先要打开摄像头，因此ho_Image始终是输出有值的，而ho_Image_Static却只有在载入了静态图像之后才会有值
                //调用Halcon的写入图像的方法（HOperatorSet.WriteImage(HObject Image,HTuple format,HTuple fillColor,HTuple fileName)）
                HOperatorSet.WriteImage(ho_Image, "png", 255, StaticImagePath);
            }

            //读取静态图像并输出到窗口
            public void GetStaticImage()
            {
                HOperatorSet.ReadImage(out ho_Image_Static, StaticImage);
                HOperatorSet.DispObj(ho_Image_Static, hv_ExpDefaultWinHandle);
            }

            //显示静态图像的匹配结果
            public void MatchStaticImage()
            {
                //绘制矩形（可以做一个弹窗什么的）
                MessageBox.Show("请在右图中绘制匹配的区块，按鼠标右键结束绘制");
                HOperatorSet.DrawRectangle1(hv_ExpDefaultWinHandle, out hv_LeftTopY, out hv_LeftTopX, out hv_RightBottomY, out hv_RightBottomX);

                //获取这个鼠标绘制出来的矩形框
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, hv_LeftTopY, hv_LeftTopX, hv_RightBottomY, hv_RightBottomX);

                hv_pi = ((new HTuple(0.0)).TupleAcos()) * 2;
                hv_arrowLength = 50;

                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ho_Image_Static, ho_Rectangle, out ho_ImageReduced);

                //从ImageReduced中创建一个该特征的唯一TemplateID用于后续匹配作为代号
                HOperatorSet.CreateTemplateRot(ho_ImageReduced, 4, -hv_pi, 2 * hv_pi, hv_pi / 45, "sort", "original", out hv_TemplateID);

                //在图像中查找与TemplateID对应的区块的最佳匹配，并把最佳匹配的结果保存到Column,Angle,Error中
                HOperatorSet.BestMatchRotMg(ho_ImageReduced, hv_TemplateID, -hv_pi, 2 * hv_pi, 40, "true", 4, out hv_Row, out hv_Column, out hv_Angle, out hv_Error);

                //显示箭头
                HOperatorSet.DispArrow(hv_ExpDefaultWinHandle, hv_Row, hv_Column, hv_Row + ((hv_Angle.TupleSin()) * hv_arrowLength), hv_Column + ((hv_Angle.TupleCos()) * hv_arrowLength), 1);

                //打开开关并发送当前的匹配结果
                DoMatch = true;
                SendMatchResult();

            }

            //显示动态图像的匹配结果
            public void MatchDynamicImage()
            {

                //在图像中查找与TemplateID对应的区块的最佳匹配，并把最佳匹配的结果保存到Column,Angle,Error中
                HOperatorSet.BestMatchRotMg(ho_Image, hv_TemplateID, -hv_pi, 2 * hv_pi, 40, "true", 4, out hv_Row, out hv_Column, out hv_Angle, out hv_Error);

                //显示匹配结果的箭头
                HOperatorSet.DispArrow(hv_ExpDefaultWinHandle, hv_Row, hv_Column, hv_Row + ((hv_Angle.TupleSin()) * hv_arrowLength), hv_Column + ((hv_Angle.TupleCos()) * hv_arrowLength), 1);

                SendMatchResult();
            }

            //向外传递匹配结果
            public void SendMatchResult()
            {
                //到这里我就完全得到了静态图像的特征区块的中心点坐标和偏转角度(更新这个数组)
                MatchDataArray[0] = hv_Column;
                MatchDataArray[1] = hv_Row[0];
                //角度转换为角度值(为了方便，角度和弧度都传递出去，要用哪个就用哪个)
                MatchDataArray[2] = hv_Angle.TupleDeg();
                MatchDataArray[3] = hv_Angle;
                MatchDataArray[4] = hv_Error;
                OnGetMatchPatternSuccess(MatchDataArray);
            }

        }

        //从相机得到数据之后更新主界面的数据
        public void UpdateMatchValue(double[] MatchDataArray)
        {
            this.label_X.Text = MatchDataArray[0].ToString("0.0");
            this.label_Y.Text = MatchDataArray[1].ToString("0.0");
            this.label_R_Radius.Text = MatchDataArray[3].ToString("0.000");
            this.label_R_Angle.Text =  MatchDataArray[2].ToString("0.0");
            this.label_Error.Text = MatchDataArray[4].ToString("0.0");
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

        private void btnRun_Click(object sender, EventArgs e)
        {
            Running = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Running = false;
        }

        private void btnSnapshot_Click(object sender, EventArgs e)
        {
            SaveFileDialog Savefile = new SaveFileDialog();
            //设置保存文件的默认路径
            Savefile.InitialDirectory = "C:\\";
            Savefile.Filter = "png文件(*.png)|*.png";
            if (Savefile.ShowDialog() == DialogResult.OK)
            {
                //获取到了当前的保存文件名
                //MessageBox.Show(Savefile.FileName);
                StaticImagePath = Savefile.FileName;
                HD.SaveStaticImage();
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(Openfile.FileName);
                StaticImage = Openfile.FileName;

                //执行Halcon的内部方法
                HD.GetStaticImage();
            }
        }

        private void btnSetMatch_Click(object sender, EventArgs e)
        {
            HD.MatchStaticImage();
        }

        private void btnRecordSnapPos_Click(object sender, EventArgs e)
        {
            if (ifConnected)
            {
                CurrentPosInt = GetPose();
                CurrentPosStr = GetPoseString(CurrentPosInt);
                //写入控件，也写入配置文件
                txtCurrentSnapPos.Text = CurrentPosStr;
                ConfigController.INIWrite("UR运动参数", "BaseSnapPos", CurrentPosStr, DefaultINIPath);
            }
            else
            {
                MessageBox.Show("请确认目标UR已连接");
            }
        }

        //回拍照位置
        private void btnGoSnapPos_Click(object sender, EventArgs e)
        {
            if (ifConnected)
            {
                //用一个比较慢的速度回原点(设置回原点的速度为v =0.1)
                string CMD = "movel(" + CurrentPosStr + ",v = 0.1)";
                URController.Send_command(CMD);
            }
            else
            {
                MessageBox.Show("请确认目标UR已连接");
            }
        }

        //计算当前标定结果，就是为了得到像素精度
        private void btnCalculateResult_Click(object sender, EventArgs e)
        {
            //用截图的方式可以轻松的获取当前画板的O,A,B三点的像素坐标,从而获得像素精度
            double OAPixel_Width = Math.Abs(Convert.ToDouble(txt_O_X.Text) - Convert.ToDouble(txt_A_X.Text));
            double OBPixel_Width = Math.Abs(Convert.ToDouble(txt_O_Y.Text) - Convert.ToDouble(txt_B_Y.Text));

            //这里我们已知画板的OA和OB的物理长度分别为250mm和200mm(并且已知截图的实际像素尺寸只有实际图像的一半)
            double Accurary_OA = 250 / (OAPixel_Width*2);
            double Accurary_OB = 200 / (OBPixel_Width*2);

            //MessageBox.Show(Accurary_OA.ToString("0.000") + "|" + Accurary_OB.ToString("0.000"));
            //我们取两个方向的平均值来减小误差（也可以两个方向单独算，因为相机一定是有畸变的）但是从结果来看，像素精度还是差距很小的
            double Average_Accurary = (Accurary_OA + Accurary_OB) / 2;
            label_Accurary.Text = Average_Accurary.ToString("0.000");

            //计算之后把数据写入配置文件，方便下次载入和调用
            ConfigController.INIWrite("UR运动参数", "HomeX_Camera_Pixel", txt_O_X.Text, DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeY_Camera_Pixel", txt_O_Y.Text, DefaultINIPath);

            ConfigController.INIWrite("UR运动参数", "HomeX_A_Camera_Pixel", txt_A_X.Text, DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeY_A_Camera_Pixel", txt_A_Y.Text, DefaultINIPath);

            ConfigController.INIWrite("UR运动参数", "HomeX_B_Camera_Pixel", txt_B_X.Text, DefaultINIPath);
            ConfigController.INIWrite("UR运动参数", "HomeY_B_Camera_Pixel", txt_B_Y.Text, DefaultINIPath);

        }

        //用Socket方式发送给UR一个字符串，该字符串已经是一个六维向量，UR可以直接拿来用，就是走这个坐标值就行了，在UR上面不需要做任何设置
        //采用socket纯主机的方式优点是代码都藏在上位机中，方便调试和实现复杂应用，而且别人不好抄袭或者模仿，因为UR的程序是很好破解的
        private void btn_TriggerSocket_Click(object sender, EventArgs e)
        {
            try
            {
                //1 把当前的特征的XY像素坐标计算出相对于O点的差值(注意这里对我填到文本框里的结果要*2，因为实际确实是他的两倍
                double PixelDiff_X = (Convert.ToDouble(label_X.Text) - (Convert.ToDouble(txt_O_X.Text) * 2));
                double PixelDiff_Y = (Convert.ToDouble(label_Y.Text) - (Convert.ToDouble(txt_O_Y.Text) * 2));

                //2 把这个差值转换为实际的mm差值（我们在前面已经得到了像素精度），最后再转换成M做向量合成
                double RealDiff_X = Convert.ToDouble(label_Accurary.Text) * PixelDiff_X/1000;
                double RealDiff_Y = Convert.ToDouble(label_Accurary.Text) * PixelDiff_Y/1000;
                //角度值和弧度值都取出来，想用哪个用哪个
                double PixelDiff_R_Angle = Convert.ToDouble(label_R_Angle.Text);
                double PixelDiff_R_Radius = Convert.ToDouble(label_R_Radius.Text);

                //3 把得到的两个差值放射到OA和OB两个方向向量中(只关心XYZ，不关心UVW)
                ObjectVectorOA[0] = CurrentActiveOA[0] * RealDiff_X;
                ObjectVectorOA[1] = CurrentActiveOA[1] * RealDiff_X;
                ObjectVectorOA[2] = CurrentActiveOA[2] * RealDiff_X;
                ObjectVectorOA[3] = 0;
                ObjectVectorOA[4] = 0;
                ObjectVectorOA[5] = 0;

                ObjectVectorOB[0] = CurrentActiveOB[0] * RealDiff_Y;
                ObjectVectorOB[1] = CurrentActiveOB[1] * RealDiff_Y;
                ObjectVectorOB[2] = CurrentActiveOB[2] * RealDiff_Y;
                ObjectVectorOB[3] = 0;
                ObjectVectorOB[4] = 0;
                ObjectVectorOB[5] = 0;

                //4 得到最终的向量
               
                ObjectVectorTotal[0] = ObjectVectorOA[0] + ObjectVectorOB[0];
                ObjectVectorTotal[1] = ObjectVectorOA[1] + ObjectVectorOB[1];
                ObjectVectorTotal[2] = ObjectVectorOA[2] + ObjectVectorOB[2];
                ObjectVectorTotal[3] = 0;
                ObjectVectorTotal[4] = 0;
                ObjectVectorTotal[5] = 0;

                //5 向目标UR发送指定向量（注意UR从拍照位置SnapPos就不需要再去这个O点，再用get_forward_kin()获取一遍数值，这样反而麻烦，所以直接和实际的O点坐标合成即可）
                ObjectVectorFinal[0] = ObjectVectorTotal[0] + PosInt1[0];
                ObjectVectorFinal[1] = ObjectVectorTotal[1] + PosInt1[1];
                ObjectVectorFinal[2] = ObjectVectorTotal[2] + PosInt1[2];
                ObjectVectorFinal[3] = PosInt1[3];
                ObjectVectorFinal[4] = PosInt1[4];
                ObjectVectorFinal[5] = PosInt1[5];

                //string OriginalPos = GetPoseString(PosInt1);
                string NewPos = GetPoseString(ObjectVectorFinal);

                //注意这里就通过纯上位机控制了，UR上不写任何程序了
                if (ifConnected)
                {
                    //注意虽然发送的只是一个movel指令，里面有一个表示UR位置的六维向量，我们仍然无法把角度值直接传递过去
                    //比如movel(p[x,y,z,u,v,w])跟movel(p[x,y,z,u,v,w+3.14])的区别，并不是最后一个轴旋转180度的效果
                    //movej[A,B,C,D,E,F,G]跟movej[A,B,C,D,E,F,G+3.14]倒是真的这个关系，可惜我们已然要求movel方式移动了
                    //其实解决方法就是直接发送一串函数，让UR自己去用pose_add翻译出到底我要最后一个关节旋转指定角度如何分配到最后三个关节值上
                    /*
                     //下面的代码如果是单纯的移动，不需要转换关节角度值可以这么干
                    string CMD = "movel(" + NewPos + ",v = 0.1)";
                    URController.Send_command(CMD);
                    */

                    string Embed_head = "def VisionCatch_test():\r\n";
                    string Embed_NewPos = "NewPos = " + NewPos + "\r\n";
                    string Embed_RotatePos = "RotatePos = p[0,0,0,0,0," + -PixelDiff_R_Radius + "]\r\n";
                    string Embed_TotalPos = "TotalPos = pose_add(NewPos,RotatePos)\r\n";
                    string Embed_CMD = "movel(TotalPos,v = 0.1)\r\n";
                    string Embed_Bottom = "end\r\n";
                    string Embed_Run = "VisionCatch_test()\r\n";

                    textBoxCommand.Text = "";
                    textBoxCommand.Text = Embed_head + Embed_NewPos + Embed_RotatePos + Embed_TotalPos + Embed_CMD + Embed_Bottom + Embed_Run;

                    string CMD = textBoxCommand.Text;
                    URController.Send_command(CMD);
                }
                else
                {
                    MessageBox.Show("请确认目标UR已连接");
                }

                //把每次比对的差值放到listBox中方便查看
                this.listBox_Debug.Items.Clear();
                this.listBox_Debug.Items.Add("特征像素差（px）：X" + PixelDiff_X.ToString("0.0"));
                this.listBox_Debug.Items.Add("特征像素差（px）：Y" + PixelDiff_Y.ToString("0.0"));
                this.listBox_Debug.Items.Add("特征实际差（mm）：X" + RealDiff_X.ToString("0.0000"));
                this.listBox_Debug.Items.Add("特征实际差（mm）：Y" + RealDiff_Y.ToString("0.0000"));

                this.listBox_Debug.Items.Add("基准向量OA：" + GetPoseString(CurrentActiveOA));
                this.listBox_Debug.Items.Add("基准向量OB：" + GetPoseString(CurrentActiveOB));

                this.listBox_Debug.Items.Add("差异向量OA：" + GetPoseString(ObjectVectorOA));
                this.listBox_Debug.Items.Add("差异向量OB：" + GetPoseString(ObjectVectorOB));

                this.listBox_Debug.Items.Add("参考基准向量：" + GetPoseString(PosInt1));
                this.listBox_Debug.Items.Add("差异向量整体：" + GetPoseString(ObjectVectorTotal));

                this.listBox_Debug.Items.Add("合成向量整体：" + GetPoseString(ObjectVectorFinal));
            }
            catch(Exception ConvertError)
            {
                MessageBox.Show("数据转换出错" + ConvertError.ToString());
            }

        }

        //socket主从方式触发（字符串）
        //socket主从方式触发，把目标六维向量发送给UR，相比于后面的Modbus方式，不需要做数据转换，所以很方便，而且也比纯主机的方式容易适应现场环境
        private void btn_TriggerSlave_Click(object sender, EventArgs e)
        {

        }

        //Modbus方式触发
        //Modbus方式触发，是写寄存器的方式，很容易理解，通用性也很广，因为大多数的工业设备，PLC，传感器都支持Modbus协议
        private void btn_TriggerModbus_Click(object sender, EventArgs e)
        {

        }


        private void btnRestModbus_Click(object sender, EventArgs e)
        {

        }







    }
}
