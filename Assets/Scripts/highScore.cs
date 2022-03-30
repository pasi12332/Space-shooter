using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class highScore : MonoBehaviour
{

    public TextMeshProUGUI highscore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        highscore.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");
    }
}
