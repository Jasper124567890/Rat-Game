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

        RaycastHit[] hits = Physics.RaycastAll(
            ray,
            100f,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Collide
        );

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("RepairTarget"))
            {
                Window window =
                    hit.collider.GetComponentInParent<Window>();

                if (window != null)
                {
                    float distance = Vector3.Distance(
                        player.position,
                        window.transform.position
                    );

                    if (distance <= repairDistance)
                    {
                        if (window.currentBoards < window.maxBoards)
                        {
                            if (boardInventory.UseBoard())
                            {
                                window.AddBoard();

                                Debug.Log("Board Added!");
                            }
                        }
                    }
                }

                break;
            }
        }

        rectTransform.anchoredPosition = startPosition;
    }
}