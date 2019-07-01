using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float timer = 3;
    private float save;
    public GameObject barrel;
    // Start is called before the first frame update
    void Start()
    {
        float save = timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 1)
        {
            timer = 6;
            GameObject newBullet = Instantiate(barrel);
            newBullet.transform.position = transform.position;

        }
    }
}
