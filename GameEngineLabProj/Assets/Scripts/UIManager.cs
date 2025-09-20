using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _lifeCount;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UpdateLifeCount(GameManager.Instance.LifeCount);
    }

    public void UpdateTimer(int seconds)
    {
        _timerText.text = $"Time: {seconds}";
    }

    public void UpdateLifeCount(int lives)
    {
        _lifeCount.text = $"x{lives}";
    }
}
