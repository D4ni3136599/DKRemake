using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    private void On(Collision collision)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
