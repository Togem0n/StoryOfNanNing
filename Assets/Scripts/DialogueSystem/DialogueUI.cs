using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text tmp_text;
    [SerializeField] private DialoeObject testDialogueObject;
    [SerializeField] private GameObject dialogue_box;

    private ResponseHandler responseHandler;
    private Printing printing;

    private void Awake()
    {
        responseHandler = GetComponent<ResponseHandler>();
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
/*        foreach(string dialogueContent in dialoeObject.DialogueArray)
        {
            //Corouint inside a coroutine?
            yield return printing.RunCoroutine(dialogueContent, tmp_text);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }*/

        for(int i = 0; i < dialoeObject.DialogueArray.Length; i++)
        {
            string dialogueContent = dialoeObject.DialogueArray[i];
            yield return printing.RunCoroutine(dialogueContent, tmp_text);

            if (i == dialoeObject.DialogueArray.Length - 1 && dialoeObject.HasResponses) break;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }

        if (dialoeObject.HasResponses)
        {
            responseHandler.showResponses(dialoeObject.ResponseArray);
        }
        else
        {
            CloseDialogue();
        }

    }

    public void CloseDialogue()
    {
        dialogue_box.SetActive(false);
        tmp_text.text = string.Empty;
    }
}
