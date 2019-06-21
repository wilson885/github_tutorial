using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gy_button : MonoBehaviour {
	public GameObject ori;
	public GameObject acc;
	public GameObject gyro;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.G)) 
		{
			ori.SetActive(false);
			acc.SetActive(false);
			gyro.SetActive (true);
		}
	}
	public void bb()
	{
		ori.SetActive(false);
		acc.SetActive(false);
		gyro.SetActive (true);
	}
}
