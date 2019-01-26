using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SpeechBubbleController : MonoBehaviour
{
    
    public string text;
    public TMP_Text textObj;
    public Vector2 margin;
    public bool flipBubble;

    Image bubbleImage;
	
	void Start ()
    {
        bubbleImage = GetComponent<Image>();
	}
	
	
	void Update ()
    {
        if (textObj != null)
        {
            textObj.text = text;
        }
    }

    void LateUpdate()
    {
        if (textObj != null)
        {
            textObj.text = text;
            GetComponent<RectTransform>().sizeDelta = textObj.GetComponent<RectTransform>().sizeDelta + (margin * 2);

            transform.localScale = new Vector3((flipBubble ? -1 : 1), 1, 1);
            textObj.transform.localScale = new Vector3((flipBubble ? -1 : 1), 1, 1);
        }
    }
}
