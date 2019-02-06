using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace FiberopticServer
{
    class NetConn
    {
        class server
        {
            static void tcpserver()
            {
                #region    //TCP协议通信
                int recv;     //客户端发送信息长度
                byte[] data;  //缓存客户端发送的信息，sockets传递的信息必须为字节数组
                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);   //本机预使用的IP和端口

                Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);  
                newsock.Bind(ipep);     //绑定
                newsock.Listen(10);     //监听
                Console.WriteLine("waiting for a client......");
                Socket client = newsock.Accept();   //有客户连接尝试时执行，并返回一个新的socket，用于与客户之间通信
                IPEndPoint clientip = (IPEndPoint)client.RemoteEndPoint;
                Console.WriteLine("connect with client " + clientip.Address + " at port " + clientip.Port);
                string welcom = "welcom here!";
                data = Encoding.ASCII.GetBytes(welcom);
                client.Send(data, data.Length, SocketFlags.None);   //发消息
                while (true)   //死循环不断从客户端获取信息
                {
                    data = new byte[1024];
                    recv = client.Receive(data);
                    Console.WriteLine("recv = " + recv);
                    if (recv == 0) break;     //信息长度为0，说明客户端断开
                    Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                    client.Send(data, recv, SocketFlags.None);
                    //在服务器端输入，发送到客户端
                    //string input = Console.ReadLine();
                    //if (input == "exit") break;
                    //client.Send(Encoding.ASCII.GetBytes(input));
                    //data = new byte[1024];
                    //recv = client.Receive(data);
                    //input = Encoding.ASCII.GetString(data, 0, recv);
                    //Console.WriteLine(input);
                }
                Console.WriteLine("Disconnect from " + clientip.Address);
                client.Close();
                newsock.Close();
                #endregion
            }
        }
        class client
        {
            //返回一个绑定了指定ip和端口的socket
            Socket tcpclient(string ip, int port)
            {
                Socket newclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                newclient.Bind(new IPEndPoint(IPAddress.Any, 9050));
                IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ip), port);   //服务器IP和端口
                try
                {
                    newclient.Connect(ie);   //客户端想特定的服务器发送信息，所以不需要绑定本机的IP和端口
                }
                catch (SocketException e)
                {
                    Console.WriteLine("unable to connect to server");
                    Console.WriteLine(e.Message);
                    return null;
                }
                return newclient;
            }
            void tcpsend(string ip, int port,string senddata)
            {
                byte[] data = new byte[1024];
                Socket newclient=tcpclient(ip,port);
                int receivedDataLebgth = newclient.Receive(data);
                string stringdata = Encoding.ASCII.GetString(data, 0, receivedDataLebgth);
                Console.WriteLine(stringdata);
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input == "exit") break;
                    newclient.Send(Encoding.ASCII.GetBytes(input));
                    data = new byte[1024];
                    receivedDataLebgth = newclient.Receive(data);
                    stringdata = Encoding.ASCII.GetString(data, 0, receivedDataLebgth);
                    Console.WriteLine(stringdata);
                }
                Console.WriteLine("disconnect from server");
                newclient.Shutdown(SocketShutdown.Both);
                newclient.Close();
            }
        }
    }
}
