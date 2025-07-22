using UnityEngine;

/// <summary>
/// Responsible for fairly spawning and respawning picked objects
/// </summary>
public class CollectableSpawner : MonoBehaviour
{
    public static CollectableSpawner Instance { get; private set; }

    [SerializeField] private GameObject coinPrefab;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    // Initially spawns few objects on same spots on all quarters
    private void Start() {
        Vector2 randomStartSpawnPosition = PickQuarterRandomPosition(new Vector2(1, 1));
        Spawn(coinPrefab, randomStartSpawnPosition);
        Spawn(coinPrefab, new Vector2(-randomStartSpawnPosition.x, randomStartSpawnPosition.y));
        Spawn(coinPrefab, new Vector2(randomStartSpawnPosition.x, -randomStartSpawnPosition.y));
        Spawn(coinPrefab, new Vector2(-randomStartSpawnPosition.x, -randomStartSpawnPosition.y));
    }




    /// <summary>
    /// Spawns an object in same quarter from which it was picked
    /// </summary>
    /// <param name="prefab">Object whcih was picked and needs te be respawned</param>
    /// <param name="prevPos">Position from which it was picked</param>
    public void QuarterSpawn(GameObject prefab, Vector2 prevPos) {
        Spawn(prefab, PickQuarterRandomPosition(prevPos));
    }

    private void Spawn(GameObject prefab, Vector2 spawnPos) {
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
 
    /// <summary>
    /// Picks another random position but from same quarter from which it was picked
    /// </summary>
    /// <param name="prevPos">Previous position - so it can be determined which quarter it was</param>
    /// <returns></returns>
    private Vector2 PickQuarterRandomPosition(Vector2 prevPos) {
        int minSpawnDistance = 1;
        int maxSpawnDistance = 5;
        return new Vector2(
            (prevPos.x > 0 ? 1 : -1) * Random.Range(minSpawnDistance, maxSpawnDistance),
            (prevPos.y > 0 ? 1 : -1) * Random.Range(minSpawnDistance, maxSpawnDistance));
    }
}
