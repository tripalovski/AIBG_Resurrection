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
    }


    private void Map_OnStartNight(object sender, System.EventArgs e) {
        NightBoost();
    }

    private void NightBoost() {
        Debug.Log("Night boost for " + MapEvents.NIGHT_DURATION_SECONDS + " seconds");
    }
}
