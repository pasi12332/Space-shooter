using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using Photon.Pun;

public class player_controller : MonoBehaviour
{
    private float _acceleration;    // Beschleunigung
    private float _steering;        // Drehen

    public Camera cam;
    public GameObject canvas;
    public GameObject gameController;

    public float AccelerationSpeed = 3;
    public float SteeringSpeed = 3;

    private int _currentWeaponSystemIndex = 0;

    public GameObject flame1;

    public GameObject exp;

    public Rigidbody2D Rigidbody;

    public bool dead = false;

    public float radius;

    float currentTime = 0f;
    public float startTime = 5f;
    bool safeMode = true;

    public float fuel = 100f;
    public float currentFuel;
    public Slider slider;

    public float playerSpeed;

    PhotonView view;


    private void Start()
    {
        flame1.SetActive(false);
        currentTime = startTime;
        currentFuel = fuel;
        slider = GameObject.FindGameObjectWithTag("slider").GetComponent<Slider>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        StartCoroutine(CalcSpeed());
    }



    private void Update()
    {
        if(currentTime >= 0)
        {
            currentTime -= 1 * Time.deltaTime;
        }
        else
        {
            safeMode = false;
        }

        if(currentFuel >= 0)
        {
            currentFuel -= 1 * Time.deltaTime;
            slider.value = currentFuel;
        }
        else
        {
            isDead();
        }


        if (!dead)
        {
            _acceleration = Input.GetAxis("Vertical");
            _steering = Input.GetAxis("Horizontal");
            flame1.SetActive(_acceleration > 0.5);
            if(_acceleration > 0.5)
            {
                currentFuel -= 2 * Time.deltaTime;
                flame1.SetActive(true);
            }
            else
            {
                flame1.SetActive(false);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                gameObject.GetComponent<WeaponSystem>().Fire();
            }
        }
    }

    IEnumerator CalcSpeed()
    {
        bool isPlaying = true;
        while (isPlaying)
        {
            Vector3 prevPos = transform.position;

            yield return new WaitForFixedUpdate();

            playerSpeed = Mathf.RoundToInt(Vector3.Distance(transform.position, prevPos) / Time.fixedDeltaTime);
        }
    }

    public float GetSpeed()
    {
        return playerSpeed;
    }

    public void addFuel(int fuel)
    {
        currentFuel += fuel;
    }

    public void delFuel(int fuel)
    {
        currentFuel -= fuel;
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            Rigidbody.AddRelativeForce(new Vector2(0, _acceleration * AccelerationSpeed));
            Rigidbody.AddTorque(-_steering * SteeringSpeed);
        }
        exp.transform.position = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            delFuel(5);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            if (!safeMode)
            {
                isDead();
            }

        }

        if (col.gameObject.CompareTag("Energy"))
        {
            if (!safeMode)
            {
                isDead();
            }
        }
    }

    void isDead()
    {
            exp.SetActive(true);
            Destroy(gameObject);
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
            dead = true;
    }
}
