using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour{
    [SerializeField] private float rayDistance = 5f; // Distance du raycast
    [SerializeField] private LayerMask targetLayer; // Couches à détecter (par exemple, ennemis, murs)

    private void Update(){
        // Obtenir la direction du Raycast (dans ce cas, vers l'avant de l'objet)
        Vector2 rayDirection = transform.up; // "up" pour un top-down (axe Y)

        // Lancer le raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, rayDistance, targetLayer);

        // Debug Ray dans l'éditeur
        Debug.DrawRay(transform.position, rayDirection * rayDistance, Color.red);

        // Si le raycast touche quelque chose
        if (hit.collider)
        {
            var item = hit.transform.GetComponent<GameEntityGUID>();
            item.EntityEvent.Event.Execute(item);
            
            Destroy(hit.transform.gameObject);
        }
    }
}