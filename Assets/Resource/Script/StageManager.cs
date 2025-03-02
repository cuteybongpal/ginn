using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance {  get { return instance; } }
    public PlayerController Player;
    
    public GameObject PlayerPrefab;
    public GameObject Monster1Prefab;
    public GameObject Monster2Prefab;
    public GameObject Monster3Prefab;

    public GameObject GameOverPrefab;
    public GameObject StageClearPrefab;

    public GameObject Boss;

    public int MonsterCount;
    public int ItemCount;

    bool isPlayerMoving = false;
    bool isCuring = false;
    bool isOver = false;
    

    public List<MonsterController> monsters = new List<MonsterController>();
    public List<GameObject> ItemPrefabs = new List<GameObject>();

    int curedAreaCount;
    int corruptedAreaCount;
    int score;

    public Action<int> ScoreChanged;
    public Action<int> CuredAreaCountChanged;
    public Action<int> CorruptedAreaCountChanged;
    public Action<int> PlayerHpChanged;
    public int CuredAreaCount
    {
        get { return curedAreaCount; }
        set
        {
            if (value >= (int)(StageManager.Instance.GetMap().GetAreaCount() * .8f))
            {
                value = (int)(StageManager.Instance.GetMap().GetAreaCount() * .8f);
                if (Boss == null)
                StageClear();
            }
            curedAreaCount = value;
            CuredAreaCountChanged?.Invoke(value);
        }
    }
    public int CorruptedAreaCount
    {
        get { return corruptedAreaCount; }
        set
        {
            if (value >= (int)(StageManager.Instance.GetMap().GetAreaCount() * .8f))
            {
                value = (int)(StageManager.Instance.GetMap().GetAreaCount() * .8f);
                GameOver();
            }
            CorruptedAreaCountChanged?.Invoke(value);
            corruptedAreaCount = value;
        }
    }
    public int Score
    {
        get { return score; }
        set 
        {
            score = value;
            ScoreChanged?.Invoke(value + GameManager.Instance.Score);
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    private void Start()
    {
        Time.timeScale = 1;
        Player = Instantiate(PlayerPrefab).GetComponent<PlayerController>();
        Player.transform.position = new Vector2(-15,-15);


        StartCoroutine(SpawnMonster());
        StartCoroutine(SpawnItem());
    }

    IEnumerator SpawnItem()
    {
        int count = 0;
        while ( count < ItemCount)
        {
            yield return new WaitForSeconds(1);
            int rnd = UnityEngine.Random.Range(0, ItemPrefabs.Count);
            GameObject go = Instantiate(ItemPrefabs[rnd]);
            Vector2Int Pos = Vector2Int.zero;
            while (true)
            {
                int rndX = UnityEngine.Random.Range(-16, 16);
                int rndY = UnityEngine.Random.Range(-16, 16);
                Pos = new Vector2Int(rndX, rndY);

                if (GetMap().GetTileByPos(Pos).Type == Map.TileType.Blocked)
                    continue;

                break;
            }
            go.GetComponent<Item>().controller = Player;
            go.transform.position = (Vector2)Pos;
            count++;
            
        }
    }
    IEnumerator SpawnMonster()
    {
        int spawnedMonsterCount = 0;
        while (MonsterCount > spawnedMonsterCount)
        {
            MonsterController monstercontroller = null;
            int rnd = UnityEngine.Random.Range(0, 3);

            if (rnd == 0)
            {
                monstercontroller = Instantiate(Monster1Prefab).GetComponent<MonsterController>();
            }
            else if (rnd == 1)
            {
                monstercontroller = Instantiate(Monster2Prefab).GetComponent<MonsterController>();
            }
            else if (rnd == 2)
            {
                monstercontroller = Instantiate(Monster3Prefab).GetComponent<MonsterController>();
            }
            Vector2Int RandomPos;
            while (true)
            {
                int randX = UnityEngine.Random.Range(-16, 16);
                int randY = UnityEngine.Random.Range(-16, 16);

                RandomPos = new Vector2Int(randX, randY);
                if (GetMap().GetTileByPos(RandomPos).Type != Map.TileType.Normal)
                {
                    yield return null;
                    continue;
                }
                break;
            }
            monstercontroller.transform.position = (Vector2)RandomPos;
            monstercontroller.Player = Player;
            monsters.Add(monstercontroller);
            spawnedMonsterCount++;
            yield return new WaitForSeconds(.5f);
        }
        if (Boss != null)
            StartCoroutine(ReSpawnMonster());
    }
    public Map GetMap()
    {
        return GetComponent<Map>();
    }
    public bool MovePlayer(Vector2Int dir, float speed)
    {
        Vector2Int nextPos = Player.CurrentPos + dir;
        Tile targetTile = GetMap().GetTileByPos(nextPos);

        if (isPlayerMoving)
            return false;
        if (targetTile == null)
            return false;
        if (targetTile.Type == Map.TileType.Blocked)
            return false;

        StartCoroutine(MovePlayer(Player.CurrentPos, nextPos, speed));
        CheckAndFillPreCureArea(targetTile, dir);
        return true;
    }

    void CheckAndFillPreCureArea(Tile tile, Vector2Int dir)
    {
        if (tile.Type == Map.TileType.PreCured)
        {
            if (isCuring)
            {
                GetMap().AddPreCuredArea(tile, dir);
                GetMap().FillArea();
                isCuring = false;
            }
        }
        else if (tile.Type == Map.TileType.Normal)
        {
            isCuring = true;
            tile.SetTileType(Map.TileType.PreCured);
            GetMap().AddPreCuredArea(tile, dir);
        }
        else if (tile.Type == Map.TileType.Cured)
        {
            if (isCuring)
            {
                tile.SetTileType(Map.TileType.PreCured);
                GetMap().AddPreCuredArea(tile, dir);
            }
        }
        else if (tile.Type == Map.TileType.Corrupted)
        {
            isCuring = true;
            tile.SetTileType(Map.TileType.PreCured);
            GetMap().AddPreCuredArea(tile, dir);
        }
    }
    IEnumerator MovePlayer(Vector2Int currentPos, Vector2Int TargetPos, float speed)
    {
        float elapsedTime = 0;
        float duration = 1/speed;
        isPlayerMoving = true;
        while (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;
            Player.transform.position = Vector2.Lerp(currentPos, TargetPos, elapsedTime / duration);
            yield return null;
        }
        isPlayerMoving = false;
        Player.CurrentPos = TargetPos;
    }

    IEnumerator ReSpawnMonster()
    {
        while(true)
        {
            yield return null;
            if (monsters.Count >= 4)
                continue;
            break;
        }
        Boss boss = Boss.GetComponent<Boss>();

        if (boss == null)
            yield break;

        StartCoroutine(boss.ReSpawnMonsters());
    }

    public void GameOver()
    {
        if (isOver)
            return;
        Debug.Log("게임 오버");
        Instantiate(GameOverPrefab);
        isOver = true;
        Time.timeScale = 0;
    }
    public void StageClear()
    {
        if (isOver)
            return;
        Debug.Log("게임 클리어");
        Instantiate(StageClearPrefab);
        GameManager.Instance.Score += Score;
        isOver = true;
        Time.timeScale = 0;
    }
}
