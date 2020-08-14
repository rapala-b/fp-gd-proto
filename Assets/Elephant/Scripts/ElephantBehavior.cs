using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElephantBehavior : MonoBehaviour
{
    public Transform waterSprayPoint;
    public GameObject waterParticle;
    
    public Image water1;
    public Image water2;
    public Image water3;

    float sprayCountDown = 0.5f;
    float timer = 0f;

    int shotNum = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown("e") && PlayerBehavior.activeChar == 1) {
            if(timer >= sprayCountDown && shotNum > 0) {
                GameObject water = Instantiate(waterParticle, waterSprayPoint.position, transform.rotation) as GameObject;
                Destroy(water, 3f);
                timer = 0f;
                shotNum--;
            }
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1)) {
            if(hit.transform.CompareTag("WaterSource")) {
                shotNum = 3;
            }
        }
            

        timer += Time.deltaTime;

        HandleWaterUI();
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("Box")) {
            Destroy(hit.gameObject);
        }
    }

    void HandleWaterUI()
    {
        if (PlayerBehavior.activeChar != 1)
        {
            water1.enabled = false;
            water2.enabled = false;
            water3.enabled = false;
        }
        else
        {
            water1.enabled = true;
            water2.enabled = true;
            water3.enabled = true;
            switch(shotNum)
            {
                case 0: water1.color = Color.black;
                water2.color = Color.black;
                water3.color = Color.black;
                break;

                case 1: water1.color = Color.white;
                water2.color = Color.black;
                water3.color = Color.black;
                break;

                case 2: water1.color = Color.white;
                water2.color = Color.white;
                water3.color = Color.black;
                break;

                case 3: water1.color = Color.white;
                water2.color = Color.white;
                water3.color = Color.white;
                break;

                
                

            }
        }
    }

}
