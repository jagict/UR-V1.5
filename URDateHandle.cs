using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//调用外部类
using ModbusCommunication;
using System.Windows.Forms;


//这是一个专门处理UR数据的类，包括抓取数据以及存储数据，供主程序调用
namespace URDate
{

    class URDateHandle
    {
        //我需要定义几组全局的变量，用来实时存储当前的六个坐标值，角度值，以及机械手的状态等，以方便在任何时候调用
        //六个坐标值
        public static double Positions_X;
        public static double Positions_Y;
        public static double Positions_Z;
        public static double Positions_U;
        public static double Positions_V;
        public static double Positions_W;

        //六个角度值
        public static double Angles_X;
        public static double Angles_Y;
        public static double Angles_Z;
        public static double Angles_U;
        public static double Angles_V;
        public static double Angles_W;


        //申明刷新速度
        public static int ScanRate;

        //在这里生成一个Modbus的Socket
        ModbusSocket Socket502 = new ModbusSocket();

        //在这里申明一个定时器，用于实时获取UR数据
        public System.Timers.Timer GetRobotInformationTimer;

        //申明第一组委托(把收到的六个坐标值传递出去)
        public delegate void GetPositionSuccess(float[] PositionArray);
        public event GetPositionSuccess OnGetPositionSuccess;

        //申明第二组委托(把收到的六个坐标值传递出去)
        public delegate void GetAngleSuccess(double[] AngleArray);
        public event GetAngleSuccess OnGetAngleSuccess;

        //申明第三组委托(把收到的六个机械手状态值传递出去)
        public delegate void GetRobotStateSuccess(int[] RobotState);
        public event GetRobotStateSuccess OnGetRobotStateSuccess;


        //初始化监视器的方法
        public void InitialMoniter(string UR_IP)
        {
            //初始化这个Socket502端口
            Socket502.RemoteIP = UR_IP;
            Socket502.RemotePort = 502;
            Socket502.SocketTimeOut = 1000;

            //调用定时器，在线程中定时执行任务
            GetRobotInformationTimer = new System.Timers.Timer(ScanRate);
            GetRobotInformationTimer.Elapsed += new System.Timers.ElapsedEventHandler(GetRobotInformation);
            GetRobotInformationTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            GetRobotInformationTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；

        }

        //这里定时抓取UR信息
        public void GetRobotInformation(object source, System.Timers.ElapsedEventArgs e)
        {
            //定时器里面只要不断的添加要监视的对象的方法即可(目前只添加了对六个坐标和六个角度的监视)
            GetPositions();
            GetAngles();

            GetRobotState();
        }

        //取得六个坐标值的方法
        public void GetPositions()
        {
            //这里实时读取UR数据（先读取六个坐标值，手册来看地址就是400）
            int[] SixPositions = Socket502.ReadMultipleRegister(6, 400);

            //对于XYZ，取得的并不能直接使用，而是要经过转换
            float[] PositionsFiltered = new float[6];

            for (int i = 0; i < 6; i++)
            {
                //见Modbus UR参考文档：如果XYZ有超过32768，则变成负值（虽然是0-65535，但是我们要自己变成有符号数“-32767”到“32768”）
                if (SixPositions[i] > 32768)
                {
                    SixPositions[i] = SixPositions[i] - 65535;
                }

                //前面三个XYZ做同样处理(对于所有的XYZ都要除以10)
                if (i < 3)
                {
                    PositionsFiltered[i] = (float)SixPositions[i]/10;
                }
                //后面三个UVW只要直接除以1000即可
                else 
                {
                    PositionsFiltered[i] = (float)SixPositions[i] / 1000;
                }

            }


            //读到之后就改写自己的成员值(注意XYZ值还是要原汁原味的浮点数，方便计算)

            URDateHandle.Positions_X = (PositionsFiltered[0] / 1000);
            URDateHandle.Positions_Y = (PositionsFiltered[1] / 1000);
            URDateHandle.Positions_Z = (PositionsFiltered[2] / 1000);

            URDateHandle.Positions_U = (PositionsFiltered[3]);
            URDateHandle.Positions_V = (PositionsFiltered[4]);
            URDateHandle.Positions_W = (PositionsFiltered[5]);


            #region 测试是否定时收到数据
            /* 
            //这里可以测试一下有没有成功实现
            string temp = "";
            for (int i = 0; i < PositionsFiltered.Length; i++)
            {
                temp += Convert.ToString(PositionsFiltered[i]) + "|";
            }
            MessageBox.Show(temp);
            */
            #endregion

            //还是需要借助委托触发，我这里执行完了，怎么通知主程序是关键
            OnGetPositionSuccess(PositionsFiltered);


        }

        //取得六个角度值的方法
        public void GetAngles()
        {
            //这里实时读取UR数据（读取六个关节角度值，手册来看地址就是270）
            int[] SixAngles = Socket502.ReadMultipleRegister(6, 270);

            //对于六个角度值，只有double类型可以放这种双精度类型值
            double[] AnglesFiltered = new double[6];

            for (int i = 0; i < 6; i++)
            {
                if (SixAngles[i] > 32768)
                {
                    SixAngles[i] = SixAngles[i] - 65535;
                }

                //这里取到的都是弧度值，要转换为角度值(除以1000之后还是弧度，要再转换为角度)
                AnglesFiltered[i] = (double)SixAngles[i] / 1000*(180/3.14);

            }

            //读到之后就改写自己的成员值(转换为弧度值)
            URDateHandle.Angles_X = (AnglesFiltered[0] * (3.14 / 180));
            URDateHandle.Angles_Y = (AnglesFiltered[1] * (3.14 / 180));
            URDateHandle.Angles_Z = (AnglesFiltered[2] * (3.14 / 180));
            URDateHandle.Angles_U = (AnglesFiltered[3] * (3.14 / 180));
            URDateHandle.Angles_V = (AnglesFiltered[4] * (3.14 / 180));
            URDateHandle.Angles_W = (AnglesFiltered[5] * (3.14 / 180));

            //还是需要借助委托触发，我这里执行完了，怎么通知主程序是关键
            OnGetAngleSuccess(AnglesFiltered);

        }

        //从手册可以知道，260-265的寄存器保存了当前机械手的状态信息
        public void GetRobotState()
        {
            //这里实时读取UR数据（读取是否开机，是否紧急停机等状态信息，手册来看地址就是260）
            int[] RobotState = Socket502.ReadMultipleRegister(6, 260);
            OnGetRobotStateSuccess(RobotState);

        }




    }
}
