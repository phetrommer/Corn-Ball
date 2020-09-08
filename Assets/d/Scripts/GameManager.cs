using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentRound = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!PlayerIsAlive() && SceneManager.GetActiveScene().buildIndex != 1)
        {
            //SceneManagerScript.Instance.GoToMainMenu();
            SceneManager.LoadScene("MainMenu");

        }
        else
        {
            return;
        }
    }

    private float searchCountdown = 1f;

    bool PlayerIsAlive()
    {
        
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                return false;
            }
        }
        return true;
    }
}
