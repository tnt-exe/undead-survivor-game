using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    public GameObject uiEnemyNotice;

    float timer;
    int spawnLevel;
    int enemyLevel;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / (spawnData.Length * 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        spawnLevel = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length - 1);

        if (timer > spawnData[spawnLevel].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void LateUpdate()
    {
        if (spawnLevel != 0 && enemyLevel != spawnLevel)
        {
            enemyLevel = spawnLevel;
            StartCoroutine(EnemyNoticeRoutine());
        }
    }

    IEnumerator EnemyNoticeRoutine()
    {
        uiEnemyNotice.gameObject.SetActive(true);
        uiEnemyNotice.transform.GetChild(enemyLevel).gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(5);
        uiEnemyNotice.gameObject.SetActive(false);
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[spawnLevel]);
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
    public int mass;
    public int damage;
}