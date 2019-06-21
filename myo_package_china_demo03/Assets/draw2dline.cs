using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class draw2dline : MonoBehaviour
{
    public Image [] _image;
    public Vector2 [] updatepos;
    public GameObject _cc;
    public OSCReceiver _osc;
    public JointOrientation _joi;
    int booldata = 0;
    void Start()
    {
        for (int i = 0; i < _image.Length; i++)
        {
            updatepos[i] = new Vector2(
                _image[i].transform.position.x,
                _image[i].transform.position.y
            );
        }
        InvokeRepeating("drawline", 0, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (booldata < 10)
            {
                booldata += 1;
            }
            if (booldata == 10) 
            {
                booldata = 1;
            }

        }
    }
    public void drawline()
    {
        for (int i = 0; i < _image.Length; i++)
        {
            float gyx = _image[i].transform.position.y + _joi.togyro_x() * 30;
            float gyy = _image[i].transform.position.y + _joi.togyro_y() * 30;
            float gyz = _image[i].transform.position.y + _joi.togyro_z() * 30;

            float accx = _image[i].transform.position.y + _joi.toacceleration_x() * 30;
            float accy = _image[i].transform.position.y + _joi.toacceleration_y() * 30;
            float accz = _image[i].transform.position.y + _joi.toacceleration_z() * 30;

            float orix = _image[i].transform.position.y + _joi.toorientation_x() * 30;
            float oriy = _image[i].transform.position.y + _joi.toorientation_y() * 30;
            float oriz = _image[i].transform.position.y + _joi.toorientation_z() * 30;

            //updatepos[i] += new Vector2(1, 0);
            if (booldata==1) {
                Image gg = Instantiate(_image[i], new Vector2(updatepos[i].x, gyx), _image[i].transform.rotation);
                gg.transform.parent = _cc.transform;
            }
            if (booldata == 2)
            {
                Image gg = Instantiate(_image[i], new Vector2(updatepos[i].x, gyy), _image[i].transform.rotation);
                gg.transform.parent = _cc.transform;
            }
            if (booldata == 3)
            {
                Image gg = Instantiate(_image[i], new Vector2(updatepos[i].x, gyz), _image[i].transform.rotation);
                gg.transform.parent = _cc.transform;
            }
            if (booldata == 4)
            {
                Image gg = Instantiate(_image[i], new Vector2(updatepos[i].x, accx), _image[i].transform.rotation);
                gg.transform.parent = _cc.transform;
            }
            if (booldata == 5)
            {
                Image gg = Instantiate(_image[i], new Vector2(updatepos[i].x, accy), _image[i].transform.rotation);
                gg.transform.parent = _cc.transform;
            }
            if (booldata == 6)
            {
                Image gg = Instantiate(_image[i], new Vector2(updatepos[i].x, accz), _image[i].transform.rotation);
                gg.transform.parent = _cc.transform;
            }
            if (booldata == 7)
            {
                Image gg = Instantiate(_image[i], new Vector2(updatepos[i].x, orix), _image[i].transform.rotation);
                gg.transform.parent = _cc.transform;
            }
            if (booldata == 8)
            {
                Image gg = Instantiate(_image[i], new Vector2(updatepos[i].x, oriy), _image[i].transform.rotation);
                gg.transform.parent = _cc.transform;
            }
            if (booldata == 9)
            {
                Image gg = Instantiate(_image[i], new Vector2(updatepos[i].x, oriz), _image[i].transform.rotation);
                gg.transform.parent = _cc.transform;
            }


        }
    }
}
