using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public GameObject Timer;
    public Text timeText;
    public Controller controller;
    public Interactor interactor;
    public enum difficulity { Easy, Normal, Hard };
    public difficulity mode = difficulity.Easy;
    public float delay;
    public float offset;
    public float startTime;
    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    public void changeDiffculity(string level)
    {
        if (level.ToLower().Equals("easy"))
        {
            mode = difficulity.Easy;
        }
        if (level.ToLower().Equals("normal"))
        {
            mode = difficulity.Normal;
        }
        if (level.ToLower().Equals("hard"))
        {
            mode = difficulity.Hard;
        }
    }
    public void changeDiffculity(int val)
    {
        if (val == 0)
        {
            mode = difficulity.Easy;
        }
        if (val == 1)
        {
            mode = difficulity.Normal;
        }
        if (val == 2)
        {
            mode = difficulity.Hard;
        }
    }
    public void EndGame(bool win)
    {
        controller.enabled = false;
        interactor.enabled = false;
        if(Timer)
        {
            Timer.SetActive(false);
        }
        if(win)
        {
            timeText.text = ((int)Time.timeSinceLevelLoad - startTime).ToString() + " seconds";
            winCanvas.SetActive(true);
        }
        else
        {
            Invoke("DelayedLose", gameManager.delay + offset);
        }
    }

    public void DelayedLose()
    {
        loseCanvas.SetActive(true);
    }
    

    public void Restart()
    {

        Record.record.Restart();
        loseCanvas.SetActive(false);
        winCanvas.SetActive(false);
    }

    public void changeLvl(string name)
    {
        Restart();
        if(name.Equals("Menu"))
            mode = difficulity.Easy;
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
