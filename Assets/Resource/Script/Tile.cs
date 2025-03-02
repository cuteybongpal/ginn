using UnityEditor;
using UnityEngine;
using static Map;

public class Tile : MonoBehaviour
{
    public Map.TileType Type;
    public bool IsCured = false;
    public bool IsCorrupted = false;
    public Vector2Int Pos;

    public GameObject CuredTile;
    public GameObject BlockedTile;
    public GameObject CorruptedTile;

    public MonsterController monster;

    private void Awake()
    {
        Pos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        transform.position = (Vector2)Pos;
        SetTileType(Type);
    }
    public void SetTileType(Map.TileType type)
    {
        Color color = Color.white;
        switch (type)
        {
            case Map.TileType.Cured:
                
                CuredTile.SetActive(true);
                BlockedTile.SetActive(false);
                CorruptedTile.SetActive(false);
                color = CuredTile.GetComponent<SpriteRenderer>().color;
                color.a = 1f;
                CuredTile.GetComponent<SpriteRenderer>().color = color;
                if (Type == TileType.Cured)
                    return;
                else if (Type  == TileType.PreCured)
                {
                    if (!IsCured)
                    {
                        StageManager.Instance.CuredAreaCount++;
                    }
                    if (IsCorrupted)
                    {
                        StageManager.Instance.CorruptedAreaCount--;
                    }
                }
                else if (Type == TileType.Corrupted)
                {
                    StageManager.Instance.CuredAreaCount++;
                    StageManager.Instance.CorruptedAreaCount--;
                }
                else if (Type == TileType.Normal)
                {
                    StageManager.Instance.CuredAreaCount++;
                }
                StageManager.Instance.Score += 10;
                IsCured = true;
                IsCorrupted = false;
                break;
            case Map.TileType.PreCured:
                CuredTile.SetActive(true);
                BlockedTile.SetActive(false);
                CorruptedTile.SetActive(false);
                color  = CuredTile.GetComponent<SpriteRenderer>().color;
                color.a = 0.5f;
                CuredTile.GetComponent<SpriteRenderer>().color = color;
                break;
            case Map.TileType.Blocked:
                CuredTile.SetActive(false);
                BlockedTile.SetActive(true);
                CorruptedTile.SetActive(false);
                break;
            case Map.TileType.Normal:
                CuredTile.SetActive(false);
                BlockedTile.SetActive(false);
                CorruptedTile.SetActive(false);
                break;
            case TileType.Corrupted:
                CuredTile.SetActive(false);
                BlockedTile.SetActive(false);
                CorruptedTile.SetActive(true);
                IsCured = false;
                IsCorrupted = true;
                if (Type == TileType.Corrupted)
                    return;
                else if (Type == TileType.Normal)
                {
                    StageManager.Instance.CorruptedAreaCount++;
                }
                else if (Type == TileType.Cured)
                {
                    StageManager.Instance.CorruptedAreaCount++;
                    StageManager.Instance.CuredAreaCount--;
                }
                break;
        }
        Type = type;
    }
    
}
