using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void OnStartButtonDown()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
    }

    public void OnRestartButtonDown()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
