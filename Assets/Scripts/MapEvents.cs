using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapEvents : MonoBehaviour
{
    [SerializeField] private List<GameObject> players = new List<GameObject>();
    [SerializeField] private GameObject rainEffect;


    // Game parameters
    private const float RAIN_SPEED_REDUCE = -3;
    private const float RAIN_START_MIN_SECONDS = 120;
    private const float RAIN_START_MAX_SECONDS = 300;
    private const float RAIN_DURATION_MIN_SECONDS = 30;
    private const float RAIN_DURATION_MAX_SECONDS = 120;

    void Start()
    {
        StartCoroutine(_Rain());

    }

    /// <summary>
    /// In random time rains start and slows players
    /// Rain will stop after some random time and everything goes back to normal
    /// </summary>
    private IEnumerator _Rain() {
        yield return new WaitForSeconds(Random.Range(RAIN_START_MIN_SECONDS, RAIN_START_MAX_SECONDS));
        rainEffect.SetActive(true);
        float rainDuration = Random.Range(RAIN_DURATION_MIN_SECONDS, RAIN_DURATION_MAX_SECONDS);
        foreach (GameObject player in players) {
            StartCoroutine(player.GetComponent<PlayerMovement>().ChangeSpeed(RAIN_SPEED_REDUCE, rainDuration));
        }
        yield return new WaitForSeconds(rainDuration);
        rainEffect.SetActive(false);
    }


}
