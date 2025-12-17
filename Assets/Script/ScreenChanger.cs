using UnityEngine;

public class ScreenChanger : MonoBehaviour
{
    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void LoadMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }

    public void LoadGameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }
}
