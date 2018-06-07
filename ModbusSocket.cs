using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

//Modbus的两种通讯方式(Socket跟SerialPort)
namespace ModbusCommunication
{
    //定义功能码
    public enum FunctionCode : byte
    {
        //Read Multiple Registers
        Read = 3,
        //Write Multiple Registers
        Write = 16
    }

    //网口的ModbusSocket类
    class ModbusSocket
    {
        #region 静态属性
        //有静态属性（远程IP，远程端口，超时时间，起始地址）
        public string RemoteIP;
        public int RemotePort;
        public int SocketTimeOut;

        //有一个Socket对象和IPE对象
        public Socket socket = null;
        public IPEndPoint ipe;

        //我个人定义，如果写多个寄存器，则不同寄存器的值以“|”分开(不喜欢的可以自己更改)
        public char RegisterDivision = '|';

        # endregion

        //连接的方法（Modbus诡异的一点就是连接就断开，而不是始终保持连接，所以在写入和读取方法的第一步都是要connect）
        public void connectServer()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //设置这个socket的超时时间
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, SocketTimeOut);
            //用同步方法进行连接
            ipe = new IPEndPoint(IPAddress.Parse(RemoteIP), RemotePort);
            try
            {
                socket.Connect(ipe);
            }
            catch(Exception ConnectionError)
            {
                //MessageBox.Show("未取得UR的连接，请确认连接正常。");
            }
            

        }

        //WriteMultipleRegister方法需要传入写入寄存器的字符串（如“123|234|5555”，如果只有一个则不用分隔符），要写入寄存器的个数（1,2,3）要一一对应和写入的起始地址
        public void WriteMultipleRegister(string RegisterString,int RegisterNum,int StartAddress)
        {
            //将传入的字符串按RegisterDivision进行分割
            string[] RegisterStringArray = RegisterString.Split(new char[] { RegisterDivision });

            //把分割好的字符串存入byte数组(这里已经知道了到底要写入几个寄存器，也就知道了数组长度，因为寄存器分高低，如果我只要写1个寄存器，则就要有2个byte，以此类推)
            byte[] data = new byte[RegisterNum*2];

            //如果发现要写入的寄存器数量与给的不一致，则提示
            if (RegisterStringArray.Length != RegisterNum)
            {
                MessageBox.Show("你到底要写入几个寄存器，方法参数对不上");
            }
            else
            { 
                 //把分割好的每个寄存器要写入的值依次添加进来
                for (int i = 0; i < RegisterStringArray.Length; i++)
                {
                    //把每个数字提取到tempArray里面
                    byte[] tempArray = IntTobyteArray(Convert.ToInt32(RegisterStringArray[i]));
                    data[i*2] = tempArray[0];
                    data[i*2+1] = tempArray[1];
                }

                //调用连接方法
                this.connectServer();

                //MessageBox.Show("连接成功");
                List<byte> values = new List<byte>(255);

                //1 凑Header

                //1.1（数据位：1-2） 定义我是谁(Transaction Identifier),协议给了我2byte空间定义我是谁（我代号就是01，如果Modbus TCP Server接收成功，返回信息也要有这个代号）
                values.AddRange(new Byte[] { 0, 1 });

                //1.2 （数据位：3-4）定义协议号，协议给了我2byte空间定义协议号（协议号就是00，表示这是MODBUS 协议）
                values.AddRange(new Byte[] { 0, 0 });

                //1.3 （数据位：5-6）现在考虑将56两位凑到一块
                byte[] DataByte = BitConverter.GetBytes((data.Length + 7));
                values.Add(DataByte[1]);
                values.Add(DataByte[0]);

                //1.4 （数据位：7）只要补全一个0即可
                values.Add(0);

                //1.5 （数据位：8）定义功能码（把写多个寄存器这个16码转换为byte类型数据）Function Code : 16 (Write Multiple Register)
                values.Add((byte)FunctionCode.Write);

                //1.6 （数据位：9-10）起始地址，现在把地址这个int类型转换为两个byte(地址他说我差1，我就给他减掉1好了)
                byte[] AddressByte = BitConverter.GetBytes(StartAddress);
                values.Add(AddressByte[1]);
                values.Add(AddressByte[0]);

                //1.7  （数据位：11-12）寄存器数量（不可能超过255个）
                values.Add(0);
                values.Add((byte)RegisterNum);

                //1.8 （数据位：13）发送数据的长度，跟前面保持不变
                values.Add((byte)data.Length);

                //2 发数据
                values.AddRange(data);
                socket.Send(values.ToArray());

            }

        }

        //ReadMultipleRegister需要传入的参数包括：要读取的寄存器个数（比如1,2,3），要读取的寄存器首地址（比如100）
        public int[] ReadMultipleRegister(int ResigterNum,int StartAddress)
        {
            //我确实要读取的东西(byte永远是两个表示一个)
            byte[] TruelyDateByte = new byte[ResigterNum*2];
            int[] TruelyDateInt = new int[ResigterNum];


            //调用连接方法
            try
            {
                this.connectServer();
            }
            catch (Exception ConnectionError)
            { 
                
            }

            //定义长度不确定的数组sendData，每个数组元素都是byte类型的整数
            List<byte> sendData = new List<byte>(255);

            //1 凑Header

            //1.1（数据位：1-2） 定义我是谁(Transaction Identifier),协议给了我2byte空间定义我是谁（我代号就是01，如果Modbus TCP Server接收成功，返回信息也要有这个代号）
            sendData.AddRange(new Byte[] { 0, 1 });

            //1.2 （数据位：3-4）定义协议号，协议给了我2byte空间定义协议号（协议号就是00，表示这是MODBUS 协议）
            sendData.AddRange(new Byte[] { 0, 0 });

            //1.3 （数据位：5-6）header还有几位？（对于读取来说，header后面定死了还有6位）
            byte[] DataByte = BitConverter.GetBytes(6);
            sendData.Add(DataByte[1]);
            sendData.Add(DataByte[0]);

            //1.4 （数据位：7）只要补全一个0即可
            sendData.Add(0);

            //1.5 （数据位：8）定义功能码（把读多个寄存器这个03码转换为byte类型数据）Function Code : 03 (Read Multiple Registers)
            sendData.Add((byte)FunctionCode.Read);

            //1.6 （数据位：9-10）起始地址，现在把地址这个int类型转换为两个byte
            byte[] AddressByte = BitConverter.GetBytes(StartAddress);
            sendData.Add(AddressByte[1]);
            sendData.Add(AddressByte[0]);

            //1.7  （数据位：11-12）寄存器数量，默认要读取的寄存器数量不超过255个，所以高位不管了
            sendData.Add(0);
            sendData.Add((byte)ResigterNum);

            //2 发送查询命令
            try
            {
                socket.Send(sendData.ToArray());
            }
            catch
            { 
            
            }

            //立即等待返回（由于是同步的操作，所以定义“我是谁”毫无意义，除非是异步的，返回值不知道要返回给谁）
            int ReadBuffer = 256;
            byte[] ReadBufferData = new byte[ReadBuffer];
            //socket的Receive方法直接把读取到的数据返回给ReadBufferData
            try
            {
                socket.Receive(ReadBufferData);
            }
            catch
            { 
            
            }
            //虽然一次把所有都读出来，但是并不是所有都是我需要的，我前面规定了我要读取几个，你给我返回几个就可以了
            //（0|1|0|0|0|9|0|3|6|）前面9位对我来说都是无意义的
            for (int i = 0; i < TruelyDateByte.Length; i++)
            {
                TruelyDateByte[i] = ReadBufferData[i + 9];
            }

            //然后byte再放回int
            for (int i = 0; i < TruelyDateInt.Length;i++ )
            {
                TruelyDateInt[i] = TruelyDateByte[i * 2] * 256 + TruelyDateByte[i * 2+1];
            }

            //尝试自己关闭，避免通讯错误
            bool closeMyself = true;
            if (closeMyself)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch
                {

                }
            }

            return TruelyDateInt;

        }

        //定义一个将int类型数据转换为寄存器高低字节的方法（int大小为0-65535）
        public byte[] IntTobyteArray(int InputNum)
        {
            byte[] temp = new byte[2];
            int InputNum_Hign = InputNum / 256;
            int InputNum_Low = InputNum % 256;

            //整数部分给高位，余数部分给低位
            temp[0] = (byte)InputNum_Hign;
            temp[1] = (byte)InputNum_Low;

            return temp;
        }


    }
}
