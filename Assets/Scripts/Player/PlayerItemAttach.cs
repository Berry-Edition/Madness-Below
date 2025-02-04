using System;
using UnityEngine;

public sealed class PlayerItemAttach : MonoBehaviour{
    [Header("Item Attach :")]
    [SerializeField] Transform _itemAttach;
    [SerializeField] GameEntity _itemAttachEntity;
    
    
    [HideInInspector] public GameObject ItemAttachGameObject;


    private bool isFacingRight = false;
    
    private void Start(){
        ItemAttachGameObject = Instantiate(_itemAttachEntity.EntityPrefab, _itemAttach.position, _itemAttachEntity.EntityPrefab.transform.rotation, _itemAttach.transform);
    }

    public void Flip(Transform player){
        if (player.position.x > _itemAttach.position.x && !isFacingRight)
        {
            Flip(player, 1);
        }
        else if (player.position.x < _itemAttach.position.x && isFacingRight)
        {
            Flip(player, -1);
        }
    }

    public void ForceFlipRight(Transform player){
        Flip(player, 1);
    }

    public void ForceFlipLeft(Transform player){
        Flip(player, -1);
    }
    
    void Flip(Transform player, int offset){
        switch (offset)
        {
            case 1:
                _itemAttach.position = -_itemAttach.position;
                isFacingRight = true;
                break;
            case -1:
                
                isFacingRight = false;
                break;
        }
    }
    
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_itemAttach.position, 0.2f);
    }
}