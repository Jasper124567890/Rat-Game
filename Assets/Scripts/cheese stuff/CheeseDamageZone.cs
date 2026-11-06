using System.Collections.Generic;
using UnityEngine;

public class CheeseDamageZone : MonoBehaviour
{
    [SerializeField] private Health cheese;
    [SerializeField] private float damagePerRatPerSecond = 2f;

    private List<Collider> rats = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rat"))
        {
            if (!rats.Contains(other))
            {
                rats.Add(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rat"))
        {
            rats.Remove(other);
        }
    }

    private void Update()
    {
        rats.RemoveAll(rat => rat == null);

        if (rats.Count > 0)
        {
            float damage =
                rats.Count *
                damagePerRatPerSecond *
                Time.deltaTime;

            cheese.TakeDamage(damage);
        }
    }
}