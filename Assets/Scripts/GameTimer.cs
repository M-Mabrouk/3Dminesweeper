using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameTimer : MonoBehaviour {

    public GameObject parent;
    Text timer;

    // Use this for initialization
    void Start () {
        timer = GetComponent<Text>();
        GameManager.gameManager.Timer = parent;
	}
	
	// Update is called once per frame
	void Update () {
        timer.text = ((int)Time.timeSinceLevelLoad - GameManager.gameManager.startTime).ToString();
	}
}
