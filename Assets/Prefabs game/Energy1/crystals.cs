using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystals : MonoBehaviour
{
    int fuel = 5;
    public float maxThrust;
    public float maxTorque;
    public Rigidbody2D rb;
    public player_controller player;

    void Start()
    {

        Destroy(gameObject, 15f);

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<player_controller>();
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
        if (other.gameObject.CompareTag("Player"))
        {
            player.addFuel(fuel);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
