using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    [SerializeField] Color backgroundActiveColor;
    [SerializeField] Color handleActiveColor;

    RawImage backgroundImage, handleImage;
    Color backgroundDefaultColor, handleDefaultColor;

    Toggle toggle;

    Vector2 handlePosition;

    private void Awake() 
    {
        toggle = GetComponent<Toggle>();
        handlePosition = uiHandleRectTransform.anchoredPosition; 

        backgroundImage = uiHandleRectTransform.parent.GetComponent<RawImage>();
        handleImage = uiHandleRectTransform.GetComponent<RawImage>();

        backgroundDefaultColor = backgroundImage.color;
        handleDefaultColor = handleImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);

        int savedToggleValue = PlayerPrefs.GetInt("musicValue", 1);
        toggle.isOn = (savedToggleValue == 1);
                
    }

    void OnSwitch(bool on)
    {   
        if(on)
        {
            uiHandleRectTransform.anchoredPosition = handlePosition * -1;
            backgroundImage.color = backgroundActiveColor;
            handleImage.color = handleActiveColor; 
        }
        else
        {
            uiHandleRectTransform.anchoredPosition = handlePosition;
            backgroundImage.color = backgroundDefaultColor;
            handleImage.color = handleDefaultColor; 
        }

        PlayerPrefs.SetInt("musicValue", on ? 1 : 0);
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);  
    }
}
