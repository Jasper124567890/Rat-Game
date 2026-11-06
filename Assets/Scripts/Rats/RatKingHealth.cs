using UnityEngine;
using System.Collections;

public class RatKingHealth : MonoBehaviour
{
    [SerializeField] private int health = 1;

    [SerializeField] private float deathTime = 3f;
    [SerializeField] private float deathForce = 10f;
    [SerializeField] private float spinForce = 15f;

    private Rigidbody rb;
    private bool isDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void RatTakeDamage(int damage)
    {
        if (isDead)
            return;

        health -= damage;

        if (health <= 0)
        {
            StartCoroutine(RatDie());
        }
    }

    private IEnumerator RatDie()
    {
        isDead = true;

        if (rb != null)
        {
            rb.isKinematic = false;

            rb.AddForce(
                (Vector3.up + Random.insideUnitSphere).normalized * deathForce,
                ForceMode.Impulse
            );

            rb.AddTorque(
                Random.insideUnitSphere * spinForce,
                ForceMode.Impulse
            );
        }

        yield return new WaitForSeconds(deathTime);

        Spawner spawner = FindObjectOfType<Spawner>();

        if (spawner != null)
        {
            spawner.EnemyDestroyed();
        }

        Destroy(gameObject);
    }
}