using UnityEngine;

/// <summary>
/// For managing money, experience, hp and other player stats
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int currMoney = 0;

    public void AddMoney(int money) {
        currMoney += money;
    }
}
