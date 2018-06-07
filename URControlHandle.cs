using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

//调用外部类
using ModbusCommunication;


namespace URControl
{
    class URControlHandle
    {

        public Socket ClientSocket;
        public IPAddress myIP;
        public IPEndPoint ipe;

        //创建只需要实例化这个socket即可，不需要连接
        public void Creat_client(string IP, int PORT)
        {
            //所有UR的控制都在这里完成，先生成一个ClientSocket
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            myIP = IPAddress.Parse(IP);
            ipe = new IPEndPoint(myIP, PORT);

            //一次连接终生使用
            try
            {
                ClientSocket.Connect(ipe);
            }

            catch (Exception connectError)
            {
                MessageBox.Show(connectError.ToString());

            }

        }

        //因为连接是同步的方法，会导致阻塞，所以把连接功能放到与发送一起执行
        public void Send_command(string command)
        {
            command += "\r\n";
            byte[] buffersend = System.Text.Encoding.Default.GetBytes(command);
            
            try
            {
                ClientSocket.Send(buffersend);
            }
            catch (Exception sendError)
            {
                //MessageBox.Show(sendError.ToString());
                //这就是说明没有连接到UR(实际操作中，即便连接正常了，也会有这样的问题)
                //MessageBox.Show("未取得与UR的连接，请确认连接正常。");

            }

        }

        //前面发送的指令都是单向的，但是有的指令是请求UR提供返回值的
        public string Send_command_WithFeedback(string command)
        {
            command += "\r\n";
            byte[] buffersend = System.Text.Encoding.Default.GetBytes(command);

            try
            {

                ClientSocket.Send(buffersend);
            }
            catch (Exception sendError)
            {
                MessageBox.Show(sendError.ToString());
            }

             //发送完了之后立即等待接收
            byte[] bytes = new byte[1024];
            string data = "";
            int bytesRec = ClientSocket.Receive(bytes);

            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
            return data;
        }

        //还有最极端的一种情况，我刚连接到Dashboard的时候，Dashboard会主动给我发送一条命令
        public string No_command_WaitFeedback()
        {
            byte[] bytes = new byte[1024];
            string data = "";
            int bytesRec = ClientSocket.Receive(bytes);

            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
            return data;
        }

        //只有在点击退出按钮才关闭
        public void Close_client()
        {
            try
            {
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
            }
            catch
            { 
            
            }

        }


    }
}
