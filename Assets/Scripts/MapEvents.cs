using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapEvents : MonoBehaviour
{
    [SerializeField] private List<GameObject> players = new List<GameObject>();

    // Game parameters
    private const float RAIN_SPEED_REDUCE = -3;
    private const float RAIN_DURATION_SECONDS = 3;

    void Start()
    {
        StartCoroutine(_StartRain());
    }

    /// <summary>
    /// In random time rains start and slows players
    /// Rain will stop after some random time and everything goes back to normal
    /// </summary>
    private IEnumerator _StartRain() {
        yield return new WaitForSeconds(Random.Range(1, 5));
        foreach (GameObject player in players) {
            StartCoroutine(player.GetComponent<PlayerMovement>().ChangeSpeed(RAIN_SPEED_REDUCE, RAIN_DURATION_SECONDS));
        }
    }
}
