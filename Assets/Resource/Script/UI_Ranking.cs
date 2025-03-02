using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Ranking : MonoBehaviour
{
    public Text[] RankingTexts;
    public Button Quit;
    public Button PlayAgain;
    void Start()
    {
        for (int i = 0; i < RankingTexts.Length; i++)
        {
            if (GameManager.Instance.Scores.Count - 1 < i)
                break;
            RankingTexts[i].text = $"{GameManager.Instance.Scores[i]} Á¡";
        }
        Quit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        PlayAgain.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
}
