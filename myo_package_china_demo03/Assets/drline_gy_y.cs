using UnityEngine;
using System.Collections;

public class drline_gy_y : MonoBehaviour
{
    public GameObject startpoint;
    LineRenderer _line;
    public OSCReceiver _osc;
    public JointOrientation _joi;
    int forward = 0;
    float _data;
    int i;
	public csvInput _csv;
    public float wave_strength = 50;
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        //InvokeRepeating("record", 0, 0.03f);
        /*for (int i = 0; i < 10000; i++)
        {
            _line.SetPosition(i, new Vector3(
            i * 10 + startpoint.transform.position.x,
            startpoint.transform.position.y,
             1000));
        }*/
		
    }

    void Update()
    {
		for (int k = 0; k < _csv.gyro_x.Length; k++) 
		{
			forward += 10;
			_data = float.Parse (_csv.gyro_x[k]);
			_line.SetPosition(k, new Vector3(forward + startpoint.transform.position.x, _data * wave_strength + startpoint.transform.position.y, 1000));
		}
        //_line.positionCount=100;
        // _line.SetPosition(50, new Vector3(startpoint.transform.position.x + 500, startpoint.transform.position.y, 0));
    }
    public void record()
    {
        forward += 10;
        _data = _joi.togyro_y();
        if (i < 10000)
        {
            i++;
        }
        if (i == 10000)
        {
            i = 0;
            forward = 0;
        }
        _line.SetPosition(i, new Vector3(forward + startpoint.transform.position.x, _data * wave_strength + startpoint.transform.position.y, 1000));

    }
}
