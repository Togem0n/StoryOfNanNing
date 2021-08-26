using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Printing : MonoBehaviour
{
    [SerializeField] private float printingSpeed;

    public Coroutine RunCoroutine(string printContent, TMP_Text tmp_text)
    {
        return StartCoroutine(Print(printContent, tmp_text));
    }

    private IEnumerator Print(string printContent, TMP_Text tmp_text)
    {
        //Debug.Log(printContent);
        tmp_text.text = string.Empty;
        
        float t = 0;
        int charIndex = 0;

        while (charIndex < printContent.Length)
        {
            t += Time.deltaTime * printingSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, printContent.Length);

            tmp_text.text = printContent.Substring(0, charIndex);
            yield return null;
        }

        tmp_text.text = printContent;
    }

    // tmr write a stopCorotine function

}
