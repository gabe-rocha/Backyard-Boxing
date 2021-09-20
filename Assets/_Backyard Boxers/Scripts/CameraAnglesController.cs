using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraAnglesController : MonoBehaviour {
    private int curCamera = 1;
    private int numCameras;
    private Animator anim;
    private CinemachineStateDrivenCamera cam;

    private void OnEnable() {
        EventManager.Instance.StartListening(EventManager.Events.ShowFightRoster, OnShowingRoster);
        EventManager.Instance.StartListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
        EventManager.Instance.StartListening(EventManager.Events.StartFightCountdown, OnStartFight);
        EventManager.Instance.StartListening(EventManager.Events.FightEnded, OnFightEnded);
        EventManager.Instance.StartListening(EventManager.Events.ShowResults, OnShowResults);
    }
    private void OnDisable() {
        EventManager.Instance.StopListening(EventManager.Events.ShowFightRoster, OnShowingRoster);
        EventManager.Instance.StopListening(EventManager.Events.ShowRingGirl, OnShowRingGirl);
        EventManager.Instance.StopListening(EventManager.Events.StartFightCountdown, OnStartFight);
        EventManager.Instance.StopListening(EventManager.Events.FightEnded, OnFightEnded);
        EventManager.Instance.StopListening(EventManager.Events.ShowResults, OnShowResults);
    }

    private void Awake() {
        anim = GetComponent<Animator>();
        cam = GetComponent<CinemachineStateDrivenCamera>();
    }

    private void Start() {
        numCameras = cam.ChildCameras.Length;
    }
    // public void OnCameraButtonPressed() {
    //     curCamera = curCamera == numCameras ? 1 : curCamera + 1;
    //     anim.SetTrigger($"Camera {curCamera}");
    // }

    private void OnShowingRoster() {
        // anim.SetTrigger($"Camera Roster"); // no need
    }
    private void OnShowRingGirl() {
        StartCoroutine(OnShowRingGirlCor());
    }

    private IEnumerator OnShowRingGirlCor() {
        yield return new WaitForSeconds(0.4f);;
        anim.SetTrigger($"Camera Ring Girl");
    }
    private void OnStartFight() {

        StartCoroutine(OnStartFightCor());
    }
    private IEnumerator OnStartFightCor() {
        yield return new WaitForSeconds(0.3f);;
        anim.SetTrigger($"Camera Fight");
    }

    private void OnFightEnded() {
        StartCoroutine(OnFightEndedCor());
    }

    private IEnumerator OnFightEndedCor() {
        yield return new WaitForSeconds(2.5f);
        anim.SetTrigger($"Fight Ended");
    }

    void OnShowResults() {
        StartCoroutine(OnShowResultsCor());
    }

    private IEnumerator OnShowResultsCor() {
        yield return new WaitForSeconds(0.4f);
        anim.SetTrigger($"Camera Roster");
    }
}