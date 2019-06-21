using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_rotation : MonoBehaviour
{
	public JointOrientation _joi;
	float a;
	float b;
	float c;
	float e;
	float f;
	float g;
	float [] acc_x = new float[10000];
	float [] vel_x= new float[10000];
	float [] pos_x= new float[10000];
	int time_count = 1;
	public csvInput _csv;
    // Start is called before the first frame update
    void Start()
    {
		acc_x [0] = 0.0f;
		acc_x [1] = 0.0f;
		vel_x [0] = 0.0f;
    }

    // Update is called once per frame
    void Update()
	{
		//this.gameObject.transform.rotation = new Quaternion (_joi.toorientation_x(), _joi.toorientation_y(), _joi.toorientation_z(), 1);
		time_count += 1;
		a += _joi.togyro_x () * Time.deltaTime;
		b += _joi.togyro_y () * Time.deltaTime;
		c += _joi.togyro_z () * Time.deltaTime;
		this.gameObject.transform.eulerAngles = new Vector3 (float.Parse(_csv.gyro_x[time_count])*10, float.Parse(_csv.gyro_y[time_count])*10,float.Parse(_csv.gyro_z[time_count])*10);
		//Debug.Log (time_count);
		//acc_x[].add = _joi.toacceleration_x ();

		//vel_x [time_count - 1] = (acc_x [time_count] - acc_x [time_count - 1]) * Time.deltaTime;
		//pos_x[time_count - 2] = (vel_x [time_count-1] - vel_x [time_count - 2]) * Time.deltaTime;

		//e += 0.5f * Time.deltaTime * _joi.toacceleration_x ()* Time.deltaTime;
		//f += 0.5f * Time.deltaTime * _joi.toacceleration_y ()* Time.deltaTime;
		//g += 0.5f * Time.deltaTime * _joi.toacceleration_z ()* Time.deltaTime;

		//this.gameObject.transform.position=new Vector3(float.Parse(_csv.acc_x[time_count]), float.Parse(_csv.acc_y[time_count]), float.Parse(_csv.acc_z[time_count]));
	

    }
}
