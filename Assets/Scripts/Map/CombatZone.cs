using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// After some delay, map starts shrinking
/// </summary>
public class CombatZone : MonoBehaviour
{
    public const int NARROWING_START_TIME = 600;
    public const int MIN_COMBAT_RADIUS = 3;

    [SerializeField] private float narrowingSpeed = 0.1f;
    private bool narrowingStarted = false;

    private CircleCollider2D combatZone;


    // Events
    public event EventHandler<float> OnNarrowRadius;

    private void Awake() {
        combatZone = GetComponent<CircleCollider2D>();
    }
    void Start()
    {
        StartCoroutine(StartNarrowing());
    }

    private void Update() {
        if (narrowingStarted && combatZone.radius > MIN_COMBAT_RADIUS) {
            float narrowRadius = Time.deltaTime * narrowingSpeed;
            combatZone.radius -= narrowRadius;

            OnNarrowRadius(this, narrowRadius);
        }
    }


    private IEnumerator StartNarrowing() {
        yield return new WaitForSeconds(NARROWING_START_TIME);
        narrowingStarted = true;
    }
}
