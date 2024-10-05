using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private string[] _textArray;
    [SerializeField] private string _character;
    [SerializeField] private Sprite _avatar;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        dialogueSystem = GameObject.Find("DialogueSystem").GetComponent<DialogueSystem>();
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            dialogueSystem.InitDialogue(_textArray, _character, _avatar); 
            Destroy(gameObject); 
        }
    }
 
}
