using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    public Tile[] Tiles;

    Vector2Int[] dirs =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    public Queue<Tile> PrecureTiles = new Queue<Tile>();
    public List<Tile>[] PreCureTileList =
    {
        // 0 : 위
        new List<Tile>(),

        // 1 : 아래
        new List<Tile>(),

        // 2 : 왼쪽
        new List<Tile>(),

        // 3 : 오른쪽
        new List<Tile>()
    };

    private void Awake()
    {
        Tiles = new Tile[transform.childCount];
        for (int i = 0; i < Tiles.Length; i++)
        {
            Tiles[i] = transform.GetChild(i).GetComponent<Tile>();
        }
    }

    private void Start()
    {
        for (int y = -16; y < 16; y++)
        {
            for (int x = -16; x < 16; x++)
            {
                Tile tile = GetTileByPos(new Vector2Int(x, y));

                if (y == -16 || y == 15)
                {
                    tile.TileTypes = Tile.TileType.Blocked;
                    continue;
                }
                if (x == -16 || x == 15)
                {
                    tile.TileTypes = Tile.TileType.Blocked;
                    continue;
                }
            }
        }
    }
    public Tile GetTileByPos(Vector2Int pos)
    {
        for (int i = 0; i < Tiles.Length; i++)
        {
            if (Tiles[i].Pos == pos)
                return Tiles[i];
        }
        return null;
    }
    public void Add(Tile tile, Vector2Int dir)
    {
        PrecureTiles.Enqueue(tile);
        tile.TileTypes = Tile.TileType.PreCured;
        for (int i = 0; i < PreCureTileList.Length; i++)
        {
            if (dirs[i] != dir)
                continue;

            PreCureTileList[i].Add(tile);


            if (PreCureTileList[i].Count <= 1)
                break;

            Tile _tile = null;
            if (dir == Vector2Int.up || dir == Vector2Int.down)
            {
                if (PreCureTileList[i][^2].Pos.x != PreCureTileList[i][^1].Pos.x)
                {
                    _tile = PreCureTileList[i][^2];
                }
            }
            else
            {
                if (PreCureTileList[i][^2].Pos.y != PreCureTileList[i][^1].Pos.y)
                {
                    _tile = PreCureTileList[i][^2];
                }
            }
            if (_tile == null)
                break;
            while (true)
            {
                Tile dd = PrecureTiles.Dequeue();
                Debug.Log(dd);
                dd.TileTypes = Tile.TileType.Normal;
                for (int j = 0; j < PreCureTileList.Length; j++)
                {
                    PreCureTileList[j].Remove(dd);
                }
                if (dd == _tile)
                    break;
            }
        }
    }
    public void FillArea()
    {
        if (PreCureTileList)
    }
}
