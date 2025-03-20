using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MazePuzzle : UI_Popup
{
    public Button StartMove;
    public List<UI_Block> Path = new List<UI_Block>();
    public UI_Block[,] blocks = new UI_Block[11, 11];
    Color[] colors = { Color.blue, new Color(.7f, .7f, 1) };
    public Text text;
    private void Start()
    {
        Init();
        text.gameObject.SetActive(false);
        for (int y = 0; y < blocks.GetLength(0); y++)
        {
            for (int x = 0; x < blocks.GetLength(1); x++)
            {
                
                blocks[y, x] = transform.GetChild(0).GetChild(y * 11 + x).GetComponent<UI_Block>();
                //Debug.Log($"y : {y}, blockName : {blocks[y, x]}");
            }
        }
        StartMove.onClick.AddListener(() =>
        {
            StartMove.enabled = false;
            StartCoroutine(Move());
        });
    }
    public void Remove(UI_Block block)
    {
        int index = Path.IndexOf(block);

        if (index == -1)
            return;

        for (int i = Path.Count - 1; i >= index; i--)
        {
            Path[i].type = UI_Block.BlockType.Normal;
            Path[i].GetComponent<Image>().color = Color.white;
            Path.RemoveAt(i);
        }
    }

    public void Add(UI_Block block)
    {
        if (block.type == UI_Block.BlockType.Blocked)
            return;
        if (Path.IndexOf(block) != -1)
            return;
        Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left };
        int[] index = new int[2];
        for(int y = 0;y < blocks.GetLength(0);y++)
        {
            bool isFind = false;
            for (int x = 0;x < blocks.GetLength(1); x++)
            {
                if (blocks[y, x] == block)
                {
                    index[0] = x;
                    index[1] = y;
                    isFind = true;
                    break;
                }
            }
            if (isFind)
                break;
        }
        bool isAdjacentToChoosed = false;
        for (int i = 0; i < dirs.Length; i++)
        {
            int x = dirs[i].x + index[0];
            int y = dirs[i].y + index[1];

            if (x < 0  || y < 0)
                continue;
            if (x >= 11 ||  y >= 11)
                continue;
            if (Path.Count != 0 && blocks[y, x] == Path[^1])
            {
                isAdjacentToChoosed = true;
                break;
            }
            else
            {
                if (blocks[y, x].type == UI_Block.BlockType.Player && Path.Count == 0)
                {
                    isAdjacentToChoosed = true;
                    break;
                }
                isAdjacentToChoosed = false;
            }
        }

        if (isAdjacentToChoosed)
        {
            Path.Add(block);
            if (block.type != UI_Block.BlockType.Goal)
            {
                block.type = UI_Block.BlockType.Choosed;
                block.GetComponent<Image>().color = colors[1];
            }
        }
    }

    IEnumerator Move()
    {
        UI_Block prevBlock = null;
        foreach (UI_Block block in Path)
        {
            block.GetComponent <Image>().color = colors[0];
            if (prevBlock != null)
            {
                prevBlock.type = UI_Block.BlockType.Normal;
                prevBlock.GetComponent<Image>().color = Color.white;
            }
            else
            {
                blocks[1, 1].type = UI_Block.BlockType.Normal;
                blocks[1, 1].GetComponent<Image>().color = Color.white;
            }
            if (block.type == UI_Block.BlockType.Goal)
            { 
                text.gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                Debug.Log("성공티비");
                Cage.PuzzleClear?.Invoke();
                Hide();
                break;
            }
            block.type = UI_Block.BlockType.Player;
            prevBlock = block;
            yield return new WaitForSeconds(.1f);
        }

    }
}
