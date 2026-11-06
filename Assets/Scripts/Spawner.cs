using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private GameObject ratPrefab;
    [SerializeField] private GameObject ratKingPrefab;

    [Header("Spawn Area")]
    [SerializeField] private BoxCollider spawnArea;

    [Header("Day Settings")]
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private int ratsPerDayMultiplier = 5;

    [Header("Rat King")]
    [SerializeField] private int ratKingDay = 10;

    [Header("Sleep Settings")]
    [SerializeField] private Image sleepImage;
    [SerializeField] private float sleepTime = 5f;
    [SerializeField] private float morningDelay = 2f;

    [Header("Win Screen")]
    [SerializeField] private Image winImage;
    [SerializeField] private float winScreenTime = 5f;

    [Header("UI")]
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private TMP_Text killText;
    [SerializeField] private TMP_Text sleepText;

    [Header("Upgrades")]
    [SerializeField] private UpgradeManager upgradeManager;


    private int currentDay = 1;
    private int killsThisDay = 0;
    private int ratsToSpawn;
    private int ratsAlive;

    private bool dayComplete = false;
    private bool ratKingDefeated = false;

    private void Start()
    {
        if (sleepImage != null)
            sleepImage.enabled = false;

        if (winImage != null)
            winImage.enabled = false;

        if (sleepText != null)
            sleepText.gameObject.SetActive(false);

        StartCoroutine(StartDay());
    }

    private IEnumerator StartDay()
    {
        dayText.gameObject.SetActive(true);

        if (!ratKingDefeated && currentDay < ratKingDay)
        {
            int daysRemaining = ratKingDay - currentDay;

            string dayWord = daysRemaining == 1 ? "Day" : "Days";

            dayText.text =
                "DAY " + currentDay +
                "\nRat King Arrives In " +
                daysRemaining +
                " " +
                dayWord;
        }
        else if (!ratKingDefeated && currentDay == ratKingDay)
        {
            dayText.text =
                "DAY " + currentDay +
                "\nTHE RAT KING HAS ARRIVED!";
        }
        else
        {
            dayText.text = "DAY " + currentDay;
        }

        yield return new WaitForSeconds(morningDelay);

        

        BeginDay();
    }

    private void BeginDay()
    {
        dayComplete = false;

        killsThisDay = 0;

        if (currentDay == ratKingDay && !ratKingDefeated)
        {
            SpawnRatKing();
            return;
        }

        ratsToSpawn = currentDay * ratsPerDayMultiplier;
        ratsAlive = ratsToSpawn;

        killText.text = "Rats Gone: 0 / " + ratsToSpawn;

        StartCoroutine(SpawnRats());
    }

    private IEnumerator SpawnRats()
    {
        for (int i = 0; i < ratsToSpawn; i++)
        {
            Vector3 spawnPos = GetRandomPointInBox();

            Instantiate(
                ratPrefab,
                spawnPos,
                Quaternion.identity
            );

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnRatKing()
    {
        Vector3 spawnPos = GetRandomPointInBox();

        Instantiate(
            ratKingPrefab,
            spawnPos,
            Quaternion.identity
        );

        ratsAlive = 1;

        killText.text = "Rat King: Present";
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
        if (currentDay == ratKingDay && !ratKingDefeated)
        {
            ratsAlive--;

            if (ratsAlive <= 0)
            {
                ratKingDefeated = true;
                StartCoroutine(RatKingDefeatedRoutine());
            }

            return;
        }

        killsThisDay++;
        ratsAlive--;

        killText.text =
            "Rats Gone: " +
            killsThisDay +
            " / " +
            ratsToSpawn;

        if (ratsAlive <= 0)
        {
            FinishDay();
        }
    }

    private IEnumerator RatKingDefeatedRoutine()
    {
        if (winImage != null)
            winImage.enabled = true;

        yield return new WaitForSeconds(winScreenTime);

        if (winImage != null)
            winImage.enabled = false;

        FinishDay();
    }

    private void FinishDay()
    {
        dayComplete = true;

        sleepText.gameObject.SetActive(true);
        upgradeManager.GiveUpgradeToken();

        if (currentDay == ratKingDay && ratKingDefeated)
        {
            sleepText.text =
                "THE RAT KING IS DEAD...\nTIME FOR BED";
        }
        else
        {
            sleepText.text = "TIME FOR BED";
        }
    }

    public bool CanSleep()
    {
        return dayComplete;
    }

    public void StartNextDay()
    {
        if (!dayComplete)
            return;

        StartCoroutine(SleepRoutine());
    }

    private IEnumerator SleepRoutine()
    {
        dayComplete = false;

        sleepText.gameObject.SetActive(false);

        if (sleepImage != null)
            sleepImage.enabled = true;

        yield return new WaitForSeconds(sleepTime - 0.5f);

        currentDay++;

        yield return new WaitForSeconds(0.5f);

        if (sleepImage != null)
            sleepImage.enabled = false;

        StartCoroutine(StartDay());
    }
}