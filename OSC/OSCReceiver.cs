using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OSCReceiver : MonoBehaviour
{
	public string RemoteIP = "127.0.0.1";
	public int SendToPort = 57131;
	public int ListenerPort = 8000;
	//public Transform controller;
	private Osc handler;
    string rr;
    public Text _emg01;
    public Text _emg02;
    public Text _emg03;
    public Text _emg04;
    public Text _emg05;
    public Text _emg06;
    public Text _emg07; 
    public Text _emg08;
    string[] str3;
    // Use this for initialization
    void Start()
	{
		//Initializes on start up to listen for messages
		//make sure this game object has both UDPPackIO and OSC script attached
		UDPPacketIO udp = GetComponent("UDPPacketIO") as UDPPacketIO;
		udp.init(RemoteIP, SendToPort, ListenerPort);
		handler = GetComponent("Osc") as Osc;
		handler.init(udp);

        handler.SetAddressHandler("/myo1/emg/scaled", Example1);
    }

	//these fucntions are called when messages are received
	public void Example1(OscMessage oscMessage)
	{
		//How to access values: 
		//oscMessage.Values[0], oscMessage.Values[1], etc
		//r01=(float)oscMessage.Values[1];
        rr = Osc.OscMessageToString(oscMessage);
        string str1 = rr.Replace("/myo1/emg/scaled ,ffffffff ", " ");
        string str2 = str1.Replace(" ", "/");
        str3 = str2.Split('/');
       //Debug.Log (str2);
       
        //Debug.Log("Called Example One > " + Osc.OscMessageToString(oscMessage));

    }

	//these fucntions are called when messages are received
	public void Example2(OscMessage oscMessage)
	{
		//How to access values: 
		//oscMessage.Values[0], oscMessage.Values[1], etc

		//Debug.Log("Called Example Two > " + Osc.OscMessageToString(oscMessage));
	}

	void Update()
	{
        _emg01.text = str3[0];
        _emg02.text = str3[1];
        _emg03.text = str3[2];
        _emg04.text = str3[3];
        _emg05.text = str3[4];
        _emg06.text = str3[5];
        _emg07.text = str3[6];
        _emg08.text = str3[7];
        if (Input.GetKeyDown(KeyCode.A))
		{

			handler.Send(Osc.StringToOscMessage("/imu"));
			print("A key was pressed");
		}
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