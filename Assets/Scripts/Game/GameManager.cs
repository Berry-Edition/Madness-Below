using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get;
        set;
    }

    public GameEntityManager EntityManager;
    
    private void Awake(){
        if (Instance)
        {
            // im blind. I must need 3 red logs for being aware of this problem ;-)
            Debug.LogError("There is already an instance of GameManager!");
            Debug.LogError("There is already an instance of GameManager!");
            Debug.LogError("There is already an instance of GameManager!");
            
            Destroy(Instance);
        }
        
        Instance = this;
    }
}