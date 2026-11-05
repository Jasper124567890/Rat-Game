using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 2f;

    [SerializeField] private Transform playerCamera;

    private float verticalRotation = 0f;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;

        controller.Move(movement.normalized * moveSpeed * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;

        verticalRotation = Mathf.Clamp(verticalRotation, -45f, 30f);

        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}


