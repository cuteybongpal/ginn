using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using JetBrains.Annotations;
using System;
using System.IO;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            ScoreList = Load();
            if (ScoreList == null)
                ScoreList = new List<int>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int Score;

    public int CurrentStage = 1; 


    public List<int> ScoreList = new List<int>();


    public void SettleScore()
    {
        ScoreList.Add(Score);
        Score = 0;


        ScoreList.Sort();
        ScoreList.Reverse();
    }

    public void Save()
    {
        ScoreData scoreData = new ScoreData();
        scoreData.ScoreList = ScoreList;
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ScoreData));
        using (StreamWriter writer = new StreamWriter("Assets\\Resources\\Data\\scoreData.xml"))
        {
            xmlSerializer.Serialize(writer, scoreData);
        }
    }
    public List<int> Load()
    {
        ScoreData scoreData = new ScoreData();
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ScoreData));
        try
        {
            using (StreamReader reader = new StreamReader("Assets\\Resources\\Data\\scoreData.xml"))
            {
                scoreData = (ScoreData)xmlSerializer.Deserialize(reader);
            }
        }
        catch (Exception e)
        {
            return null;
        }
        return scoreData.ScoreList;
    }
}
public class ScoreData
{
    public List<int> ScoreList;

    public ScoreData()
    {

    }
}