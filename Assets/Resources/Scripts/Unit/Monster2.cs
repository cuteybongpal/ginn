using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster2 : MonsterController
{
    Vector2Int[] size = 
    {
        new Vector2Int(1,1),
        new Vector2Int(0,1),
        new Vector2Int(-1,1),
        new Vector2Int(1,0),
        new Vector2Int(0,0),
        new Vector2Int(-1,0),
        new Vector2Int(1,-1),
        new Vector2Int(0,-1),
        new Vector2Int(-1,-1),
    };
    void Update()
    {
        
    }
    protected override Vector2Int GetRandomAvailableDir()
    {
        List<Vector2Int> _dirs = dirs.ToList<Vector2Int>();


        for (int i = 0; i < _dirs.Count; i++)
        {
            Vector2Int nextPos = CurrentPos + dirs[i];
            for (int j = 0; j < size.Length; j++)
            {
                Tile _tile = StageManager.Instance.Map.GetTileByPos(nextPos + size[j]);
                if (_tile.TileTypes == Tile.TileType.Blocked)
                {
                    _dirs.Remove(dirs[i]);
                    break;
                }
            }
        }

        for(int i = 0; i < _dirs.Count; i++)
        {
            Debug.Log(_dirs[i]);
        }

        int rnd = Random.Range(0, _dirs.Count);

        return _dirs[rnd];
    }
}
