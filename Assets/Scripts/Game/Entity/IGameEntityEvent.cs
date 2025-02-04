using UnityEngine;

public interface IGameEntityEvent{
    public bool CanExecute();
    public void Execute(GameEntityGUID entity);
    public void Execute(GameEntityGUID entity, Object obj1);
    public void Execute(GameEntityGUID entity, Object obj1, Object obj2);
    public void Execute(GameEntityGUID entity, Object obj1, Object obj2, Object obj3);
    
    public KeyCode KeyToListen { get; }
}