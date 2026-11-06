using UnityEngine;

public class Window : MonoBehaviour
{
    public GameObject board1;
    public GameObject board2;
    public GameObject board3;
    public GameObject board4;

    public Collider enemyBlocker;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateBoards();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateBoards();
    }

    public void RepairHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateBoards();
    }

    private void UpdateBoards()
    {
        float healthPercent = currentHealth / maxHealth;

        board4.SetActive(healthPercent > 0.75f);
        board3.SetActive(healthPercent > 0.50f);
        board2.SetActive(healthPercent > 0.25f);
        board1.SetActive(healthPercent > 0f);

        enemyBlocker.enabled = currentHealth > 0;
    }
}