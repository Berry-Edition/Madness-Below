using UnityEngine;

[CreateAssetMenu(menuName = "ZombieArena/Events/101 Flashlight Event")]
public class FlashlightEvent : GameEntityEventSO{
    public override bool CanExecute(){
        return true;
    }

    public override void Execute(GameEntityGUID guid, Object obj1, Object obj2){

        Debug.Log(obj1);
        Debug.Log(obj2);
        
        GameObject light = obj1 as GameObject;
        EntityFlashlightController flashlightController = obj2 as EntityFlashlightController;
        
        
        if (Input.GetKeyDown(KeyToListen))
            flashlightController.EnableFlashLight(light);
        
        else if (Input.GetKeyUp(KeyToListen))
            flashlightController.DisableFlashLight(light);
    }
}