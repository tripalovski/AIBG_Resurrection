using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public const float BASE_SPEED = 5;
    public const float MIN_SPEED = 2;

    public float speed = 5;
    public float speedChange = 0; // For buffs and debuffs, in case it drops below zero (or some minimum) and so it can be restored to normal
    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector2(horizontal, vertical).normalized * speed;
    }

    /// <summary>
    /// Changes speed for given time. When time is up, returns speed to previous amount.
    /// </summary>
    /// <param name="amount">Amount for which speed is changed</param>
    /// <param name="time">Time after which change will be nillified</param>
    public void ChangeSpeed(float amount, float time) {
        StartCoroutine(_StartChangeSpeed(amount, time));
    }



    public IEnumerator _StartChangeSpeed(float amount, float time) {
        speedChange += amount;
        speed = (BASE_SPEED + speedChange < MIN_SPEED) ? MIN_SPEED : BASE_SPEED + speedChange;
        yield return new WaitForSeconds(time);
        speedChange -= amount;
        speed = (BASE_SPEED + speedChange < MIN_SPEED) ? MIN_SPEED : BASE_SPEED + speedChange;
    }


}
