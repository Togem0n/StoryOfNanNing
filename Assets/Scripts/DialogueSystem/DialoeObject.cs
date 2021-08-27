using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Dialogue/DialogueObejct")]
public class DialoeObject : ScriptableObject
{
    /// <summary>
    /// Inside DialogueObject, two main object;
    /// One is dialogueArray which stores the dialogue content
    /// The other one is responseArray which stores the response content(if it has)
    /// </summary>

    [SerializeField] [TextArea] private string[] dialogueArray;
    [SerializeField] private Response[] reponseArray;

    public string[] DialogueArray => dialogueArray;

    public bool HasResponses => ResponseArray != null && ResponseArray.Length > 0;

    public Response[] ResponseArray => reponseArray;
}   
