using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsMenuBehavior : MonoBehaviour
{

    public TMPro.TMP_Text candyOne;
    public TMPro.TMP_Text candyTwo;
    public TMPro.TMP_Text candyThree;
    public TMPro.TMP_Text candyTotal;


    // Start is called before the first frame update
    void Start()
    {
        candyOne.text = "Level 1 Candy: " + PlayerPrefs.GetInt("levelOneCandyFound").ToString() + "/"
           + PlayerPrefs.GetInt("levelOneCandy").ToString();
        candyTwo.text = "Level 2 Candy: " + PlayerPrefs.GetInt("levelTwoCandyFound").ToString() + "/"
           + PlayerPrefs.GetInt("levelTwoCandy").ToString();
        candyThree.text = "Level 3 Candy: " + PlayerPrefs.GetInt("levelThreeCandyFound").ToString() + "/"
           + PlayerPrefs.GetInt("levelThreeCandy").ToString();
        candyTotal.text = "Total Candy: " + PlayerPrefs.GetInt("levelOneCandyFound").ToString() + "/"
           + (PlayerPrefs.GetInt("levelOneCandy") + PlayerPrefs.GetInt("levelTwoCandy") + PlayerPrefs.GetInt("levelThreeCandy")).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
