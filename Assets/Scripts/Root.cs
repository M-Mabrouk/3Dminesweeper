using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour {

    public bool Explodable, Empty, Uncovered, flagged;
    int neighbours = 0;
    public Rigidbody RB;
    public Collider col;
    public Display display;
    public GameObject block;
    public Renderer blockRend;
    List<Vector3> dirs = new List<Vector3>();
    List<Root> neighbourRs = new List<Root>();
    private void Awake()
    {
        flagged = false;
        RB = gameObject.AddComponent<Rigidbody>();
        RB.useGravity = false;
        col = gameObject.AddComponent<BoxCollider>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                for (int k = -1; k < 2; k++)
                {
                    if(i!=0 || j!=0 || k!=0)
                    {
                        dirs.Add(new Vector3(i, j, k));
                        
                    }
                }
            }
        }
        

    }
    public void DetectNeighbours()
    {
        foreach (Vector3 vec in dirs)
        {
            Ray ray = new Ray(transform.position, vec);
            
            RaycastHit info ;
            if (Physics.Raycast(ray, out info, Mathf.Sqrt(3)))
            {
                neighbourRs.Add(Record.record.GetRoot(info.collider.gameObject.name));

            }
            
        }
        Empty = true;
        display.updateValue(0);
    }

    public void clicked(bool direct)
    {

        block.SetActive(false);
        Uncovered = true;
        col.enabled = false;
        if(flagged)
            Record.record.flag(false, -1);
        if(Explodable && direct)
        {
            GameManager.gameManager.EndGame(false);
            Invoke("DelayedExplode", GameManager.gameManager.delay);
        }
        else if (neighbours == 0 && !Explodable)
        {
            foreach (Root root in neighbourRs)
            {
                if (root.Uncovered) continue;
                root.clicked(false);
            }
        }
    }

    public void AddBombN()
    {
        Empty = false;
        neighbours++;
        display.updateValue(neighbours);
    }

    public void Rclicked()
    {
        if(flagged)
        {
            blockRend.material.color = Color.gray;
            Record.record.flag(Explodable, -1);
        }
        else
        {
            blockRend.material.color = Color.red;
            Record.record.flag(Explodable, 1);
        }
        flagged = !flagged;
    }

    void DelayedExplode()
    {
        Record.record.ExplodeAll(transform.position, Record.record.explRad, Record.record.explForce);
    }
}
