using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public static ScreenFlash instance;

    private Image flashImage;

    private void Awake()
    {
        instance = this;
        flashImage = GetComponent<Image>();
        flashImage.CrossFadeAlpha(0,0,true);
        flashImage.enabled = true;
    }
    public void Flash(float duration){
        StartCoroutine(FlashCor(duration));
    }

    private IEnumerator FlashCor(float duration){
        var startTime = Time.time;
        flashImage.CrossFadeAlpha(0,0,true);
        while(Time.time < startTime + duration/4){
            flashImage.CrossFadeAlpha(1, duration/4, true);
            yield return null;
        }

        startTime = Time.time;        
        while(Time.time < startTime + duration/2){
            flashImage.CrossFadeAlpha(0, duration/2, true);
            yield return null;
        }
    }

}
