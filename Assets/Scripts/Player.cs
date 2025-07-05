using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private GameInput gameInput;

    public event EventHandler OnAttack;

    private float interactDistance = 1.5f;
    private float moveSpeed = 2f;
    private float runningSpeed = 3.5f;
    private float sightRange = 7f;
    private float fieldOfView = 30f;
    private float playerRadius = 0.4f;
    private bool  isCrouching = false;
    private Vector2 lastInteractDir;

    private LayerMask enemyMask;
    private LayerMask obstacleMask;
    private LayerMask foodMask;

    private Rigidbody2D rigidBody2D;
    private void Awake() {
        Instance = this;
    }

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector2 moveDir = new Vector2(inputVector.x, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics2D.CircleCast(rigidBody2D.position, playerRadius, moveDir, moveDistance, obstacleMask);

        if (!canMove) {
            // Try only x
            Vector2 moveDirX = new Vector2(moveDir.x, 0).normalized;
            canMove = moveDir.x != 0 && !Physics2D.CircleCast(rigidBody2D.position, playerRadius, moveDirX, moveDistance, obstacleMask);

            if (canMove) {
                moveDir = moveDirX;
            } else {
                // Try only y
                Vector2 moveDirY = new Vector2(0, moveDir.y).normalized;
                canMove = moveDir.y != 0 && !Physics2D.CircleCast(rigidBody2D.position, playerRadius, moveDirY, moveDistance, obstacleMask);

                if (canMove) {
                    moveDir = moveDirY;
                } else {
                    moveDir = Vector2.zero;
                }

            }
        }

        if (canMove && moveDir != Vector2.zero) {
            rigidBody2D.MovePosition(rigidBody2D.position + moveDir);
        }
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector2 moveDir = new Vector2(inputVector.x, inputVector.y);

        if (moveDir!=Vector2.zero) {
            lastInteractDir = moveDir;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastInteractDir, interactDistance, enemyMask | foodMask);
        // handling intercations with food items, animals that are for food, enemies and obstacles
        if (hit.collider!=null) {
            // trying to determine what object is near
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                // if it's an enemy, we attack
                // have different startegies for enemies
                OnAttack?.Invoke(this, EventArgs.Empty);
            } else {
                // it's food, we eat it and update hunger system
                ConsumeFood();
            }
        } else {
            // nothing in front, we're moving
            rigidBody2D.MovePosition(rigidBody2D.position + moveSpeed*moveDir*Time.deltaTime);
        }
    }

    public bool IsCrouching() { 
        return isCrouching;
    }

    private void ConsumeFood() {
        // update hunger system, health
    }

    private void PickDirection() {
        //float angle = UnityEngine.Random.Range(0f, 360f);
    }
}
