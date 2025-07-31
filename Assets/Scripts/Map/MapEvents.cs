using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapEvents : MonoBehaviour
{
    public static MapEvents Instance { get; private set; }

    [SerializeField] private List<GameObject> players = new List<GameObject>();
    [SerializeField] private GameObject rainEffect;


    // Game parameters

    //Day Night
    public const int DAY_DURATION_SECONDS = 90;
    public const int NIGHT_DURATION_SECONDS = 60;
    public const int DAYS_PER_MATCH = 4;

    //Rain
    public const float RAIN_SPEED_REDUCE = -3;
    public const int RAIN_START_MIN_SECONDS = 120;
    public const int RAIN_START_MAX_SECONDS = 300;
    public const int RAIN_DURATION_MIN_SECONDS = 30;
    public const int RAIN_DURATION_MAX_SECONDS = 120;



    // Events
    public event EventHandler OnStartNight;
    public event EventHandler OnStartDay;
    public event EventHandler<OnStartRainEventArgs> OnStartRain;
    public class OnStartRainEventArgs : EventArgs {
        public float speedReduce = RAIN_SPEED_REDUCE;
        public int reduceDuration;
    }


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(_Rain());
        StartCoroutine(_DayNightCycle());
    }

    /// <summary>
    /// In random time rains start and slows players
    /// Rain will stop after some random time and everything goes back to normal
    /// </summary>
    private IEnumerator _Rain() {
        yield return new WaitForSeconds(Random.Range(RAIN_START_MIN_SECONDS, RAIN_START_MAX_SECONDS));
        int rainDuration = Random.Range(RAIN_DURATION_MIN_SECONDS, RAIN_DURATION_MAX_SECONDS);
        rainEffect.SetActive(true); // TODO delegiraj nekoj drugoj klasi
        OnStartRain?.Invoke(this, new OnStartRainEventArgs { reduceDuration = rainDuration });
        yield return new WaitForSeconds(rainDuration);
        rainEffect.SetActive(false); // TODO isto
    }


    /// <summary>
    /// After 4. night cycle stops and activates another mode
    /// </summary>
    private IEnumerator _DayNightCycle() {
        for(int day=1; day<=DAYS_PER_MATCH; day++) {
            yield return new WaitForSeconds(DAY_DURATION_SECONDS);
            OnStartNight?.Invoke(this, EventArgs.Empty);
            yield return new WaitForSeconds(NIGHT_DURATION_SECONDS);
            OnStartDay?.Invoke(this, EventArgs.Empty);
        }
        // Activate some boost till end of match, so it can finish faster
    }
}
