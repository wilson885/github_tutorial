using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class emg_to : MonoBehaviour {
    public GameObject _i01;
    public GameObject _i02;
    public GameObject _i03;
    public GameObject _i04;
    public GameObject _i05;
    public GameObject _i06;
    public GameObject _i07;
    public GameObject _i08;
    public OSCReceiver _osc;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //_i01.transform.localScale = new Vector3(10, _osc.toemg01(), 10);
       // _i02.transform.localScale = new Vector3(10, _osc.toemg02(), 10);
        _i03.transform.localScale = new Vector3(10, _osc.toemg03() * 100, 10);
        _i04.transform.localScale = new Vector3(10, _osc.toemg04() * 100, 10);
        _i05.transform.localScale = new Vector3(10, _osc.toemg05() * 100, 10);
        _i06.transform.localScale = new Vector3(10, _osc.toemg06() * 100, 10);
        _i07.transform.localScale = new Vector3(10, _osc.toemg07() * 100, 10);
        _i08.transform.localScale = new Vector3(10, _osc.toemg08() * 100, 10);
    }
}
