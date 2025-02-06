using System;
using UnityEngine;

public sealed class PlayerItemAttach : MonoBehaviour
{
    [Header("Settings :")] 
    [SerializeField] private Player _player;

    [Header("Item Attach :")]
    [SerializeField] private Transform _itemAttach;
    [SerializeField] private GameEntity _itemAttachEntity;
    [Space] [SerializeField, Tooltip("Leave blank if the _itemAttachEntity is used.\nInput the item's GUID you wish to use.")] private int _setItemAttachID;
    
    [Tooltip("Item Sides Order : \n1:Top\n2:Bottom\n3:Right\n4:Left"), SerializeField] private Transform[] _itemSides;

    [HideInInspector] public GameObject ItemAttachGameObject;

    private Vector2 direction = Vector2.zero;
    
    private void Start()
    {
        if (_setItemAttachID != 0)
        {
            _itemAttachEntity = GameManager.Instance.EntityManager.FindEntityByGUID(_setItemAttachID);
        }

        else
        {
            ItemAttachGameObject = Instantiate(
                _itemAttachEntity.EntityPrefab, 
                _itemAttach.position, 
                _itemAttachEntity.EntityPrefab.transform.rotation, 
                _itemAttach
            );
        }
    }
    
    private void Update()
    {
        if (ItemAttachGameObject)
            UpdateItemPositionAndRotation();
    }

    /// <summary>
    /// Met à jour la position et la rotation de l'item attaché
    /// </summary>
    private void UpdateItemPositionAndRotation()
    {
        if (direction == Vector2.zero) return; // Ne rien faire si le joueur ne bouge pas
        
        // Déterminer le point d'attache en fonction de la direction du joueur
        Transform attachPoint = GetAttachPoint();
        
        // Mettre à jour la position de l'item
        ItemAttachGameObject.transform.position = attachPoint.position;

        // Calculer et appliquer la rotation de l'item
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (attachPoint.Equals(_itemSides[0]))
        {
            // player is moving top (0,1,0)
            ItemAttachGameObject.transform.rotation = Quaternion.Euler(0, 0, angle + 180);
        }
        
        else if (attachPoint.Equals(_itemSides[1]))
        {
            // player is moving bottom (0,-1,0)
            ItemAttachGameObject.transform.rotation = Quaternion.Euler(0, 0, angle + -180);
        }
        
        else if (attachPoint.Equals(_itemSides[2]))
        {
            // player is moving right (-1,0,0)
            ItemAttachGameObject.transform.rotation = Quaternion.Euler(0, 0, -angle);
        }
        
        else
        {
            // player is moving left (1,0,0)
            ItemAttachGameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    /// <summary>
    /// Retourne le point d'attache approprié selon la direction du joueur
    /// </summary>
    private Transform GetAttachPoint()
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return direction.x > 0 ? _itemSides[1] : _itemSides[0]; // Droite / Gauche
        }
        
        return direction.y > 0 ? _itemSides[2] : _itemSides[3]; // Haut / Bas
    }

    /// <summary>
    /// Fonction à appeler pour changer la direction de l'item attaché
    /// </summary>
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; // Normalisation pour éviter les valeurs incohérentes
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_itemAttach.position, 0.2f);
    }
}