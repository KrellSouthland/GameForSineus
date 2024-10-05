using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLightTrigger : MonoBehaviour
{
    [SerializeField] private LightDialogueSystem dialogueSystem;
    [SerializeField] private string[] _textArray;

    void Start()
    { 
        dialogueSystem = GameObject.Find("DialogueSystem").GetComponent<LightDialogueSystem>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            dialogueSystem.InitDialogue(_textArray);

            Destroy(gameObject);

        }
    }
 
}
