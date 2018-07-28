using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour {

    public Text counter;

    public void updateValue(int value)
    {
        if (value > 0)
            counter.text = value.ToString();
        else
            counter.text = "";
        counter.color = Color.Lerp(Color.blue, Color.red, value / 5f);
    }

    public void Disable()
    {
        counter.transform.parent.gameObject.SetActive(false);
    }
}
