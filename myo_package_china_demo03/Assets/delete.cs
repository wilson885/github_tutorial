using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position += new Vector3 (1, 0, 0);
        if (this.gameObject.name == "line_point(Clone)") 
        {
            StartCoroutine(die());
         }
    }
    IEnumerator die() 
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
     }
}
