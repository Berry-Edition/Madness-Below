using System;
using UnityEngine;

public sealed class PlayerItemAttach : MonoBehaviour{
    [Header("Settings :")] [SerializeField]
    private Player _player;
    [Header("Item Attach :")]
    [SerializeField] Transform _itemAttach;
    [SerializeField] GameEntity _itemAttachEntity;
    /// <summary>
    /// 1: left
    /// 2: right
    /// 3: top
    /// 4: bottom
    /// </summary>
    [SerializeField] private Transform[] _itemSides;
    
    [HideInInspector] public GameObject ItemAttachGameObject;
    
    private Vector3 baseScale;
    private Vector2 direction;
    
    private void Start(){
        ItemAttachGameObject = Instantiate(_itemAttachEntity.EntityPrefab, _itemAttach.position, _itemAttachEntity.EntityPrefab.transform.rotation, _itemAttach.transform);
        baseScale = _itemAttach.localScale;
    }
    
    private void Update(){
        if (ItemAttachGameObject)
            FlipItem();
    }
    
    void FlipItem()
    {
        // TODO
    }

    // Fonction à appeler pour changer la direction
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }
    
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_itemAttach.position, 0.2f);
    }
}