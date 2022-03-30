using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EnergyScript : MonoBehaviour
{
    public float maxThrust;
    public float maxTorque;
    public Rigidbody2D rb;
    public TextMeshProUGUI textDisplay;
    public int scoreValue;
    public gameControll gameController;
    public player_controller player;
    public int fuel;
    public int numberToSpawn;
    public List<GameObject> spawnpool;
    public GameObject quad;

    public GameObject partical;
    public GameObject animation;
    public int astHP;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
        scoreValue = 10;
        fuel = 10;

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<player_controller>();
        }

        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        if (GameControllerObject != null)
        {
            gameController = GameControllerObject.GetComponent<gameControll>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        move();

    }


    void move()
    {
        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);

        rb.AddForce(thrust);
        rb.AddTorque(torque);
    }
    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet1"))
        {
            damage = other.gameObject.GetComponent<Bullet>().GetDamage();
            astHP -= damage;
            if (astHP <= 0)
            {
                astdead();
            }
        }

    }

    private void FixedUpdate()
    {
        partical.transform.position = transform.position;
        animation.transform.position = transform.position;
        quad.transform.position = transform.position;
    }

    public void CreateCrystals()
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
            pos = new Vector3(screenx, screeny, screenz);

            Instantiate(toSpawn, pos, toSpawn.transform.rotation);


        }
    }

    public void astdead()
    {
        CreateCrystals();
        partical.SetActive(true);
        animation.SetActive(true);
        Destroy(gameObject);
        gameController.AddScore(scoreValue, 0);
    }
}