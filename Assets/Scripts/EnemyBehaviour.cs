using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // This is a parent class for enemies
    // They all have to flee from the player (wolf), avoid obstacles while running
    // Their difference is way of moving (some jump, some walk) 
    // They will override the wander function (if it's necessary, maybe I will make animations for the enemies)

    [SerializeField] private Transform player;
    private float wanderSpeed = 1.5f;
    private float fleeSpeed = 3f;
    private float sightRange = 5f;
    private float fieldOfView = 30f;
    private float changeDirectionPeriod = 2f;
    private float roadClearDistance = 0.5f;

    private LayerMask obstacleMask;
    private LayerMask playerMask;
    private LayerMask enemyMask;

    private Rigidbody2D rigidBody2D;
    private Vector2 wanderDirection;
    private float nextDirectionTime;

    protected virtual void Start() {
        rigidBody2D = GetComponent<Rigidbody2D>();
        PickDirection();
    }

    protected virtual void Update() { 
        if (CanSeePlayer()) {
            FleeFromPlayer();
        } else {
            Wander();
        }
    }

    protected bool CanSeePlayer() {
        Vector2 directionToPlayer = ((Vector2)player.position - rigidBody2D.position);
        float angle = Vector2.Angle(transform.right, directionToPlayer);

        if (directionToPlayer.magnitude < sightRange && angle < fieldOfView/2f ) {
            // if the player is close and inside our field of view
            RaycastHit2D hit = Physics2D.Raycast(rigidBody2D.position, directionToPlayer.normalized, sightRange, playerMask | obstacleMask | enemyMask);
            if (hit.collider!=null && hit.collider.gameObject == player.gameObject) {
                // if the object we hit is the player and not an obstacle
                return true;
            }
        }
        return false;
    }

    protected void PickDirection() {
        float angle = Random.Range(0f, 360f);
        wanderDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        nextDirectionTime = Time.time + changeDirectionPeriod;
    }

    protected void FleeFromPlayer() { 
        Vector2 direction = (rigidBody2D.position - (Vector2)player.position).normalized; // direction away from the player
        Vector2 fleePosition = rigidBody2D.position + direction * fleeSpeed * Time.deltaTime;

        if (!Physics2D.Raycast(rigidBody2D.position, direction, roadClearDistance, obstacleMask | enemyMask)) {
            // if the road ahead is clear then flee that way
            rigidBody2D.MovePosition(fleePosition);
        } else {
            // if not, then the direction is blocked and we have to choose a different direction
            PickDirection();
        }
    }

    protected virtual void Wander() { 
        if (Time.time > nextDirectionTime) {
            // Pick a random direction every few seconds
            PickDirection();
        }

        Vector2 movePosition = rigidBody2D.position + wanderDirection*wanderSpeed*Time.deltaTime;

        if (!Physics2D.Raycast(rigidBody2D.position, wanderDirection, roadClearDistance, obstacleMask | enemyMask)) {
            rigidBody2D.MovePosition(movePosition);
        } else {
            PickDirection();
        }
    }
}
