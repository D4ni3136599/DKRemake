// DK Remake/Assets/Barrel.cs
// Created: 6/28/19
// Owner: Liam Murray

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    Rigidbody rb;
    public float speed = -6;
    bool CoolDown;

    // Start is called before the first frame update
    void Start()
    {
        CoolDown = true;
        rb = GetComponent<Rigidbody>();
    }
    bool OnGround()
    {
        CoolDown = true;
        return Physics.Raycast(transform.position, Vector3.down, transform.localScale.y / 2f + 0.1f)
            || Physics.Raycast(transform.position + Vector3.right / 2f, Vector3.down, transform.localScale.y / 2f + 0.1f)
            || Physics.Raycast(transform.position + Vector3.left / 2f, Vector3.down, transform.localScale.y / 2f + 0.1f);
    }
    void Death()
    {
        if (transform.position.y <= -16)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        {
            if (collision.collider.GetComponent<PlayerController>())
            {
                collision.collider.transform.position = new Vector3(0, -8, 0);
                foreach (Transform child in transform.parent)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        float dy = other.transform.position.y - transform.position.y;
        if (dy < -2.5f)
        {
            if (Random.Range(0f, 2f) >= 1)
            {
                float x = transform.position.x;
                float y = transform.position.y - 2;
                float z = transform.position.z;
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                transform.position = new Vector3(x,y,z);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        Death();
        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
        if (!CoolDown)
        {
            if (OnGround())
            {
                speed = speed * -1;
            }
        }
        if (!OnGround())
        {
            CoolDown = false;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (rb)
            Gizmos.DrawLine(transform.position, transform.position + rb.velocity);
    }
}