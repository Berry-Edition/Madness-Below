using UnityEngine;

[CreateAssetMenu(menuName = "ZombieArena/New Entity List")]
public class GameEntityList : ScriptableObject {
    public GameEntity[] Entities
    {
        get => _entities;
    }
    
    [SerializeField] GameEntity[] _entities;
}