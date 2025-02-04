using UnityEngine;

[CreateAssetMenu(menuName = "ZombieArena/New Entity")]
public class GameEntity : ScriptableObject {
    public int GUID
    {
        get => _entityGuid;
        set => _entityGuid = value;
    }

    public GameObject EntityPrefab { get => _entityPrefab; set => _entityPrefab = value; }
    public string EntityName { get => _entityName; set => _entityName = value; }
    public bool IsItem { get => _isItem; set => _isItem = value; }
    public EntityItemConsumableData ItemConsumableData { get => _itemConsumableData; set => _itemConsumableData = value; }
    
    [Header("Entity Data :")]
    [SerializeField] private int _entityGuid;
    [SerializeField] private string _entityName;
    [SerializeField] private bool _isItem;
    [SerializeField] private GameObject _entityPrefab;
    
    [Header("Item Only :")]
    [SerializeField] private EntityItemConsumableData _itemConsumableData;
}