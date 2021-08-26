using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Dialogue/DialogueObejct")]
public class DialoeObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogueArray;

    public string[] DialogueArray => dialogueArray;
}
