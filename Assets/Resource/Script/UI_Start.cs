using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Start : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
