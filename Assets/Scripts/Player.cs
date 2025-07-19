using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start() {
        //Events
        MapEvents.Instance.OnStartNight += Map_OnStartNight;
        MapEvents.Instance.OnStartRain += Map_OnStartRain;
    }



    private void NightBoost() {
        Debug.Log("Night boost for " + MapEvents.NIGHT_DURATION_SECONDS + " seconds");
    }



    // Events methods
    private void Map_OnStartRain(object sender, MapEvents.OnStartRainEventArgs e) {
        playerMovement.ChangeSpeed(e.speedReduce, e.reduceDuration);
    }

    private void Map_OnStartNight(object sender, System.EventArgs e) {
        NightBoost();
    }
}
