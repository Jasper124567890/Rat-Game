using UnityEngine;
using System.Collections;

public class BreakBoard : MonoBehaviour
{
    [SerializeField] private float attackInterval = 3f;

    private bool attacking = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barricade"))
        {
            Debug.Log("Rat hit barricade");

            Window barricade =
                collision.gameObject.GetComponentInParent<Window>();

            if (barricade != null && !attacking)
            {
                StartCoroutine(AttackBarricade(barricade));
            }
        }
    }

    private IEnumerator AttackBarricade(Window barricade)
    {
        attacking = true;

        while (barricade.currentBoards > 0)
        {
            yield return new WaitForSeconds(attackInterval);

            barricade.RemoveBoard();

            Debug.Log("Board Removed");
        }

        attacking = false;
    }
}
