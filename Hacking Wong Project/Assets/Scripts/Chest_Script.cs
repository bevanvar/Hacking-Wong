// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Chest_Script : MonoBehaviour, IInteractable
// {
//     [SerializeField]
//     private SpriteRenderer spriteRenderer;
//     [SerializeField]
//     private Sprite openSprite, closedSprite;
//     private bool isOpen;


//     public void Interact(){

//         if(isOpen){
//             StopInteract();
//         }
//         else{
//             isOpen = true;
//             spriteRenderer.sprite = openSprite;
//         }

//     }

//     public void StopInteract(){
//         isOpen = false;
//         spriteRenderer.sprite = closedSprite;

//     }

// public void OnTriggerEnter2D(Collider2D collision){
//         if (collision.tag == "Player"){
//             if (Input.GetMouseButtonDown(0)){


//             }
//         }
//     }
// }