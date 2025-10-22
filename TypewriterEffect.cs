using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI textDisplay;
    public float waitingSeconds = Constants.DEFAULT_WAITING_SECONDS;

    private Coroutine typingCoroutiue;
    private bool isTyping;

    public void StartTyping(string text)
    {
        if (typingCoroutiue != null)
        {
            StopCoroutine(typingCoroutiue);
        }
        typingCoroutiue = StartCoroutine(TypeLine(text));

    }

    private IEnumerator TypeLine(string text)
    {
        isTyping = true;
        textDisplay.text = text;
        textDisplay.maxVisibleCharacters = 0;

        for (int i = 0; i < text.Length; i++)
        {
            textDisplay.maxVisibleCharacters = i+1;
            yield return new WaitForSeconds(waitingSeconds);

        }
        isTyping = false;


    }
    public void CompleteLine()
    {
        if (typingCoroutiue != null)
        {
            StopCoroutine(typingCoroutiue);
            
        }

        
        
            textDisplay.maxVisibleCharacters = textDisplay.text.Length;
        
        isTyping =false;
    }

    public bool IsTyping()
    {
        return isTyping;
    }








}