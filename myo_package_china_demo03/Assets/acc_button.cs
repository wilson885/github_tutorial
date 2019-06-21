using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acc_button : MonoBehaviour {
	public GameObject ori;
	public GameObject acc;
	public GameObject gyro;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) 
		{
			ori.SetActive(false);
			acc.SetActive(true);
			gyro.SetActive (false);
		}
	}
	public void bb()
	{
		ori.SetActive(false);
		acc.SetActive(true);
		gyro.SetActive (false);
	}
}
