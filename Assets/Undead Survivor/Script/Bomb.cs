using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public static Bomb instance;

    [Header("Bomb")]
    public GameObject enemyCleaner;
    public GameObject bomb;

    public void SpawnBomb()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        Vector3 spawnDirection = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        Vector3 spawnPosition = Player.instance.transform.position +
            spawnDirection * 5f;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);

        bomb.transform.position = spawnPosition;
        bomb.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Farmer"))
            return;
        StartCoroutine(KillAllRoutine());
    }

    IEnumerator KillAllRoutine()
    {
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0f);
        bomb.SetActive(false);
        GameManager.instance.isSpawningBombs = false;
    }
}