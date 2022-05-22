using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Main_Menue : MonoBehaviour
{
    public TextMeshProUGUI HighScore;

    void Start()
    {
        if(PlayerPrefs.GetInt("HighScore") != null)
        {
            HighScore.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SelectSkin");
    }

    public void QuitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void MultiPlayer()
    {
        SceneManager.LoadScene("ConnectLobby");
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
