using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour {

#region Public Fields

#endregion

#region Private Serializable Fields

#endregion

#region Private Fields
    private Image imgBlack;
#endregion

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ShowFightRoster, OnShowRoster);
        EventManager.Instance.StartListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
        EventManager.Instance.StartListening(EventManager.Events.StartFightCountdown, OnStartFightCountdown);
        EventManager.Instance.StartListening(EventManager.Events.ShowResults, OnShowResults);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ShowFightRoster, OnShowRoster);
        EventManager.Instance.StopListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
        EventManager.Instance.StopListening(EventManager.Events.StartFightCountdown, OnStartFightCountdown);
        EventManager.Instance.StartListening(EventManager.Events.ShowResults, OnShowResults);
    }

#region MonoBehaviour CallBacks
    void Awake() {
        imgBlack = GetComponent<Image>();
        if(imgBlack == null) {
            Debug.LogError($"{name} is missing imgBlack");
        }
        imgBlack.enabled = true;
    }

#endregion

#region Private Methods

    private void OnShowRoster() {
        imgBlack.color = Color.black;
        FadeScreenIn();
    }

    private void OnShowRingGirl() {
        var cor = StartCoroutine(OnShowRingGirlCor());
    }

    private IEnumerator OnShowRingGirlCor() {
        FadeScreenOut();
        yield return new WaitForSeconds(1);
        FadeScreenIn();
    }

    private void OnStartFightCountdown() {
        var cor = StartCoroutine(OnStartFightCountdownCor());
    }

    private IEnumerator OnStartFightCountdownCor() {
        FadeScreenOut();
        yield return new WaitForSeconds(1);
        FadeScreenIn();
    }

    void OnShowResults() {
        StartCoroutine(OnShowResultsCor());
    }

    private IEnumerator OnShowResultsCor() {
        FadeScreenOut();
        yield return new WaitForSeconds(1);
        FadeScreenIn();
    }

    private void FadeScreenIn() {
        var cor = StartCoroutine(FadeScreenInCor());
    }

    private IEnumerator FadeScreenInCor() {
        while (imgBlack.color.a > 0) {
            Color c = imgBlack.color;
            c.a -= Time.deltaTime * 3f;
            imgBlack.color = c;
            yield return null;
        }
    }

    private void FadeScreenOut() {
        var cor = StartCoroutine(FadeScreenOutCor());
    }

    private IEnumerator FadeScreenOutCor() {
        while (imgBlack.color.a < 1) {
            Color c = imgBlack.color;
            c.a += Time.deltaTime * 3f;;
            imgBlack.color = c;
            yield return null;
        }
    }
#endregion

#region Public Methods

#endregion
}