using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class csvInput : MonoBehaviour
{
	public string [] gyro_x=new string[1000000];
	public string [] gyro_y=new string[1000000];
	public string [] gyro_z=new string[1000000];
	public string [] acc_x=new string[1000000];
	public string [] acc_y=new string[1000000];
	public string [] acc_z=new string[1000000];

    // Start is called before the first frame update
    void Start()
    {
		readCSV ();
		readCSV02 ();
		readCSV03 ();
		readCSV04 ();
		readCSV05 ();
		readCSV06 ();

    }

    // Update is called once per frame
    void Update()
    {
       
    }
	public void readCSV ()
	{
		TextAsset binAsset = Resources.Load ("gyro_x", typeof(TextAsset)) as TextAsset;
		Debug.Log (binAsset);
		string lineArray = binAsset.text.Replace ("\n", ",");
		string[] lineArray1 = lineArray.Split ("," [0]);
		for (int i = 0; i < lineArray1.Length; i+=1) 
		{
			gyro_x [i] = lineArray1 [i];

		}
	}
	public void readCSV02()
	{
		TextAsset binAsset = Resources.Load ("gyro_y", typeof(TextAsset)) as TextAsset;
		Debug.Log (binAsset);
		string lineArray = binAsset.text.Replace ("\n", ",");
		string[] lineArray1 = lineArray.Split ("," [0]);
		for (int i = 0; i < lineArray1.Length; i+=1) 
		{
			gyro_y [i] = lineArray1 [i];

		}
	}
	public void readCSV03()
	{
		TextAsset binAsset = Resources.Load ("gyro_z", typeof(TextAsset)) as TextAsset;
		Debug.Log (binAsset);
		string lineArray = binAsset.text.Replace ("\n", ",");
		string[] lineArray1 = lineArray.Split ("," [0]);
		for (int i = 0; i < lineArray1.Length; i+=1) 
		{
			gyro_z [i] = lineArray1 [i];
		}
	}

	public void readCSV04 ()
	{
		TextAsset binAsset = Resources.Load ("acc_x", typeof(TextAsset)) as TextAsset;
		Debug.Log (binAsset);
		string lineArray = binAsset.text.Replace ("\n", ",");
		string[] lineArray1 = lineArray.Split ("," [0]);
		for (int i = 0; i < lineArray1.Length; i+=1) 
		{
			acc_x [i] = lineArray1 [i];

		}
	}
	public void readCSV05()
	{
		TextAsset binAsset = Resources.Load ("acc_y", typeof(TextAsset)) as TextAsset;
		Debug.Log (binAsset);
		string lineArray = binAsset.text.Replace ("\n", ",");
		string[] lineArray1 = lineArray.Split ("," [0]);
		for (int i = 0; i < lineArray1.Length; i+=1) 
		{
			acc_y [i] = lineArray1 [i];

		}
	}
	public void readCSV06()
	{
		TextAsset binAsset = Resources.Load ("acc_z", typeof(TextAsset)) as TextAsset;
		Debug.Log (binAsset);
		string lineArray = binAsset.text.Replace ("\n", ",");
		string[] lineArray1 = lineArray.Split ("," [0]);
		for (int i = 0; i < lineArray1.Length; i+=1) 
		{
			acc_z [i] = lineArray1 [i];
		}
	}
}
