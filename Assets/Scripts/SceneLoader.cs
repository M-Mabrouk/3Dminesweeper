using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {



    public void changeLvl(string name)
    {
        GameManager.gameManager.changeLvl(name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void changeDiffculity(int val)
    {
        GameManager.gameManager.changeDiffculity(val);
    }

    public void Extra()
    {
        SceneManager.LoadScene("Menu");
    }
}
