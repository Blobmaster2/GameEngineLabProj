using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player Player;

    private Scene loadedScene;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (loadedScene.name != "Game")
        {
            return;
        }

        if (Player.transform.position.y < -30)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
        Time.timeScale = 0;

        loadedScene = SceneManager.GetActiveScene();
    }

    public void YouWin()
    {
        SceneManager.LoadScene("YouWin");
        Time.timeScale = 0;

        loadedScene = SceneManager.GetActiveScene();
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;

        loadedScene = SceneManager.GetActiveScene();
    }
}
