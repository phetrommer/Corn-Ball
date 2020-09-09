using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript Instance { get; private set; }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GoToMainMenu();
        }
    }

    public void GoToMainMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Debug.Log("Already in main menu!");
        }
    }
}
