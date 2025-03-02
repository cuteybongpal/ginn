using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Map : MonoBehaviour
{
    public enum TileType
    {
        Normal,
        Cured,
        PreCured,
        Corrupted,
        Blocked
    }
    Queue<Tile> tileQueue = new Queue<Tile>();
    public Tile[] Tiles;
    List<Tile>[] preCuredArea = new List<Tile>[4]
    {
        //위
        new List<Tile>(),
        //아래
        new List<Tile>(),
        //왼쪽
        new List<Tile>(),
        //오른쪽
        new List<Tile>()
    };
    public Vector2Int[] dirs = 
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    Vector2 prevDir;
    List<Tile> FillPreCuredArea = new List<Tile>();
    public Tile GetTileByPos(Vector2Int pos)
    {
        foreach (Tile tile in Tiles)
        {
            if (tile.Pos == pos)
                return tile;
        }
        return null;
    }

    public int GetAreaCount()
    {
        int count = 0;
        foreach (Tile tile in Tiles)
        {
            if (tile.Type != TileType.Blocked)
                count++;
        }
        return count;
    }

    public void AddPreCuredArea(Tile tile, Vector2Int dir)
    {

        tileQueue.Enqueue(tile);
        for (int k = 0; k < dirs.Length; k++)
        {
            if (dir != dirs[k])
                continue;
            preCuredArea[k].Add(tile);

            if (preCuredArea[k].Count <= 1)
                break;
            if (dirs[k] == Vector2Int.up || dirs[k] ==  Vector2Int.down )
            {
                if (preCuredArea[k][^2].Pos.x == preCuredArea[k][^1].Pos.x)
                    break;
            }
            else if (dirs[k] == Vector2Int.left || dirs[k] == Vector2Int.right)
            {
                if (preCuredArea[k][^2].Pos.y == preCuredArea[k][^1].Pos.y)
                    break;
            }
            Tile lastTile = preCuredArea[k][^2];
            while (true)
            {
                Tile t  = tileQueue.Dequeue();
                for (int i = 0; i < preCuredArea.Length; i++)
                {
                    if (preCuredArea[i].Remove(t))
                    {
                        if (!t.IsCured && !t.IsCorrupted)
                            t.SetTileType(TileType.Normal);
                        else if (t.IsCorrupted)
                            t.SetTileType(TileType.Corrupted);
                        else if (t.IsCured)
                            t.SetTileType(TileType.Cured);
                    }
                }
                if (t == lastTile)
                    break;
            }
            break;
        }
        if (dir == Vector2Int.up)
        {
            if (prevDir == Vector2Int.right)
            {
                if (preCuredArea[3].Count < preCuredArea[2].Count)
                {
                    Tile t = GetTileByPos(new Vector2Int(tile.Pos.x, tile.Pos.y + preCuredArea[1].Count - 1));
                    while (true)
                    {
                        Tile tt = tileQueue.Peek();
                        if (tt == t)
                            break;
                        tt = tileQueue.Dequeue();
                        for (int i = 0; i < preCuredArea.Length; i++)
                        {
                            if (preCuredArea[i].Remove(tt))
                            {
                                if (!tt.IsCured && !tt.IsCorrupted)
                                    tt.SetTileType(TileType.Normal);
                                else if (tt.IsCorrupted)
                                    tt.SetTileType(TileType.Corrupted);
                                else if (tt.IsCured)
                                    tt.SetTileType(TileType.Cured);
                            }
                        }

                    }
                }
                
            }
            else if (prevDir == Vector2Int.left)
            {
                if (preCuredArea[2].Count < preCuredArea[3].Count)
                {
                    Tile t = GetTileByPos(new Vector2Int(tile.Pos.x, tile.Pos.y + preCuredArea[1].Count - 1));
                    while (true)
                    {
                        Tile tt = tileQueue.Peek();
                        if (tt == t)
                            break;
                        tt = tileQueue.Dequeue();
                        for (int i = 0; i < preCuredArea.Length; i++)
                        {
                            if (preCuredArea[i].Remove(tt))
                            {
                                if (!tt.IsCured && !tt.IsCorrupted)
                                    tt.SetTileType(TileType.Normal);
                                else if (tt.IsCorrupted)
                                    tt.SetTileType(TileType.Corrupted);
                                else if (tt.IsCured)
                                    tt.SetTileType(TileType.Cured);
                            }
                        }
                    }
                }
            }
        }
        if (dir == Vector2Int.down)
        {
            if (prevDir == Vector2Int.right)
            {
                if (preCuredArea[3].Count < preCuredArea[2].Count)
                {
                    Tile t = GetTileByPos(new Vector2Int(tile.Pos.x, tile.Pos.y - preCuredArea[0].Count + 1));
                    while (true)
                    {
                        Tile tt = tileQueue.Peek();
                        if (tt == t)
                            break;
                        tt = tileQueue.Dequeue();
                        for (int i = 0; i < preCuredArea.Length; i++)
                        {
                            if (preCuredArea[i].Remove(tt))
                            {
                                if (!tt.IsCured && !tt.IsCorrupted)
                                    tt.SetTileType(TileType.Normal);
                                else if (tt.IsCorrupted)
                                    tt.SetTileType(TileType.Corrupted);
                                else if (tt.IsCured)
                                    tt.SetTileType(TileType.Cured);
                            }
                        }
                    }
                }

            }
            else if (prevDir == Vector2Int.left)
            {
                if (preCuredArea[2].Count < preCuredArea[3].Count)
                {
                    Tile t = GetTileByPos(new Vector2Int(tile.Pos.x, tile.Pos.y - preCuredArea[0].Count + 1));
                    
                    while (true)
                    {
                        Tile tt = tileQueue.Peek();
                        if (tt == t)
                            break;
                        tt = tileQueue.Dequeue();
                        for (int i = 0; i < preCuredArea.Length; i++)
                        {
                            if (preCuredArea[i].Remove(tt))
                            {
                                if (!tt.IsCured && !tt.IsCorrupted)
                                    tt.SetTileType(TileType.Normal);
                                else if (tt.IsCorrupted)
                                    tt.SetTileType(TileType.Corrupted);
                                else if (tt.IsCured)
                                    tt.SetTileType(TileType.Cured);
                            }
                        }
                    }
                }
            }
        }
        if (dir == Vector2Int.left)
        {
            if (prevDir == Vector2Int.up)
            {
                if (preCuredArea[0].Count < preCuredArea[1].Count)
                {
                    Tile t = GetTileByPos(new Vector2Int(tile.Pos.x - preCuredArea[3].Count + 1, tile.Pos.y));
                    while (true)
                    {
                        Tile tt = tileQueue.Peek();
                        if (tt == t)
                            break;
                        tt = tileQueue.Dequeue();
                        for (int i = 0; i < preCuredArea.Length; i++)
                        {
                            if (preCuredArea[i].Remove(tt))
                            {
                                if (!tt.IsCured && !tt.IsCorrupted)
                                    tt.SetTileType(TileType.Normal);
                                else if (tt.IsCorrupted)
                                    tt.SetTileType(TileType.Corrupted);
                                else if (tt.IsCured)
                                    tt.SetTileType(TileType.Cured);
                            }
                        }
                    }
                }

            }
            else if (prevDir == Vector2Int.down)
            {
                if (preCuredArea[1].Count < preCuredArea[0].Count)
                {
                    Tile t = GetTileByPos(new Vector2Int(tile.Pos.x - preCuredArea[3].Count + 1, tile.Pos.y));
                    while (true)
                    {
                        Tile tt = tileQueue.Peek();
                        if (tt == t)
                            break;
                        tt = tileQueue.Dequeue();
                        for (int i = 0; i < preCuredArea.Length; i++)
                        {
                            if (preCuredArea[i].Remove(tt))
                            {
                                if (!tt.IsCured && !tt.IsCorrupted)
                                    tt.SetTileType(TileType.Normal);
                                else if (tt.IsCorrupted)
                                    tt.SetTileType(TileType.Corrupted);
                                else if (tt.IsCured)
                                    tt.SetTileType(TileType.Cured);
                            }
                        }
                    }
                }
            }
        }
        if (dir == Vector2Int.right)
        {
            if (prevDir == Vector2Int.up)
            {
                if (preCuredArea[0].Count < preCuredArea[1].Count)
                {
                    Tile t = GetTileByPos(new Vector2Int(tile.Pos.x + preCuredArea[2].Count - 1, tile.Pos.y));
                    while (true)
                    {
                        Tile tt = tileQueue.Peek();
                        if (tt == t)
                            break;
                        tt = tileQueue.Dequeue();
                        for (int i = 0; i < preCuredArea.Length; i++)
                        {
                            if (preCuredArea[i].Remove(tt))
                            {
                                if (!tt.IsCured && !tt.IsCorrupted)
                                    tt.SetTileType(TileType.Normal);
                                else if (tt.IsCorrupted)
                                    tt.SetTileType(TileType.Corrupted);
                                else if (tt.IsCured)
                                    tt.SetTileType(TileType.Cured);
                            }
                        }
                    }
                }

            }
            else if (prevDir == Vector2Int.down)
            {
                if (preCuredArea[1].Count < preCuredArea[0].Count)
                {
                    Tile t = GetTileByPos(new Vector2Int(tile.Pos.x + preCuredArea[2].Count - 1,tile.Pos.y));
                    while (true)
                    {
                        Tile tt = tileQueue.Peek();
                        if (tt == t)
                            break;
                        tt = tileQueue.Dequeue();
                        for (int i = 0; i < preCuredArea.Length; i++)
                        {
                            if (preCuredArea[i].Remove(tt))
                            {
                                if (!tt.IsCured && !tt.IsCorrupted)
                                    tt.SetTileType(TileType.Normal);
                                else if (tt.IsCorrupted)
                                    tt.SetTileType(TileType.Corrupted);
                                else if (tt.IsCured)
                                    tt.SetTileType(TileType.Cured);
                            }
                        }
                    }
                }
            }
        }
        prevDir = dir;

        int count = 0;
        for (int i = 0; i < preCuredArea.Length; i++)
        {
            for (int j = 0; j < preCuredArea[i].Count; j++)
            {
                if (preCuredArea[i][j] == tile)
                    count++;
                if (count >= 2)
                {
                    preCuredArea[i].RemoveAt(j);
                    count = 0;
                    break;
                }
            }
        }
    }
    public bool isExist(Vector2Int pos)
    {
        bool isExistWall = false;
        int xpos;
        for (xpos = pos.x + 1; xpos < 16; xpos++)
        {
            if (GetTileByPos(new Vector2Int(xpos, pos.y)).Type == TileType.Cured)
            {
                isExistWall = true;
                break;
            }
        }

        if (Mathf.Abs(xpos - pos.x) <= 1)
            isExistWall = false;

        return isExistWall;
    }
    public void FillArea()
    {
        for (int y = -16; y < 16; y++)
        {
            Tile prevtile = null;
            bool isInner = false;
            for(int x = -16; x < 16; x++)
            {
                Tile tile = GetTileByPos(new Vector2Int(x, y));
                if (tile.Type == TileType.Normal || tile.Type == TileType.Corrupted)
                {
                    
                    if (isInner)
                        FillPreCuredArea.Add(tile);
                    else
                    {
                        prevtile = tile;
                        continue;   
                    }
                    prevtile = tile;
                }
                //if (tile.Type == TileType.Cured || tile.Type == TileType.Blocked)
                //{
                //    prevTileType = tile.Type;
                //    continue;
                //}
                
                for (int i =0; i < preCuredArea.Length; i++)
                {
                    for (int j = 0; j < preCuredArea[i].Count; j++)
                    {
                        if (tile == preCuredArea[i][j])
                        {
                            if (i == 2 || i == 3)
                            {
                                isInner = false;
                                break;
                            }
                            else if (prevtile.Type == TileType.PreCured)
                            {
                                isInner = false;
                                break;
                            }
                            isInner = !isInner;
                            break;
                        }
                    }
                }
                prevtile = tile;
            }
        }

        for (int i = 0; i < preCuredArea.Length; i++)
        {
            for (int j = 0; j < preCuredArea[i].Count; j++)
            {
                preCuredArea[i][j].SetTileType(TileType.Cured);
                preCuredArea[i][j].monster?.Damaged(100);
            }
        }
        preCuredArea[0].Clear();
        preCuredArea[1].Clear();
        preCuredArea[2].Clear();
        preCuredArea[3].Clear();

        for (int i = 0; i < FillPreCuredArea.Count; i++)
        {
            FillPreCuredArea[i].SetTileType(TileType.Cured);
            if (FillPreCuredArea[i].monster != null)
            {
                FillPreCuredArea[i].monster.Damaged(100);
                FillPreCuredArea[i].monster = null;
            }
        }
        FillPreCuredArea.Clear();
        tileQueue.Clear();
        
    }

    private void Start()
    {
        Tiles = transform.GetComponentsInChildren<Tile>();
        for (int y = -16; y < 16; y++)
        {
            for (int x = -16; x < 16; x++)
            {
                if (x == -16 || x == 15 || y == -16 || y == 15)
                    GetTileByPos(new Vector2Int(x, y)).SetTileType(TileType.Blocked);
            }
        }
    }
}
