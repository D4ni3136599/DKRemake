// DK Remake/Assets/Barrel.cs
// Created: 6/28/19
// Owner: Liam Murray

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 3;
    bool CoolDown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, transform.localScale.y / 2f + 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
        if (!OnGround())
        {
            if (CoolDown)
            {

                    speed = speed * -1;
            }
        }
        else
        {
            CoolDown = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (rb)
            Gizmos.DrawLine(transform.position, transform.position + rb.velocity);
    }
}
