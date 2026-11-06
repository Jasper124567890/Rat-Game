using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BarricadeDamageZone : MonoBehaviour
{
    [SerializeField] private Window window;
    [SerializeField] private float damagePerEnemyPerSecond = 5f;

    [SerializeField] private TMP_Text barricadeText;

    private float damageMultiplier = 1f;

    private List<Collider> enemies = new List<Collider>();

    private void Start()
    {
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rat"))
        {
            if (!enemies.Contains(other))
            {
                enemies.Add(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rat"))
        {
            enemies.Remove(other);
        }
    }

    private void Update()
    {
        enemies.RemoveAll(enemy => enemy == null);

        if (window == null || enemies.Count == 0)
            return;

        float damage =
            enemies.Count *
            damagePerEnemyPerSecond *
            damageMultiplier *
            Time.deltaTime;

        window.TakeDamage(damage);
    }

    public void UpgradeBarricadeStrength()
    {
        damageMultiplier *= 0.9f;

        UpdateUI();

        Debug.Log(
            "Damage Multiplier: " +
            damageMultiplier
        );
    }

    private void UpdateUI()
    {
        if (barricadeText != null)
        {
            barricadeText.text =
                "Barricade x" +
                damageMultiplier.ToString("0.00");
        }
    }

    public float GetDamageMultiplier()
    {
        return damageMultiplier;
    }
}