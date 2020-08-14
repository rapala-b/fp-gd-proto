using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTipBox : MonoBehaviour
{
    public float fadeTime = 1;
    float bestRange;
    Text tipBox;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        tipBox = gameObject.GetComponent<Text>();
        bestRange = 10000;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > fadeTime)
        {
            tipBox.enabled = false;
            bestRange = 10000;
        }
        else
        {
            time += Time.deltaTime;
            bestRange += Time.deltaTime;
        }
        
    }

    public void SetText(float range, string message)
    {
        if(range < bestRange)
        {
            tipBox.enabled = true;
            bestRange = range;
            tipBox.text = message;
        }
    }
}
