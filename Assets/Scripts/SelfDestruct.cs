using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    public float counter; 
	// Use this for initialization
	void Start () {
        Invoke("Destruct", counter);
	}
	
	void Destruct()
    {
        Destroy(gameObject);
    }
}
