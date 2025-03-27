using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAndPlay : MonoBehaviour
{
    bool paused = false;
    public GameObject pauseObject;

    public void PauseGame()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0;
            pauseObject.SetActive(true);
        }
        else
        {
            paused = false;
            Time.timeScale = 1;
            pauseObject.SetActive(false);
        }
    }

}
