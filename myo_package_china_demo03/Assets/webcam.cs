using UnityEngine;
using System.Collections;

public class webcam : MonoBehaviour
{
    Renderer _r;
    void Start()
    {
        _r = GetComponent<Renderer>();
        WebCamTexture c = new WebCamTexture();
       _r.material.mainTexture = c; // 將目前物體貼圖換成攝影機貼圖
        c.Play();
    }

}
