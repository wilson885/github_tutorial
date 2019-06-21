using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;

public class UdpServer : MonoBehaviour
{
    public Text _tt_receive;
    public Text _tt_send;

    Socket socket; 
    EndPoint clientEnd; 
    IPEndPoint ipEnd;
    string recvStr; 
    string sendStr; 
    byte[] recvData = new byte[1024]; 
    byte[] sendData = new byte[1024]; 
    int recvLen; 
    Thread connectThread; 



    void InitSocket()
    {
       
        ipEnd = new IPEndPoint(IPAddress.Any, 8001);
      
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    
        socket.Bind(ipEnd);
   
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        clientEnd = (EndPoint)sender;
        print("waiting for UDP dgram");

       
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();
    }

    void SocketSend(string sendStr)
    {
      
        sendData = new byte[1024];
        sendData = Encoding.ASCII.GetBytes(sendStr);
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, clientEnd);
    }

    void SocketReceive()
    {

        while (true)
        {
           
            recvData = new byte[1024];
          
            recvLen = socket.ReceiveFrom(recvData, ref clientEnd);
            print("message from: " + clientEnd.ToString()); 
    
            recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
            print(recvStr);
        
        }
    }


    void SocketQuit()
    {
 
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }

        if (socket != null)
            socket.Close();
        print("disconnect");
    }


    void Start()
    {
        InitSocket();
    }


    // Update is called once per frame
    void Update()
    {
        _tt_receive.text = recvStr;
        sendStr = _tt_send.text;
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            SocketSend(sendStr);
        }
    }

    void OnApplicationQuit()
    {
        SocketQuit();
    }
}