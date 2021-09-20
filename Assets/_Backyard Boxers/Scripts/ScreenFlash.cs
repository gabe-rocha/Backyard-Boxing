using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour {
    public static ScreenFlash instance;

    private Image flashImage;
    private Coroutine flashCor;

    private void Awake() {
        instance = this;
        flashImage = GetComponent<Image>();

        Color color = flashImage.color;
        color.a = 0;
        flashImage.color = color;
        flashImage.enabled = true;
    }
    public void Flash(float duration) {
        if(flashCor != null) {
            StopCoroutine(flashCor);
        }
        flashCor = StartCoroutine(FlashCor(duration));
    }
    private IEnumerator FlashCor(float duration) {
        Color color = flashImage.color;
        color.a = 0;
        flashImage.color = color;

        float targetAlpha = 0.3f;
        while (flashImage.color.a < targetAlpha) {
            Color c = flashImage.color;
            c.a += targetAlpha * Time.deltaTime / duration * 2f;
            flashImage.color = c;
            yield return null;
        }

        while (flashImage.color.a > 0) {
            Color c = flashImage.color;
            c.a -= targetAlpha * Time.deltaTime / duration * 2f;
            flashImage.color = c;
            yield return null;
        }
    }

    // private IEnumerator FlashCor(float duration) {
    //     var startTime = Time.time;
    //     flashImage.CrossFadeAlpha(0, 0, true);
    //     while (Time.time < startTime + duration / 4) {
    //         flashImage.CrossFadeAlpha(1, duration / 4, true);
    //         yield return null;
    //     }

    //     startTime = Time.time;
    //     while (Time.time < startTime + duration / 2) {
    //         flashImage.CrossFadeAlpha(0, duration / 2, true);
    //         yield return null;
    //     }
    // }

}