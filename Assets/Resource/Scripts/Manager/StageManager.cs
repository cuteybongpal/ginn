using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance { get { return instance; } }

    bool PlayerIsMoving = false;

    public PlayerController Player;
    public Map Map;
    Coroutine playerMove;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Map = GetComponent<Map>();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public bool PlayerMove(Vector2Int dir, Vector2Int originPos)
    {
        if (PlayerIsMoving)
            return false;
        if (playerMove != null)
            return false;
        Tile tile = Map.GetTileByPos(originPos + dir);
        if (tile == null)
            return false;
        if (tile.TileTypes == Tile.TileType.Blocked)
            return false;


        playerMove = StartCoroutine(Move(originPos, originPos + dir));
        if (tile.TileTypes == Tile.TileType.PreCured)
            Map.FillArea();
        else
            Map.Add(tile, dir);

        return true;
    }

    IEnumerator Move(Vector2Int originPos, Vector2Int targetPos)
    {
        PlayerIsMoving = true;
        float elapsedTime = 0;
        float duration = 1f / Player.Speed;
        while (elapsedTime < duration)
        {
            Vector2 pos = Vector2.Lerp(originPos, targetPos, elapsedTime / duration);
            Player.transform.position = pos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        PlayerIsMoving = false;
        Player.transform.position = (Vector2)targetPos;
        Player.CurrentPos = targetPos;
        playerMove = null;
    }
}
