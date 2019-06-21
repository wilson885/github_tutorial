using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OSCReceiver1 : MonoBehaviour
{
    public string RemoteIP = "127.0.0.1";
    public int SendToPort = 57131;
    public int ListenerPort = 5432;
    //public Transform controller;
    private Osc handler;
    string rr;
    string[] str3;
    string[] str4;
    // Use this for initialization
    void Start()
    {
        //Initializes on start up to listen for messages
        //make sure this game object has both UDPPackIO and OSC script attached

        UDPPacketIO udp = GetComponent("UDPPacketIO") as UDPPacketIO;
        udp.init(RemoteIP, SendToPort, ListenerPort);
        handler = GetComponent("Osc") as Osc;
        handler.init(udp);

        handler.SetAddressHandler("/myo1/EMG/scaled", Example1);
    }

    //these fucntions are called when messages are received
    public void Example1(OscMessage oscMessage)
    {
        //How to access values: 
        //oscMessage.Values[0], oscMessage.Values[1], etc
        //r01=(float)oscMessage.Values[1];
        rr = Osc.OscMessageToString(oscMessage);
        string str1 = rr.Replace("/myo1/EMG/scaled ,ffffffff ", " ");
        string str2 = str1.Replace(" ", "/");
        str3 = str2.Split('/');
        Debug.Log(str3);

        Debug.Log("Called Example One > " + Osc.OscMessageToString(oscMessage));

    }

    void Update()
    {
        Debug.Log(str3);
    }

    public float toemg01()
    {
        return float.Parse(str3[0]);
    }
    public float toemg02()
    {
        return float.Parse(str3[1]);
    }
    public float toemg03()
    {
        return float.Parse(str3[2]);
    }
    public float toemg04()
    {
        return float.Parse(str3[3]);
    }
    public float toemg05()
    {
        return float.Parse(str3[4]);
    }
    public float toemg06()
    {
        return float.Parse(str3[5]);
    }
    public float toemg07()
    {
        return float.Parse(str3[6]);
    }
    public float toemg08()
    {
        return float.Parse(str3[7]);
    }
}
