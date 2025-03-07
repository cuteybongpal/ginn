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
                dd.TileTypes = Tile.TileType.Normal;
                for (int j = 0; j < PreCureTileList.Length; j++)
                {
                    PreCureTileList[j].Remove(dd);
                }
                if (dd == _tile)
                    break;
            }
        }


        bool isDuplicate = false;
        int count = 0;
        //중복 검사
        for (int i = 0; i < PreCureTileList.Length; i++)
        {
            for (int j = 0; j < PreCureTileList[i].Count; j++)
            {
                if (PreCureTileList[i][j] == tile)
                    count++;
            }
        }
        isDuplicate = count >= 2;
        if (isDuplicate)
        {
            while (true)
            {
                Tile _tile =PrecureTiles.Peek();
                if (_tile == tile)
                    break;
                _tile = PrecureTiles.Dequeue();
                _tile.TileTypes = Tile.TileType.Normal;

                for (int j = 0; j < PreCureTileList.Length; j++)
                {
                    PreCureTileList[j].Remove(_tile);
                }
                
            }
        }



        if (dir == dirs[0])
        {
            Vector2Int tilePos =  tile.Pos;
            Tile PreCuredTile = null;
            for (int i = tilePos.y + 1; i < 16; i++)
            {
                Tile _tile = GetTileByPos(new Vector2Int(tilePos.x, i));
                Tile _tile2 = GetTileByPos(new Vector2Int(tilePos.x, i + 1));
                if (_tile.TileTypes == Tile.TileType.PreCured)
                {
                    if (_tile2.TileTypes != Tile.TileType.PreCured)
                        PreCuredTile = _tile;
                    break;
                }
            }
            if (PreCuredTile == null)
                return;
            while (true)
            {
                Tile t = PrecureTiles.Dequeue();
                t.TileTypes = Tile.TileType.Normal;
                for (int j = 0; j < PreCureTileList.Length; j++)
                {
                    PreCureTileList[j].Remove(t);
                }
                if (t == PreCuredTile)
                    break;
            }
        }

        if (dir == dirs[1])
        {
            Vector2Int tilePos = tile.Pos;
            Tile PreCuredTile = null;
            for (int i = tilePos.y - 1; i >= -16; i--)
            {
                Tile _tile = GetTileByPos(new Vector2Int(tilePos.x, i));
                Tile _tile2 = GetTileByPos(new Vector2Int(tilePos.x, i - 1));
                if (_tile.TileTypes == Tile.TileType.PreCured && _tile2 != null)
                {
                    if (_tile2.TileTypes != Tile.TileType.PreCured)
                        PreCuredTile = _tile;
                    break;
                }
            }
            if (PreCuredTile == null)
                return;
            while (true)
            {
                Tile t = PrecureTiles.Dequeue();
                t.TileTypes = Tile.TileType.Normal;
                for (int j = 0; j < PreCureTileList.Length; j++)
                {
                    PreCureTileList[j].Remove(t);
                }
                if (t == PreCuredTile)
                    break;
            }
        }

        if (dir == dirs[2])
        {
            Vector2Int tilePos = tile.Pos;
            Tile PreCuredTile = null;
            for (int i = tilePos.x - 1; i >= -16; i--)
            {
                Tile _tile = GetTileByPos(new Vector2Int(i, tile.Pos.y));
                Tile _tile2 = GetTileByPos(new Vector2Int(i - 1, tile.Pos.y));
                if (_tile.TileTypes == Tile.TileType.PreCured)
                {
                    if (_tile2.TileTypes != Tile.TileType.PreCured)
                        PreCuredTile = _tile;
                    break;
                }
            }
            if (PreCuredTile == null)
                return;
            while (true)
            {
                Tile t = PrecureTiles.Dequeue();
                t.TileTypes = Tile.TileType.Normal;
                for (int j = 0; j < PreCureTileList.Length; j++)
                {
                    PreCureTileList[j].Remove(t);
                }
                if (t == PreCuredTile)
                    break;
            }
        }

        if (dir == dirs[3])
        {
            Vector2Int tilePos = tile.Pos;
            Tile PreCuredTile = null;
            for (int i = tilePos.x + 1; i < 16; i++)
            {
                Tile _tile = GetTileByPos(new Vector2Int(i, tilePos.y));
                Tile _tile2 = GetTileByPos(new Vector2Int(i + 1, tile.Pos.y));
                if (_tile.TileTypes == Tile.TileType.PreCured)
                {
                    if (_tile2.TileTypes != Tile.TileType.PreCured)
                        PreCuredTile = _tile;
                    break;
                }
            }
            if (PreCuredTile == null)
                return;
            while (true)
            {
                Tile t = PrecureTiles.Dequeue();
                t.TileTypes = Tile.TileType.Normal;
                for (int j = 0; j < PreCureTileList.Length; j++)
                {
                    PreCureTileList[j].Remove(t);
                }
                if (t == PreCuredTile)
                    break;
            }
        }

    }
    public void FillArea()
    {
        int MinX = 0;
        int MinY = 0;
        int MaxX = 0;
        int MaxY = 0;
        if (PreCureTileList[0][0].Pos.x < PreCureTileList[1][0].Pos.x)
        {
            MinX = PreCureTileList[0][0].Pos.x;
            MaxX = PreCureTileList[1][0].Pos.x;
        }
        else
        {
            MaxX = PreCureTileList[0][0].Pos.x;
            MinX = PreCureTileList[1][0].Pos.x;
        }

        if (PreCureTileList[2][0].Pos.y < PreCureTileList[3][0].Pos.y)
        {
            MaxY = PreCureTileList[3][0].Pos.y;
            MinY = PreCureTileList[2][0].Pos.y;
        }
        else
        {
            MaxY = PreCureTileList[2][0].Pos.y;
            MinY = PreCureTileList[3][0].Pos.y;
        }


        for (int y = MinY; y <= MaxY; y++)
        {
            for (int x = MinX; x <= MaxX; x++)
            {
                Tile tile = GetTileByPos(new Vector2Int(x, y));
                if (tile.TileTypes != Tile.TileType.Blocked)
                    tile.TileTypes = Tile.TileType.Cured;
            }
        }
        for (int i = 0; i <PreCureTileList.Length; i++)
        {
            PreCureTileList[i].Clear();
        }
        PrecureTiles.Clear();
    }
    public int GetArea()
    {
        int area = 0;
        for (int i = -16; i < 16; i++)
        {
            for (int j = -16; j < 16; j++)
            {
                Tile tile = GetTileByPos(new Vector2Int(j, i));
                if (tile.TileTypes == Tile.TileType.Blocked)
                    continue;
                area++;
            }
        }
        return area;
    }
}
