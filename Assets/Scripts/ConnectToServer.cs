using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public TMP_InputField PlayerName;
    public TMP_Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void OnClickConnect()
    {
        if(PlayerName.text.Length >= 1)
        {
            PhotonNetwork.NickName = PlayerName.text;
            buttonText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.PhotonServerSettings.AppSettings.Server = null;
            PhotonNetwork.PhotonServerSettings.AppSettings.Port = 0;
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "eu";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }

}
