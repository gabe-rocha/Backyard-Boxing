using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class RingGirl : MonoBehaviour {

    [SerializeField] private float walkSpeed;
    [SerializeField] private Transform ringGirlSpawnPosition, ringGirlTargetPosition;

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
        EventManager.Instance.StartListening(EventManager.Events.StartFightCountdown, OnStartFight);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
        EventManager.Instance.StopListening(EventManager.Events.StartFightCountdown, OnStartFight);
    }

    private void Start() {
        transform.position = ringGirlSpawnPosition.position * 1000;
    }

    void OnShowRingGirl() {
        StartCoroutine(OnShowRingGirlCor());
    }

    private IEnumerator OnShowRingGirlCor() {
        yield return new WaitForSeconds(0.55f);
        transform.position = ringGirlSpawnPosition.position;
        StartCoroutine(WalkToTargetCor());
    }

    private IEnumerator WalkToTargetCor() {
        while (true) {
            transform.position = Vector3.MoveTowards(transform.position, ringGirlTargetPosition.position, walkSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void OnStartFight() {
        StartCoroutine(OnStartFightCor());
    }

    private IEnumerator OnStartFightCor() {
        yield return new WaitForSeconds(0.55f);
        gameObject.SetActive(false);
    }
}