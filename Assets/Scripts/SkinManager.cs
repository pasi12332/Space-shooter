using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class SkinManager : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> skins = new List<Sprite>();
    public List<Sprite> newskins = new List<Sprite>();
    private int selectedSkin = 0;
    public GameObject playerskin;
    public GameObject skinShop;
    


    void Start()
    {
        skinShop.SetActive(false);
        for(int i = 0; i < newskins.Count; i++)
        {
            Debug.Log("" + newskins[i]);
            if (PlayerPrefs.HasKey("" + newskins[i]))
            {
                skins.Add(newskins[i]);
                
            }
        }
    }

    public void NextOption()
    {
        
        selectedSkin = selectedSkin + 1;
        if(selectedSkin == skins.Count)
        {
            selectedSkin = 0;
        }
        sr.sprite = skins[selectedSkin];
    }

    public void BackOption()
    {
        selectedSkin = selectedSkin - 1;
        if (selectedSkin < 0)
        {
            selectedSkin = skins.Count -1;
        }
        sr.sprite = skins[selectedSkin];
    }


    public void PlayeGame()
    {
        SpriteRenderer spriteR;
        spriteR = playerskin.gameObject.GetComponent<SpriteRenderer>();
        PlayerPrefs.SetString("Skin", "" + spriteR.sprite);
        LoadLevel(3);
    }

    public void Back()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("Start");
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            yield return null;
        }
    }
}
