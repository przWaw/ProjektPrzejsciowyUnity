using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgramMenu : MonoBehaviour
{
    public void ResetScene()
    {
        SceneManager.LoadScene("SceneCreator");
    }
    public void QuitCreator()
    {
        Application.Quit();
    }
}
