using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int numberToSpawn;
    public List<GameObject> spawnpool;
    public GameObject quad;
    public Transform player;
    float currentTime = 0f;
    public float respawnTime = 20f;
    bool respawn = true;
    public GameObject Player;
    public bool playerFound = false;


    void Start()
    {
        currentTime = respawnTime;
    }

    void Update()
    {
        if (currentTime >= 0)
        {
            currentTime -= 1 * Time.deltaTime;
        }
        else if (respawn)
        {
            numberToSpawn = 20;
            spawnObjects();
            currentTime = respawnTime;
        }

        if (Player == null && !playerFound)
        {
            Player = GameObject.FindWithTag("Player");
            player = Player.transform;
            playerFound = true;
            spawnObjects();
        }
    }

    public void spawnObjects()
    {

        int randomItem = 0;
        GameObject toSpawn;
        MeshCollider c = quad.GetComponent<MeshCollider>();

        float screenx, screeny, screenz;
        Vector3 pos;

        for(int i = 0; i < numberToSpawn; i++)
        {
            randomItem = Random.Range(0, spawnpool.Count);
            toSpawn = spawnpool[randomItem];

            screenx = Random.Range(c.bounds.min.x, c.bounds.max.x);
            screeny = Random.Range(c.bounds.min.y, c.bounds.max.y);
            screenz = -1;

            pos = new Vector3(screenx, screeny, screenz);

            try
            {
                float distanceToPlayer = Vector3.Distance(player.position, pos);
                if (distanceToPlayer < 20f)
                {
                    screenx += 30f;
                    screeny += 30f;
                    pos = new Vector3(screenx, screeny, screenz);
                }

                Instantiate(toSpawn, pos, toSpawn.transform.rotation);
            }
            catch
            {
                respawn = false;
            }
            
            
        }
    }

    private void destroyObjects()
    {
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("Spawnable"))
        {
            Destroy(o);
        }
    }

}
