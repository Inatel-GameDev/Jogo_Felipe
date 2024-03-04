using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    public void Interact(Transform init)
    {
        StartCoroutine(DialogueManager.Instance.StartDialogue(dialogue));
    }
}
