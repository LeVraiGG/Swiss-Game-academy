using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("tuto");
    }
    public void HowToPlay()
    {
        SceneManager.LoadScene("howtoplay");
    }
    public void Quit()
    {
        Application.Quit();
    }
}

