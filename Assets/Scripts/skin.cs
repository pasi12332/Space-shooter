using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class skin : MonoBehaviour
{

    public TextMeshProUGUI textpoints;

    // Start is called before the first frame update
    void Start()
    {
        textpoints.text = "Points: " + PlayerPrefs.GetInt("Points");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
