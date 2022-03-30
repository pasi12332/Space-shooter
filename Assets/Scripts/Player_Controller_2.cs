using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using Photon.Pun;

public class Player_Controller_2 : MonoBehaviour
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

    public GameObject deadUI;

    public bool dead = false;

    public float radius;

    float currentTime = 0f;
    public float startTime = 5f;
    bool safeMode = true;

    public float fuel = 100f;
    private float currentFuel;
    public Slider slider;

    PhotonView view;


    private void Start()
    {
        flame1.SetActive(false);
        currentTime = startTime;
        currentFuel = fuel;
        view = GetComponent<PhotonView>();
        if (!view.IsMine)
        {
        }
    }

    private void Update()
    {
        if (currentTime >= 0)
        {
            currentTime -= 1 * Time.deltaTime;
        }
        else
        {
            safeMode = false;
        }

        if (currentFuel >= 0)
        {
            currentFuel -= 1 * Time.deltaTime;
            slider.value = currentFuel;
        }
        else
        {
            isDead();
        }


        if (!dead && view.IsMine)
        {
            _acceleration = Input.GetAxis("Vertical");
            _steering = Input.GetAxis("Horizontal");
            flame1.SetActive(_acceleration > 0.5);
            if (_acceleration > 0.5)
            {
                currentFuel -= 2 * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                gameObject.GetComponent<WeaponSystem>().Fire();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            Rigidbody.AddRelativeForce(new Vector2(0, _acceleration * AccelerationSpeed));
            Rigidbody.AddTorque(-_steering * SteeringSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet1"))
        {
            if (view.IsMine)
            {
                isDead();
            }

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
    }

    void isDead()
    {
        if (view.IsMine)
        {
            exp.SetActive(true);
            Destroy(gameObject);
            deadUI.SetActive(true);
            dead = true;
        }
    }
}

