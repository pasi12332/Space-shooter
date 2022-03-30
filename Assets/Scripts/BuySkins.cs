using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuySkins : MonoBehaviour
{
    public int cost;
    private int points;
    public GameObject pan;
    public TextMeshProUGUI textpoints;
    public SkinManager skinManager;
    public SpriteRenderer sr;

    void Start()
    {
        if (PlayerPrefs.HasKey("" + sr.sprite))
        {
            pan.SetActive(false);
            skinManager.skins.Add(sr.sprite);
        }

        points = PlayerPrefs.GetInt("Points");
    }

    public void OnClickBuy()
    {
        points = PlayerPrefs.GetInt("Points");
        if (cost <= points)
        {
            pan.SetActive(false);
            points -= cost;
            PlayerPrefs.SetInt("Points", points);
            textpoints.text = "Points: " + points;
            PlayerPrefs.SetInt("" + sr.sprite, 1);
            skinManager.skins.Add(sr.sprite);
        }
        
    }
}
