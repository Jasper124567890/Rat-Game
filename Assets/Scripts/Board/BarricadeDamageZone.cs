using System.Collections.Generic;
using UnityEngine;

public class BarricadeDamageZone : MonoBehaviour
{
    [SerializeField] private Window window;
    [SerializeField] private float damagePerEnemyPerSecond = 5f;

    private List<Collider> enemies = new List<Collider>();

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

        if (enemies.Count > 0)
        {
            float damage = enemies.Count * damagePerEnemyPerSecond * Time.deltaTime;
            window.TakeDamage(damage);
        }
    }
}