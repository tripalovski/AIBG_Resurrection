using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEnemy : MonoBehaviour {

    private Rigidbody2D rb;
    public Transform player;

    private Animator anim;

    private bool isChasing;
    private int facingDirection = -1;

    public float speed = 3f;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        if(isChasing == true) {
            if(player.position.x < transform.position.x && facingDirection == -1 ||
            player.position.x > transform.position.x && facingDirection == 1) {
                Flip();
            } 
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction*speed;
        }
        else {}
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            if(player == null){
                player = collision.transform;
            }
            isChasing = true;
        }
    }
     private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            isChasing = false;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void Flip() {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x *(-1), transform.localScale.y, transform.localScale.z);
    }
}