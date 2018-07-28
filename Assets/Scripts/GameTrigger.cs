using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameTrigger : MonoBehaviour {
    
    [Serializable]
    public class modes
    {
        public Vector3 Extent;
        public int mines;
    }
    GameObject[,,] Blocks;
    int Mines;
    Vector3 Extent;
    public float offset;
    public GameObject Block, Canvas;
    public modes Easy, Normal, Hard;
    public Transform Parent;
    // Use this for initialization
    private void Start()
    {
        GameManager.difficulity mode = GameManager.gameManager.mode;
        if(mode == GameManager.difficulity.Easy)
        {
            Extent = Easy.Extent;
            Mines = Easy.mines;
        }
        else if(mode == GameManager.difficulity.Normal)
        {
            Extent = Normal.Extent;
            Mines = Normal.mines;
        }
        else
        {
            Extent = Hard.Extent;
            Mines = Hard.mines;
        }
        Record.record.mines = Mines;
        Build(Extent, offset, Mines, Block, Parent);
    }
    public void Build(Vector3 Extent, float offset, int Mines, GameObject Block, Transform parent)
    {
        int Gname = 0;
        List<Vector3Int> idxList = new List<Vector3Int>();
        Blocks = new GameObject[(int)Extent.x, (int)Extent.y, (int)Extent.z];
        float startx = -(Extent.x-1)/2 * (1 + offset), starty = -(Extent.y - 1)/2 * (1 + offset), startz = -(Extent.z - 1)/2 * (1 + offset);
        for (int i = 0; i < Extent.x; i++)
        {
            for (int j = 0; j < Extent.y; j++)
            {
                for (int k = 0; k < Extent.z; k++)
                {
                    Blocks[i, j, k] = new GameObject(Gname.ToString());
                    Blocks[i, j, k].transform.position = new Vector3(startx + i * (1 + offset) , starty + j * (1 + offset), startz + k * (1 + offset));
                    Blocks[i, j, k].transform.parent = parent;
                    Blocks[i, j, k].layer = LayerMask.NameToLayer("Blocks");
                    Root myRoot = Blocks[i, j, k].AddComponent<Root>();
                    myRoot.block = Instantiate(Block, Blocks[i, j, k].transform.position, Quaternion.identity, Blocks[i, j, k].transform);
                    myRoot.blockRend = myRoot.block.GetComponent<Renderer>();
                    myRoot.display = Instantiate(Canvas, Blocks[i, j, k].transform.position, Quaternion.identity, Blocks[i, j, k].transform).GetComponent<Display>();
                    Record.record.AddRoot(Blocks[i, j, k].name, myRoot);
                    idxList.Add(new Vector3Int(i, j, k));
                    Gname++;
                }
            }
        }

        Record.record.AddAll();

        UnityEngine.Random.seed = System.DateTime.Now.Millisecond;
        for (int m = 0; m < Mines; m++)
        {
            int idx = UnityEngine.Random.Range(0, idxList.Count);
            int x = idxList[idx].x, y = idxList[idx].y, z = idxList[idx].z;
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    for (int k = z - 1; k < z + 2; k++)
                    {
                        if (i >= Extent.x || j >= Extent.y || k >= Extent.z) continue;
                        if (i <= -1 || j <= -1 || k <= -1) continue;
                        if (i == x && j == y && k == z) continue;
                        Record.record.GetRoot(Blocks[i, j, k].name).AddBombN();
                    }
                }
            }
            idxList.RemoveAt(idx);
            Record.record.GetRoot(Blocks[x, y, z].name).Explodable = true;
        }
        GameManager.gameManager.startTime = Time.timeSinceLevelLoad;
    }
    
}
