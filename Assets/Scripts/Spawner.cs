using UnityEngine;
using TMPro;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn Area")]
    [SerializeField] private BoxCollider spawnArea;

    [Header("Wave Settings")]
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private int ratsPerWaveMultiplier = 5;

    [Header("UI")]
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text killText;
    [SerializeField] private TMP_Text countdownText;

    private int currentWave = 1;
    private int killsThisWave = 0;
    private int ratsToSpawn;
    private int ratsAlive;

    private void Start()
    {
        StartWave();
    }

    private void StartWave()
    {
        killsThisWave = 0;

        ratsToSpawn = currentWave * ratsPerWaveMultiplier;
        ratsAlive = ratsToSpawn;

        waveText.text = "Wave: " + currentWave;
        killText.text = "Rats: 0 / " + ratsToSpawn;
        countdownText.text = "";

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < ratsToSpawn; i++)
        {
            Vector3 spawnPos = GetRandomPointInBox();

            Instantiate(
                enemyPrefab,
                spawnPos,
                Quaternion.identity
            );

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private Vector3 GetRandomPointInBox()
    {
        Bounds bounds = spawnArea.bounds;

        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            transform.position.y,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public void EnemyDestroyed()
    {
        killsThisWave++;
        ratsAlive--;

        killText.text = "Rats: " + killsThisWave + " / " + ratsToSpawn;

        if (ratsAlive <= 0)
        {
            StartCoroutine(NextWaveCountdown());
        }
    }

    private IEnumerator NextWaveCountdown()
    {
        for (int i = 10; i > 0; i--)
        {
            countdownText.text = "Next Wave In: " + i;
            yield return new WaitForSeconds(1f);
        }

        currentWave++;
        StartWave();
    }
}