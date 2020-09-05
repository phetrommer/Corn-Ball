using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneManagerScript : MonoBehaviour
{
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GoToMainMenu();
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
