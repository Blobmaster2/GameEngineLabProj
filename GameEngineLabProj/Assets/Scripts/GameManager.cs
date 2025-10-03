using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player Player;

    private Scene loadedScene;

    private int _currentTime;
    [SerializeField] private int _maxTime;

    [SerializeField] private int _lifeCount;
    public int LifeCount => _lifeCount;

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

            StartCoroutine(TimerRoutine());
            _currentTime = _maxTime;
            loadedScene = SceneManager.GetActiveScene();
            UIManager.Instance.UpdateTimer(_currentTime);

            SceneManager.sceneLoaded += delegate
            {
                TryGetPlayer();
            };
        }

        StartCoroutine(WaitToInit());
    }

    private void Update()
    {
        if (loadedScene.name != "Game")
        {
            return;
        }

        if (Player.transform.position.y < -30)
        {
            Player.Die();
        }

        Debug.Log(Player.transform.position);
    }

    public void RemoveLives()
    {
        _lifeCount--;

        if (_lifeCount < 0)
        {
            GameOver();
        }
        else
        {
            ReloadGame();
        }
    }

    private bool TryGetPlayer()
    {
        var playerObj = GameObject.Find("Player");

        if (playerObj != null)
        {
            Player = playerObj.GetComponent<Player>();
            return true;
        }

        return false;
    }

    private void GameOver()
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
        _currentTime = _maxTime;
        UIManager.Instance.UpdateTimer(_currentTime);

        loadedScene = SceneManager.GetActiveScene();
    }

    IEnumerator TimerRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(1);
        while (true)
        {
            yield return delay;
            _currentTime -= 1;
            UIManager.Instance.UpdateTimer(_currentTime);

            if (_currentTime < 0)
            {
                RemoveLives();
            }
        }
    }

    private void Exit()
    {
        Application.Quit();
    }

    private IEnumerator WaitToInit()
    {
        yield return new WaitForEndOfFrame();

        Player.PlayerInput.actions["Escape"].performed += (ctx) => Exit();
        Player.PlayerInput.actions["ReloadGame"].performed += (ctx) => ReloadGame();
    }
}
