using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour {

    public static Record record;
    Dictionary<string, Root> Roots = new Dictionary<string, Root>();
    int flagged = 0, flaggedCorrect = 0;
    public int mines;
    public float explForce, explRad;
    public GameObject Explosion;
    private void Awake()
    {
        if (record == null)
        {
            record = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Restart()
    {
        Roots.Clear();
        flagged = 0;
        flaggedCorrect = 0;
    }


    public void AddRoot(string name, Root root)
    {
        Roots.Add(name, root);
    }

    public Root GetRoot(string name)
    {
        Root ret;
        if(Roots.TryGetValue(name,out ret))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    public void ExplodeAll(Vector3 initialPoint,float rad, float force)
    {
        Instantiate(Explosion, initialPoint,Quaternion.identity);
        foreach(Root root in Roots.Values)
        {
            root.RB.AddExplosionForce(force, initialPoint, rad);
            root.display.Disable();
        }
    }

    public void AddAll()
    {
        foreach (Root root in Roots.Values)
        {
            root.DetectNeighbours();
        }
    }
    public void flag(bool correct, int sign)
    {
        if(correct)
        {
            flaggedCorrect += sign;
        }
        flagged += sign;
        if(flagged == flaggedCorrect && flagged == mines)
        {
            GameManager.gameManager.EndGame(true);
        }
    }
}
