using UnityEngine;

public class Damageonhit : MonoBehaviour
{
    public string targetTag = "Cheese";

    [SerializeField] private int damage = 25;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Health cheeseHealth =
                collision.gameObject.GetComponent<Health>();

            if (cheeseHealth != null)
            {
                cheeseHealth.CheeseTakeDamage(damage);
            }

            Spawner spawner = FindObjectOfType<Spawner>();

            if (spawner != null)
            {
                spawner.EnemyDestroyed();
            }

            Destroy(gameObject);
        }
    }
}