using UnityEngine;

[System.Serializable]
public struct EntityItemConsumableData{
    [Header("Item Consumable Settings :")] 
    public bool TargetHunger;
    public bool TargetThirst,
        TargetHealth;

    [Space]
    public int HungerBonus;
    public int ThirstBonus, HealthBonus;
}