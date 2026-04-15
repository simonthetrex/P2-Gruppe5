using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  
using System.Collections;

public class MainMenuButton : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadMainMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
