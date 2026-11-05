using UnityEngine;
using UnityEngine.EventSystems;

public class BoardUI : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    public Camera playerCamera;

    public Transform player;
    public float repairDistance = 3f;

    private RectTransform rectTransform;
    private Vector2 startPosition;

    public BoardInventory boardInventory;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = playerCamera.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("RepairTarget"))
            {
                float distance = Vector3.Distance(
                    player.position,
                    hit.collider.transform.position
                );

                if (distance <= repairDistance)
                {
                    Window window =
                        hit.collider.GetComponentInParent<Window>();

                    if (window != null)
                    {
                        if (window.currentBoards < window.maxBoards)
                        {
                            if (boardInventory.UseBoard())
                            {
                                window.AddBoard();
                            }
                        }
                    }
                }
            }
        }

        rectTransform.anchoredPosition = startPosition;
    }
}
