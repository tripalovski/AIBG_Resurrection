using UnityEngine;

/// <summary>
/// Attach this to a collectable object (money, xp, hp..)
/// </summary>
public abstract class Collectable : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;

    private void OnTriggerEnter2D(Collider2D player) {
        if (player.CompareTag("Player")) {
            Collect(player.gameObject);
            CollectableSpawner.Instance.QuarterSpawn(prefab, new Vector2(transform.position.x, transform.position.y));
            Destroy(gameObject);
        }
    }

    protected abstract void Collect(GameObject player);
}
