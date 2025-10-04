using UnityEngine;

public class GameEndUIHandler : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.Instance.ReloadGame(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
