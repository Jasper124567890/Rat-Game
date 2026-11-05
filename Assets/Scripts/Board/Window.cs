using UnityEngine;

public class Window : MonoBehaviour
{
    public GameObject board1;
    public GameObject board2;
    public GameObject board3;
    public GameObject board4;

    public Collider enemyBlocker;

    public int maxBoards = 4;
    public int currentBoards = 4;

    private void Start()
    {
        UpdateBoards();
    }

    public void AddBoard()
    {
        Debug.Log("Current Boards Before: " + currentBoards);

        if (currentBoards < maxBoards)
        {
            currentBoards++;
            Debug.Log("Current Boards After: " + currentBoards);

            UpdateBoards();
        }
    }

    public void RemoveBoard()
    {
        if (currentBoards > 0)
        {
            currentBoards--;
            UpdateBoards();
        }
    }

    private void UpdateBoards()
    {
        board1.SetActive(currentBoards >= 1);
        board2.SetActive(currentBoards >= 2);
        board3.SetActive(currentBoards >= 3);
        board4.SetActive(currentBoards >= 4);

        enemyBlocker.enabled = currentBoards > 0;
    }
}