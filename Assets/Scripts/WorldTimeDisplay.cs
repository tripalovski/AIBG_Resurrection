using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class WorldTimeDisplay : MonoBehaviour
{
    private TMP_Text timeText;

    private void Awake() {
        timeText = GetComponent<TMP_Text>();
    }

    private void Start() {
        WorldTime.Instance.OnWorldTimeChange += OnWorldTimeChange;
    }

    private void OnDestroy() {
        WorldTime.Instance.OnWorldTimeChange -= OnWorldTimeChange;
    }


    private void OnWorldTimeChange(object sender, System.TimeSpan newTime) {
        timeText.SetText(newTime.ToString(@"hh\:mm"));
    }
}
