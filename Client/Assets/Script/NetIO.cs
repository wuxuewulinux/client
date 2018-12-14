using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.IO;

public class NetIO {

    private static NetIO instance;

    private Socket socket;

    private string ip;

    private int port;

    private byte[] readbuff = new byte[1024];

    List<byte> cache = new List<byte>();

    public List<CSMsg> messages = new List<CSMsg>();
    private bool isReading = false;

    //序列化和反序列化需要的类型如下定义：

 
    private MemoryStream stream;                          //创建一个流对象，该流序列化时会用到

    private CSMsg build;                                  //创建一个protobuf对象，该对象序列化时会用到


    /// <summary>
    /// 单例对象
    /// </summary>
    public static NetIO Instance {
        get {
            if (instance == null) {
                //初始化所有配置XML文件
                if (!LogicConfigManager.Instance().Init())
                {
                    Debug.Log("初始化所有配置XML文件 失败 !");
                    return null;
                }
                my_server rServerCofig = LogicConfigManager.Instance().GetServerMysqlConfig().GetServerConfig();
                instance = new NetIO(rServerCofig.ip,rServerCofig.port);
            }
            return instance;
        }
        
    }

    private NetIO(string ServerIp,int serverPort) {
        try
        {
            ip = ServerIp;
            port = serverPort;
            //创建客户端连接对象
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //连接到服务器
            socket.Connect(ip, port);
            Debug.Log("连接服务器成功 !");
            //开启异步消息接收 消息到达后会直接写入 缓冲区 readbuff
            socket.BeginReceive(readbuff, 0, 1024, SocketFlags.None, ReceiveCallBack, readbuff);
        }
        catch (Exception e) {
            Debug.Log(e.Message);
        }

    }

    //收到消息回调
    private void ReceiveCallBack(IAsyncResult ar) {
        try
        {
            //获取当前收到的消息长度()
            int length = socket.EndReceive(ar);
            byte[] message = new byte[length];
            Buffer.BlockCopy(readbuff, 0, message, 0, length);
            cache.AddRange(message);
            if (!isReading)
            {
                isReading = true;
                onData();
            }
            //尾递归 再次开启异步消息接收 消息到达后会直接写入 缓冲区 readbuff
            socket.BeginReceive(readbuff, 0, 1024, SocketFlags.None, ReceiveCallBack, readbuff);
        }
        catch (Exception e) {
            Debug.Log("远程服务器主动断开连接");
            socket.Close();

        }
    }

    /*
     *  build = proto.Build();
        build.WriteTo(stream);
        mess.buff = stream.ToArray();
     */

    public void Write(CSMsg rCSMsg)
    {
        //序列化数据把数据库发送出去
        CSMsg.Builder proto = CSMsg.CreateBuilder();
        MemoryStream stream = new MemoryStream();
        rCSMsg = proto.Build();
        rCSMsg.WriteTo(stream);
        byte[] buff = stream.ToArray();

        //先处理头部，把头部字节数转换成网络字节序
        short x = (short)buff.Length;
        ushort b = (ushort)System.Net.IPAddress.HostToNetworkOrder(x);                        //把x转成相应的大端字节数 
        byte[] intBuff = System.BitConverter.GetBytes(b);                            //这样直接取到的就是大端字节序字节数组。大端字节序就是网络字节序

        //把头部和包体字节保存在一个数组中。

        byte[] sendbuff = new byte[intBuff.Length + buff.Length];
        Array.Copy(intBuff, sendbuff, intBuff.Length);                               //先把头部int字节 拷贝到sendbuff
        Array.Copy(buff, 0, sendbuff, intBuff.Length, buff.Length);        //在把数据包字节拷贝到sendbuff数组中

        try
        {
            socket.Send(sendbuff);
        }catch (Exception e){
            Debug.Log("网络错误，请重新登录"+e.Message);
        }
        
    }

    //缓存中有数据处理
    void onData()
    {
        //长度解码
         byte[] result= decode(ref cache);

        //长度解码返回空 说明消息体不全，等待下条消息过来补全
         if (result == null) {
             isReading = false;
             return;
         }

         CSMsg message = mdecode(result);

         if (message == null)
         {
             isReading = false;
             return;
         }
        //进行消息的处理,把消息压入一个容器中
         messages.Add(message);
        //尾递归 防止在消息处理过程中 有其他消息到达而没有经过处理
        onData();
    }
    public static byte[] decode(ref List<byte> cache)
    {
        if (cache.Count < sizeof(ushort)) return null;

        MemoryStream ms = new MemoryStream(cache.ToArray());//创建内存流对象，并将缓存数据写入进去
        BinaryReader br = new BinaryReader(ms);//二进制读取流
        ushort length = br.ReadUInt16();//从缓存中读取int型消息体长度
        //如果消息体长度 大于缓存中数据长度 说明消息没有读取完 等待下次消息到达后再次处理
        if (length > (ushort)(ms.Length - ms.Position))
        {
            return null;
        }
        //读取正确长度的数据
        byte[] result = br.ReadBytes(length);
        //清空缓存
        cache.Clear();
        //将读取后的剩余数据写入缓存
        cache.AddRange(br.ReadBytes((int)(ms.Length - ms.Position)));
        br.Close();
        ms.Close();
        return result;
    }


    public static CSMsg mdecode(byte[] value)     //反序列化protobuf得到真正的数据
    {
        CSMsg.Builder builder = new CSMsg.Builder();
        if(builder.MergeFrom(value) == null)
            Debug.Log("反序列化protobuf 失败 ！");
        CSMsg Data = builder.Build();
        return Data;
    }

}
