using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private GameInput gameInput;

    public event EventHandler OnAttack;

    private float attackDistance = 2f;
    private float moveSpeed = 2f;
    private float runningSpeed = 3.5f;
    private float sightRange = 7f;
    private float fieldOfView = 30f;

    private LayerMask enemyMask;
    private LayerMask obstacleMask;


    private void Awake() {
        Instance = this;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleMovement() {

    }

    private void HandleInteractions() {

    }
}
