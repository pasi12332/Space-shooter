using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public int numberToSpawn;
    public List<GameObject> spawnpool;
    public GameObject quad;
    public Transform player;
    float currentTime = 0f;
    public float respawnTime = 20f;
    bool respawn = true;


    void Start()
    {
        if(PlayerPrefs.GetInt("Points") == null)
        {
            PlayerPrefs.SetInt("Points", 0);
        }
        spawnObjects();
    }

    void Update()
    {
       
    }

    public void spawnObjects()
    {

        int randomItem = 0;
        GameObject toSpawn;
        MeshCollider c = quad.GetComponent<MeshCollider>();

        float screenx, screeny, screenz;
        Vector3 pos;

        for (int i = 0; i < numberToSpawn; i++)
        {
            randomItem = Random.Range(0, spawnpool.Count);
            toSpawn = spawnpool[randomItem];

            screenx = Random.Range(c.bounds.min.x, c.bounds.max.x);
            screeny = Random.Range(c.bounds.min.y, c.bounds.max.y);
            screenz = -1;

            pos = new Vector3(screenx, screeny, screenz);

            Instantiate(toSpawn, pos, toSpawn.transform.rotation);

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
