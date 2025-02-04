using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieArena/Events/Eat Event")]
public class Eat : GameEntityEventSO
{
    public override bool CanExecute(){
        return true;
    }

    public override void Execute(GameEntityGUID entity, Object obj1){
        // Why the fuck does the Eat event is in a non-item entity ?
        // I'll just leave this condition as it.

        // if (!entity.GUID.IsItem)
        //     return;
        
        var go = obj1 as GameObject;
        var playerStats = go.gameObject.GetComponent<PlayerStats>();

        if (entity.GUID.ItemConsumableData.TargetHunger)
        {
            playerStats.Hunger.AddValue(entity.GUID.ItemConsumableData.HungerBonus);
            GameHUD.UpdateHungerBar(playerStats.Hunger.Value, playerStats.Hunger.Max);
        }
            
        if (entity.GUID.ItemConsumableData.TargetThirst)
        {
            playerStats.Thirst.AddValue(entity.GUID.ItemConsumableData.ThirstBonus);
            GameHUD.UpdateThirstBar(playerStats.Thirst.Value, playerStats.Thirst.Max);
        }
            
        if (entity.GUID.ItemConsumableData.TargetHealth)
        {
            playerStats.Health.AddValue(entity.GUID.ItemConsumableData.HealthBonus);
            GameHUD.UpdateHealthBar(playerStats.Health.Value, playerStats.Health.Max);
        }
    }
}