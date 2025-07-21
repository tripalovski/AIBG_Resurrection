using System;
using System.Collections;
using UnityEngine;

// Timer in format of hours:Minutes
public class WorldTime : MonoBehaviour
{
    public static WorldTime Instance { get; private set; }

    [SerializeField] private float dayLength;
    private TimeSpan currentTime;
    private float minuteLength;

    // Constants
    public const int MINUTES_IN_DAY = 1440;

    // Events
    public event EventHandler<TimeSpan> OnWorldTimeChange;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        minuteLength =  dayLength / MINUTES_IN_DAY;
    }

    private void Start() {
        StartCoroutine(AddMinute());
    }


    private IEnumerator AddMinute() {
        yield return new WaitForSeconds(minuteLength);
        currentTime += TimeSpan.FromMinutes(1);
        OnWorldTimeChange?.Invoke(this, currentTime);
        StartCoroutine(AddMinute());
    }
}
