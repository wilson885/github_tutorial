using UnityEngine;
using System.Collections;

public class drline_gy_x : MonoBehaviour
{
	public GameObject ball;
    int i;
	public csvInput _csv;
    void Start()
    {
		for (int k = 1; k < _csv.gyro_x.Length; k++) 
		{
			Instantiate (ball,new Vector3(i, float .Parse(_csv.gyro_x [i]), 0), new Quaternion(0,90,0,0));
			i++;
		}
    }

    void Update()
    {
		
    }
    public void record()
    {
		

    }
}