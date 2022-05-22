using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerSingle : MonoBehaviour
{
    private GameObject Player;
    public GameObject[] playerPrefabs;

    public GameObject PlayerHolder;

    private SpriteRenderer spriteR;
    Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector2(0, 0);
        for (int i = 0; i < playerPrefabs.Length; i++)
        {
            PlayerHolder = playerPrefabs[i].gameObject;
            Player = PlayerHolder.transform.GetChild(0).gameObject;
            spriteR = Player.gameObject.GetComponent<SpriteRenderer>();
            Debug.Log("" + Player);
            Debug.Log("" + spriteR.sprite + " and " + PlayerPrefs.GetString("Skin"));
            if ("" + spriteR.sprite == PlayerPrefs.GetString("Skin"))
            {
                Instantiate(playerPrefabs[i], pos, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
