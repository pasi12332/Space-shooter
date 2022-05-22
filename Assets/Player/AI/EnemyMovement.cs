using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float minimumDistance;
    public GameObject exp;

    public GameObject projectile;
    public Rigidbody2D rb;
    RigidbodyConstraints pos;
    public int scoreValue;
    public gameControll gameController;
    Spawn_Enemy spawnEnemy;


    void Start()
    {
        scoreValue = 10;
        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        if (GameControllerObject != null)
        {
            gameController = GameControllerObject.GetComponent<gameControll>();
            spawnEnemy = GameControllerObject.GetComponent<Spawn_Enemy>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        try
        {
            
            

            if (Vector2.Distance(transform.position, target.position) < 10)
            {
                gameObject.GetComponent<AiWeaponSystem>().Fire();
            }

            if (Vector2.Distance(transform.position, target.position) > minimumDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                Vector2 direction = target.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward,direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 40f * Time.deltaTime);
                //transform.up = direction;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
            }
        }
        catch
        {
            Destroy(gameObject);
        }
        

    }

    void FixedUpdate()
    {
        exp.transform.position = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet1"))
        {
            died();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            
        }
    }

    void died()
    {
        gameController.AddScore(scoreValue, 2);
        spawnEnemy.nextSpawn += 1;
        exp.SetActive(true);
        Destroy(gameObject);
    }
}