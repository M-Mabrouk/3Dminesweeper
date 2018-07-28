using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    Interactor Interact;
    public Transform Cube;
    public float speed;
    public float threshhold;
    Vector3 mouseMove = Vector3.zero;
    Vector3 lastPoint = Vector3.zero;

    bool compareThreshhold(Vector3 mv, Vector3 lim)
    {
        bool ret;
        ret = (mv.x <= lim.x && mv.y <= lim.y && mv.z <= lim.z);
        ret = ret && (mv.x >= -lim.x && mv.y >= -lim.y && mv.z >= -lim.z);
        return ret;
    }

    private void Start()
    {
        GameManager.gameManager.controller = this;
        Interact = GetComponent<Interactor>();
    }
    void Update ()
    {
        if(Input.GetMouseButton(0))
        {
            mouseMove = Input.mousePosition - lastPoint;
            if (!compareThreshhold(mouseMove, Vector3.one * threshhold))
            {
                Interact.interact = false;
            }
            Cube.Rotate(new Vector3(mouseMove.y,-mouseMove.x,0)*Time.deltaTime*speed,Space.World);
        }
        lastPoint = Input.mousePosition;
    }
}
