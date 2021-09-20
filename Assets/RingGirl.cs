using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class RingGirl : MonoBehaviour {
    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
        EventManager.Instance.StartListening(EventManager.Events.StartFightCountdown, OnStartFight);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
        EventManager.Instance.StopListening(EventManager.Events.StartFightCountdown, OnStartFight);
    }

    [SerializeField] private Transform ringGirlSpawnPosition;

    private void Start() {
        transform.position = ringGirlSpawnPosition.position * 1000;
    }

    void OnShowRingGirl() {
        StartCoroutine(OnShowRingGirlCor());
    }

    private IEnumerator OnShowRingGirlCor() {
        yield return new WaitForSeconds(0.55f);
        transform.position = ringGirlSpawnPosition.position;
    }

    void OnStartFight() {
        StartCoroutine(OnStartFightCor());
    }

    private IEnumerator OnStartFightCor() {
        yield return new WaitForSeconds(0.55f);
        gameObject.SetActive(false);
    }
}