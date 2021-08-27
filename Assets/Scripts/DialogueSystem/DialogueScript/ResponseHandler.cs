using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;
    private List<GameObject> temResponseButtons = new List<GameObject>();

    private void Awake()
    {
        dialogueUI = GetComponent<DialogueUI>();    
    }

    public void showResponses(Response[] responseArray)
    {
        float responseBoxHeight = 0;

        foreach(Response response in responseArray)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));

            temResponseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        Debug.Log(responseBoxHeight);
        responseBox.gameObject.SetActive(true); 
    }

    private void OnPickedResponse(Response response)
    {
        responseBox.gameObject.SetActive(false);  
        
        foreach(GameObject button in temResponseButtons)
        {
            Destroy(button);
        }

        dialogueUI.ShowDialogue(response.DialoeObject);
    }
}
