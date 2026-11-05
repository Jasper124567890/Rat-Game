using UnityEngine;
using TMPro;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab;

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
            Instantiate(
                enemyPrefab,
                transform.position,
                Quaternion.identity
            );

            yield return new WaitForSeconds(spawnDelay);
        }
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