using UnityEngine;

public class GameEntityGUID : MonoBehaviour, IEntity{
    public GameEntity GUID
    {
        get => _entity;
    }

    [Header("Game Entity Data :")] 
    [SerializeField] private GameEntity _entity;

    public GameEntityEvent EntityEvent;
}