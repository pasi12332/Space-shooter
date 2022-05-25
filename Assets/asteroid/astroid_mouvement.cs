using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class astroid_mouvement : MonoBehaviour
{
    public float maxThrust = 30f;
    public float maxTorque;

    public Rigidbody2D rb;
    public TextMeshProUGUI textDisplay;
    public int scoreValue;
    public gameControll gameController;
    public GameObject partical;
    public GameObject animation;
    public int astHP;
    public int damage;


    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 10;
        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        if(GameControllerObject != null)
        {
            gameController = GameControllerObject.GetComponent<gameControll>();
        }
        if(gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        move();
        
    }


    void move()
    {
        Vector2 thrust = new Vector2(Random.Range(4*-maxThrust, 4*maxThrust), Random.Range(4*-maxThrust, 4*maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);
        rb.AddForce(thrust);
        rb.AddTorque(torque);

        //alte version
        // Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        // float torque = Random.Range(-maxTorque, maxTorque);
        // rb.AddForce(thrust);
        // rb.AddTorque(torque);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        partical.transform.position = transform.position;
        animation.transform.position = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet1"))
        {
            damage = other.gameObject.GetComponent<Bullet>().GetDamage();
            astHP -= damage;
            if(astHP <= 0)
            {
                astdead();
            }
        }
        
    }

    public void astdead()
    {
        partical.SetActive(true);
        animation.SetActive(true);
        Destroy(gameObject);
        gameController.AddScore(scoreValue, 1);
    }
}