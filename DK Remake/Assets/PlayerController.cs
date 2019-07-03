// Preston
// 6/28

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject spawner;
    public KeyCode up = KeyCode.Space;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode climb = KeyCode.W;
    public float jumpspeed = 5;
    public float ladderspeed = 3;
    public float speed = 3;
    Rigidbody rb;
    float save;
    bool Ladder;
    GameObject collide;
    public GameObject text;

    public int score = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Death()
    {
        if (transform.position.y <= -16)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ladder" && Ladder == false && (Input.GetKey(climb) || Input.GetKey(down)))
        {
                Ladder = true;
            collide = other.gameObject;
        }
        if (other.name == "Win")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Points" && (rb.useGravity))
        {
            score += 100;
            text.gameObject.GetComponent<Text>().text = "Score:"+ score.ToString();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladder" && Ladder == true)
        {
            Ladder = false;
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
        }
    }

    bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, transform.localScale.y / 2f + 0.1f)
        || Physics.Raycast(transform.position + Vector3.right / 2f, Vector3.down, transform.localScale.y / 2f + 0.1f)
        || Physics.Raycast(transform.position + Vector3.left / 2f, Vector3.down, transform.localScale.y / 2f + 0.1f);
    }

    void ladder()
    {
        if (Ladder)
        {
             Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), true);
            rb.useGravity = false;
            float x = collide.transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            Vector3 newVel = Vector3.zero;
            newVel.y = ladderspeed;
            newVel.x = 0;
            newVel.z = 0;

            if (Input.GetKey(climb) || (Input.GetKey(down)))
            {
                if (Input.GetKey(climb))
                {
                    {
                        rb.velocity = newVel;
                        transform.position = new Vector3(x, y, z);
                    }
                }
                if (Input.GetKey(down))
                {
                    rb.velocity = -newVel;
                    transform.position = new Vector3(x, y, z);
                }
            }
            else
            {
                if (!Input.GetKey(left) && !Input.GetKey(right))
                    rb.velocity = Vector3.zero;
            }
        }
        else
        {
            rb.useGravity = true;
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
        }
    }
    void Update()
    {
        ladder();
        Death();
        Vector3 newVel = rb.velocity;
        if (Input.GetKey(left) && OnGround())
        {
            Ladder = false;
            newVel.x = -speed;
        }
        if (Input.GetKey(right) && OnGround())
        {
            Ladder = false;
            newVel.x = speed;
        }
        if (Input.GetKeyDown(up) && OnGround())
        {
            newVel.y = jumpspeed;
        }
        rb.velocity = newVel;
    }
}