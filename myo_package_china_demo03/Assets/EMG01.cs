using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMG01 : MonoBehaviour
{
    public GameObject startpoint;
    LineRenderer _line;
    public OSCReceiver _osc;
    public JointOrientation _joi;
    int forward = 0;
    float _data;
    int i;
    float wave_strength = 0.2f;
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        InvokeRepeating("record", 0, 0.03f);
        for (int i = 0; i < 50; i++)
        {
            _line.SetPosition(i, new Vector3(
            i * 10 + startpoint.transform.position.x,
            startpoint.transform.position.y,
             1000));
        }
    }

    void Update()
    {
        //_line.positionCount=100;
        //_line.SetPosition(50, new Vector3(startpoint.transform.position.x+500, startpoint.transform.position.y, 0));
    }
    public void record()
    {
        forward += 10;
		_data = _joi.toemg01();
        if (i < 49)
        {
            i++;
        }
        if (i == 49)
        {
            i = 0;
            forward = 0;
        }
        _line.SetPosition(i, new Vector3(forward + startpoint.transform.position.x, _data * wave_strength + startpoint.transform.position.y, 1000));

    }
}
