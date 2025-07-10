using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class FoodEnemy : Enemy
{
    // This is a parent class for food enemies
    // They all have to flee from the player (wolf), avoid obstacles while running
    // Their difference is way of moving (some jump, some walk) 
    // They will override the wander function (if it's necessary, maybe I will make animations for the food enemies)

    public override int lifePoints 
    { 
        get => _lifePoints;
        set => _lifePoints = value;
    }
    private int _lifePoints = 3;

    protected override void Player_OnAttack(object sender, EventArgs e) {
        lifePoints--;
        if (lifePoints <= 0 ) {
            Destroy(gameObject);
        }
    }
}
