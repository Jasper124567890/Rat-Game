using UnityEngine;
using System.Collections;

public class RatHealth : MonoBehaviour
{
    [SerializeField] private int health = 1;

    [SerializeField] private Animator animator;

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
            RatDie();
        }
    }

    private void RatDie()
    {
        if (isDead)
            return;

        isDead = true;

        RigidbodyConstraints originalConstraints = rb.constraints;

        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;

        animator.SetTrigger("ratdie");

        StartCoroutine(DeathAnimation());
    }

    private IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(1.2f);

        Spawner spawner = FindObjectOfType<Spawner>();

        if (spawner != null)
        {
            spawner.EnemyDestroyed();
        }

        Destroy(gameObject);
    }
}
