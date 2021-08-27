using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text tmp_text;
    [SerializeField] private GameObject dialogue_box;
    // [SerializeField] private DialoeObject testDialogueObject;

    public bool dialogueIsOpen { get; private set; }

    private ResponseHandler responseHandler;
    private Printing printing;

    private void Awake()
    {
        responseHandler = GetComponent<ResponseHandler>();
        printing = GetComponent<Printing>();
        CloseDialogue();
    }

    public void ShowDialogue(DialoeObject dialoeObject)
    {
        dialogue_box.SetActive(true);
        dialogueIsOpen = true;
        StartCoroutine(StepThroughDialogue(dialoeObject));
    }

    private IEnumerator StepThroughDialogue(DialoeObject dialoeObject)
    {

        for(int i = 0; i < dialoeObject.DialogueArray.Length; i++)
        {
            string dialogueContent = dialoeObject.DialogueArray[i];
            yield return RunPrinting(dialogueContent);
            //yield return printing.RunCoroutine(dialogueContent, tmp_text);

            tmp_text.text = dialogueContent;

            if (i == dialoeObject.DialogueArray.Length - 1 && dialoeObject.HasResponses) break;

            yield return null;
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

    private IEnumerator RunPrinting(string dialogue)
    {
        printing.RunCoroutine(dialogue, tmp_text);

        while (printing.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                printing.Stop();
            }
        }
    }

    public void CloseDialogue()
    {
        dialogueIsOpen = false;
        dialogue_box.SetActive(false);
        tmp_text.text = string.Empty;
    }
}
