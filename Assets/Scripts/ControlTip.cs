using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTip : MonoBehaviour
{
    public string message;
    // destroyKey can be none, scroll, fire1, or any keyboard key
    public string destroyKey = "none";
    public float duration = -1;
    public float range = -1;
    GameObject[] chars;
    ControlTipBox ctb;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        chars = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>().chars;
        ctb = GameObject.FindGameObjectWithTag("TipBox").GetComponent<ControlTipBox>();

    }

    // Update is called once per frame
    void Update()
    {
        if (duration != -1)
        {
            time += Time.deltaTime;
            if (time > duration)
            {
                Destroy(gameObject);
            }
        }

        if (range == -1 || Vector3.Distance(chars[PlayerBehavior.activeChar].transform.position, transform.position) < range)
        {
            ctb.SetText(Vector3.Distance(chars[PlayerBehavior.activeChar].transform.position, transform.position), message);

            if (destroyKey != "none")
            {
                if (destroyKey == "scroll")
                {
                    if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                    {
                        Destroy(gameObject);
                    }
                }
                else if (destroyKey == "fire1")
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Destroy(gameObject);
                    }
                }
                else if (Input.GetKeyDown(destroyKey))
                {
                    Destroy(gameObject);
                }
            
            }
        }
    }
}
