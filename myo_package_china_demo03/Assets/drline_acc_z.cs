using UnityEngine;
using System.Collections;

public class drline_acc_z: MonoBehaviour
{
    public GameObject startpoint;
    LineRenderer _line;
    public OSCReceiver _osc;
    public JointOrientation _joi;
    int forward = 0;
    float _data;
    int i;
    public int wave_strength = 50;
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
        //_line.SetPosition(50, new Vector3(startpoint.transform.position.x + 500, startpoint.transform.position.y, 0));
    }
    public void record()
    {
        forward += 10;
        _data = _joi.toacceleration_z();
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
