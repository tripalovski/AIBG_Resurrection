using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Simulates map shrinking
/// </summary>
public class CombatZoneLight : MonoBehaviour
{
    [SerializeField] private CombatZone combatZone;
    private Light2D combatZoneLight;
    private bool started = false;


    private void Awake() {
        combatZoneLight = GetComponent<Light2D>();
    }

    private void Start() {
        combatZone.OnNarrowRadius += NarrowLight;
    }

    private void OnDestroy() {
        combatZone.OnNarrowRadius -= NarrowLight;
    }


    public void NarrowLight(object sender, float radius) {
        combatZoneLight.pointLightInnerRadius -= radius;
        combatZoneLight.pointLightOuterRadius -= radius;
    }
}
