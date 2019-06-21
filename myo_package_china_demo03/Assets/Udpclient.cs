using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;

public class Udpclient : MonoBehaviour
{
    string editString = "hello wolrd"; 
    public Text _tt_receive;
    public Text _tt_send;
   
    Socket socket;
    EndPoint serverEnd; 
    IPEndPoint ipEnd; 
    string recvStr; 
    string sendStr;
    byte[] recvData = new byte[1024]; 
    byte[] sendData = new byte[1024]; 
    int recvLen; 
    Thread connectThread; 

    void InitSocket()
    {
      
        ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8001);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        serverEnd = (EndPoint)sender;
        print("waiting for sending UDP dgram");
        SocketSend("hello");
        
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();
    }

    void SocketSend(string sendStr)
    {

        sendData = new byte[1024];  
        sendData = Encoding.ASCII.GetBytes(sendStr);
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipEnd);
    }


    void SocketReceive()
    {
  
        while (true)
        {
         
            recvData = new byte[1024];
            recvLen = socket.ReceiveFrom(recvData, ref serverEnd);
            print("message from: " + serverEnd.ToString()); 
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
    }

    void Start()
    {
        InitSocket();
    }

    /*void OnGUI()
    {
        editString = GUI.TextField(new Rect(10, 10, 100, 20), editString);
        if (GUI.Button(new Rect(10, 30, 60, 20), "send"))
            SocketSend(editString);
    }*/

    // Update is called once per frame
    void Update()
    {
        _tt_receive.text = recvStr;
        sendStr = _tt_send.text;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SocketSend(_tt_send.text);
        }
    }

    void OnApplicationQuit()
    {
        SocketQuit();
    }
}