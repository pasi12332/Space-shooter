using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{
    public Vector3 offset;
    public Transform exp;
    public Transform target;
    public bool dead = false;
    public GameObject Player;


    void Start()
    {
        
    }

    void Update()
    {
        if(Player == null)
        {
            try
            {
                Player = GameObject.FindWithTag("Player");
                target = Player.transform;
            }
            catch
            {

            }
            
        }
    }

    private void FixedUpdate()
    {
        if(!dead && Player != null)
        {
            try
            {
                transform.position = target.position + offset;
            }
            catch
            {
                dead = true;
            }
        }
        
        
    }
}
