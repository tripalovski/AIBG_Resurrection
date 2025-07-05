using UnityEngine;

public class Rabbit : FoodEnemy
{
    [SerializeField] private Transform groundCheckPoint;

    private float jumpForce = 8f;
    private float forwardForce = 3f;
    private float jumpInterval = 2f;
    private float groundCheckRadius = 0.2f;
    private float jumpTimer;
    private bool isGrounded;
    private bool facingRight = true;
    public float directionChangeChance = 0.3f;

    private LayerMask groundMask;
    private Rigidbody2D rigidBody;

    protected override void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        jumpTimer = jumpForce;
    }

    protected override void Update() {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundMask);
        Wander();
    }

    protected override void Wander() {
        jumpTimer -= Time.deltaTime;

        if (jumpTimer <=0f && isGrounded) {
            // random chance to change direction before jumping
            if (Random.value < directionChangeChance) {
                FlipDirection();
            }

            Jump();
            jumpTimer = Random.Range(1.5f, jumpInterval);
        }
    }

    private void FlipDirection() {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Jump() {
        float dir = facingRight ? 1f : -1f;
        Vector2 jumpVelocity = new Vector2(dir * forwardForce, jumpForce);
        rigidBody.linearVelocity = jumpVelocity;
    }
}
