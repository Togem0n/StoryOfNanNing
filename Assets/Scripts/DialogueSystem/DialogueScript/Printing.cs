using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Printing : MonoBehaviour
{
    [SerializeField] private float printingSpeed;

    public bool IsRunning { get; private set; }

    private readonly Dictionary<HashSet<char>, float> punctuations = new Dictionary<HashSet<char>, float>()
    {
        {new HashSet<char>(){'.', '!', '?'}, 0.6f},
        {new HashSet<char>(){',', ';', ':'}, 0.3f}
    };

    private Coroutine typingCoroutine;

    public void RunCoroutine(string printContent, TMP_Text tmp_text)
    {
        typingCoroutine =  StartCoroutine(Print(printContent, tmp_text));
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }


    private IEnumerator Print(string printContent, TMP_Text tmp_text)
    {
        IsRunning = true;

        tmp_text.text = string.Empty;
        
        float t = 0;
        int charIndex = 0;

        while (charIndex < printContent.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime * printingSpeed;

            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, printContent.Length);

            for(int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= printContent.Length - 1;

                tmp_text.text = printContent.Substring(0, i + 1);

                if(IsPunctuation(printContent[i], out float waitTime) && !isLast && !IsPunctuation(printContent[i+1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }

            }

            yield return null;
        }

        IsRunning = false;
    }

    // tmr write a stopCorotine function

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach(KeyValuePair<HashSet<char>, float> punctuationCatelogy in punctuations)
        {
            if (punctuationCatelogy.Key.Contains(character))
            {
                waitTime = punctuationCatelogy.Value;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

}
