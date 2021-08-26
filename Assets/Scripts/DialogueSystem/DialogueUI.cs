using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text tmp_text;
    [SerializeField] private DialoeObject testDialogueObject;
    [SerializeField] private GameObject dialogue_box;

    private Printing printing;

    private void Awake()
    {
        printing = GetComponent<Printing>();
        CloseDialogue();
        ShowDialogue(testDialogueObject);
    }

    public void ShowDialogue(DialoeObject dialoeObject)
    {
        dialogue_box.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialoeObject));
    }

    private IEnumerator StepThroughDialogue(DialoeObject dialoeObject)
    {
        foreach(string dialogueContent in dialoeObject.DialogueArray)
        {
            //Debug.Log(dialogueContent);
            yield return printing.RunCoroutine(dialogueContent, tmp_text);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        CloseDialogue();
    }

    public void CloseDialogue()
    {
        dialogue_box.SetActive(false);
        tmp_text.text = string.Empty;
    }
}
