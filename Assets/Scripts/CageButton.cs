using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageButton : MonoBehaviour
{
    public static int cageButtonsPressed = 0;
    public static int cageButtonCount = 3;
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

        if (Input.GetKeyDown("e") & PlayerBehavior.activeChar == 0 & !done & (playerBehavior.chars[PlayerBehavior.activeChar].transform.position - transform.position).magnitude < 0.5f){
            done = true;
            cageButtonsPressed += 1;
            AudioSource.PlayClipAtPoint(buttonSound, transform.position);
            if (cageButtonsPressed == cageButtonCount) {
                playerBehavior.status[1] = 1;
                GameObject.Destroy(GameObject.FindGameObjectWithTag("Cage"));
            }
        }
    }
}
