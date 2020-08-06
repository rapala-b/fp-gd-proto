using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageButton : MonoBehaviour
{
    public static int pressed = 0;
    public AudioClip buttonSound;

    bool done = false;

    PlayerBehavior playerBehavior;
    

    // Start is called before the first frame update
    void Start()
    {
        playerBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") & playerBehavior.activeChar == 0 & !done & (playerBehavior.chars[playerBehavior.activeChar].transform.position - transform.position).magnitude < 0.5f){
            done = true;
            pressed += 1;
            AudioSource.PlayClipAtPoint(buttonSound, transform.position);
            if (pressed == 3) {
                playerBehavior.status[1] = 0;
                playerBehavior.canSwitch = true;
                GameObject.Destroy(GameObject.FindGameObjectWithTag("Cage"));
            }
        }
    }
}
