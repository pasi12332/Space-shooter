using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Photon.Pun;

public class gameControll : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI endScore;
    public TextMeshProUGUI Points;
    public int Score;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject player;
    public GameObject wholePlayer;
    public int points;



    void Start()
    {
        Score = 0;
        points = PlayerPrefs.GetInt("Points");
        UpdateScore();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameObject playerObject = GameObject.FindWithTag("Player");
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        player.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        player.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void backMenu()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("SelectSkin");
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Resume();
        
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Start");
        Destroy(wholePlayer);
    }

    public void AddScore(int newScoreValue, int newpointsvalue)
    {
        Score += newScoreValue;
        points += newpointsvalue;
        PlayerPrefs.SetInt("Points", points);
        UpdateScore();
        Debug.Log("" + newScoreValue);
    }

    public void UpdateScore()
    {
        textDisplay.text = "" + Score;
        endScore.text = "" + Score;
        Points.text = "" + PlayerPrefs.GetInt("Points");

        if (Score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }
            
    }

}
