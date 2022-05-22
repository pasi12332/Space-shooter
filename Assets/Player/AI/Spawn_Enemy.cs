using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    public int numberToSpawn;
    public List<GameObject> spawnpool;
    public GameObject quad;
    public Transform player;
    bool respawn = true;
    public GameObject Player;
    public bool playerFound = false;
    private int waveToSpawn;
    public int nextSpawn = 0;


    void Start()
    {
        waveToSpawn = 0;
    }

    void Update()
    {
        if(nextSpawn >=5)
        {
            respawn = true;
            waveToSpawn += 1;
        }
        if (respawn)
        {
            numberToSpawn = 5;
            spawnObjects();
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

        GameObject toSpawn;
        MeshCollider c = quad.GetComponent<MeshCollider>();

        float screenx, screeny, screenz;
        Vector3 pos;

        for (int i = 0; i < numberToSpawn; i++)
        {
            toSpawn = spawnpool[waveToSpawn];

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
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Spawnable"))
        {
            Destroy(o);
        }
    }
}
