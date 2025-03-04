using JetBrains.Annotations;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType
    {
        Normal,
        PreCured,
        Blocked,
        Corrupted,
        Cured,
    }
    public TileType tiletype;

    public bool IsCorrupted;
    public bool IsCured;

    public GameObject CuredTile;
    public GameObject BlockedTile;
    public GameObject CorruptedTile;

    public Vector2Int Pos;

    public TileType TileTypes {
        get { return tiletype; }
        set 
        {
            tiletype = value;
            switch (tiletype)
            {
                case TileType.Cured:
                    IsCured = true;
                    IsCorrupted = false;

                    CuredTile.SetActive(true);
                    CorruptedTile.SetActive(false);
                    BlockedTile.SetActive(false);

                    Color color1 = CuredTile.GetComponent<SpriteRenderer>().color;
                    color1.a = 0.5f;
                    CuredTile.GetComponent<SpriteRenderer>().color = color1;
                    break;
                case TileType.Blocked:
                    CuredTile.SetActive(false);
                    CorruptedTile.SetActive(false);
                    BlockedTile.SetActive(true);
                    break;
                case TileType.Corrupted:
                    IsCured = false;
                    IsCorrupted = true;

                    CuredTile.SetActive(false);
                    CorruptedTile.SetActive(true);
                    BlockedTile.SetActive(false);
                    break;
                case TileType.PreCured:
                    CuredTile.SetActive(true);
                    CorruptedTile.SetActive(false);
                    BlockedTile.SetActive(false);
                    Color color2 = CuredTile.GetComponent<SpriteRenderer>().color;
                    color2.a = 0.5f;
                    CuredTile.GetComponent<SpriteRenderer>().color = color2;
                    break;
                case TileType.Normal:
                    CuredTile.SetActive(false);
                    CorruptedTile.SetActive(false);
                    BlockedTile.SetActive(false);
                    break;
            }
        }
    }
    void Awake()
    {
        transform.position = (Vector2)Vector2Int.RoundToInt((Vector2)transform.position);
        Pos = Vector2Int.RoundToInt((Vector2)transform.position);
        TileTypes = tiletype;
    }

    
    void Update()
    {
        
    }
}
