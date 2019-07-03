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
    float timer;

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
                UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        float dy = other.transform.position.y - transform.position.y;
        if (dy < -2.5f)
        {
            if (Random.Range(0f, 4f) >= 3)
            {
                float y = transform.position.y;
                float z = transform.position.z;
                float x = transform.position.x + speed/12;
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                transform.position = new Vector3(x, y, z - 5);
                timer = 0.6f;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (timer < 0)
        {
            float y = transform.position.y;
            float z = transform.position.z;
            float x = transform.position.x;
            transform.position = new Vector3(x, y, 0);
        }
        timer -= Time.deltaTime;
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