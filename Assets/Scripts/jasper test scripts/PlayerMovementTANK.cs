using UnityEngine;

public class PlayerMovementTank : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 100f;

    [SerializeField] private Transform playerCamera;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // Move forward/backward
        Vector3 movement = transform.forward * z;
        controller.Move(movement.normalized * moveSpeed * Time.deltaTime);

        // Turn left/right with A and D
        transform.Rotate(Vector3.up * x * turnSpeed * Time.deltaTime);
    }
}
