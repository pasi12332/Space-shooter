using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float Speed;
    public int Damage;
    public player_controller player;
    float playerSpeed = 0;
    public bool enemy;

    float maxSpeed = 15f;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<player_controller>();
        }

        playerSpeed = player.GetSpeed();
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        maxSpeed = Speed;
        playerSpeed /= 2;
        Speed += playerSpeed;

        
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);

        pos += transform.rotation * velocity;

        transform.position = pos;

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Energy"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Player") && enemy)
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Player3"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Player4"))
        {
            Destroy(gameObject);
        }
    }

    public int GetDamage()
    {
        return Damage;
    }
}
