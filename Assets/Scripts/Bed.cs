using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private Transform player;
    [SerializeField] private float interactDistance = 3f;

    private void Update()
    {
        if (!spawner.CanSleep())
            return;

        float distance = Vector3.Distance(
            player.position,
            transform.position
        );

        if (distance <= interactDistance)
        {
            if (Input.GetMouseButtonDown(0))
            {
                spawner.StartNextDay();
            }
        }
    }
}