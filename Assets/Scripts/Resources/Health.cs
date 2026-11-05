using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Cheese")]
    [SerializeField] private int cheeseHealth = 100;
    [SerializeField] private float shrinkAmount = 0.1f;

    [Header("Lose Screen")]
    [SerializeField] private TMP_Text loseText;

    private bool gameOver = false;

    public void CheeseTakeDamage(int cheeseDamage)
    {
        if (gameOver) return;

        cheeseHealth -= cheeseDamage;

        // Shrink cheese
        transform.localScale -= Vector3.one * shrinkAmount;

        transform.localScale = new Vector3(
            Mathf.Max(transform.localScale.x, 0.1f),
            Mathf.Max(transform.localScale.y, 0.1f),
            Mathf.Max(transform.localScale.z, 0.1f)
        );

        if (cheeseHealth <= 0)
        {
            gameOver = true;
            StartCoroutine(GameOverRoutine());
        }
    }

    private IEnumerator GameOverRoutine()
    {
        loseText.gameObject.SetActive(true);

        loseText.text = "TOO MANY RATS, NOT ENOUGH CHEESE";
        yield return new WaitForSeconds(3f);

        loseText.text = "Restarting in 3";
        yield return new WaitForSeconds(1f);

        loseText.text = "Restarting in 2";
        yield return new WaitForSeconds(1f);

        loseText.text = "Restarting in 1";
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}