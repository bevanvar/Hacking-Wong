using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueShow : MonoBehaviour
{
    public string messageToDisplay;
    public Text text;
    GameObject DialogueCanvas;

    private void Start()
    {
        DialogueCanvas = GameObject.Find("Dialogue");
        DialogueCanvas.SetActive(true);
        text.text = messageToDisplay;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
        //if (collision.tag == "Player")
        //{
            //DialogueCanvas.SetActive(true);
        //}
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            DialogueCanvas.SetActive(false);
        }
    }
}
