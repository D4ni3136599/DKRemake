﻿// Preston
// 6/28

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject spawner;
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public float jumpspeed = 5;
    public float speed = 5;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Death()
    {
        if (transform.position.y <= -16)
        {
            transform.position = new Vector3(0,-8,0);
            foreach (Transform child in spawner.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
    bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, transform.localScale.y / 2f + 0.1f);
    }
    // Update is called once per frame
    void Update()
    {
        Death();
        Vector3 newVel = rb.velocity;
        if (Input.GetKey(left))
        {
            newVel.x = -speed;
        }
        if (Input.GetKey(right))
        {
            newVel.x = speed;
        }
        if (Input.GetKeyDown(up) && OnGround())
        {
            newVel.y = jumpspeed;
        }
        rb.velocity = newVel;
    }
}
