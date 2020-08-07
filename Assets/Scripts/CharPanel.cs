using System.Collections;
using System.Collections.Generic;
using System;
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


    // 0 to 3, the current character selected in ui
    public int target;


    Image girlImage;
    Image eleImage;
    Image catImage;
    Image frogImage;

    Image girlBorder;
    Image eleBorder;
    Image catBorder;
    Image frogBorder;

    Image[] imageList;
    Image[] panelList;
    Image[] borderList;

    // assumes PlayerBehavior's status will be a 4 length int array, where -1 means inaccessible, 0 means available, 1 means active.



    // Start is called before the first frame update
    void Start()
    {
        target = 0;
        girlImage = girlPanel.GetComponentsInChildren<Image>()[1];
        eleImage = elePanel.GetComponentsInChildren<Image>()[1];
        catImage = catPanel.GetComponentsInChildren<Image>()[1];
        frogImage = frogPanel.GetComponentsInChildren<Image>()[1];

        girlBorder = girlPanel.GetComponentsInParent<Image>()[1];
        eleBorder = elePanel.GetComponentsInParent<Image>()[1];
        catBorder = catPanel.GetComponentsInParent<Image>()[1];
        frogBorder = frogPanel.GetComponentsInParent<Image>()[1];

        borderList = new Image[]{girlBorder, eleBorder, catBorder, frogBorder};
        imageList = new Image[]{girlImage, eleImage, catImage, frogImage};
        panelList = new Image[]{girlPanel.GetComponent<Image>(), elePanel.GetComponent<Image>(), catPanel.GetComponent<Image>(), frogPanel.GetComponent<Image>()};

        UpdatePanels();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) { switchTarget(1); }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) { switchTarget(-1); }

        UpdatePanels();
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
            if (i == target) { borderList[i].enabled = true; }
            else { borderList[i].enabled = false; }
        }

        panelList[pb.activeChar].color = panelActiveColor;
    }

    // amount to increase or decrease by
    void switchTarget(int delta)
    {
        int length = Array.IndexOf(pb.status, -1);
        if (length == -1) {length = 4;}
        target = (target + delta + length) % length;
        //UpdatePanels();
    }
}
