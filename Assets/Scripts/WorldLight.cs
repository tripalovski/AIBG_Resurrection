using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Changes day light depending on which time of day is
/// </summary>
[RequireComponent(typeof(Light2D))]
public class WorldLight : MonoBehaviour
{
    private Light2D worldLight;
    [SerializeField] private Gradient gradient;

    private void Awake() {
        worldLight = GetComponent<Light2D>();
    }

    private void Start() {
        WorldTime.Instance.OnWorldTimeChange += OnWorldTimeChange;
    }

    private void OnDestroy() {
        WorldTime.Instance.OnWorldTimeChange -= OnWorldTimeChange;
    }



    private void OnWorldTimeChange(object sender, System.TimeSpan currTime) {
        worldLight.color = gradient.Evaluate(PercentOfDay(currTime));
    }

    private float PercentOfDay(TimeSpan time) {
        return (float)time.TotalMinutes % WorldTime.MINUTES_IN_DAY / WorldTime.MINUTES_IN_DAY;
    }
}
