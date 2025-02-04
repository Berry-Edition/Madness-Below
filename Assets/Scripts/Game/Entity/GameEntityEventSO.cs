using UnityEngine;

public class GameEntityEventSO : ScriptableObject, IGameEntityEvent {
    public KeyCode KeyToListen
    {
        get => _keyToListen;
    }
    
    [Header("Event Settings :")]
    [SerializeField] KeyCode _keyToListen;
    
    public virtual bool CanExecute(){
        // No conditions.
        return true;
    }

    public virtual void Execute(GameEntityGUID entity){
    }
    
    public virtual void Execute(GameEntityGUID entity, Object obj1){
    }
    
    public virtual void Execute(GameEntityGUID entity, Object obj1, Object obj2){
    }
    
    public virtual void Execute(GameEntityGUID entity, Object obj1, Object obj2, Object obj3){
    }
}