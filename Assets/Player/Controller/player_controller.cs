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

    //SafeMode Timer variables
    float currentTime = 0f;
    public float startTime = 5f;
    bool safeMode = true;

    //fuel variables
    public float fuel = 100f;
    public float currentFuel;
    public Slider fuelSlider;

    //HP variables
    public float HP = 100f;
    public float currentHP;
    public Slider hpSlider;

    float playerSpeed;

    public CameraShake cameraShake;

    PhotonView view;


    private void Start()
    {
        flame1.SetActive(false);
        currentTime = startTime;
        currentFuel = fuel;
        currentHP = HP;
        fuelSlider = GameObject.FindGameObjectWithTag("fuelSlider").GetComponent<Slider>();
        hpSlider = GameObject.FindGameObjectWithTag("hpSlider").GetComponent<Slider>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();

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

        if(currentHP >= 0)
        {
            hpSlider.value = currentHP;
        }
        else
        {
            isDead();
        }

        if(currentFuel >= 0)
        {
            currentFuel -= 1 * Time.deltaTime;
            fuelSlider.value = currentFuel;
        }
        else
        {
            delHP(5 * Time.deltaTime);
        }


        if (!dead)
        {
            _acceleration = Input.GetAxis("Vertical");
            _steering = Input.GetAxis("Horizontal");
            flame1.SetActive(_acceleration > 0.5);
            //ThrustForward(_acceleration * AccelerationSpeed);
            //Rotate(transform, _steering * -rotationSpeed);
            if(_acceleration > 0.5 && currentFuel >= 0)
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

    public void delFuel(float fuel)
    {
        currentFuel -= fuel;
    }

    public void addHP(float Heal)
    {
        currentHP += Heal;
    }

    public void delHP(float dmg)
    {
        currentHP -= dmg;
    }

    private void FixedUpdate()
    {
        if (!dead && currentFuel >= 0)
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
            delHP(2);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Obstacle") || col.gameObject.CompareTag("Energy"))
        {
            if (!safeMode)
            {
                StartCoroutine(cameraShake.Shake(.15f, GetSpeed() / 25));
                delHP(GetSpeed());
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
