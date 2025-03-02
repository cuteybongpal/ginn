using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

    int currentStage = 1;
    public int CurrentStage
    {
        get { return currentStage; }
        set
        {
            currentStage = value;
            Debug.Log(value);
        }
    }
    public int Score;
    public List<int> Scores = new List<int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        
    }
    public void GameClear()
    {
        Scores.Add(Score);
        Score = 0;
        Scores.Sort();
        Scores.Reverse();
        CurrentStage = 1;
        SceneManager.LoadScene(2);
    }
}
