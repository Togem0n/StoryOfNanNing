using UnityEngine;

[System.Serializable]
public class Response
{
    // ResponseObject stored in DialogueArray
    [SerializeField] private string responseText;
    [SerializeField] private DialoeObject dialogueObject;

    public string ResponseText => responseText;

    public DialoeObject DialoeObject => dialogueObject;
}
