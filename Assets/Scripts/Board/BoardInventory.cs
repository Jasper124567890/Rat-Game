using UnityEngine;
using TMPro;

public class BoardInventory : MonoBehaviour
{
    public Camera playerCamera;

    [Header("Boards")]
    public int maxBoards = 3;
    public int currentBoards;

    [Header("Refill")]
    public float refillDistance = 3f;

    [Header("UI")]
    public TMP_Text boardText;

    private void Start()
    {
        currentBoards = maxBoards;
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray =
                playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.collider.CompareTag("BoardBox"))
                {
                    float distance =
                        Vector3.Distance(
                            transform.position,
                            hit.collider.transform.position);

                    if (distance <= refillDistance)
                    {
                        RefillBoards();
                    }
                }
            }
        }
    }

    public bool UseBoard()
    {
        if (currentBoards > 0)
        {
            currentBoards--;
            UpdateUI();
            return true;
        }

        return false;
    }

    public void RefillBoards()
    {
        currentBoards = maxBoards;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (boardText != null)
        {
            boardText.text =
                "Boards: " +
                currentBoards +
                "/" +
                maxBoards;
        }
    }
}