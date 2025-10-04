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
        if (Instance != null && Instance != this)
        {
            Debug.Log(gameObject);
            Destroy(gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            StartCoroutine(TimerRoutine());
            _currentTime = _maxTime;
            loadedScene = SceneManager.GetActiveScene();
            UIManager.Instance.UpdateTimer(_currentTime);

            SceneManager.activeSceneChanged += delegate
            {
                loadedScene = SceneManager.GetActiveScene();
            };

            StartCoroutine(WaitToInit());
        }
    }

    private void Update()
    {
        if (loadedScene.name != "Game")
        {
            return;
        }

        if (Player == null)
        {
            TryGetPlayer();
        }
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
            ReloadGame(false);
        }
    }

    private void TryGetPlayer()
    {
        var playerObj = GameObject.Find("Player");

        if (playerObj != null)
        {
            Player = playerObj.GetComponent<Player>();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void YouWin()
    {
        SceneManager.LoadScene("YouWin");
    }

    public void ReloadGame(bool resetLives)
    {
        SceneManager.LoadScene("Game");
        _currentTime = _maxTime;
        UIManager.Instance.UpdateTimer(_currentTime);

        if (resetLives)
        {
            _lifeCount = 3;
        }
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

    public void Exit()
    {
        UnInit();

        Application.Quit();
    }

    private IEnumerator WaitToInit()
    {
        yield return new WaitForEndOfFrame();

        Player.PlayerInput.actions["Escape"].performed += (ctx) => Exit();
        Player.PlayerInput.actions["ReloadGame"].performed += (ctx) => ReloadGame(false);
    }

    private void UnInit()
    {
        Player.PlayerInput.actions["Escape"].performed -= (ctx) => Exit();
        Player.PlayerInput.actions["ReloadGame"].performed -= (ctx) => ReloadGame(false);
    }
}
