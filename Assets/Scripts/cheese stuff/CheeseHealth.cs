using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [Header("Lose Screen")]
    [SerializeField] private TMP_Text loseText;

    private Vector3 originalScale;
    private bool gameOver = false;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        originalScale = transform.localScale;

        UpdateScale();
    }

    public void TakeDamage(float damage)
    {
        if (gameOver)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateScale();

        if (currentHealth <= 0)
        {
            gameOver = true;
            StartCoroutine(GameOverRoutine());
        }
    }

    private void UpdateScale()
    {
        float healthPercent = currentHealth / maxHealth;

        transform.localScale =
            originalScale * Mathf.Max(healthPercent, 0.1f);
    }

    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;

        UpdateScale();
    }

    private IEnumerator GameOverRoutine()
    {
        loseText.gameObject.SetActive(true);

        loseText.text = "TOO MANY RATS, NOT ENOUGH CHEESE";
        yield return new WaitForSeconds(3f);

        loseText.text = "GAME OVER!";
        yield return new WaitForSeconds(3f);



        SceneManager.LoadScene("Main Menu");
       
    }
}