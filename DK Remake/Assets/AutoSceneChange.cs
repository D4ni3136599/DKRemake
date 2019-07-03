using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneChange : MonoBehaviour
{
    public float delay = 2f;
    public string nextScene = "Gameplay";

    private void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
