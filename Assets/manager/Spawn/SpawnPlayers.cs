using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{

    public List<GameObject> playerPrefabs;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public int player = 0;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        
        if(GameObject.Find(playerPrefabs[player].name))
        {
            player++;
        }

        PhotonNetwork.Instantiate(playerPrefabs[player].name, randomPosition, Quaternion.identity);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
