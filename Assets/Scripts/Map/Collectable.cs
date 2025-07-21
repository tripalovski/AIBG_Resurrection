using UnityEngine;

/// <summary>
/// Attach this to a collectable object (money, xp, hp..)
/// </summary>
public abstract class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D player) {
        if (player.CompareTag("Player")) {
            Collect(player.gameObject);
            Destroy(gameObject);
        }
    }

    protected abstract void Collect(GameObject player);
}
