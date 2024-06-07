using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Assets/StarterAssets/FirstPersonController/Scenes/Playground.unity");
    }

    public void Credit() 
    {
        SceneManager.LoadScene("Menu/Scenes/Credit");
    }

    public void About()
    {
        SceneManager.LoadScene("Menu/Scenes/About");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
