using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class example : MonoBehaviour {

	public GameObject _txt01;
	public GameObject _txt02;
	public GameObject _txt03;
	public GameObject _txt04;

	public Vector3 R_Shoulder;
	public Vector3 L_Shoulder;
	public Vector3 R_Hand;
	public Vector3 L_Hand;
	public Vector3 L_Elbow;
	public Vector3 R_Elbow;
	public Vector3 Spine_SH;
	public Vector3 Spine_B;

	public Vector3 R_ACC;
	public Vector3 L_ACC;


	List<float> side_list = new List<float> ();
	List<float> move_list_RX = new List<float> ();
	List<float> move_list_RY = new List<float> ();
	List<float> move_list_RZ = new List<float> ();
	List<float> move_list_LX = new List<float> ();
	List<float> move_list_LY = new List<float> ();
	List<float> move_list_LZ = new List<float> ();

	public OSCReceiver _osc;
	public JointOrientation _joi;
	public JointOrientation _joi2;
	public BodySourceView _body;

	public float hand_dis;
	public float hip_dis;
	public float R_Elbow_Angle;
	public float L_Elbow_Angle;
	public float R_iner_power;
	public float L_iner_power;
	public float error_power;
	public float add_power;
	public int record_flag;
	public int side_flag;
	public int move_flag;
	public Text output;
	public Text output1;
	public Text output2;
	public Text output3;
	   















	// Use this for initialization
	void Start () {
		record_flag  = 0;
		side_flag = 0;
		move_flag = 0;
		output2.text = "waiting";

		
	}
	
	// Update is called once per frame v
	void Update () {
		
		R_Hand = _body.HandRight();
		L_Hand = _body.HandLeft();
		R_Elbow = _body.ElbowRight();
		L_Elbow = _body.ElbowLeft();
		Spine_SH = _body.SpineSH ();
		Spine_B = _body.SpineB ();
		R_Shoulder = _body.ShoulderRight();
		L_Shoulder = _body.ShoulderLeft();

		Vector3 spine = new Vector3 (Spine_B.x - Spine_SH.x,Spine_B.y - Spine_SH.y,Spine_B.z - Spine_SH.z);
		Vector3 R_Arm = new Vector3 (R_Hand.x - R_Shoulder.x,R_Hand.y - R_Shoulder.y,R_Hand.z - R_Shoulder.z);
		Vector3 L_Arm = new Vector3 (L_Hand.x - L_Shoulder.x,L_Hand.y - L_Shoulder.y,L_Hand.z - L_Shoulder.z);

		R_Elbow_Angle = Mathf.Acos (Vector3.Dot (spine, R_Arm));
		L_Elbow_Angle = Mathf.Acos (Vector3.Dot (spine, L_Arm));

		R_Elbow_Angle *= 180 / Mathf.PI;
		L_Elbow_Angle *= 180 / Mathf.PI;

		//Debug.Log;
		hand_dis = Vector3.Distance (R_Hand, L_Hand);
		//output.text = hand_dis.ToString (); 

		if (hand_dis <= 0.2 && R_Elbow_Angle + L_Elbow_Angle > 170) {
			count = 0;
			will_stop = false;
			record_flag = 1;
		}


		if (record_flag == 1) {
			collecttion ();
			output.text = "ready~go!";


		} else {
			output.text = "waiting";

		}


		//= _body.HandLeft ().x;
			
		//Debug.Log ();
	}

	bool will_stop = false;
	int count = 0;
	void collecttion(){
		

		R_Shoulder = _body.ShoulderRight();
		L_Shoulder = _body.ShoulderLeft();
		R_Hand = _body.HandRight();
		L_Hand = _body.HandLeft();
		hip_dis = R_Shoulder.z - L_Shoulder.z;
		R_ACC = new Vector3 (_joi.toacceleration_x(), _joi.toacceleration_y(), _joi.toacceleration_z());
		L_ACC = new Vector3 (_joi2.toacceleration_x(), _joi2.toacceleration_y(), _joi2.toacceleration_z());
		R_iner_power = Vector3.Magnitude (R_ACC);
		L_iner_power = Vector3.Magnitude (L_ACC);
		side_list.Add (hip_dis);
		move_list_RX.Add (_joi.toacceleration_x());
		move_list_LX.Add (_joi2.toacceleration_x());
		move_list_RY.Add (_joi.toacceleration_y());
		move_list_LY.Add (_joi2.toacceleration_y());
		move_list_RZ.Add (_joi.toacceleration_z());
		move_list_LZ.Add (_joi2.toacceleration_z());

		add_power = Mathf.Abs (Mathf.Pow(R_iner_power,2) + Mathf.Pow(L_iner_power,2));

		if (add_power > 5) {
			will_stop = true;
			float side = side_list.Average ();
			R_Elbow = _body.ElbowRight();
			L_Elbow = _body.ElbowLeft();
			//output1.text = side.ToString ();
			output.text = "waiting";
			if (side < 0) {
				output2.text = "Left";
				if (will_stop == true) {
					count++;
					if (count >= 10) {
						float x_pow = move_list_LX.Average ();
						float y_pow = move_list_LY.Average ();
						float z_pow = move_list_LZ.Average ();
						movement_judge_L (x_pow,y_pow,z_pow,R_Hand.y,L_Hand.y);




						record_flag = 0;
					}

				}



			} 

			else {
				output2.text = "Right";
				if (will_stop == true) {
					count++;
					if (count >= 10) {
						float x_pow = move_list_RX.Average ();
						float y_pow = move_list_RY.Average ();
						float z_pow = move_list_RZ.Average ();
						movement_judge_R (x_pow,y_pow,z_pow,R_Hand.y,L_Hand.y);




						record_flag = 0;
					}

				}

			}

		}









		//error_power = Mathf.Abs (Mathf.Pow(R_iner_power,2)  - Mathf.Pow(L_iner_power,2));







		//output3.text = add_power.ToString ();


		 
		
		
	}

	void movement_judge_R(float x_pow, float y_pow, float z_pow, float R_Hand, float L_Hand){

		side_list.Clear ();
		//float RE = R_Elbow.magnitude;
		//float LE = L_Elbow.magnitude;
		if (R_Hand > L_Hand) {
			output3.text = "Pitch";
			_txt01.SetActive (false);
			_txt02.SetActive (true);
			_txt03.SetActive (false);
			_txt04.SetActive (false);
			StartCoroutine (wait ());
			
		} else {
			output3.text = "Bat";
			_txt01.SetActive (false);
			_txt02.SetActive (false);
			_txt03.SetActive (false);
			_txt04.SetActive (true);
			StartCoroutine (wait ());
		}
		
		move_list_LX.Clear ();
		move_list_LY.Clear ();
		move_list_LZ.Clear ();
		move_list_RX.Clear ();
		move_list_RY.Clear ();
		move_list_RZ.Clear ();
		
		
	}

	void movement_judge_L(float x_pow, float y_pow, float z_pow, float R_Hand, float L_Hand){

		side_list.Clear ();

		if (R_Hand < L_Hand) {
			output3.text = "Pitch";
			_txt01.SetActive (true);
			_txt02.SetActive (false);
			_txt03.SetActive (false);
			_txt04.SetActive (false);
			StartCoroutine (wait ());

		} else {
			output3.text = "Bat";
			_txt01.SetActive (false);
			_txt02.SetActive (false);
			_txt03.SetActive (true);
			_txt04.SetActive (false);
			StartCoroutine (wait ());
		}


		move_list_LX.Clear ();
		move_list_LY.Clear ();
		move_list_LZ.Clear ();
		move_list_RX.Clear ();
		move_list_RY.Clear ();
		move_list_RZ.Clear ();


	}
	IEnumerator wait()
	{
		yield return new WaitForSeconds (1);
		_txt01.SetActive (false);
		_txt02.SetActive (false);
		_txt03.SetActive (false);
		_txt04.SetActive (false);
	}
}
