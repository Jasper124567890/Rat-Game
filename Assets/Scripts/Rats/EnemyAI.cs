using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string moveTargetTag = "Cheese";
    public string lookTargetTag = "Player";

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private float minJumpTime = 1f;
    [SerializeField] private float maxJumpTime = 4f;

    private Transform moveTarget;
    private Transform lookTarget;
    private Rigidbody rb;

    private float jumpTimer;
    private float nextJumpTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject moveObject =
            GameObject.FindGameObjectWithTag(moveTargetTag);

        if (moveObject != null)
        {
            moveTarget = moveObject.transform;
        }

        GameObject lookObject =
            GameObject.FindGameObjectWithTag(lookTargetTag);

        if (lookObject != null)
        {
            lookTarget = lookObject.transform;
        }

        nextJumpTime = Random.Range(minJumpTime, maxJumpTime);
    }

    private void Update()
    {
        if (lookTarget != null)
        {
            Vector3 lookPos = lookTarget.position;
            lookPos.y = transform.position.y;

            transform.LookAt(lookPos);
        }

        jumpTimer += Time.deltaTime;

        if (jumpTimer >= nextJumpTime)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            jumpTimer = 0f;
            nextJumpTime = Random.Range(minJumpTime, maxJumpTime);
        }
    }

    void FixedUpdate()
    {
        if (moveTarget == null) return;

        Vector3 direction =
            (moveTarget.position - transform.position).normalized;

        direction.y = 0;

        rb.MovePosition(
            rb.position +
            direction * moveSpeed * Time.fixedDeltaTime);
    }

}
