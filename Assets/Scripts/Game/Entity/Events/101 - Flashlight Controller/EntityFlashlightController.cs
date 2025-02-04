using UnityEngine;

public class EntityFlashlightController : MonoBehaviour{
    [SerializeField]
    private GameEntityEvent _gameEntityEvent;

    private const float FLASHLIGHT_MAX_TIMER = 30.0f;
    private const float FLASHLIGHT_DIMMING_START = 15.0f;
    
    [Header("Debugging :")]
    [SerializeField] private float flashlightTimer = 0.0f;
    [SerializeField] private eFLASHLIGHT_STATES flashlightState = eFLASHLIGHT_STATES.FLASHLIGHT_OFF;

    private GameObject lightObject;
    
    public void EnableFlashLight(GameObject light){
        lightObject = light;
        
        if (flashlightState == eFLASHLIGHT_STATES.FLASHLIGHT_OFF)
        {   
            lightObject.SetActive(true);
            flashlightState = eFLASHLIGHT_STATES.FLASHLIGHT_ON;
            flashlightTimer = 0.0f;
        }
        else
        {
            DisableFlashLight(lightObject);   
        }
    }

    public void DisableFlashLight(GameObject light) {
        lightObject.SetActive(false);
        flashlightState = eFLASHLIGHT_STATES.FLASHLIGHT_OFF;
        print("flashlight is disabled");
    }


    private void Update(){
        if (flashlightState != eFLASHLIGHT_STATES.FLASHLIGHT_OFF)
        {
            flashlightTimer += Time.deltaTime;

            // Passer en état DIMMING si nécessaire
            if (flashlightTimer is >= FLASHLIGHT_DIMMING_START and < FLASHLIGHT_MAX_TIMER)
            {
                flashlightState = eFLASHLIGHT_STATES.FLASHLIGHT_DIMMING;
            }

            // Éteindre la lampe si le temps maximal est dépassé
            if (flashlightTimer >= FLASHLIGHT_MAX_TIMER)
            {
                DisableFlashLight(lightObject);
            }
        }
        else if (flashlightState == eFLASHLIGHT_STATES.FLASHLIGHT_DIMMING)
        {
            print("flashlight is dimming");
        }
    }
}