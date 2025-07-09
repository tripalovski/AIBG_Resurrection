using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform[] players;
    public float detectionRadius = 10f;
    public float moveSpeed = 3f;

    private Vector3 bossStartPosition;

    void Start()
    {
        bossStartPosition = new Vector3(0f, 0f, 0f);
    }

    void Update()
    {
        Transform targetPlayer = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform player in players)
        {
            float distance = Vector3.Distance(player.position, bossStartPosition);

            if (distance <= detectionRadius && distance < shortestDistance)
            {
                shortestDistance = distance;
                targetPlayer = player;
            }
        }

        if (targetPlayer != null)
        {
            Vector3 direction = (targetPlayer.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Ogranicenje da boss ne izadje iz svoje zone
            if (Vector3.Distance(transform.position, bossStartPosition) > detectionRadius)
            {
                transform.position = bossStartPosition + (transform.position - bossStartPosition).normalized * detectionRadius;
            }
        }
    }

}
