using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour{
    public Vital Health, Hunger, Thirst;
    
    public const int HUNGER_DECREASE_RATE = 3;
    public const int THIRST_DECREASE_RATE = 2;
    
    WaitForSeconds hungerWfs = new WaitForSeconds(HUNGER_DECREASE_RATE),
        thirstWfs = new WaitForSeconds(THIRST_DECREASE_RATE);

    private void Start(){
        List<IEnumerator> routines = new List<IEnumerator>();
        
        if (Hunger.Value > Hunger.Min)
        {
            routines.Add(DecreaseHungerOverTime());
        }
        
        if (Thirst.Value > Thirst.Min)
        {
            routines.Add(DecreaseThirstOverTime());
        }
        
        foreach (var r in routines)
        {
            StartCoroutine(r);
        }
    }

    private IEnumerator DecreaseHungerOverTime(){
        while (Hunger.Value > Hunger.Min)
        {
            Hunger.DecreaseValue(1);
            GameHUD.UpdateHungerBar(Hunger.Value, Hunger.Max);
            
            yield return hungerWfs;
        }
    }

    private IEnumerator DecreaseThirstOverTime(){
        while (Thirst.Value > Thirst.Min)
        {
            Thirst.DecreaseValue(1);
            GameHUD.UpdateThirstBar(Hunger.Value, Hunger.Max);
            
            yield return thirstWfs;
        }
    }
}