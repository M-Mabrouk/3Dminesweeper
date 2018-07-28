using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {

    public bool interact = true;
    Camera cam;

    private void Start()
    {
        GameManager.gameManager.interactor = this;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetMouseButtonUp(0))
        {
            if(interact)
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (!hit.collider.gameObject.name.Equals("plane"))
                        Record.record.GetRoot(hit.collider.gameObject.name).clicked(true);
                }
            }
            interact = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.collider.gameObject.name.Equals("plane"))
                    Record.record.GetRoot(hit.collider.gameObject.name).Rclicked();
            }
        }
    }
}
