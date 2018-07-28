using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustView : MonoBehaviour {

    public float zoomValue;

	// Use this for initialization
	void Start () {
        GameManager.difficulity mode = GameManager.gameManager.mode;
        if (mode == GameManager.difficulity.Easy)
        {
            transform.position += transform.forward * zoomValue;
        }
        else if (mode == GameManager.difficulity.Hard)
        {
            transform.position += -transform.forward * zoomValue;
        }
    }
	
}
