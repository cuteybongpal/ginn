using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Block : MonoBehaviour
{
    public enum BlockType
    {
        Blocked,
        Choosed,
        Normal,
        Player,
        Goal
    }
    public BlockType type;
    Button button;
    
    private void Start()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(() =>
        {
            SetType(BlockType.Choosed);
        });
    }

    public void SetType(BlockType type)
    {
        if (type == BlockType.Choosed)
        {
            if (this.type == BlockType.Blocked)
                return;
            else if (this.type == BlockType.Normal)
            {
                UI_MazePuzzle puzzle = UIManager.Instance.GetCurrentPopup as UI_MazePuzzle;
                puzzle.Add(this);
            }
            else if (this.type == BlockType.Choosed)
            {
                UI_MazePuzzle puzzle = UIManager.Instance.GetCurrentPopup as UI_MazePuzzle;
                puzzle.Remove(this);
            }
            else
            {
                UI_MazePuzzle puzzle = UIManager.Instance.GetCurrentPopup as UI_MazePuzzle;
                puzzle.Add(this);
            }
        }
    }
}
