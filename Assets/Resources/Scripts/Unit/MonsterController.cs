using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class MonsterController : MonoBehaviour
{
    public Vector2Int[] dirs = new Vector2Int[4]
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right,
    };

    public Vector2Int CurrentPos = new Vector2Int();

    public float Speed;
    void Start()
    {
        CurrentPos = Vector2Int.RoundToInt((Vector2)transform.position);
        StartCoroutine(CommandMove());
    }


    IEnumerator CommandMove()
    {
        Vector2Int dir  = GetRandomAvailableDir();
        yield return Move(dir);
    }

    IEnumerator Move(Vector2Int dir)
    {
        float duration = 1 / Speed;
        float elapsedTime = 0;

        while (elapsedTime <= duration)
        {
            Vector2 pos = Vector2.Lerp(CurrentPos, CurrentPos + dir, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            transform.position = pos;
            yield return null;
        }
        CurrentPos += dir;
        SetPos(CurrentPos);
        StartCoroutine(CommandMove());
    }
    public void Die()
    {
        Destroy(this);
    }
    protected virtual Vector2Int GetRandomAvailableDir()
    {
        List<Vector2Int> _dirs = dirs.ToList<Vector2Int>();


        for (int i = 0; i < _dirs.Count; i++)
        {
            Vector2Int nextPos = CurrentPos + dirs[i];
            Tile _tile = StageManager.Instance.Map.GetTileByPos(nextPos);
            if (_tile.TileTypes == Tile.TileType.Blocked)
            {
                _dirs.Remove(dirs[i]);
            }
        }
        int rnd = Random.Range(0, _dirs.Count);

        return _dirs[rnd];

    }
    protected virtual void SetPos(Vector2Int pos)
    {
        CurrentPos = pos;
    }
}
