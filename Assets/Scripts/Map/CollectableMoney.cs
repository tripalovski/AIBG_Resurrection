using UnityEngine;

public class CollectableMoney : Collectable
{
    [SerializeField] private int value = 5;
    protected override void Collect(GameObject player) {
        player.GetComponent<PlayerStats>().AddMoney(value);
    }
}
