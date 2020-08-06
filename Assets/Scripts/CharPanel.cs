using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharPanel : MonoBehaviour
{
    public PlayerBehavior pb;
    public GameObject girlPanel;
    public GameObject elePanel;
    public GameObject catPanel;
    public GameObject frogPanel;

    public Color panelColor;
    public Color panelActiveColor;


    Image girlImage;
    Image eleImage;
    Image catImage;
    Image frogImage;

    Image[] imageList;
    Image[] panelList;

    // assumes PlayerBehavior's status will be a 4 length int array, where -1 means inaccessible, 0 means available, 1 means active.



    // Start is called before the first frame update
    void Start()
    {
        girlImage = girlPanel.GetComponentsInChildren<Image>()[1];
        eleImage = elePanel.GetComponentsInChildren<Image>()[1];
        catImage = catPanel.GetComponentsInChildren<Image>()[1];
        frogImage = frogPanel.GetComponentsInChildren<Image>()[1];
        imageList = new Image[]{girlImage, eleImage, catImage, frogImage};
        panelList = new Image[]{girlPanel.GetComponent<Image>(), elePanel.GetComponent<Image>(), catPanel.GetComponent<Image>(), frogPanel.GetComponent<Image>()};

        UpdatePanels();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePanels(){
        int[] status = pb.status;
        for (int i = 0; i < 4; i++)
        {
            switch (status[i]){
                case -1: imageList[i].enabled = false;
                panelList[i].enabled = false;
                break;

                case 0: panelList[i].enabled = true;
                panelList[i].color = panelColor;
                imageList[i].enabled = true;
                imageList[i].color = Color.black;
                break;

                case 1: panelList[i].enabled = true;
                panelList[i].color = panelColor;
                imageList[i].enabled = true;
                imageList[i].color = Color.white;
                break;
            }
        }

        panelList[pb.activeChar].color = panelActiveColor;

    }
}
